namespace Lab3_Bai6
{
    partial class Server
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
            this.btnListen = new System.Windows.Forms.Button();
            this.btnSend = new System.Windows.Forms.Button();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.btnSendFlie = new System.Windows.Forms.Button();
            this.gbMessage = new System.Windows.Forms.GroupBox();
            this.rtbChatBox = new System.Windows.Forms.RichTextBox();
            this.flpFile = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.gbMessage.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnListen
            // 
            this.btnListen.Location = new System.Drawing.Point(21, 40);
            this.btnListen.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnListen.Name = "btnListen";
            this.btnListen.Size = new System.Drawing.Size(259, 60);
            this.btnListen.TabIndex = 9;
            this.btnListen.Text = "Listen";
            this.btnListen.UseVisualStyleBackColor = true;
            this.btnListen.Click += new System.EventHandler(this.btnListen_Click);
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(780, 23);
            this.btnSend.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(100, 32);
            this.btnSend.TabIndex = 14;
            this.btnSend.Text = "send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // txtMessage
            // 
            this.txtMessage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMessage.Location = new System.Drawing.Point(5, 23);
            this.txtMessage.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtMessage.Multiline = true;
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.Size = new System.Drawing.Size(727, 32);
            this.txtMessage.TabIndex = 15;
            this.txtMessage.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMessage_KeyDown);
            // 
            // btnSendFlie
            // 
            this.btnSendFlie.Location = new System.Drawing.Point(739, 23);
            this.btnSendFlie.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSendFlie.Name = "btnSendFlie";
            this.btnSendFlie.Size = new System.Drawing.Size(36, 32);
            this.btnSendFlie.TabIndex = 17;
            this.btnSendFlie.Text = "I";
            this.btnSendFlie.UseVisualStyleBackColor = true;
            this.btnSendFlie.Click += new System.EventHandler(this.btnSendFlie_Click);
            // 
            // gbMessage
            // 
            this.gbMessage.Controls.Add(this.txtMessage);
            this.gbMessage.Controls.Add(this.btnSendFlie);
            this.gbMessage.Controls.Add(this.btnSend);
            this.gbMessage.Location = new System.Drawing.Point(12, 421);
            this.gbMessage.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gbMessage.Name = "gbMessage";
            this.gbMessage.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gbMessage.Size = new System.Drawing.Size(885, 71);
            this.gbMessage.TabIndex = 18;
            this.gbMessage.TabStop = false;
            this.gbMessage.Text = "Message";
            // 
            // rtbChatBox
            // 
            this.rtbChatBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rtbChatBox.Font = new System.Drawing.Font("Yu Gothic UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbChatBox.Location = new System.Drawing.Point(296, 12);
            this.rtbChatBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rtbChatBox.Name = "rtbChatBox";
            this.rtbChatBox.ReadOnly = true;
            this.rtbChatBox.Size = new System.Drawing.Size(600, 402);
            this.rtbChatBox.TabIndex = 19;
            this.rtbChatBox.Text = "";
            // 
            // flpFile
            // 
            this.flpFile.AutoScroll = true;
            this.flpFile.Location = new System.Drawing.Point(9, 23);
            this.flpFile.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.flpFile.Name = "flpFile";
            this.flpFile.Size = new System.Drawing.Size(259, 233);
            this.flpFile.TabIndex = 20;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.flpFile);
            this.groupBox1.Location = new System.Drawing.Point(12, 151);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(276, 263);
            this.groupBox1.TabIndex = 21;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "File";
            // 
            // Server
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(912, 506);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.rtbChatBox);
            this.Controls.Add(this.gbMessage);
            this.Controls.Add(this.btnListen);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Server";
            this.Text = "TCP Server";
            this.gbMessage.ResumeLayout(false);
            this.gbMessage.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnListen;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.TextBox txtMessage;
        private System.Windows.Forms.Button btnSendFlie;
        private System.Windows.Forms.GroupBox gbMessage;
        private System.Windows.Forms.RichTextBox rtbChatBox;
        private System.Windows.Forms.FlowLayoutPanel flpFile;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}