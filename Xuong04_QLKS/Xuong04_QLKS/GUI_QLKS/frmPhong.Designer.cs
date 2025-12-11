namespace GUI_QLKS
{
    partial class frmPhong
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPhong));
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges9 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges10 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            sqlCommand1 = new Microsoft.Data.SqlClient.SqlCommand();
            panel2 = new Panel();
            label1 = new Label();
            guna2GroupBox1 = new Guna.UI2.WinForms.Guna2GroupBox();
            cboLoaiPhong = new ComboBox();
            txtGiaPhong = new TextBox();
            label7 = new Label();
            rdbDeActive = new RadioButton();
            label6 = new Label();
            rdbActive = new RadioButton();
            label8 = new Label();
            dtpNgayTao = new DateTimePicker();
            label5 = new Label();
            btnMoiP = new Button();
            btnXoaP = new Button();
            btnSuaP = new Button();
            btnThemP = new Button();
            txtGhiChu = new TextBox();
            label4 = new Label();
            txtTenPhong = new TextBox();
            label3 = new Label();
            txtPhongID = new TextBox();
            label2 = new Label();
            guna2GroupBox2 = new Guna.UI2.WinForms.Guna2GroupBox();
            dgrDanhSachP = new Guna.UI2.WinForms.Guna2DataGridView();
            guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            txtTimKiemP = new Guna.UI2.WinForms.Guna2TextBox();
            btnTimKiemP = new Guna.UI2.WinForms.Guna2Button();
            panel2.SuspendLayout();
            guna2GroupBox1.SuspendLayout();
            guna2GroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgrDanhSachP).BeginInit();
            guna2Panel1.SuspendLayout();
            SuspendLayout();
            // 
            // sqlCommand1
            // 
            sqlCommand1.CommandTimeout = 30;
            sqlCommand1.EnableOptimizedParameterBinding = false;
            // 
            // panel2
            // 
            panel2.Controls.Add(label1);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(1414, 69);
            panel2.TabIndex = 15;
            // 
            // label1
            // 
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            label1.ForeColor = SystemColors.HotTrack;
            label1.Location = new Point(0, 11);
            label1.Name = "label1";
            label1.Size = new Size(1411, 46);
            label1.TabIndex = 4;
            label1.Text = "PHÒNG";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // guna2GroupBox1
            // 
            guna2GroupBox1.Controls.Add(cboLoaiPhong);
            guna2GroupBox1.Controls.Add(txtGiaPhong);
            guna2GroupBox1.Controls.Add(label7);
            guna2GroupBox1.Controls.Add(rdbDeActive);
            guna2GroupBox1.Controls.Add(label6);
            guna2GroupBox1.Controls.Add(rdbActive);
            guna2GroupBox1.Controls.Add(label8);
            guna2GroupBox1.Controls.Add(dtpNgayTao);
            guna2GroupBox1.Controls.Add(label5);
            guna2GroupBox1.Controls.Add(btnMoiP);
            guna2GroupBox1.Controls.Add(btnXoaP);
            guna2GroupBox1.Controls.Add(btnSuaP);
            guna2GroupBox1.Controls.Add(btnThemP);
            guna2GroupBox1.Controls.Add(txtGhiChu);
            guna2GroupBox1.Controls.Add(label4);
            guna2GroupBox1.Controls.Add(txtTenPhong);
            guna2GroupBox1.Controls.Add(label3);
            guna2GroupBox1.Controls.Add(txtPhongID);
            guna2GroupBox1.Controls.Add(label2);
            guna2GroupBox1.CustomizableEdges = customizableEdges1;
            guna2GroupBox1.Dock = DockStyle.Left;
            guna2GroupBox1.Font = new Font("Segoe UI", 9F);
            guna2GroupBox1.ForeColor = Color.FromArgb(125, 137, 149);
            guna2GroupBox1.Location = new Point(0, 69);
            guna2GroupBox1.Name = "guna2GroupBox1";
            guna2GroupBox1.ShadowDecoration.CustomizableEdges = customizableEdges2;
            guna2GroupBox1.Size = new Size(420, 757);
            guna2GroupBox1.TabIndex = 16;
            guna2GroupBox1.Text = "Thông Tin";
            // 
            // cboLoaiPhong
            // 
            cboLoaiPhong.FormattingEnabled = true;
            cboLoaiPhong.Location = new Point(150, 214);
            cboLoaiPhong.Name = "cboLoaiPhong";
            cboLoaiPhong.Size = new Size(241, 28);
            cboLoaiPhong.TabIndex = 36;
            // 
            // txtGiaPhong
            // 
            txtGiaPhong.Font = new Font("Segoe UI", 12F);
            txtGiaPhong.Location = new Point(150, 266);
            txtGiaPhong.Margin = new Padding(3, 4, 3, 4);
            txtGiaPhong.Name = "txtGiaPhong";
            txtGiaPhong.Size = new Size(241, 34);
            txtGiaPhong.TabIndex = 35;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.BackColor = Color.Transparent;
            label7.Font = new Font("Segoe UI", 9F);
            label7.ForeColor = Color.Maroon;
            label7.Location = new Point(15, 277);
            label7.Name = "label7";
            label7.Size = new Size(77, 20);
            label7.TabIndex = 33;
            label7.Text = "Giá Phòng";
            // 
            // rdbDeActive
            // 
            rdbDeActive.AutoSize = true;
            rdbDeActive.BackColor = Color.Transparent;
            rdbDeActive.Font = new Font("Segoe UI", 9F);
            rdbDeActive.ForeColor = Color.Maroon;
            rdbDeActive.Location = new Point(291, 375);
            rdbDeActive.Margin = new Padding(3, 4, 3, 4);
            rdbDeActive.Name = "rdbDeActive";
            rdbDeActive.Size = new Size(109, 24);
            rdbDeActive.TabIndex = 31;
            rdbDeActive.Text = "Tạm Ngưng";
            rdbDeActive.UseVisualStyleBackColor = false;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.BackColor = Color.Transparent;
            label6.Font = new Font("Segoe UI", 9F);
            label6.ForeColor = Color.Maroon;
            label6.Location = new Point(15, 219);
            label6.Name = "label6";
            label6.Size = new Size(83, 20);
            label6.TabIndex = 32;
            label6.Text = "Loại Phòng";
            // 
            // rdbActive
            // 
            rdbActive.AutoSize = true;
            rdbActive.BackColor = Color.Transparent;
            rdbActive.Checked = true;
            rdbActive.Font = new Font("Segoe UI", 9F);
            rdbActive.ForeColor = Color.Maroon;
            rdbActive.Location = new Point(140, 375);
            rdbActive.Margin = new Padding(3, 4, 3, 4);
            rdbActive.Name = "rdbActive";
            rdbActive.Size = new Size(144, 24);
            rdbActive.TabIndex = 30;
            rdbActive.TabStop = true;
            rdbActive.Text = "Đang Hoạt Động";
            rdbActive.UseVisualStyleBackColor = false;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.BackColor = Color.Transparent;
            label8.Font = new Font("Segoe UI", 9F);
            label8.ForeColor = Color.Maroon;
            label8.Location = new Point(15, 381);
            label8.Name = "label8";
            label8.Size = new Size(78, 20);
            label8.TabIndex = 29;
            label8.Text = "Tình Trạng";
            // 
            // dtpNgayTao
            // 
            dtpNgayTao.CustomFormat = "dd/MM/yyyy";
            dtpNgayTao.Font = new Font("Segoe UI", 10F);
            dtpNgayTao.Format = DateTimePickerFormat.Custom;
            dtpNgayTao.Location = new Point(150, 321);
            dtpNgayTao.Margin = new Padding(3, 4, 3, 4);
            dtpNgayTao.Name = "dtpNgayTao";
            dtpNgayTao.Size = new Size(236, 30);
            dtpNgayTao.TabIndex = 28;
            dtpNgayTao.ValueChanged += dtpNgayTao_ValueChanged;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.BackColor = Color.Transparent;
            label5.Font = new Font("Segoe UI", 9F);
            label5.ForeColor = Color.Maroon;
            label5.Location = new Point(15, 331);
            label5.Name = "label5";
            label5.Size = new Size(73, 20);
            label5.TabIndex = 27;
            label5.Text = "Ngày Tạo";
            // 
            // btnMoiP
            // 
            btnMoiP.BackColor = SystemColors.Window;
            btnMoiP.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnMoiP.Image = (Image)resources.GetObject("btnMoiP.Image");
            btnMoiP.Location = new Point(304, 601);
            btnMoiP.Margin = new Padding(3, 4, 3, 4);
            btnMoiP.Name = "btnMoiP";
            btnMoiP.Size = new Size(96, 55);
            btnMoiP.TabIndex = 26;
            btnMoiP.Text = "Mới";
            btnMoiP.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnMoiP.UseVisualStyleBackColor = false;
            btnMoiP.Click += btnMoiP_Click_1;
            // 
            // btnXoaP
            // 
            btnXoaP.BackColor = SystemColors.Window;
            btnXoaP.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnXoaP.Image = (Image)resources.GetObject("btnXoaP.Image");
            btnXoaP.Location = new Point(206, 601);
            btnXoaP.Margin = new Padding(3, 4, 3, 4);
            btnXoaP.Name = "btnXoaP";
            btnXoaP.Size = new Size(91, 55);
            btnXoaP.TabIndex = 25;
            btnXoaP.Text = "Xóa";
            btnXoaP.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnXoaP.UseVisualStyleBackColor = false;
            btnXoaP.Click += btnXoaP_Click_1;
            // 
            // btnSuaP
            // 
            btnSuaP.BackColor = SystemColors.Window;
            btnSuaP.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnSuaP.Image = (Image)resources.GetObject("btnSuaP.Image");
            btnSuaP.Location = new Point(113, 601);
            btnSuaP.Margin = new Padding(3, 4, 3, 4);
            btnSuaP.Name = "btnSuaP";
            btnSuaP.Size = new Size(86, 55);
            btnSuaP.TabIndex = 24;
            btnSuaP.Text = "Sửa";
            btnSuaP.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnSuaP.UseVisualStyleBackColor = false;
            btnSuaP.Click += btnSuaP_Click_1;
            // 
            // btnThemP
            // 
            btnThemP.BackColor = SystemColors.Window;
            btnThemP.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnThemP.Image = (Image)resources.GetObject("btnThemP.Image");
            btnThemP.Location = new Point(14, 601);
            btnThemP.Margin = new Padding(3, 4, 3, 4);
            btnThemP.Name = "btnThemP";
            btnThemP.Size = new Size(93, 55);
            btnThemP.TabIndex = 23;
            btnThemP.Text = "Thêm";
            btnThemP.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnThemP.UseVisualStyleBackColor = false;
            btnThemP.Click += btnThemP_Click_1;
            // 
            // txtGhiChu
            // 
            txtGhiChu.Font = new Font("Segoe UI", 12F);
            txtGhiChu.Location = new Point(150, 422);
            txtGhiChu.Margin = new Padding(3, 4, 3, 4);
            txtGhiChu.Multiline = true;
            txtGhiChu.Name = "txtGhiChu";
            txtGhiChu.Size = new Size(241, 160);
            txtGhiChu.TabIndex = 22;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = Color.Transparent;
            label4.Font = new Font("Segoe UI", 9F);
            label4.ForeColor = Color.Maroon;
            label4.Location = new Point(15, 433);
            label4.Name = "label4";
            label4.Size = new Size(60, 20);
            label4.TabIndex = 19;
            label4.Text = "Ghi Chú";
            // 
            // txtTenPhong
            // 
            txtTenPhong.Font = new Font("Segoe UI", 12F);
            txtTenPhong.Location = new Point(150, 142);
            txtTenPhong.Margin = new Padding(3, 4, 3, 4);
            txtTenPhong.Name = "txtTenPhong";
            txtTenPhong.Size = new Size(241, 34);
            txtTenPhong.TabIndex = 21;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.Transparent;
            label3.Font = new Font("Segoe UI", 9F);
            label3.ForeColor = Color.Maroon;
            label3.Location = new Point(15, 153);
            label3.Name = "label3";
            label3.Size = new Size(78, 20);
            label3.TabIndex = 18;
            label3.Text = "Tên Phòng";
            // 
            // txtPhongID
            // 
            txtPhongID.Font = new Font("Segoe UI", 12F);
            txtPhongID.Location = new Point(150, 77);
            txtPhongID.Margin = new Padding(3, 4, 3, 4);
            txtPhongID.Name = "txtPhongID";
            txtPhongID.ReadOnly = true;
            txtPhongID.Size = new Size(241, 34);
            txtPhongID.TabIndex = 20;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Segoe UI", 9F);
            label2.ForeColor = Color.Maroon;
            label2.Location = new Point(15, 81);
            label2.Name = "label2";
            label2.Size = new Size(76, 20);
            label2.TabIndex = 17;
            label2.Text = "Mã Phòng";
            // 
            // guna2GroupBox2
            // 
            guna2GroupBox2.Controls.Add(dgrDanhSachP);
            guna2GroupBox2.Controls.Add(guna2Panel1);
            guna2GroupBox2.CustomizableEdges = customizableEdges9;
            guna2GroupBox2.Dock = DockStyle.Fill;
            guna2GroupBox2.Font = new Font("Segoe UI", 9F);
            guna2GroupBox2.ForeColor = Color.FromArgb(125, 137, 149);
            guna2GroupBox2.Location = new Point(420, 69);
            guna2GroupBox2.Name = "guna2GroupBox2";
            guna2GroupBox2.ShadowDecoration.CustomizableEdges = customizableEdges10;
            guna2GroupBox2.Size = new Size(994, 757);
            guna2GroupBox2.TabIndex = 17;
            guna2GroupBox2.Text = "Danh Sách";
            // 
            // dgrDanhSachP
            // 
            dataGridViewCellStyle1.BackColor = Color.White;
            dgrDanhSachP.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(100, 88, 255);
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dgrDanhSachP.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dgrDanhSachP.ColumnHeadersHeight = 50;
            dgrDanhSachP.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = Color.White;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dataGridViewCellStyle3.ForeColor = Color.FromArgb(125, 137, 149);
            dataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(231, 229, 255);
            dataGridViewCellStyle3.SelectionForeColor = Color.FromArgb(71, 69, 94);
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            dgrDanhSachP.DefaultCellStyle = dataGridViewCellStyle3;
            dgrDanhSachP.Dock = DockStyle.Fill;
            dgrDanhSachP.GridColor = Color.FromArgb(231, 229, 255);
            dgrDanhSachP.Location = new Point(0, 101);
            dgrDanhSachP.Name = "dgrDanhSachP";
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = SystemColors.Control;
            dataGridViewCellStyle4.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle4.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.True;
            dgrDanhSachP.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            dgrDanhSachP.RowHeadersVisible = false;
            dgrDanhSachP.RowHeadersWidth = 51;
            dgrDanhSachP.Size = new Size(994, 656);
            dgrDanhSachP.TabIndex = 11;
            dgrDanhSachP.ThemeStyle.AlternatingRowsStyle.BackColor = Color.White;
            dgrDanhSachP.ThemeStyle.AlternatingRowsStyle.Font = null;
            dgrDanhSachP.ThemeStyle.AlternatingRowsStyle.ForeColor = Color.Empty;
            dgrDanhSachP.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = Color.Empty;
            dgrDanhSachP.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = Color.Empty;
            dgrDanhSachP.ThemeStyle.BackColor = Color.White;
            dgrDanhSachP.ThemeStyle.GridColor = Color.FromArgb(231, 229, 255);
            dgrDanhSachP.ThemeStyle.HeaderStyle.BackColor = Color.FromArgb(100, 88, 255);
            dgrDanhSachP.ThemeStyle.HeaderStyle.BorderStyle = DataGridViewHeaderBorderStyle.None;
            dgrDanhSachP.ThemeStyle.HeaderStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dgrDanhSachP.ThemeStyle.HeaderStyle.ForeColor = Color.White;
            dgrDanhSachP.ThemeStyle.HeaderStyle.HeaightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dgrDanhSachP.ThemeStyle.HeaderStyle.Height = 50;
            dgrDanhSachP.ThemeStyle.ReadOnly = false;
            dgrDanhSachP.ThemeStyle.RowsStyle.BackColor = Color.White;
            dgrDanhSachP.ThemeStyle.RowsStyle.BorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgrDanhSachP.ThemeStyle.RowsStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dgrDanhSachP.ThemeStyle.RowsStyle.ForeColor = SystemColors.HotTrack;
            dgrDanhSachP.ThemeStyle.RowsStyle.Height = 29;
            dgrDanhSachP.ThemeStyle.RowsStyle.SelectionBackColor = Color.FromArgb(231, 229, 255);
            dgrDanhSachP.ThemeStyle.RowsStyle.SelectionForeColor = Color.FromArgb(71, 69, 94);
            dgrDanhSachP.CellDoubleClick += dgrDanhSachP_CellDoubleClick;
            // 
            // guna2Panel1
            // 
            guna2Panel1.Controls.Add(txtTimKiemP);
            guna2Panel1.Controls.Add(btnTimKiemP);
            guna2Panel1.CustomizableEdges = customizableEdges7;
            guna2Panel1.Dock = DockStyle.Top;
            guna2Panel1.Location = new Point(0, 40);
            guna2Panel1.Name = "guna2Panel1";
            guna2Panel1.ShadowDecoration.CustomizableEdges = customizableEdges8;
            guna2Panel1.Size = new Size(994, 61);
            guna2Panel1.TabIndex = 0;
            // 
            // txtTimKiemP
            // 
            txtTimKiemP.CustomizableEdges = customizableEdges3;
            txtTimKiemP.DefaultText = "";
            txtTimKiemP.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            txtTimKiemP.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            txtTimKiemP.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            txtTimKiemP.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            txtTimKiemP.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            txtTimKiemP.Font = new Font("Segoe UI", 9F);
            txtTimKiemP.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            txtTimKiemP.Location = new Point(704, 13);
            txtTimKiemP.Margin = new Padding(3, 5, 3, 5);
            txtTimKiemP.Name = "txtTimKiemP";
            txtTimKiemP.PasswordChar = '\0';
            txtTimKiemP.PlaceholderText = "";
            txtTimKiemP.SelectedText = "";
            txtTimKiemP.ShadowDecoration.CustomizableEdges = customizableEdges4;
            txtTimKiemP.Size = new Size(229, 40);
            txtTimKiemP.TabIndex = 9;
            // 
            // btnTimKiemP
            // 
            btnTimKiemP.BorderRadius = 15;
            btnTimKiemP.CustomBorderColor = Color.Black;
            btnTimKiemP.CustomizableEdges = customizableEdges5;
            btnTimKiemP.DisabledState.BorderColor = Color.DarkGray;
            btnTimKiemP.DisabledState.CustomBorderColor = Color.DarkGray;
            btnTimKiemP.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnTimKiemP.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnTimKiemP.FillColor = Color.LightBlue;
            btnTimKiemP.Font = new Font("Segoe UI", 9F);
            btnTimKiemP.ForeColor = Color.WhiteSmoke;
            btnTimKiemP.Image = (Image)resources.GetObject("btnTimKiemP.Image");
            btnTimKiemP.Location = new Point(939, 13);
            btnTimKiemP.Margin = new Padding(3, 4, 3, 4);
            btnTimKiemP.Name = "btnTimKiemP";
            btnTimKiemP.ShadowDecoration.CustomizableEdges = customizableEdges6;
            btnTimKiemP.Size = new Size(38, 40);
            btnTimKiemP.TabIndex = 10;
            btnTimKiemP.Click += btnTimKiemP_Click_1;
            // 
            // frmPhong
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1414, 826);
            Controls.Add(guna2GroupBox2);
            Controls.Add(guna2GroupBox1);
            Controls.Add(panel2);
            Margin = new Padding(3, 4, 3, 4);
            Name = "frmPhong";
            Text = "Phòng";
            Load += frmPhong_Load;
            panel2.ResumeLayout(false);
            guna2GroupBox1.ResumeLayout(false);
            guna2GroupBox1.PerformLayout();
            guna2GroupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgrDanhSachP).EndInit();
            guna2Panel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private Microsoft.Data.SqlClient.SqlCommand sqlCommand1;
        private Panel panel2;
        private Label label1;
        private Guna.UI2.WinForms.Guna2GroupBox guna2GroupBox1;
        private TextBox txtGiaPhong;
        //private ComboBox cboLoaiPhong;
        private Label label7;
        private RadioButton rdbDeActive;
        private Label label6;
        private RadioButton rdbActive;
        private Label label8;
        private DateTimePicker dtpNgayTao;
        private Label label5;
        private Button btnMoiP;
        private Button btnXoaP;
        private Button btnSuaP;
        private Button btnThemP;
        private TextBox txtGhiChu;
        private Label label4;
        private TextBox txtTenPhong;
        private Label label3;
        private TextBox txtPhongID;
        private Label label2;
        private Guna.UI2.WinForms.Guna2GroupBox guna2GroupBox2;
        private Guna.UI2.WinForms.Guna2DataGridView dgrDanhSachP;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private Guna.UI2.WinForms.Guna2TextBox txtTimKiemP;
        private Guna.UI2.WinForms.Guna2Button btnTimKiemP;
        private ComboBox cboLoaiPhong;
    }
}