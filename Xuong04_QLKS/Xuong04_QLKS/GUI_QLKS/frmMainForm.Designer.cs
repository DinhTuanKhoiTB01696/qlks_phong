namespace GUI_QLKS
{
    partial class frmMainForm
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
            menuStrip1 = new MenuStrip();
            heThongToolStripMenuItem = new ToolStripMenuItem();
            doimatKhauToolStripMenuItem = new ToolStripMenuItem();
            đangXuatToolStripMenuItem = new ToolStripMenuItem();
            thoatToolStripMenuItem1 = new ToolStripMenuItem();
            danhMucToolStripMenuItem = new ToolStripMenuItem();
            phongToolStripMenuItem = new ToolStripMenuItem();
            datPhongToolStripMenuItem = new ToolStripMenuItem();
            loaitrangThaiToolStripMenuItem = new ToolStripMenuItem();
            trangThaiĐatPhongToolStripMenuItem = new ToolStripMenuItem();
            nhanVienToolStripMenuItem = new ToolStripMenuItem();
            khachHangToolStripMenuItem = new ToolStripMenuItem();
            dichVuToolStripMenuItem = new ToolStripMenuItem();
            loạiDịchVụToolStripMenuItem = new ToolStripMenuItem();
            quảnLýDịchVụToolStripMenuItem = new ToolStripMenuItem();
            doanhThuToolStripMenuItem = new ToolStripMenuItem();
            phòngToolStripMenuItem = new ToolStripMenuItem();
            nhânViênToolStripMenuItem = new ToolStripMenuItem();
            thanhToánToolStripMenuItem = new ToolStripMenuItem();
            hướngDẫnToolStripMenuItem = new ToolStripMenuItem();
            hướngDẫnSửDụngToolStripMenuItem = new ToolStripMenuItem();
            giớiThiệuPhầnMềmToolStripMenuItem = new ToolStripMenuItem();
            pnMain = new Panel();
            loạiPhòngToolStripMenuItem = new ToolStripMenuItem();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { heThongToolStripMenuItem, danhMucToolStripMenuItem, datPhongToolStripMenuItem, nhanVienToolStripMenuItem, khachHangToolStripMenuItem, dichVuToolStripMenuItem, doanhThuToolStripMenuItem, thanhToánToolStripMenuItem, hướngDẫnToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Padding = new Padding(7, 3, 0, 3);
            menuStrip1.Size = new Size(1432, 30);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // heThongToolStripMenuItem
            // 
            heThongToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { doimatKhauToolStripMenuItem, đangXuatToolStripMenuItem, thoatToolStripMenuItem1 });
            heThongToolStripMenuItem.Name = "heThongToolStripMenuItem";
            heThongToolStripMenuItem.Size = new Size(88, 24);
            heThongToolStripMenuItem.Text = "Hệ Thống";
            // 
            // doimatKhauToolStripMenuItem
            // 
            doimatKhauToolStripMenuItem.Name = "doimatKhauToolStripMenuItem";
            doimatKhauToolStripMenuItem.Size = new Size(183, 26);
            doimatKhauToolStripMenuItem.Text = "Đổi Mật Khẩu";
            doimatKhauToolStripMenuItem.Click += doimatKhauToolStripMenuItem_Click;
            // 
            // đangXuatToolStripMenuItem
            // 
            đangXuatToolStripMenuItem.Name = "đangXuatToolStripMenuItem";
            đangXuatToolStripMenuItem.Size = new Size(183, 26);
            đangXuatToolStripMenuItem.Text = "Đăng Xuất";
            đangXuatToolStripMenuItem.Click += đăngXuấtToolStripMenuItem_Click;
            // 
            // thoatToolStripMenuItem1
            // 
            thoatToolStripMenuItem1.Name = "thoatToolStripMenuItem1";
            thoatToolStripMenuItem1.Size = new Size(183, 26);
            thoatToolStripMenuItem1.Text = "Thoát";
            thoatToolStripMenuItem1.Click += thoátToolStripMenuItem1_Click;
            // 
            // danhMucToolStripMenuItem
            // 
            danhMucToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { phongToolStripMenuItem, loạiPhòngToolStripMenuItem });
            danhMucToolStripMenuItem.Name = "danhMucToolStripMenuItem";
            danhMucToolStripMenuItem.Size = new Size(90, 24);
            danhMucToolStripMenuItem.Text = "Danh Mục";
            // 
            // phongToolStripMenuItem
            // 
            phongToolStripMenuItem.Name = "phongToolStripMenuItem";
            phongToolStripMenuItem.Size = new Size(224, 26);
            phongToolStripMenuItem.Text = "Phòng";
            phongToolStripMenuItem.Click += phongToolStripMenuItem_Click;
            // 
            // datPhongToolStripMenuItem
            // 
            datPhongToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { loaitrangThaiToolStripMenuItem, trangThaiĐatPhongToolStripMenuItem });
            datPhongToolStripMenuItem.Name = "datPhongToolStripMenuItem";
            datPhongToolStripMenuItem.Size = new Size(93, 24);
            datPhongToolStripMenuItem.Text = "Đặt Phòng";
            // 
            // loaitrangThaiToolStripMenuItem
            // 
            loaitrangThaiToolStripMenuItem.Name = "loaitrangThaiToolStripMenuItem";
            loaitrangThaiToolStripMenuItem.Size = new Size(267, 26);
            loaitrangThaiToolStripMenuItem.Text = "Loại Trạng Thái Đặt Phòng";
            loaitrangThaiToolStripMenuItem.Click += loaitrangThaiToolStripMenuItem_Click;
            // 
            // trangThaiĐatPhongToolStripMenuItem
            // 
            trangThaiĐatPhongToolStripMenuItem.Name = "trangThaiĐatPhongToolStripMenuItem";
            trangThaiĐatPhongToolStripMenuItem.Size = new Size(267, 26);
            trangThaiĐatPhongToolStripMenuItem.Text = "Trạng Thái Đặt Phòng";
            trangThaiĐatPhongToolStripMenuItem.Click += trangThaiĐatPhongToolStripMenuItem_Click;
            // 
            // nhanVienToolStripMenuItem
            // 
            nhanVienToolStripMenuItem.Name = "nhanVienToolStripMenuItem";
            nhanVienToolStripMenuItem.Size = new Size(91, 24);
            nhanVienToolStripMenuItem.Text = "Nhân Viên";
            nhanVienToolStripMenuItem.Click += nhanVienToolStripMenuItem_Click;
            // 
            // khachHangToolStripMenuItem
            // 
            khachHangToolStripMenuItem.Name = "khachHangToolStripMenuItem";
            khachHangToolStripMenuItem.Size = new Size(103, 24);
            khachHangToolStripMenuItem.Text = "Khách Hàng";
            khachHangToolStripMenuItem.Click += khachHangToolStripMenuItem_Click;
            // 
            // dichVuToolStripMenuItem
            // 
            dichVuToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { loạiDịchVụToolStripMenuItem, quảnLýDịchVụToolStripMenuItem });
            dichVuToolStripMenuItem.Name = "dichVuToolStripMenuItem";
            dichVuToolStripMenuItem.Size = new Size(74, 24);
            dichVuToolStripMenuItem.Text = "Dịch Vụ";
            // 
            // loạiDịchVụToolStripMenuItem
            // 
            loạiDịchVụToolStripMenuItem.Name = "loạiDịchVụToolStripMenuItem";
            loạiDịchVụToolStripMenuItem.Size = new Size(199, 26);
            loạiDịchVụToolStripMenuItem.Text = "Loại Dịch Vụ";
            loạiDịchVụToolStripMenuItem.Click += loạiDịchVụToolStripMenuItem_Click;
            // 
            // quảnLýDịchVụToolStripMenuItem
            // 
            quảnLýDịchVụToolStripMenuItem.Name = "quảnLýDịchVụToolStripMenuItem";
            quảnLýDịchVụToolStripMenuItem.Size = new Size(199, 26);
            quảnLýDịchVụToolStripMenuItem.Text = "Quản Lý Dịch Vụ";
            quảnLýDịchVụToolStripMenuItem.Click += quảnLýDịchVụToolStripMenuItem_Click;
            // 
            // doanhThuToolStripMenuItem
            // 
            doanhThuToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { phòngToolStripMenuItem, nhânViênToolStripMenuItem });
            doanhThuToolStripMenuItem.Name = "doanhThuToolStripMenuItem";
            doanhThuToolStripMenuItem.Size = new Size(95, 24);
            doanhThuToolStripMenuItem.Text = "Doanh Thu";
            // 
            // phòngToolStripMenuItem
            // 
            phòngToolStripMenuItem.Name = "phòngToolStripMenuItem";
            phòngToolStripMenuItem.Size = new Size(160, 26);
            phòngToolStripMenuItem.Text = "Phòng";
            phòngToolStripMenuItem.Click += phòngToolStripMenuItem_Click;
            // 
            // nhânViênToolStripMenuItem
            // 
            nhânViênToolStripMenuItem.Name = "nhânViênToolStripMenuItem";
            nhânViênToolStripMenuItem.Size = new Size(160, 26);
            nhânViênToolStripMenuItem.Text = "Nhân Viên";
            nhânViênToolStripMenuItem.Click += nhânViênToolStripMenuItem_Click;
            // 
            // thanhToánToolStripMenuItem
            // 
            thanhToánToolStripMenuItem.Name = "thanhToánToolStripMenuItem";
            thanhToánToolStripMenuItem.Size = new Size(99, 24);
            thanhToánToolStripMenuItem.Text = "Thanh Toán";
            thanhToánToolStripMenuItem.Click += thanhToánToolStripMenuItem_Click;
            // 
            // hướngDẫnToolStripMenuItem
            // 
            hướngDẫnToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { hướngDẫnSửDụngToolStripMenuItem, giớiThiệuPhầnMềmToolStripMenuItem });
            hướngDẫnToolStripMenuItem.Name = "hướngDẫnToolStripMenuItem";
            hướngDẫnToolStripMenuItem.Size = new Size(104, 24);
            hướngDẫnToolStripMenuItem.Text = "Hướng Dẫn ";
            // 
            // hướngDẫnSửDụngToolStripMenuItem
            // 
            hướngDẫnSửDụngToolStripMenuItem.Name = "hướngDẫnSửDụngToolStripMenuItem";
            hướngDẫnSửDụngToolStripMenuItem.Size = new Size(233, 26);
            hướngDẫnSửDụngToolStripMenuItem.Text = "Hướng Dẫn Sử Dụng";
            // 
            // giớiThiệuPhầnMềmToolStripMenuItem
            // 
            giớiThiệuPhầnMềmToolStripMenuItem.Name = "giớiThiệuPhầnMềmToolStripMenuItem";
            giớiThiệuPhầnMềmToolStripMenuItem.Size = new Size(233, 26);
            giớiThiệuPhầnMềmToolStripMenuItem.Text = "Giới Thiệu Phần Mềm";
            // 
            // pnMain
            // 
            pnMain.Dock = DockStyle.Fill;
            pnMain.Location = new Point(0, 30);
            pnMain.Margin = new Padding(3, 4, 3, 4);
            pnMain.Name = "pnMain";
            pnMain.Size = new Size(1432, 873);
            pnMain.TabIndex = 1;
            // 
            // loạiPhòngToolStripMenuItem
            // 
            loạiPhòngToolStripMenuItem.Name = "loạiPhòngToolStripMenuItem";
            loạiPhòngToolStripMenuItem.Size = new Size(224, 26);
            loạiPhòngToolStripMenuItem.Text = "Loại Phòng";
            loạiPhòngToolStripMenuItem.Click += loạiPhòngToolStripMenuItem_Click;
            // 
            // frmMainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1432, 903);
            Controls.Add(pnMain);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Margin = new Padding(3, 4, 3, 4);
            Name = "frmMainForm";
            Text = "frmMainForm";
            FormClosing += frmMainForm_FormClosing;
            Load += frmMainForm_Load;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem heThongToolStripMenuItem;
        private ToolStripMenuItem doimatKhauToolStripMenuItem;
        private ToolStripMenuItem danhMucToolStripMenuItem;
        private ToolStripMenuItem phongToolStripMenuItem;
        private ToolStripMenuItem đangXuatToolStripMenuItem;
        private ToolStripMenuItem thoatToolStripMenuItem1;
        private ToolStripMenuItem datPhongToolStripMenuItem;
        private ToolStripMenuItem loaitrangThaiToolStripMenuItem;
        private ToolStripMenuItem trangThaiĐatPhongToolStripMenuItem;
        private ToolStripMenuItem nhanVienToolStripMenuItem;
        private ToolStripMenuItem dichVuToolStripMenuItem;
        private ToolStripMenuItem khachHangToolStripMenuItem;
        private Panel pnMain;
        private ToolStripMenuItem quảnLýDịchVụToolStripMenuItem;
        private ToolStripMenuItem doanhThuToolStripMenuItem;
        private ToolStripMenuItem hướngDẫnToolStripMenuItem;
        private ToolStripMenuItem phòngToolStripMenuItem;
        private ToolStripMenuItem nhânViênToolStripMenuItem;
        private ToolStripMenuItem hướngDẫnSửDụngToolStripMenuItem;
        private ToolStripMenuItem giớiThiệuPhầnMềmToolStripMenuItem;
        private ToolStripMenuItem loạiDịchVụToolStripMenuItem;
        private ToolStripMenuItem thanhToánToolStripMenuItem;
        private ToolStripMenuItem loạiPhòngToolStripMenuItem;
    }
}