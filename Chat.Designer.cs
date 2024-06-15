namespace Royal
{
    partial class Chat
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Chat));
            this.ConnectBTN = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.btnSendImg = new System.Windows.Forms.Button();
            this.chatRTB = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.kryptonButton1 = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // ConnectBTN
            // 
            this.ConnectBTN.AccessibleRole = System.Windows.Forms.AccessibleRole.OutlineButton;
            this.ConnectBTN.ButtonStyle = ComponentFactory.Krypton.Toolkit.ButtonStyle.Gallery;
            this.ConnectBTN.Location = new System.Drawing.Point(35, 283);
            this.ConnectBTN.Name = "ConnectBTN";
            this.ConnectBTN.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2010Blue;
            this.ConnectBTN.Size = new System.Drawing.Size(179, 36);
            this.ConnectBTN.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ConnectBTN.TabIndex = 293;
            this.ConnectBTN.UseWaitCursor = true;
            this.ConnectBTN.Values.Text = "Connect to chat";
            this.ConnectBTN.Click += new System.EventHandler(this.ConnectBTN_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.SteelBlue;
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(12, 75);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(230, 192);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 292;
            this.pictureBox2.TabStop = false;
            // 
            // btnSendImg
            // 
            this.btnSendImg.Location = new System.Drawing.Point(1023, 413);
            this.btnSendImg.Name = "btnSendImg";
            this.btnSendImg.Size = new System.Drawing.Size(26, 23);
            this.btnSendImg.TabIndex = 291;
            this.btnSendImg.Text = "|||";
            this.btnSendImg.UseVisualStyleBackColor = true;
            this.btnSendImg.Click += new System.EventHandler(this.btnSendImg_Click);
            // 
            // chatRTB
            // 
            this.chatRTB.Location = new System.Drawing.Point(281, 37);
            this.chatRTB.Name = "chatRTB";
            this.chatRTB.Size = new System.Drawing.Size(767, 370);
            this.chatRTB.TabIndex = 290;
            this.chatRTB.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.SteelBlue;
            this.label1.Location = new System.Drawing.Point(46, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(158, 31);
            this.label1.TabIndex = 287;
            this.label1.Text = "TRÒ CHUYỆN";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(281, 413);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(736, 22);
            this.textBox3.TabIndex = 289;
            // 
            // kryptonButton1
            // 
            this.kryptonButton1.AccessibleRole = System.Windows.Forms.AccessibleRole.OutlineButton;
            this.kryptonButton1.ButtonStyle = ComponentFactory.Krypton.Toolkit.ButtonStyle.Gallery;
            this.kryptonButton1.Location = new System.Drawing.Point(281, 441);
            this.kryptonButton1.Name = "kryptonButton1";
            this.kryptonButton1.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2010Blue;
            this.kryptonButton1.Size = new System.Drawing.Size(110, 36);
            this.kryptonButton1.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.kryptonButton1.TabIndex = 288;
            this.kryptonButton1.UseWaitCursor = true;
            this.kryptonButton1.Values.Text = "SEND";
            this.kryptonButton1.Click += new System.EventHandler(this.kryptonButton1_Click_1);
            // 
            // Chat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1061, 505);
            this.Controls.Add(this.ConnectBTN);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.btnSendImg);
            this.Controls.Add(this.chatRTB);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.kryptonButton1);
            this.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "Chat";
            this.Text = "Chat";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonButton ConnectBTN;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button btnSendImg;
        private System.Windows.Forms.RichTextBox chatRTB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox3;
        private ComponentFactory.Krypton.Toolkit.KryptonButton kryptonButton1;
    }
}