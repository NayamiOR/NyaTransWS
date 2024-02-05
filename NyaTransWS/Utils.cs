namespace NyaTransWS;

public class Utils
{
    public static void CreateDirectory(string path)
    {
        if (!Directory.Exists(path)) Directory.CreateDirectory(path);
    }

    public static void CreateFile(string path)
    {
        if (!File.Exists(path)) File.Create(path).Close();
    }

    public static void Initialize()
    {
        var root = Path.Combine(Application.StartupPath, "Trans-Data");

        // Delete the directory if it exists.
        // if (Directory.Exists(root)) Directory.Delete(root, true);
        if (Directory.Exists(root)) return;

        // Init the directory.
        var path = root;
        CreateDirectory(path);
        path = Path.Combine(root, "files");
        CreateDirectory(path);
        path = Path.Combine(root, "messages.txt");
        CreateFile(path);
    }
}