using System.Diagnostics;

namespace NyaTransWS;

public partial class Panel : Form
{
    public Panel()
    {
        InitializeComponent();
        // link.Text = "ws://localhost:18080";
        Server = new WsServer();
        Client = new WsClient();
        Server.UpdateStatusDel += UpdateStatus;
        Client.UpdateStatusDel += UpdateStatus;
    }

    private WsServer Server { get; }
    private WsClient Client { get; }
    private List<string> filePaths { get; } = new();

    public void UpdateStatus(Status status)
    {
        connect_status.Text = status.ToString();
        connect_status.ForeColor = status switch
        {
            Status.ServerStarted => Color.Blue,
            Status.Coned2Client => Color.Green,
            Status.Coned2Server => Color.Green,
            Status.Unconed => Color.Red,
            _ => Color.Black
        };
    }

    private void open_data_root_Click(object sender, EventArgs e)
    {
        // Open "Trans-Data" directory.
        var path = Path.Combine(Application.StartupPath, "Trans-Data");
        if (Directory.Exists(path))
            Process.Start("explorer.exe", path);
        else
            MessageBox.Show("Directory does not exist: " + path);
    }

    private void connect_btn_Click(object sender, EventArgs e)
    {
        var csType = type.Text;
        switch (csType)
        {
            case "":
                MessageBox.Show("Please select a type.");
                break;
            case "Server":
                switch (Server.Status)
                {
                    case Status.ServerStarted:
                        MessageBox.Show("Server already started.");
                        return;
                    case Status.Coned2Server:
                        MessageBox.Show("This is a client.");
                        return;
                    case Status.Coned2Client:
                        MessageBox.Show("Client already connected.");
                        return;
                    case Status.Unconed:
                        break;
                }

                Task.Run(() => Server.Start(18080));
                break;
            case "Client":
                switch (Client.Status)
                {
                    case Status.ServerStarted:
                        MessageBox.Show("This is a server.");
                        return;
                    case Status.Coned2Server:
                        MessageBox.Show("Server already connected.");
                        return;
                    case Status.Coned2Client:
                        MessageBox.Show("This is a client.");
                        return;
                    case Status.Unconed:
                        break;
                }

                // Client.ConnectServer(link.Text);
                Task.Run(() => Client.Connect(link.Text));
                break;
            default:
                MessageBox.Show("Invalid type: " + csType);
                break;
        }
    }

    private void disconnect_btn_Click(object sender, EventArgs e)
    {
        switch (type.Text)
        {
            case "Server":
                // Server.SharedWs.Close();
                Server.Stop();
                Server.Status = Status.Unconed;
                UpdateStatus(Status.Unconed);
                break;
            case "Client":
                Client.Stop();
                Client.Status = Status.Unconed;
                UpdateStatus(Status.Unconed);
                break;
        }
    }

    // public void UpdateStatus(Status status)
    // {
    //     connect_status.Text = status.ToString();
    //     connect_status.ForeColor = status switch
    //     {
    //         Status.ServerStarted => Color.Blue,
    //         Status.Coned2Client => Color.Green,
    //         Status.Coned2Server => Color.Green,
    //         Status.Unconed => Color.Red,
    //         _ => Color.Black
    //     };
    // }

    private void send_message_Click(object sender, EventArgs e)
    {
        var mType = MessageType.None;

        // 判断发送消息类型（文本/文件）
        foreach (var control in choose_mode.Controls)
            if (control is RadioButton { Checked: true } radioButton)
                mType = radioButton.Text switch
                {
                    "Text" => MessageType.Text,
                    "File" => selectedFileListView.Items.Count > 1 ? MessageType.Files : MessageType.File,
                    _ => MessageType.None
                };
        if (mType == MessageType.None) MessageBox.Show("Please select a type.");

        // 发送消息
        switch (mType)
        {
            case MessageType.Files:
                //TODO
                switch (type.Text)
                {
                    case "Server":
                        Task.Run(() => Server.SendFiles(filePaths));
                        break;
                    case "Client":
                        Task.Run(() => Client.SendFiles(filePaths));
                        break;
                }

                break;
            case MessageType.File:
                switch (type.Text)
                {
                    case "Server":
                        // Task.Run(() => Server.SendFile(selectedFileListView.Items[0].SubItems[0].Text));
                        Task.Run(() => Server.SendFile(filePaths[0]));
                        break;
                    case "Client":
                        Task.Run(() => Client.SendFile(filePaths[0]));
                        break;
                }

                break;
            case MessageType.Text:
                switch (type.Text)
                {
                    // TODO: 把这里的调用改成委托实现
                    case "Server":
                        Task.Run(() => Server.SendMessage(message_textbox.Text));
                        break;
                    case "Client":
                        Task.Run(() => Client.SendMessage(message_textbox.Text));
                        break;
                }

                break;
        }
    }

    private void select_file(object sender, EventArgs e)
    {
        selectSendFile.Title = "Select File";
        if (selectSendFile.ShowDialog() == DialogResult.OK)
        {
            selectedFileListView.Items.Clear();
            filePaths.Clear();
            foreach (var fileName in selectSendFile.FileNames)
            {
                filePaths.Add(fileName);
                var item = new ListViewItem(Path.GetFileName(fileName));
                selectedFileListView.Items.Add(item);
            }
        }
    }
}