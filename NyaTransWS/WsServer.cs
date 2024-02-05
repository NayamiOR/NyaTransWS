using Fleck;

namespace NyaTransWS;

public class WsServer : WsBase
{
    public WsServer()
    {
        Status = Status.Unconed;
    }

    public IWebSocketConnection SharedWs { set; get; }


    public async Task Start(int port)
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
                                Task.Run(() => SaveFile(pack.File!));
                                break;
                            case MessageType.Text:
                                Task.Run(() => SaveMessage(pack.TextMessage!));
                                break;
                            case MessageType.Files:
                                Task.Run(() => SaveFiles(pack.Files!));
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

    public override async Task Stop()
    {
        SharedWs.Close();
        Status = Status.Unconed;
        UpdateStatusDel?.Invoke(Status.Unconed);
    }


    protected override async Task Send(string json)
    {
        await SharedWs.Send(json);
    }
}