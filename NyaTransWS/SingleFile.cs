namespace NyaTransWS;

public class SingleFile
{
    public SingleFile(string fileName, byte[] file)
    {
        FileName = fileName;
        File = file;
    }

    public SingleFile()
    {
        FileName = string.Empty;
        File = new byte[0];
    }

    public string FileName { get; set; }
    public byte[] File { get; set; }
}