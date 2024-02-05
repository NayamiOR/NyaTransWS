using Newtonsoft.Json;

namespace NyaTransWS;

public class Pack
{
    public Pack(string textMessage)
    {
        Type = MessageType.Text;
        TextMessage = textMessage;
    }

    public Pack(string fileName, byte[] file)
    {
        Type = MessageType.File;
        File = file;
        FileName = fileName;
    }

    public Pack()
    {
        Type = MessageType.None;
        TextMessage = string.Empty;
        File = new byte[0];
        FileName = string.Empty;
    }

    public MessageType Type { get; set; }
    public string TextMessage { get; set; }
    public byte[] File { get; set; }
    public string FileName { get; set; }

    public static Pack JtoP(string json)
    {
        return JsonConvert.DeserializeObject<Pack>(json);
    }

    public static string PtoJ(Pack pack)
    {
        return JsonConvert.SerializeObject(pack);
    }
}