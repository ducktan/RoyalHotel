namespace Lab3_Bai6
{
    partial class Client
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Client));
            this.btnConnect = new System.Windows.Forms.Button();
            this.grbMessage = new System.Windows.Forms.GroupBox();
            this.btnSendFile = new System.Windows.Forms.Button();
            this.btnSend = new System.Windows.Forms.Button();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.rtbChatBox = new System.Windows.Forms.RichTextBox();
            this.flpFile = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.grbMessage.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(160, 230);
            this.btnConnect.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(75, 23);
            this.btnConnect.TabIndex = 2;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // grbMessage
            // 
            this.grbMessage.Controls.Add(this.btnSendFile);
            this.grbMessage.Controls.Add(this.btnSend);
            this.grbMessage.Controls.Add(this.txtMessage);
            this.grbMessage.Location = new System.Drawing.Point(15, 426);
            this.grbMessage.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grbMessage.Name = "grbMessage";
            this.grbMessage.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grbMessage.Size = new System.Drawing.Size(883, 66);
            this.grbMessage.TabIndex = 6;
            this.grbMessage.TabStop = false;
            this.grbMessage.Text = "Message";
            // 
            // btnSendFile
            // 
            this.btnSendFile.Location = new System.Drawing.Point(761, 21);
            this.btnSendFile.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSendFile.Name = "btnSendFile";
            this.btnSendFile.Size = new System.Drawing.Size(36, 32);
            this.btnSendFile.TabIndex = 2;
            this.btnSendFile.Text = "I";
            this.btnSendFile.UseVisualStyleBackColor = true;
            this.btnSendFile.Click += new System.EventHandler(this.btnSendFile_Click);
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(803, 21);
            this.btnSend.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 32);
            this.btnSend.TabIndex = 1;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // txtMessage
            // 
            this.txtMessage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMessage.Location = new System.Drawing.Point(5, 21);
            this.txtMessage.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtMessage.Multiline = true;
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.Size = new System.Drawing.Size(750, 32);
            this.txtMessage.TabIndex = 0;
            this.txtMessage.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMessage_KeyDown);
            // 
            // rtbChatBox
            // 
            this.rtbChatBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rtbChatBox.Font = new System.Drawing.Font("Yu Gothic UI", 11F);
            this.rtbChatBox.Location = new System.Drawing.Point(252, 16);
            this.rtbChatBox.Margin = new System.Windows.Forms.Padding(4);
            this.rtbChatBox.Name = "rtbChatBox";
            this.rtbChatBox.ReadOnly = true;
            this.rtbChatBox.Size = new System.Drawing.Size(644, 403);
            this.rtbChatBox.TabIndex = 16;
            this.rtbChatBox.Text = "";
            // 
            // flpFile
            // 
            this.flpFile.AutoScroll = true;
            this.flpFile.Location = new System.Drawing.Point(10, 23);
            this.flpFile.Margin = new System.Windows.Forms.Padding(4);
            this.flpFile.Name = "flpFile";
            this.flpFile.Size = new System.Drawing.Size(213, 108);
            this.flpFile.TabIndex = 17;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.flpFile);
            this.groupBox2.Location = new System.Drawing.Point(13, 264);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(231, 139);
            this.groupBox2.TabIndex = 18;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "File";
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.SteelBlue;
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(10, 21);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(230, 192);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 293;
            this.pictureBox2.TabStop = false;
            // 
            // Client
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(907, 498);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.rtbChatBox);
            this.Controls.Add(this.grbMessage);
            this.Controls.Add(this.btnConnect);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Client";
            this.Text = "TCP Client";
            this.Load += new System.EventHandler(this.Client_Load);
            this.grbMessage.ResumeLayout(false);
            this.grbMessage.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.GroupBox grbMessage;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.TextBox txtMessage;
        private System.Windows.Forms.Button btnSendFile;
        private System.Windows.Forms.RichTextBox rtbChatBox;
        private System.Windows.Forms.FlowLayoutPanel flpFile;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.PictureBox pictureBox2;
    }
}