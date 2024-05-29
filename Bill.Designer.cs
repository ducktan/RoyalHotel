namespace Royal
{
    partial class Bill
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Bill));
            this.kryptonButton2 = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.kryptonButton1 = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.dataGridBill = new System.Windows.Forms.DataGridView();
            this.MaHD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TenPhong = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TrangThai = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TenKH = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NVTao = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Ngaytao = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Dongia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Giamgia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Thanhtien = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.searchText = new ComponentFactory.Krypton.Toolkit.KryptonRichTextBox();
            this.bindingNavigatorCountItem = new System.Windows.Forms.ToolStripLabel();
            this.bindingNavigatorMoveFirstItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMovePreviousItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorPositionItem = new System.Windows.Forms.ToolStripTextBox();
            this.bindingNavigatorSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorMoveNextItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveLastItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.bindingStaff = new System.Windows.Forms.BindingNavigator(this.components);
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.DetailBut = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.delBut = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.updateBut = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.addBut = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.phong = new System.Windows.Forms.ComboBox();
            this.nhanvien = new System.Windows.Forms.ComboBox();
            this.maKHBox = new System.Windows.Forms.ComboBox();
            this.status = new System.Windows.Forms.ComboBox();
            this.date = new MetroFramework.Controls.MetroDateTime();
            this.mahoadon = new ComponentFactory.Krypton.Toolkit.KryptonRichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.total = new ComponentFactory.Krypton.Toolkit.KryptonRichTextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.discount = new ComponentFactory.Krypton.Toolkit.KryptonRichTextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.giaDon = new ComponentFactory.Krypton.Toolkit.KryptonRichTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridBill)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingStaff)).BeginInit();
            this.bindingStaff.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // kryptonButton2
            // 
            this.kryptonButton2.Cursor = System.Windows.Forms.Cursors.No;
            this.kryptonButton2.Location = new System.Drawing.Point(232, 96);
            this.kryptonButton2.Name = "kryptonButton2";
            this.kryptonButton2.Size = new System.Drawing.Size(212, 43);
            this.kryptonButton2.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.kryptonButton2.StateCommon.Border.Rounding = 20;
            this.kryptonButton2.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.kryptonButton2.TabIndex = 47;
            this.kryptonButton2.Values.Text = "CANCEL";
            // 
            // kryptonButton1
            // 
            this.kryptonButton1.Cursor = System.Windows.Forms.Cursors.No;
            this.kryptonButton1.Location = new System.Drawing.Point(232, 49);
            this.kryptonButton1.Name = "kryptonButton1";
            this.kryptonButton1.Size = new System.Drawing.Size(212, 41);
            this.kryptonButton1.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.kryptonButton1.StateCommon.Border.Rounding = 20;
            this.kryptonButton1.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.kryptonButton1.TabIndex = 46;
            this.kryptonButton1.Values.Text = "SEARCH";
            this.kryptonButton1.Click += new System.EventHandler(this.kryptonButton1_Click);
            // 
            // dataGridBill
            // 
            this.dataGridBill.AllowUserToOrderColumns = true;
            this.dataGridBill.AllowUserToResizeRows = false;
            this.dataGridBill.BackgroundColor = System.Drawing.Color.White;
            this.dataGridBill.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 12F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.SeaGreen;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridBill.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridBill.ColumnHeadersHeight = 40;
            this.dataGridBill.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridBill.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.MaHD,
            this.TenPhong,
            this.TrangThai,
            this.TenKH,
            this.NVTao,
            this.Ngaytao,
            this.Dongia,
            this.Giamgia,
            this.Thanhtien});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 12F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.SteelBlue;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.SeaGreen;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridBill.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridBill.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridBill.GridColor = System.Drawing.Color.White;
            this.dataGridBill.Location = new System.Drawing.Point(3, 73);
            this.dataGridBill.Name = "dataGridBill";
            this.dataGridBill.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 12F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.SeaGreen;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.SeaGreen;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridBill.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridBill.RowHeadersVisible = false;
            this.dataGridBill.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dataGridBill.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dataGridBill.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridBill.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.dataGridBill.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridBill.Size = new System.Drawing.Size(1010, 604);
            this.dataGridBill.TabIndex = 28;
            this.dataGridBill.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridBill_CellContentClick);
            // 
            // MaHD
            // 
            this.MaHD.HeaderText = "Mã hoá đơn";
            this.MaHD.MinimumWidth = 6;
            this.MaHD.Name = "MaHD";
            this.MaHD.ReadOnly = true;
            this.MaHD.Width = 125;
            // 
            // TenPhong
            // 
            this.TenPhong.HeaderText = "Mã phòng";
            this.TenPhong.MinimumWidth = 6;
            this.TenPhong.Name = "TenPhong";
            this.TenPhong.ReadOnly = true;
            this.TenPhong.Width = 125;
            // 
            // TrangThai
            // 
            this.TrangThai.HeaderText = "Trạng Thái";
            this.TrangThai.MinimumWidth = 6;
            this.TrangThai.Name = "TrangThai";
            this.TrangThai.ReadOnly = true;
            this.TrangThai.Width = 125;
            // 
            // TenKH
            // 
            this.TenKH.HeaderText = "Mã khách hàng";
            this.TenKH.MinimumWidth = 6;
            this.TenKH.Name = "TenKH";
            this.TenKH.ReadOnly = true;
            this.TenKH.Width = 125;
            // 
            // NVTao
            // 
            this.NVTao.HeaderText = "Nhân viên tạo";
            this.NVTao.MinimumWidth = 6;
            this.NVTao.Name = "NVTao";
            this.NVTao.ReadOnly = true;
            this.NVTao.Width = 125;
            // 
            // Ngaytao
            // 
            this.Ngaytao.HeaderText = "Ngày tạo";
            this.Ngaytao.MinimumWidth = 6;
            this.Ngaytao.Name = "Ngaytao";
            this.Ngaytao.ReadOnly = true;
            this.Ngaytao.Width = 125;
            // 
            // Dongia
            // 
            this.Dongia.HeaderText = "Đơn giá";
            this.Dongia.MinimumWidth = 6;
            this.Dongia.Name = "Dongia";
            this.Dongia.ReadOnly = true;
            this.Dongia.Width = 125;
            // 
            // Giamgia
            // 
            this.Giamgia.HeaderText = "Giảm giá";
            this.Giamgia.MinimumWidth = 6;
            this.Giamgia.Name = "Giamgia";
            this.Giamgia.ReadOnly = true;
            this.Giamgia.Width = 125;
            // 
            // Thanhtien
            // 
            this.Thanhtien.HeaderText = "Thành tiền";
            this.Thanhtien.MinimumWidth = 6;
            this.Thanhtien.Name = "Thanhtien";
            this.Thanhtien.ReadOnly = true;
            this.Thanhtien.Width = 125;
            // 
            // searchText
            // 
            this.searchText.Location = new System.Drawing.Point(10, 103);
            this.searchText.Name = "searchText";
            this.searchText.Size = new System.Drawing.Size(206, 25);
            this.searchText.TabIndex = 45;
            this.searchText.Text = "";
            // 
            // bindingNavigatorCountItem
            // 
            this.bindingNavigatorCountItem.AutoSize = false;
            this.bindingNavigatorCountItem.Name = "bindingNavigatorCountItem";
            this.bindingNavigatorCountItem.Size = new System.Drawing.Size(80, 40);
            this.bindingNavigatorCountItem.Text = "of {0}";
            this.bindingNavigatorCountItem.ToolTipText = "Total number of items";
            // 
            // bindingNavigatorMoveFirstItem
            // 
            this.bindingNavigatorMoveFirstItem.AutoSize = false;
            this.bindingNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveFirstItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveFirstItem.Image")));
            this.bindingNavigatorMoveFirstItem.Name = "bindingNavigatorMoveFirstItem";
            this.bindingNavigatorMoveFirstItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveFirstItem.Size = new System.Drawing.Size(40, 40);
            this.bindingNavigatorMoveFirstItem.Text = "Move first";
            // 
            // bindingNavigatorMovePreviousItem
            // 
            this.bindingNavigatorMovePreviousItem.AutoSize = false;
            this.bindingNavigatorMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMovePreviousItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMovePreviousItem.Image")));
            this.bindingNavigatorMovePreviousItem.Name = "bindingNavigatorMovePreviousItem";
            this.bindingNavigatorMovePreviousItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMovePreviousItem.Size = new System.Drawing.Size(40, 40);
            this.bindingNavigatorMovePreviousItem.Text = "Move previous";
            // 
            // bindingNavigatorSeparator
            // 
            this.bindingNavigatorSeparator.Name = "bindingNavigatorSeparator";
            this.bindingNavigatorSeparator.Size = new System.Drawing.Size(6, 43);
            // 
            // bindingNavigatorPositionItem
            // 
            this.bindingNavigatorPositionItem.AccessibleName = "Position";
            this.bindingNavigatorPositionItem.AutoSize = false;
            this.bindingNavigatorPositionItem.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.bindingNavigatorPositionItem.Name = "bindingNavigatorPositionItem";
            this.bindingNavigatorPositionItem.Size = new System.Drawing.Size(80, 40);
            this.bindingNavigatorPositionItem.Text = "0";
            this.bindingNavigatorPositionItem.ToolTipText = "Current position";
            // 
            // bindingNavigatorSeparator1
            // 
            this.bindingNavigatorSeparator1.Name = "bindingNavigatorSeparator1";
            this.bindingNavigatorSeparator1.Size = new System.Drawing.Size(6, 43);
            // 
            // bindingNavigatorMoveNextItem
            // 
            this.bindingNavigatorMoveNextItem.AutoSize = false;
            this.bindingNavigatorMoveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveNextItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveNextItem.Image")));
            this.bindingNavigatorMoveNextItem.Name = "bindingNavigatorMoveNextItem";
            this.bindingNavigatorMoveNextItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveNextItem.Size = new System.Drawing.Size(40, 40);
            this.bindingNavigatorMoveNextItem.Text = "Move next";
            // 
            // bindingNavigatorMoveLastItem
            // 
            this.bindingNavigatorMoveLastItem.AutoSize = false;
            this.bindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveLastItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveLastItem.Image")));
            this.bindingNavigatorMoveLastItem.Name = "bindingNavigatorMoveLastItem";
            this.bindingNavigatorMoveLastItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveLastItem.Size = new System.Drawing.Size(40, 40);
            this.bindingNavigatorMoveLastItem.Text = "Move last";
            // 
            // bindingNavigatorSeparator2
            // 
            this.bindingNavigatorSeparator2.Name = "bindingNavigatorSeparator2";
            this.bindingNavigatorSeparator2.Size = new System.Drawing.Size(6, 43);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(39, 40);
            this.toolStripLabel1.Text = "Xuất";
            this.toolStripLabel1.Click += new System.EventHandler(this.toolStripLabel1_Click);
            // 
            // bindingStaff
            // 
            this.bindingStaff.AddNewItem = null;
            this.bindingStaff.CountItem = this.bindingNavigatorCountItem;
            this.bindingStaff.DeleteItem = null;
            this.bindingStaff.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.bindingStaff.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bindingNavigatorMoveFirstItem,
            this.bindingNavigatorMovePreviousItem,
            this.bindingNavigatorSeparator,
            this.bindingNavigatorPositionItem,
            this.bindingNavigatorCountItem,
            this.bindingNavigatorSeparator1,
            this.bindingNavigatorMoveNextItem,
            this.bindingNavigatorMoveLastItem,
            this.bindingNavigatorSeparator2,
            this.toolStripLabel1,
            this.toolStripButton1});
            this.bindingStaff.Location = new System.Drawing.Point(3, 30);
            this.bindingStaff.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.bindingStaff.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.bindingStaff.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.bindingStaff.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.bindingStaff.Name = "bindingStaff";
            this.bindingStaff.PositionItem = this.bindingNavigatorPositionItem;
            this.bindingStaff.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.bindingStaff.Size = new System.Drawing.Size(1010, 43);
            this.bindingStaff.TabIndex = 29;
            this.bindingStaff.Text = "bindingNavigator1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(29, 40);
            this.toolStripButton1.Text = "toolStripButton1";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.dataGridBill);
            this.groupBox4.Controls.Add(this.bindingStaff);
            this.groupBox4.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.groupBox4.ForeColor = System.Drawing.Color.SteelBlue;
            this.groupBox4.Location = new System.Drawing.Point(519, 16);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(1016, 680);
            this.groupBox4.TabIndex = 128;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Danh sách Hoá đơn";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Mã hoá đơn",
            "Mã nhân viên",
            "Mã khách hàng",
            "Ngày lập HĐ",
            "Trạng thái HĐ"});
            this.comboBox1.Location = new System.Drawing.Point(10, 61);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(201, 31);
            this.comboBox1.TabIndex = 3;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.DetailBut);
            this.groupBox3.Controls.Add(this.delBut);
            this.groupBox3.Controls.Add(this.updateBut);
            this.groupBox3.Controls.Add(this.addBut);
            this.groupBox3.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.ForeColor = System.Drawing.Color.SteelBlue;
            this.groupBox3.Location = new System.Drawing.Point(13, 574);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(477, 144);
            this.groupBox3.TabIndex = 127;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "CHỨC NĂNG";
            // 
            // DetailBut
            // 
            this.DetailBut.Cursor = System.Windows.Forms.Cursors.No;
            this.DetailBut.Location = new System.Drawing.Point(235, 95);
            this.DetailBut.Name = "DetailBut";
            this.DetailBut.Size = new System.Drawing.Size(171, 43);
            this.DetailBut.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.DetailBut.StateCommon.Border.Rounding = 20;
            this.DetailBut.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DetailBut.TabIndex = 70;
            this.DetailBut.Values.Text = "Chi tiết";
            this.DetailBut.Click += new System.EventHandler(this.DetailBut_Click);
            // 
            // delBut
            // 
            this.delBut.Cursor = System.Windows.Forms.Cursors.No;
            this.delBut.Location = new System.Drawing.Point(34, 95);
            this.delBut.Name = "delBut";
            this.delBut.Size = new System.Drawing.Size(158, 43);
            this.delBut.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.delBut.StateCommon.Border.Rounding = 20;
            this.delBut.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.delBut.TabIndex = 69;
            this.delBut.Values.Text = "Xóa";
            this.delBut.Click += new System.EventHandler(this.kryptonButton5_Click);
            // 
            // updateBut
            // 
            this.updateBut.Cursor = System.Windows.Forms.Cursors.No;
            this.updateBut.Location = new System.Drawing.Point(231, 36);
            this.updateBut.Name = "updateBut";
            this.updateBut.Size = new System.Drawing.Size(175, 43);
            this.updateBut.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.updateBut.StateCommon.Border.Rounding = 20;
            this.updateBut.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.updateBut.TabIndex = 68;
            this.updateBut.Values.Text = "Sửa";
            this.updateBut.Click += new System.EventHandler(this.kryptonButton4_Click);
            // 
            // addBut
            // 
            this.addBut.Cursor = System.Windows.Forms.Cursors.No;
            this.addBut.Location = new System.Drawing.Point(34, 36);
            this.addBut.Name = "addBut";
            this.addBut.Size = new System.Drawing.Size(158, 43);
            this.addBut.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.addBut.StateCommon.Border.Rounding = 20;
            this.addBut.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addBut.TabIndex = 67;
            this.addBut.Values.Text = "Thêm";
            this.addBut.Click += new System.EventHandler(this.kryptonButton3_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.phong);
            this.groupBox2.Controls.Add(this.nhanvien);
            this.groupBox2.Controls.Add(this.maKHBox);
            this.groupBox2.Controls.Add(this.status);
            this.groupBox2.Controls.Add(this.date);
            this.groupBox2.Controls.Add(this.mahoadon);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.total);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.discount);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.giaDon);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.ForeColor = System.Drawing.Color.SteelBlue;
            this.groupBox2.Location = new System.Drawing.Point(16, 165);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(470, 391);
            this.groupBox2.TabIndex = 126;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "THÔNG TIN HOÁ ĐƠN";
            // 
            // phong
            // 
            this.phong.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.phong.FormattingEnabled = true;
            this.phong.IntegralHeight = false;
            this.phong.Location = new System.Drawing.Point(10, 130);
            this.phong.Name = "phong";
            this.phong.Size = new System.Drawing.Size(208, 33);
            this.phong.TabIndex = 73;
            // 
            // nhanvien
            // 
            this.nhanvien.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nhanvien.FormattingEnabled = true;
            this.nhanvien.IntegralHeight = false;
            this.nhanvien.Location = new System.Drawing.Point(10, 214);
            this.nhanvien.Name = "nhanvien";
            this.nhanvien.Size = new System.Drawing.Size(208, 33);
            this.nhanvien.TabIndex = 72;
            // 
            // maKHBox
            // 
            this.maKHBox.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.maKHBox.FormattingEnabled = true;
            this.maKHBox.IntegralHeight = false;
            this.maKHBox.Location = new System.Drawing.Point(232, 335);
            this.maKHBox.Name = "maKHBox";
            this.maKHBox.Size = new System.Drawing.Size(208, 33);
            this.maKHBox.TabIndex = 71;
            this.maKHBox.SelectedIndexChanged += new System.EventHandler(this.maKHBox_SelectedIndexChanged);
            // 
            // status
            // 
            this.status.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.status.FormattingEnabled = true;
            this.status.IntegralHeight = false;
            this.status.Items.AddRange(new object[] {
            "Đã thanh toán",
            "Chưa thanh toán"});
            this.status.Location = new System.Drawing.Point(239, 62);
            this.status.Name = "status";
            this.status.Size = new System.Drawing.Size(201, 33);
            this.status.TabIndex = 70;
            // 
            // date
            // 
            this.date.Location = new System.Drawing.Point(12, 286);
            this.date.MinimumSize = new System.Drawing.Size(0, 30);
            this.date.Name = "date";
            this.date.Size = new System.Drawing.Size(204, 31);
            this.date.TabIndex = 69;
            // 
            // mahoadon
            // 
            this.mahoadon.Location = new System.Drawing.Point(10, 62);
            this.mahoadon.Name = "mahoadon";
            this.mahoadon.ReadOnly = true;
            this.mahoadon.Size = new System.Drawing.Size(206, 25);
            this.mahoadon.TabIndex = 68;
            this.mahoadon.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.SteelBlue;
            this.label1.Location = new System.Drawing.Point(15, 340);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(152, 28);
            this.label1.TabIndex = 66;
            this.label1.Text = "Mã khách hàng";
            // 
            // total
            // 
            this.total.Location = new System.Drawing.Point(234, 292);
            this.total.Name = "total";
            this.total.ReadOnly = true;
            this.total.Size = new System.Drawing.Size(206, 25);
            this.total.TabIndex = 64;
            this.total.Text = "";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.SteelBlue;
            this.label9.Location = new System.Drawing.Point(229, 250);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(115, 28);
            this.label9.TabIndex = 63;
            this.label9.Text = "Thành tiền:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.SteelBlue;
            this.label10.Location = new System.Drawing.Point(7, 250);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(99, 28);
            this.label10.TabIndex = 62;
            this.label10.Text = "Ngày tạo:";
            // 
            // discount
            // 
            this.discount.Location = new System.Drawing.Point(234, 214);
            this.discount.Name = "discount";
            this.discount.Size = new System.Drawing.Size(206, 25);
            this.discount.TabIndex = 60;
            this.discount.Text = "";
            this.discount.TextChanged += new System.EventHandler(this.discount_TextChanged_1);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.SteelBlue;
            this.label7.Location = new System.Drawing.Point(229, 172);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(97, 28);
            this.label7.TabIndex = 59;
            this.label7.Text = "Giảm giá:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.SteelBlue;
            this.label8.Location = new System.Drawing.Point(7, 172);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(145, 28);
            this.label8.TabIndex = 58;
            this.label8.Text = "Nhân viên tạo:";
            // 
            // giaDon
            // 
            this.giaDon.Location = new System.Drawing.Point(234, 136);
            this.giaDon.Name = "giaDon";
            this.giaDon.Size = new System.Drawing.Size(206, 25);
            this.giaDon.TabIndex = 56;
            this.giaDon.Text = "";
            this.giaDon.TextChanged += new System.EventHandler(this.giaDon_TextChanged_1);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.SteelBlue;
            this.label5.Location = new System.Drawing.Point(229, 94);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(88, 28);
            this.label5.TabIndex = 55;
            this.label5.Text = "Đơn giá:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.SteelBlue;
            this.label6.Location = new System.Drawing.Point(11, 99);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(111, 28);
            this.label6.TabIndex = 53;
            this.label6.Text = "Mã phòng:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.SteelBlue;
            this.label4.Location = new System.Drawing.Point(229, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(107, 28);
            this.label4.TabIndex = 51;
            this.label4.Text = "Trạng thái:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.SteelBlue;
            this.label3.Location = new System.Drawing.Point(7, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(127, 28);
            this.label3.TabIndex = 49;
            this.label3.Text = "Mã hoá đơn:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.SteelBlue;
            this.label2.Location = new System.Drawing.Point(5, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(150, 28);
            this.label2.TabIndex = 2;
            this.label2.Text = "Tìm kiếm theo:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.kryptonButton2);
            this.groupBox1.Controls.Add(this.kryptonButton1);
            this.groupBox1.Controls.Add(this.searchText);
            this.groupBox1.Controls.Add(this.comboBox1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.SteelBlue;
            this.groupBox1.Location = new System.Drawing.Point(16, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(474, 148);
            this.groupBox1.TabIndex = 125;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Quản lý hóa đơn";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(519, 702);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(113, 34);
            this.button1.TabIndex = 71;
            this.button1.Text = "Refresh";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Bill
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1558, 745);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Bill";
            this.Text = "Bill";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridBill)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingStaff)).EndInit();
            this.bindingStaff.ResumeLayout(false);
            this.bindingStaff.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private ComponentFactory.Krypton.Toolkit.KryptonButton kryptonButton2;
        private ComponentFactory.Krypton.Toolkit.KryptonButton kryptonButton1;
        private System.Windows.Forms.DataGridView dataGridBill;
        private ComponentFactory.Krypton.Toolkit.KryptonRichTextBox searchText;
        private System.Windows.Forms.ToolStripLabel bindingNavigatorCountItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveFirstItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMovePreviousItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator;
        private System.Windows.Forms.ToolStripTextBox bindingNavigatorPositionItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator1;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveNextItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveLastItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator2;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.BindingNavigator bindingStaff;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private ComponentFactory.Krypton.Toolkit.KryptonButton updateBut;
        private ComponentFactory.Krypton.Toolkit.KryptonButton addBut;
        private System.Windows.Forms.GroupBox groupBox2;
        private ComponentFactory.Krypton.Toolkit.KryptonRichTextBox total;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private ComponentFactory.Krypton.Toolkit.KryptonRichTextBox discount;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private ComponentFactory.Krypton.Toolkit.KryptonRichTextBox giaDon;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private ComponentFactory.Krypton.Toolkit.KryptonButton DetailBut;
        private ComponentFactory.Krypton.Toolkit.KryptonButton delBut;
        private ComponentFactory.Krypton.Toolkit.KryptonRichTextBox mahoadon;
        private System.Windows.Forms.Button button1;
        private MetroFramework.Controls.MetroDateTime date;
        private System.Windows.Forms.ComboBox status;
        private System.Windows.Forms.ComboBox maKHBox;
        private System.Windows.Forms.DataGridViewTextBoxColumn MaHD;
        private System.Windows.Forms.DataGridViewTextBoxColumn TenPhong;
        private System.Windows.Forms.DataGridViewTextBoxColumn TrangThai;
        private System.Windows.Forms.DataGridViewTextBoxColumn TenKH;
        private System.Windows.Forms.DataGridViewTextBoxColumn NVTao;
        private System.Windows.Forms.DataGridViewTextBoxColumn Ngaytao;
        private System.Windows.Forms.DataGridViewTextBoxColumn Dongia;
        private System.Windows.Forms.DataGridViewTextBoxColumn Giamgia;
        private System.Windows.Forms.DataGridViewTextBoxColumn Thanhtien;
        private System.Windows.Forms.ComboBox nhanvien;
        private System.Windows.Forms.ComboBox phong;
    }
}