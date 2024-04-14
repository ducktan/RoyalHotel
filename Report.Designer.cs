namespace Royal
{
    partial class Report
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.kryptonButton5 = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.label2 = new System.Windows.Forms.Label();
            this.numericYear = new System.Windows.Forms.NumericUpDown();
            this.comboBoxMonth = new MetroFramework.Controls.MetroComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.kryptonBorderEdge1 = new ComponentFactory.Krypton.Toolkit.KryptonBorderEdge();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericYear)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.kryptonButton5);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.numericYear);
            this.groupBox3.Controls.Add(this.comboBoxMonth);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.ForeColor = System.Drawing.Color.SteelBlue;
            this.groupBox3.Location = new System.Drawing.Point(22, 83);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox3.Size = new System.Drawing.Size(1120, 85);
            this.groupBox3.TabIndex = 79;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "THỐNG KÊ";
            // 
            // kryptonButton5
            // 
            this.kryptonButton5.AccessibleRole = System.Windows.Forms.AccessibleRole.OutlineButton;
            this.kryptonButton5.ButtonStyle = ComponentFactory.Krypton.Toolkit.ButtonStyle.Gallery;
            this.kryptonButton5.Location = new System.Drawing.Point(781, 31);
            this.kryptonButton5.Name = "kryptonButton5";
            this.kryptonButton5.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2010Blue;
            this.kryptonButton5.Size = new System.Drawing.Size(312, 36);
            this.kryptonButton5.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.kryptonButton5.TabIndex = 207;
            this.kryptonButton5.UseWaitCursor = true;
            this.kryptonButton5.Values.Text = "XEM KẾT QUẢ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.SteelBlue;
            this.label2.Location = new System.Drawing.Point(11, 39);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 25);
            this.label2.TabIndex = 65;
            this.label2.Text = "Tháng:";
            // 
            // numericYear
            // 
            this.numericYear.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numericYear.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericYear.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.numericYear.Location = new System.Drawing.Point(443, 38);
            this.numericYear.Margin = new System.Windows.Forms.Padding(4);
            this.numericYear.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.numericYear.Minimum = new decimal(new int[] {
            1900,
            0,
            0,
            0});
            this.numericYear.Name = "numericYear";
            this.numericYear.Size = new System.Drawing.Size(267, 34);
            this.numericYear.TabIndex = 67;
            this.numericYear.Value = new decimal(new int[] {
            2018,
            0,
            0,
            0});
            // 
            // comboBoxMonth
            // 
            this.comboBoxMonth.BackColor = System.Drawing.Color.White;
            this.comboBoxMonth.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBoxMonth.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.comboBoxMonth.FormattingEnabled = true;
            this.comboBoxMonth.ItemHeight = 24;
            this.comboBoxMonth.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12"});
            this.comboBoxMonth.Location = new System.Drawing.Point(89, 37);
            this.comboBoxMonth.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxMonth.Name = "comboBoxMonth";
            this.comboBoxMonth.Size = new System.Drawing.Size(232, 30);
            this.comboBoxMonth.Style = MetroFramework.MetroColorStyle.Green;
            this.comboBoxMonth.TabIndex = 63;
            this.comboBoxMonth.UseCustomBackColor = true;
            this.comboBoxMonth.UseCustomForeColor = true;
            this.comboBoxMonth.UseSelectable = true;
            this.comboBoxMonth.UseStyleColors = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.SteelBlue;
            this.label3.Location = new System.Drawing.Point(377, 41);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 25);
            this.label3.TabIndex = 66;
            this.label3.Text = "Năm:";
            // 
            // kryptonBorderEdge1
            // 
            this.kryptonBorderEdge1.Location = new System.Drawing.Point(22, 62);
            this.kryptonBorderEdge1.Name = "kryptonBorderEdge1";
            this.kryptonBorderEdge1.Size = new System.Drawing.Size(1123, 1);
            this.kryptonBorderEdge1.Text = "kryptonBorderEdge1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.SteelBlue;
            this.label1.Location = new System.Drawing.Point(13, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(275, 31);
            this.label1.TabIndex = 78;
            this.label1.Text = "THỐNG KÊ DOANH THU";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3});
            this.dataGridView1.Location = new System.Drawing.Point(499, 203);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(643, 429);
            this.dataGridView1.TabIndex = 81;
            // 
            // chart1
            // 
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(25, 185);
            this.chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chart1.Series.Add(series1);
            this.chart1.Size = new System.Drawing.Size(440, 447);
            this.chart1.TabIndex = 82;
            this.chart1.Text = "chart1";
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Tên loại phòng";
            this.Column1.MinimumWidth = 6;
            this.Column1.Name = "Column1";
            this.Column1.Width = 250;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Tỷ lệ";
            this.Column2.MinimumWidth = 6;
            this.Column2.Name = "Column2";
            this.Column2.Width = 135;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Doanh thu";
            this.Column3.MinimumWidth = 6;
            this.Column3.Name = "Column3";
            this.Column3.Width = 200;
            // 
            // Report
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1158, 656);
            this.Controls.Add(this.chart1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.kryptonBorderEdge1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView1);
            this.Name = "Report";
            this.Text = "Report";
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericYear)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox3;
        private ComponentFactory.Krypton.Toolkit.KryptonButton kryptonButton5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericYear;
        private MetroFramework.Controls.MetroComboBox comboBoxMonth;
        private System.Windows.Forms.Label label3;
        private ComponentFactory.Krypton.Toolkit.KryptonBorderEdge kryptonBorderEdge1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
    }
}