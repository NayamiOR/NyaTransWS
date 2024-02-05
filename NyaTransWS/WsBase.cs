namespace NyaTransWS;

public class WsBase
{
    public delegate void UpdateStatusDelegate(Status status);

    public UpdateStatusDelegate? UpdateStatusDel { set; get; }
    public Status Status { get; set; }

    protected async Task SaveFile(SingleFile file)
    {
        var root = Path.Combine(Application.StartupPath, "Trans-Data", "files");
        var filePath = Path.Combine(root, file.FileName);
        var bytes = file.File;

        await File.WriteAllBytesAsync(filePath, bytes);
    }

    protected async Task SaveFiles(List<SingleFile> files)
    {
        foreach (var file in files) await SaveFile(file);
    }

    public async Task SaveMessage(string message)
    {
        Console.WriteLine(@"Received: " + message);
        var messagePath = Path.Combine(Application.StartupPath, "Trans-Data", "messages.txt");
        await using var sw = new StreamWriter(messagePath, true);
        var time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        var separateLine = new string('-', 20);
        message = time + "\n" + message + "\n" + separateLine;
        await sw.WriteLineAsync(message);
    }

    protected virtual Task Send(string json)
    {
        throw new NotImplementedException();
    }

    public async Task SendFile(string filePath)
    {
        var file = await File.ReadAllBytesAsync(filePath);
        var fileName = Path.GetFileName(filePath);
        var pack = new Pack(new SingleFile(fileName, file));
        var json = Pack.PtoJ(pack);
        await Send(json);
    }

    public async Task SendFiles(List<string> filePaths)
    {
        var files = new List<SingleFile>();
        foreach (var filePath in filePaths)
        {
            var file = await File.ReadAllBytesAsync(filePath);
            var fileName = Path.GetFileName(filePath);
            files.Add(new SingleFile(fileName, file));
        }

        var pack = new Pack(files);
        var json = Pack.PtoJ(pack);
        await Send(json);
    }

    public async Task SendMessage(string message)
    {
        var pack = new Pack(message);
        var json = Pack.PtoJ(pack);
        await Send(json);
    }

    public virtual Task Stop()
    {
        throw new NotImplementedException();
    }
}