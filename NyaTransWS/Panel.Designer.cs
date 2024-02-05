namespace NyaTransWS
{
    partial class Panel
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.link = new System.Windows.Forms.TextBox();
            this.connect_btn = new System.Windows.Forms.Button();
            this.connect_status = new System.Windows.Forms.Label();
            this.open_data_root = new System.Windows.Forms.Button();
            this.type = new System.Windows.Forms.ComboBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.message_panel = new System.Windows.Forms.TabControl();
            this.message_page = new System.Windows.Forms.TabPage();
            this.message_textbox = new System.Windows.Forms.TextBox();
            this.file_page = new System.Windows.Forms.TabPage();
            this.choose_mode = new System.Windows.Forms.GroupBox();
            this.Text_radio = new System.Windows.Forms.RadioButton();
            this.file_radio = new System.Windows.Forms.RadioButton();
            this.send_message = new System.Windows.Forms.Button();
            this.disconnect_btn = new System.Windows.Forms.Button();
            this.message_panel.SuspendLayout();
            this.message_page.SuspendLayout();
            this.choose_mode.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(496, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "选择本机程序类型，如果是客户端则填写主机链接，点击连接";
            // 
            // link
            // 
            this.link.Location = new System.Drawing.Point(198, 48);
            this.link.Name = "link";
            this.link.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.link.Size = new System.Drawing.Size(201, 30);
            this.link.TabIndex = 1;
            this.link.Text = "ws://localhost:18080";
            // 
            // connect_btn
            // 
            this.connect_btn.Location = new System.Drawing.Point(405, 46);
            this.connect_btn.Name = "connect_btn";
            this.connect_btn.Size = new System.Drawing.Size(112, 34);
            this.connect_btn.TabIndex = 2;
            this.connect_btn.Text = "Connect";
            this.connect_btn.UseVisualStyleBackColor = true;
            this.connect_btn.Click += new System.EventHandler(this.connect_btn_Click);
            // 
            // connect_status
            // 
            this.connect_status.AutoSize = true;
            this.connect_status.ForeColor = System.Drawing.Color.Red;
            this.connect_status.Location = new System.Drawing.Point(660, 51);
            this.connect_status.Name = "connect_status";
            this.connect_status.Size = new System.Drawing.Size(124, 24);
            this.connect_status.TabIndex = 3;
            this.connect_status.Text = "Unconnected";
            // 
            // open_data_root
            // 
            this.open_data_root.Location = new System.Drawing.Point(676, 9);
            this.open_data_root.Name = "open_data_root";
            this.open_data_root.Size = new System.Drawing.Size(112, 34);
            this.open_data_root.TabIndex = 4;
            this.open_data_root.Text = "Open Root";
            this.open_data_root.UseVisualStyleBackColor = true;
            this.open_data_root.Click += new System.EventHandler(this.open_data_root_Click);
            // 
            // type
            // 
            this.type.FormattingEnabled = true;
            this.type.Items.AddRange(new object[] {
            "Server",
            "Client"});
            this.type.Location = new System.Drawing.Point(12, 46);
            this.type.Name = "type";
            this.type.Size = new System.Drawing.Size(180, 32);
            this.type.TabIndex = 5;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // message_panel
            // 
            this.message_panel.Controls.Add(this.message_page);
            this.message_panel.Controls.Add(this.file_page);
            this.message_panel.Location = new System.Drawing.Point(12, 84);
            this.message_panel.Name = "message_panel";
            this.message_panel.SelectedIndex = 0;
            this.message_panel.Size = new System.Drawing.Size(776, 279);
            this.message_panel.TabIndex = 6;
            this.message_panel.Tag = "";
            // 
            // message_page
            // 
            this.message_page.Controls.Add(this.message_textbox);
            this.message_page.Location = new System.Drawing.Point(4, 33);
            this.message_page.Name = "message_page";
            this.message_page.Padding = new System.Windows.Forms.Padding(3);
            this.message_page.Size = new System.Drawing.Size(768, 242);
            this.message_page.TabIndex = 1;
            this.message_page.Text = "Message";
            this.message_page.UseVisualStyleBackColor = true;
            // 
            // message_textbox
            // 
            this.message_textbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.message_textbox.Location = new System.Drawing.Point(3, 3);
            this.message_textbox.Multiline = true;
            this.message_textbox.Name = "message_textbox";
            this.message_textbox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.message_textbox.Size = new System.Drawing.Size(762, 236);
            this.message_textbox.TabIndex = 0;
            // 
            // file_page
            // 
            this.file_page.Location = new System.Drawing.Point(4, 33);
            this.file_page.Name = "file_page";
            this.file_page.Padding = new System.Windows.Forms.Padding(3);
            this.file_page.Size = new System.Drawing.Size(768, 242);
            this.file_page.TabIndex = 0;
            this.file_page.Text = "File";
            this.file_page.UseVisualStyleBackColor = true;
            // 
            // choose_mode
            // 
            this.choose_mode.Controls.Add(this.Text_radio);
            this.choose_mode.Controls.Add(this.file_radio);
            this.choose_mode.Location = new System.Drawing.Point(19, 369);
            this.choose_mode.Name = "choose_mode";
            this.choose_mode.Size = new System.Drawing.Size(330, 69);
            this.choose_mode.TabIndex = 10;
            this.choose_mode.TabStop = false;
            this.choose_mode.Text = "Choose Mode";
            // 
            // Text_radio
            // 
            this.Text_radio.AutoSize = true;
            this.Text_radio.Location = new System.Drawing.Point(6, 29);
            this.Text_radio.Name = "Text_radio";
            this.Text_radio.Size = new System.Drawing.Size(71, 28);
            this.Text_radio.TabIndex = 8;
            this.Text_radio.TabStop = true;
            this.Text_radio.Text = "Text";
            this.Text_radio.UseVisualStyleBackColor = true;
            // 
            // file_radio
            // 
            this.file_radio.AutoSize = true;
            this.file_radio.Location = new System.Drawing.Point(161, 29);
            this.file_radio.Name = "file_radio";
            this.file_radio.Size = new System.Drawing.Size(65, 28);
            this.file_radio.TabIndex = 9;
            this.file_radio.TabStop = true;
            this.file_radio.Text = "File";
            this.file_radio.UseVisualStyleBackColor = true;
            // 
            // send_message
            // 
            this.send_message.Location = new System.Drawing.Point(375, 392);
            this.send_message.Name = "send_message";
            this.send_message.Size = new System.Drawing.Size(112, 34);
            this.send_message.TabIndex = 7;
            this.send_message.Text = "Send";
            this.send_message.UseVisualStyleBackColor = true;
            this.send_message.Click += new System.EventHandler(this.send_message_Click);
            // 
            // disconnect_btn
            // 
            this.disconnect_btn.Location = new System.Drawing.Point(523, 46);
            this.disconnect_btn.Name = "disconnect_btn";
            this.disconnect_btn.Size = new System.Drawing.Size(131, 34);
            this.disconnect_btn.TabIndex = 11;
            this.disconnect_btn.Text = "Disconnect";
            this.disconnect_btn.UseVisualStyleBackColor = true;
            this.disconnect_btn.Click += new System.EventHandler(this.disconnect_btn_Click);
            // 
            // Panel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.disconnect_btn);
            this.Controls.Add(this.choose_mode);
            this.Controls.Add(this.send_message);
            this.Controls.Add(this.message_panel);
            this.Controls.Add(this.type);
            this.Controls.Add(this.open_data_root);
            this.Controls.Add(this.connect_status);
            this.Controls.Add(this.connect_btn);
            this.Controls.Add(this.link);
            this.Controls.Add(this.label1);
            this.Name = "Panel";
            this.Text = "Form1";
            this.message_panel.ResumeLayout(false);
            this.message_page.ResumeLayout(false);
            this.message_page.PerformLayout();
            this.choose_mode.ResumeLayout(false);
            this.choose_mode.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label label1;
        private TextBox link;
        private Button connect_btn;
        private Label connect_status;
        private Button open_data_root;
        private ComboBox type;
        private OpenFileDialog openFileDialog1;
        private TabControl message_panel;
        private TabPage file_page;
        private TabPage message_page;
        private Button send_message;
        private TextBox message_textbox;
        private GroupBox choose_mode;
        private RadioButton Text_radio;
        private RadioButton file_radio;
        private Button disconnect_btn;
    }
}