using System.Drawing;
using System.Windows.Forms;

namespace Royal
{
    partial class BillDetail
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
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.thanhtien = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.SLG_DV = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.dongiaDV = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.maDV = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.maKH = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.maHDBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dataGridViewBillDetail = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.addBut = new System.Windows.Forms.Button();
            this.delBut = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewBillDetail)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DodgerBlue;
            this.label1.Location = new System.Drawing.Point(27, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(220, 31);
            this.label1.TabIndex = 0;
            this.label1.Text = "CHI TIẾT HOÁ ĐƠN";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button3);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.delBut);
            this.groupBox1.Controls.Add(this.addBut);
            this.groupBox1.Controls.Add(this.thanhtien);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.SLG_DV);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.dongiaDV);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.maDV);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.maKH);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.maHDBox);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.DodgerBlue;
            this.groupBox1.Location = new System.Drawing.Point(33, 68);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1070, 238);
            this.groupBox1.TabIndex = 140;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Thông tin Chi tiết";
            // 
            // thanhtien
            // 
            this.thanhtien.BackColor = System.Drawing.Color.White;
            this.thanhtien.Location = new System.Drawing.Point(606, 167);
            this.thanhtien.Name = "thanhtien";
            this.thanhtien.Size = new System.Drawing.Size(172, 34);
            this.thanhtien.TabIndex = 142;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(412, 173);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(171, 28);
            this.label7.TabIndex = 141;
            this.label7.Text = "Thành tiền dịch vụ";
            // 
            // SLG_DV
            // 
            this.SLG_DV.Location = new System.Drawing.Point(186, 167);
            this.SLG_DV.Name = "SLG_DV";
            this.SLG_DV.Size = new System.Drawing.Size(194, 34);
            this.SLG_DV.TabIndex = 9;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(20, 170);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(160, 28);
            this.label6.TabIndex = 8;
            this.label6.Text = "Số lượng dịch vụ";
            // 
            // dongiaDV
            // 
            this.dongiaDV.BackColor = System.Drawing.Color.White;
            this.dongiaDV.Location = new System.Drawing.Point(606, 106);
            this.dongiaDV.Name = "dongiaDV";
            this.dongiaDV.Size = new System.Drawing.Size(172, 34);
            this.dongiaDV.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(412, 112);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(149, 28);
            this.label5.TabIndex = 6;
            this.label5.Text = "Đơn giá dịch vụ";
            // 
            // maDV
            // 
            this.maDV.FormattingEnabled = true;
            this.maDV.Location = new System.Drawing.Point(606, 51);
            this.maDV.Name = "maDV";
            this.maDV.Size = new System.Drawing.Size(170, 36);
            this.maDV.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(412, 54);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(112, 28);
            this.label4.TabIndex = 4;
            this.label4.Text = "Mã dịch vụ:";
            // 
            // maKH
            // 
            this.maKH.BackColor = System.Drawing.Color.White;
            this.maKH.Location = new System.Drawing.Point(186, 106);
            this.maKH.Name = "maKH";
            this.maKH.ReadOnly = true;
            this.maKH.Size = new System.Drawing.Size(196, 34);
            this.maKH.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 112);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(145, 28);
            this.label3.TabIndex = 2;
            this.label3.Text = "Mã khách hàng";
            // 
            // maHDBox
            // 
            this.maHDBox.FormattingEnabled = true;
            this.maHDBox.Location = new System.Drawing.Point(186, 46);
            this.maHDBox.Name = "maHDBox";
            this.maHDBox.Size = new System.Drawing.Size(194, 36);
            this.maHDBox.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 28);
            this.label2.TabIndex = 0;
            this.label2.Text = "Hoá đơn:";
            // 
            // dataGridViewBillDetail
            // 
            this.dataGridViewBillDetail.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewBillDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewBillDetail.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6});
            this.dataGridViewBillDetail.GridColor = System.Drawing.SystemColors.WindowText;
            this.dataGridViewBillDetail.Location = new System.Drawing.Point(35, 324);
            this.dataGridViewBillDetail.Name = "dataGridViewBillDetail";
            this.dataGridViewBillDetail.RowHeadersWidth = 51;
            this.dataGridViewBillDetail.RowTemplate.Height = 24;
            this.dataGridViewBillDetail.Size = new System.Drawing.Size(1067, 265);
            this.dataGridViewBillDetail.TabIndex = 141;
            this.dataGridViewBillDetail.DefaultCellStyle.ForeColor = Color.Black;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Mã hoá đơn";
            this.Column1.MinimumWidth = 6;
            this.Column1.Name = "Column1";
            this.Column1.Width = 125;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Mã dịch vụ";
            this.Column2.MinimumWidth = 6;
            this.Column2.Name = "Column2";
            this.Column2.Width = 125;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Số lượng dịch vụ";
            this.Column3.MinimumWidth = 6;
            this.Column3.Name = "Column3";
            this.Column3.Width = 125;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Đơn giá dịch vụ";
            this.Column4.MinimumWidth = 6;
            this.Column4.Name = "Column4";
            this.Column4.Width = 125;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "Mã khách hàng";
            this.Column5.MinimumWidth = 6;
            this.Column5.Name = "Column5";
            this.Column5.Width = 125;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "Thành tiền";
            this.Column6.MinimumWidth = 6;
            this.Column6.Name = "Column6";
            this.Column6.Width = 125;
            // 
            // addBut
            // 
            this.addBut.BackColor = System.Drawing.Color.DodgerBlue;
            this.addBut.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addBut.ForeColor = System.Drawing.Color.White;
            this.addBut.Location = new System.Drawing.Point(824, 46);
            this.addBut.Name = "addBut";
            this.addBut.Size = new System.Drawing.Size(108, 41);
            this.addBut.TabIndex = 143;
            this.addBut.Text = "Thêm";
            this.addBut.UseVisualStyleBackColor = false;
            this.addBut.Click += new System.EventHandler(this.addBut_Click);
            // 
            // delBut
            // 
            this.delBut.BackColor = System.Drawing.Color.DodgerBlue;
            this.delBut.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.delBut.ForeColor = System.Drawing.Color.White;
            this.delBut.Location = new System.Drawing.Point(938, 44);
            this.delBut.Name = "delBut";
            this.delBut.Size = new System.Drawing.Size(108, 43);
            this.delBut.TabIndex = 144;
            this.delBut.Text = "Xoá";
            this.delBut.UseVisualStyleBackColor = false;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.DodgerBlue;
            this.button1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(938, 93);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(108, 43);
            this.button1.TabIndex = 146;
            this.button1.Text = "Refresh";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.DodgerBlue;
            this.button2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new System.Drawing.Point(824, 95);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(108, 41);
            this.button2.TabIndex = 145;
            this.button2.Text = "Sửa";
            this.button2.UseVisualStyleBackColor = false;
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.DodgerBlue;
            this.button3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.ForeColor = System.Drawing.Color.White;
            this.button3.Location = new System.Drawing.Point(824, 142);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(222, 59);
            this.button3.TabIndex = 147;
            this.button3.Text = "Quản lý dịch vụ";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // BillDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1115, 623);
            this.Controls.Add(this.dataGridViewBillDetail);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.Name = "BillDetail";
            this.Text = "BillDetail";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewBillDetail)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox maHDBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox maKH;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox SLG_DV;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox dongiaDV;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox maDV;
        private System.Windows.Forms.TextBox thanhtien;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DataGridView dataGridViewBillDetail;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.Button addBut;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button delBut;
    }
}