namespace GUI_QLKS
{
    partial class frmDoanhThuPhong
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
            panel1 = new Panel();
            btnThongKe = new Button();
            dtpDenNgay = new DateTimePicker();
            label4 = new Label();
            dtpTuNgay = new DateTimePicker();
            label3 = new Label();
            cbxPhong = new ComboBox();
            label2 = new Label();
            label1 = new Label();
            dgrDanhSachThongKe = new DataGridView();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgrDanhSachThongKe).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(btnThongKe);
            panel1.Controls.Add(dtpDenNgay);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(dtpTuNgay);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(cbxPhong);
            panel1.Controls.Add(label2);
            panel1.Location = new Point(16, 83);
            panel1.Margin = new Padding(3, 4, 3, 4);
            panel1.Name = "panel1";
            panel1.Size = new Size(1272, 81);
            panel1.TabIndex = 17;
            // 
            // btnThongKe
            // 
            btnThongKe.BackColor = Color.White;
            btnThongKe.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnThongKe.ForeColor = Color.Maroon;
            btnThongKe.Location = new Point(1112, 15);
            btnThongKe.Margin = new Padding(3, 4, 3, 4);
            btnThongKe.Name = "btnThongKe";
            btnThongKe.Size = new Size(136, 52);
            btnThongKe.TabIndex = 5;
            btnThongKe.Text = "Thống Kê";
            btnThongKe.UseVisualStyleBackColor = false;
            btnThongKe.Click += btnThongKe_Click_1;
            // 
            // dtpDenNgay
            // 
            dtpDenNgay.CustomFormat = "dd/MM/yyyy";
            dtpDenNgay.Font = new Font("Segoe UI", 10F);
            dtpDenNgay.Format = DateTimePickerFormat.Custom;
            dtpDenNgay.Location = new Point(845, 24);
            dtpDenNgay.Margin = new Padding(3, 4, 3, 4);
            dtpDenNgay.Name = "dtpDenNgay";
            dtpDenNgay.Size = new Size(183, 30);
            dtpDenNgay.TabIndex = 4;
            dtpDenNgay.ValueChanged += dtpDenNgay_ValueChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 9F);
            label4.ForeColor = Color.Maroon;
            label4.Location = new Point(770, 29);
            label4.Name = "label4";
            label4.Size = new Size(75, 20);
            label4.TabIndex = 3;
            label4.Text = "Đến Ngày";
            // 
            // dtpTuNgay
            // 
            dtpTuNgay.CustomFormat = "dd/MM/yyyy";
            dtpTuNgay.Font = new Font("Segoe UI", 10F);
            dtpTuNgay.Format = DateTimePickerFormat.Custom;
            dtpTuNgay.Location = new Point(488, 24);
            dtpTuNgay.Margin = new Padding(3, 4, 3, 4);
            dtpTuNgay.Name = "dtpTuNgay";
            dtpTuNgay.Size = new Size(195, 30);
            dtpTuNgay.TabIndex = 4;
            dtpTuNgay.ValueChanged += dtpTuNgay_ValueChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9F);
            label3.ForeColor = Color.Maroon;
            label3.Location = new Point(423, 29);
            label3.Name = "label3";
            label3.Size = new Size(65, 20);
            label3.TabIndex = 3;
            label3.Text = "Từ Ngày";
            // 
            // cbxPhong
            // 
            cbxPhong.Font = new Font("Segoe UI", 10F);
            cbxPhong.FormattingEnabled = true;
            cbxPhong.Location = new Point(119, 23);
            cbxPhong.Margin = new Padding(3, 4, 3, 4);
            cbxPhong.Name = "cbxPhong";
            cbxPhong.Size = new Size(242, 31);
            cbxPhong.TabIndex = 2;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9F);
            label2.ForeColor = Color.Maroon;
            label2.Location = new Point(28, 28);
            label2.Name = "label2";
            label2.Size = new Size(51, 20);
            label2.TabIndex = 1;
            label2.Text = "Phòng";
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            label1.ForeColor = SystemColors.HotTrack;
            label1.Location = new Point(356, 0);
            label1.Name = "label1";
            label1.Size = new Size(649, 46);
            label1.TabIndex = 16;
            label1.Text = "THỐNG KÊ DOANH THU THEO PHÒNG ";
            // 
            // dgrDanhSachThongKe
            // 
            dgrDanhSachThongKe.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgrDanhSachThongKe.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgrDanhSachThongKe.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgrDanhSachThongKe.Location = new Point(16, 181);
            dgrDanhSachThongKe.Margin = new Padding(3, 4, 3, 4);
            dgrDanhSachThongKe.Name = "dgrDanhSachThongKe";
            dgrDanhSachThongKe.ReadOnly = true;
            dgrDanhSachThongKe.RowHeadersWidth = 51;
            dgrDanhSachThongKe.Size = new Size(1272, 749);
            dgrDanhSachThongKe.TabIndex = 18;
            // 
            // frmDoanhThuPhong
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1301, 749);
            Controls.Add(panel1);
            Controls.Add(label1);
            Controls.Add(dgrDanhSachThongKe);
            Name = "frmDoanhThuPhong";
            Text = "frmDoanhThuPhong";
            Load += frmDoanhThuPhong_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgrDanhSachThongKe).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel1;
        private Button btnThongKe;
        private DateTimePicker dtpDenNgay;
        private Label label4;
        private DateTimePicker dtpTuNgay;
        private Label label3;
        private ComboBox cbxPhong;
        private Label label2;
        private Label label1;
        private DataGridView dgrDanhSachThongKe;
    }
}