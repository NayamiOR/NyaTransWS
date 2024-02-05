using Fleck;

namespace NyaTransWS;

public class WsServer : WsBase
{
    public WsServer()
    {
        Status = Status.Unconed;
    }

    public IWebSocketConnection SharedWs { set; get; }


    public async Task StartServer(int port)
    {
        try
        {
            await Task.Run(() =>
            {
                // var server = new WebSocketServer($"ws://localhost:{port}");
                var server = new WebSocketServer($"ws://127.0.0.1:{port}");
                server.Start(socket =>
                {
                    SharedWs = socket;
                    socket.OnOpen = () =>
                    {
                        Console.WriteLine("Open!");
                        UpdateStatusDel?.Invoke(Status.Coned2Client);
                    };
                    socket.OnClose = () =>
                    {
                        Console.WriteLine("Close!");
                        UpdateStatusDel?.Invoke(Status.ServerStarted);
                    };
                    socket.OnMessage = message =>
                    {
                        var pack = Pack.JtoP(message);
                        switch (pack!.Type)
                        {
                            case MessageType.File:
                                Task.Run(() => ReceiveFile(pack.FileName, pack.File!));
                                break;
                            case MessageType.Text:
                                Task.Run(() => ReceiveMessage(pack.TextMessage));
                                break;
                        }
                    };
                });

                Console.WriteLine("Server started.");
                Status = Status.ServerStarted;
                UpdateStatusDel?.Invoke(Status.ServerStarted);
                // 在应用程序关闭时释放资源
                AppDomain.CurrentDomain.ProcessExit += (sender, e) => server.Dispose();
            });

            MessageBox.Show("Server started.");
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Failed to start server. Error: {ex.Message}", "Error", MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }

    public async Task StopServer()
    {
        SharedWs.Close();
        Status = Status.Unconed;
        UpdateStatusDel?.Invoke(Status.Unconed);
    }

    public async Task ReceiveMessage(string message)
    {
        Console.WriteLine("Received: " + message);
        var messagePath = Path.Combine(Application.StartupPath, "Trans-Data", "messages.txt");
        using var sw = new StreamWriter(messagePath, true);
        var time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        var separateLine = new string('-', 20);
        message = time + "\n" + message + "\n" + separateLine;
        await sw.WriteLineAsync(message);
    }

    public async Task ReceiveFile(string fileName, byte[] bytes)
    {
        var filePath = Path.Combine(Application.StartupPath, fileName);
        await File.WriteAllBytesAsync(filePath, bytes);
    }

    public async Task SendMessage(string message)
    {
        var pack = new Pack(message);
        var json = Pack.PtoJ(pack);
        await SharedWs.Send(json);
    }

    /// TODO
    /// 1. Read file
    /// 2. Turn to byte[] and pack
    /// 3. Send file
    public async Task SendFile(string filePath)
    {
        var file = await File.ReadAllBytesAsync(filePath);
        var pack = new Pack(Path.GetFileName(filePath), file);
        var json = Pack.PtoJ(pack);
        await SharedWs.Send(json);
    }
}