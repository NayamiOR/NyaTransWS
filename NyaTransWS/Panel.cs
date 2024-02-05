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
                // TODO
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

                Task.Run(() => Server.StartServer(18080));
                break;
            case "Client":
                // TODO
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
                Task.Run(() => Client.ConnectServer(link.Text));
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
                // TODO
                // Server.SharedWs.Close();
                Server.StopServer();
                Server.Status = Status.Unconed;
                UpdateStatus(Status.Unconed);
                break;
            case "Client":
                // Client.SharedWs.Close();
                Client.StopClient();
                Client.Status = Status.Unconed;
                UpdateStatus(Status.Unconed);
                break;
        }
    }

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

    private void send_message_Click(object sender, EventArgs e)
    {
        var mType = MessageType.None;

        // 判断类型
        foreach (var control in choose_mode.Controls)
            if (control is RadioButton { Checked: true } radioButton)
                mType = radioButton.Text switch
                {
                    "Text" => MessageType.Text,
                    "File" => MessageType.File,
                    _ => MessageType.None
                };
        if (mType == MessageType.None) MessageBox.Show("Please select a type.");

        // 发送消息
        switch (mType)
        {
            case MessageType.File:
                // TODO
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
}