using WebSocketSharp;

namespace NyaTransWS;

public class WsClient : WsBase
{
    private WebSocket SharedWs { set; get; }

    public Task Connect(string linkIncludePort)
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
                    // Task.Run(() => ReceiveFile(pack.FileName, pack.File!));
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
        SharedWs.Connect();
        MessageBox.Show("Client connected.");
        return Task.CompletedTask;
    }

    // public override async Task Stop()
    public override Task Stop()
    {
        SharedWs.Close();
        Status = Status.Unconed;
        UpdateStatusDel?.Invoke(Status.Unconed);
        MessageBox.Show("Client disconnected.");
        return Task.CompletedTask;
    }

    protected override Task Send(string json)
    {
        SharedWs.Send(json);
        return Task.CompletedTask;
    }
}