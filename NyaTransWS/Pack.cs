using Newtonsoft.Json;

namespace NyaTransWS;

public class Pack
{
    public Pack(string textMessage)
    {
        Type = MessageType.Text;
        TextMessage = textMessage;
    }

    public Pack(SingleFile file)
    {
        Type = MessageType.File;
        File = file;
    }

    public Pack(List<SingleFile> files)
    {
        Type = MessageType.Files;
        Files = files;
    }

    public Pack(MessageType type)
    {
        Type = type;
    }

    public Pack()
    {
        Type = MessageType.None;
    }

    public MessageType Type { get; set; }

    public string? TextMessage { get; set; }


    // public List<byte[]> Files { get; set; }
    public List<SingleFile>? Files { get; set; }
    public SingleFile? File { get; set; }

    public static Pack JtoP(string json)
    {
        try
        {
            var pack = JsonConvert.DeserializeObject<Pack>(json);
            return pack ?? new Pack();
        }
        catch (Exception e)
        {
            Console.WriteLine(@"Error: " + e.Message);
            return new Pack();
        }
    }

    public static string PtoJ(Pack pack)
    {
        return JsonConvert.SerializeObject(pack);
    }
}