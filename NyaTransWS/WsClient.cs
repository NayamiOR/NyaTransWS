using WebSocketSharp;

namespace NyaTransWS;

public class WsClient : WsBase
{
    public WebSocket SharedWs { set; get; }

    public async Task ConnectServer(string linkIncludePort)
    {
        SharedWs = new WebSocket(linkIncludePort);
        SharedWs.OnOpen += (sender, e) =>
        {
            Console.WriteLine("Open!");
            UpdateStatusDel?.Invoke(Status.Coned2Server);
            Status = Status.Coned2Server;
        };
        SharedWs.OnClose += (sender, e) =>
        {
            Console.WriteLine("Close!");
            UpdateStatusDel?.Invoke(Status.Unconed);
            Status = Status.Unconed;
        };
        SharedWs.OnMessage += (sender, e) =>
        {
            var pack = Pack.JtoP(e.Data);
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
        SharedWs.Connect();
        MessageBox.Show("Client connected.");
    }

    public async Task StopClient()
    {
        SharedWs.Close();
        Status = Status.Unconed;
        UpdateStatusDel?.Invoke(Status.Unconed);
        MessageBox.Show("Client disconnected.");
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
        // TODO
        var pack = new Pack(message);
        var json = Pack.PtoJ(pack);
        SharedWs.Send(json);
    }

    public async Task SendFile(string filePath)
    {
        // TODO
        var file = await File.ReadAllBytesAsync(filePath);
        var pack = new Pack(Path.GetFileName(filePath), file);
        var json = Pack.PtoJ(pack);
        SharedWs.Send(json);
    }

    public async Task SendBytes(byte[] bytes)
    {
        // TODO
    }
}