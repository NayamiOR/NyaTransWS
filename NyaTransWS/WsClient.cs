using WebSocketSharp;

namespace NyaTransWS;

public class WsClient : WsBase
{
    private WebSocket SharedWs { set; get; }

    public Task Connect(string linkIncludePort)
    {
        SharedWs = new WebSocket(linkIncludePort);
        SharedWs.OnOpen += (_, _) =>
        {
            Console.WriteLine(@"Open!");
            UpdateStatusDel?.Invoke(Status.Coned2Server);
            Status = Status.Coned2Server;
            MessageBox.Show(@"Client connected.");
        };
        SharedWs.OnClose += (_, _) =>
        {
            Console.WriteLine(@"Close!");
            UpdateStatusDel?.Invoke(Status.Unconed);
            Status = Status.Unconed;
        };
        SharedWs.OnMessage += (_, e) =>
        {
            var pack = Pack.JtoP(e.Data);
            switch (pack.Type)
            {
                case MessageType.File:
                    // Task.Run(() => ReceiveFile(pack.FileName, pack.File!));
                    Task.Run(() => SaveFile(pack.File!));
                    break;
                case MessageType.Text:
                    Task.Run(() => SaveMessage(pack.TextMessage!));
                    break;
                case MessageType.Files:
                    Task.Run(() => SaveFiles(pack.Files!));
                    break;
                case MessageType.Acknowledgement:
                    Console.WriteLine(@"Acknowledgement received.");
                    MessageBox.Show(@"Pack sent successfully.");
                    break;
            }
        };
        // SharedWs.Connect();
        try
        {
            SharedWs.Connect();
        }
        catch (Exception e)
        {
            // 连接异常处理
            Console.WriteLine(@"连接异常:" + e.Message);
        }

        return Task.CompletedTask;
    }

    // public override async Task Stop()
    public override Task Stop()
    {
        SharedWs.Close();
        Status = Status.Unconed;
        UpdateStatusDel?.Invoke(Status.Unconed);
        MessageBox.Show(@"Client disconnected.");
        return Task.CompletedTask;
    }

    protected override Task Send(string json)
    {
        SharedWs.Send(json);
        return Task.CompletedTask;
    }
}