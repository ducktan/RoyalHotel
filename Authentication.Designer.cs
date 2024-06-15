namespace Royal
{
    partial class Authentication
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
            System.Windows.Forms.TabControl tabControl1;
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.listBoxRemainPermission = new System.Windows.Forms.ListBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.listBoxCurrentPermission = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbbStaffType = new MetroFramework.Controls.MetroComboBox();
            this.kryptonBorderEdge1 = new ComponentFactory.Krypton.Toolkit.KryptonBorderEdge();
            this.label1 = new System.Windows.Forms.Label();
            tabControl1 = new System.Windows.Forms.TabControl();
            tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(this.tabPage1);
            tabControl1.Location = new System.Drawing.Point(40, 116);
            tabControl1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new System.Drawing.Size(1341, 1050);
            tabControl1.TabIndex = 224;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.AliceBlue;
            this.tabPage1.Controls.Add(this.groupBox3);
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Location = new System.Drawing.Point(8, 39);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage1.Size = new System.Drawing.Size(1325, 1003);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Phân quyền";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.listBoxRemainPermission);
            this.groupBox3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.ForeColor = System.Drawing.Color.DodgerBlue;
            this.groupBox3.Location = new System.Drawing.Point(777, 200);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox3.Size = new System.Drawing.Size(454, 736);
            this.groupBox3.TabIndex = 230;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "CÁC QUYỀN CÒN LẠI";
            // 
            // listBoxRemainPermission
            // 
            this.listBoxRemainPermission.FormattingEnabled = true;
            this.listBoxRemainPermission.ItemHeight = 45;
            this.listBoxRemainPermission.Location = new System.Drawing.Point(32, 53);
            this.listBoxRemainPermission.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.listBoxRemainPermission.Name = "listBoxRemainPermission";
            this.listBoxRemainPermission.Size = new System.Drawing.Size(412, 634);
            this.listBoxRemainPermission.TabIndex = 1;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.listBoxCurrentPermission);
            this.groupBox2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.ForeColor = System.Drawing.Color.DodgerBlue;
            this.groupBox2.Location = new System.Drawing.Point(68, 200);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox2.Size = new System.Drawing.Size(454, 728);
            this.groupBox2.TabIndex = 229;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "QUYỀN HIỆN TẠI";
            // 
            // listBoxCurrentPermission
            // 
            this.listBoxCurrentPermission.FormattingEnabled = true;
            this.listBoxCurrentPermission.ItemHeight = 45;
            this.listBoxCurrentPermission.Location = new System.Drawing.Point(10, 53);
            this.listBoxCurrentPermission.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.listBoxCurrentPermission.Name = "listBoxCurrentPermission";
            this.listBoxCurrentPermission.Size = new System.Drawing.Size(412, 634);
            this.listBoxCurrentPermission.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbbStaffType);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.DodgerBlue;
            this.groupBox1.Location = new System.Drawing.Point(68, 31);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Size = new System.Drawing.Size(672, 134);
            this.groupBox1.TabIndex = 228;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "LOẠI NV";
            // 
            // cbbStaffType
            // 
            this.cbbStaffType.BackColor = System.Drawing.Color.White;
            this.cbbStaffType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbbStaffType.FormattingEnabled = true;
            this.cbbStaffType.ItemHeight = 23;
            this.cbbStaffType.Location = new System.Drawing.Point(21, 53);
            this.cbbStaffType.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.cbbStaffType.Name = "cbbStaffType";
            this.cbbStaffType.Size = new System.Drawing.Size(544, 29);
            this.cbbStaffType.Style = MetroFramework.MetroColorStyle.Blue;
            this.cbbStaffType.TabIndex = 53;
            this.cbbStaffType.UseCustomBackColor = true;
            this.cbbStaffType.UseCustomForeColor = true;
            this.cbbStaffType.UseSelectable = true;
            this.cbbStaffType.UseStyleColors = true;
            // 
            // kryptonBorderEdge1
            // 
            this.kryptonBorderEdge1.Location = new System.Drawing.Point(40, 105);
            this.kryptonBorderEdge1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.kryptonBorderEdge1.Name = "kryptonBorderEdge1";
            this.kryptonBorderEdge1.Size = new System.Drawing.Size(1185, 1);
            this.kryptonBorderEdge1.StateCommon.Color1 = System.Drawing.Color.DodgerBlue;
            this.kryptonBorderEdge1.StateCommon.Color2 = System.Drawing.Color.DodgerBlue;
            this.kryptonBorderEdge1.Text = "kryptonBorderEdge1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DodgerBlue;
            this.label1.Location = new System.Drawing.Point(32, 42);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(335, 50);
            this.label1.TabIndex = 223;
            this.label1.Text = "QUYỀN TRUY CẬP";
            // 
            // Authentication
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1414, 1206);
            this.Controls.Add(tabControl1);
            this.Controls.Add(this.kryptonBorderEdge1);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Authentication";
            this.Text = "Phân quyền";
            tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private ComponentFactory.Krypton.Toolkit.KryptonBorderEdge kryptonBorderEdge1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ListBox listBoxRemainPermission;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListBox listBoxCurrentPermission;
        private System.Windows.Forms.GroupBox groupBox1;
        private MetroFramework.Controls.MetroComboBox cbbStaffType;
    }
}