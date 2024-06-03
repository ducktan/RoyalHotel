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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbID = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.kryptonButton5 = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.RoleBox = new System.Windows.Forms.TextBox();
            this.chatRTB = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.kryptonButton1 = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSendImg = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbID);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.kryptonButton5);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.SteelBlue;
            this.groupBox1.Location = new System.Drawing.Point(16, 80);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(223, 193);
            this.groupBox1.TabIndex = 279;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "TÌM KIẾM";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // cbID
            // 
            this.cbID.DropDownWidth = 270;
            this.cbID.FormattingEnabled = true;
            this.cbID.Location = new System.Drawing.Point(18, 77);
            this.cbID.Name = "cbID";
            this.cbID.Size = new System.Drawing.Size(179, 33);
            this.cbID.TabIndex = 261;
            this.cbID.SelectedIndexChanged += new System.EventHandler(this.cbID_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label6.Location = new System.Drawing.Point(13, 35);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(165, 28);
            this.label6.TabIndex = 259;
            this.label6.Text = "▶ Mã nhân viên";
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // kryptonButton5
            // 
            this.kryptonButton5.AccessibleRole = System.Windows.Forms.AccessibleRole.OutlineButton;
            this.kryptonButton5.ButtonStyle = ComponentFactory.Krypton.Toolkit.ButtonStyle.Gallery;
            this.kryptonButton5.Location = new System.Drawing.Point(18, 126);
            this.kryptonButton5.Name = "kryptonButton5";
            this.kryptonButton5.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2010Blue;
            this.kryptonButton5.Size = new System.Drawing.Size(179, 36);
            this.kryptonButton5.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.kryptonButton5.TabIndex = 260;
            this.kryptonButton5.UseWaitCursor = true;
            this.kryptonButton5.Values.Text = "SEARCH";
            this.kryptonButton5.Click += new System.EventHandler(this.kryptonButton5_Click);
            // 
            // RoleBox
            // 
            this.RoleBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RoleBox.Location = new System.Drawing.Point(16, 328);
            this.RoleBox.Name = "RoleBox";
            this.RoleBox.ReadOnly = true;
            this.RoleBox.Size = new System.Drawing.Size(223, 27);
            this.RoleBox.TabIndex = 283;
            this.RoleBox.TextChanged += new System.EventHandler(this.RoleBox_TextChanged);
            // 
            // chatRTB
            // 
            this.chatRTB.Location = new System.Drawing.Point(282, 32);
            this.chatRTB.Name = "chatRTB";
            this.chatRTB.Size = new System.Drawing.Size(767, 370);
            this.chatRTB.TabIndex = 282;
            this.chatRTB.Text = "";
            this.chatRTB.TextChanged += new System.EventHandler(this.chatRTB_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.SteelBlue;
            this.label1.Location = new System.Drawing.Point(10, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(158, 31);
            this.label1.TabIndex = 277;
            this.label1.Text = "TRÒ CHUYỆN";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(282, 408);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(736, 22);
            this.textBox3.TabIndex = 281;
            this.textBox3.TextChanged += new System.EventHandler(this.textBox3_TextChanged);
            // 
            // kryptonButton1
            // 
            this.kryptonButton1.AccessibleRole = System.Windows.Forms.AccessibleRole.OutlineButton;
            this.kryptonButton1.ButtonStyle = ComponentFactory.Krypton.Toolkit.ButtonStyle.Gallery;
            this.kryptonButton1.Location = new System.Drawing.Point(282, 436);
            this.kryptonButton1.Name = "kryptonButton1";
            this.kryptonButton1.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2010Blue;
            this.kryptonButton1.Size = new System.Drawing.Size(110, 36);
            this.kryptonButton1.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.kryptonButton1.TabIndex = 278;
            this.kryptonButton1.UseWaitCursor = true;
            this.kryptonButton1.Values.Text = "SEND";
            this.kryptonButton1.Click += new System.EventHandler(this.kryptonButton1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.SteelBlue;
            this.label2.Location = new System.Drawing.Point(10, 281);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 31);
            this.label2.TabIndex = 280;
            this.label2.Text = "VAI TRÒ";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // btnSendImg
            // 
            this.btnSendImg.Location = new System.Drawing.Point(1024, 408);
            this.btnSendImg.Name = "btnSendImg";
            this.btnSendImg.Size = new System.Drawing.Size(26, 23);
            this.btnSendImg.TabIndex = 284;
            this.btnSendImg.Text = "|||";
            this.btnSendImg.UseVisualStyleBackColor = true;
            this.btnSendImg.Click += new System.EventHandler(this.button1_Click);
            // 
            // Chat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1061, 505);
            this.Controls.Add(this.btnSendImg);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.RoleBox);
            this.Controls.Add(this.chatRTB);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.kryptonButton1);
            this.Controls.Add(this.label2);
            this.Name = "Chat";
            this.Text = "Chat";
            this.Load += new System.EventHandler(this.Chat_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cbID;
        private System.Windows.Forms.Label label6;
        private ComponentFactory.Krypton.Toolkit.KryptonButton kryptonButton5;
        private System.Windows.Forms.TextBox RoleBox;
        private System.Windows.Forms.RichTextBox chatRTB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox3;
        private ComponentFactory.Krypton.Toolkit.KryptonButton kryptonButton1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSendImg;
    }
}