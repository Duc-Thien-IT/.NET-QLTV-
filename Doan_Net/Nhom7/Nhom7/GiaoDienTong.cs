using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Nhom7.Utilities;
namespace Nhom7
{
    public partial class GiaoDienTong : Form
    {
        public GiaoDienTong()
        {
            InitializeComponent();
        }

        private void GiaoDienTong_Load(object sender, EventArgs e)
        {
            this.Text = "---" + Login.taikhoan + " - " + Login.name + " - " + Login.role;
            if (Login.taikhoan == string.Empty)
            {
                mnuQLSV.Enabled = false;
                mnuHT_DX.Enabled = false;
                mnuQLMT.Enabled = false;
                mnuQLS.Enabled = false;
            }
            else
            {
                mnuQLS.Enabled = true;
                mnuQLMT.Enabled = true;
                mnuQLSV.Enabled = true;
                mnuHT_DX.Enabled = true;
            }    
            if(Login.role == "Admin")
            {
                mnuQLTK.Enabled = true;
            }
            else
            {
                mnuQLTK.Enabled = false;
            }
        }

        private void mnuQLTK_Click(object sender, EventArgs e)
        {
            this.Hide();
            QuanLyTaiKhoan QLTK = new QuanLyTaiKhoan();
            QLTK.ShowDialog();
            QLTK = null;
            this.Show();
        }

        private void mnHT_DX_Click(object sender, EventArgs e)
        {
            if(Properties.Settings.Default.username != null)
            {
                this.Hide();
                DangNhap DN = new DangNhap();
                DN.ShowDialog();
            }
        }

        private void mnuHT_DN_Click(object sender, EventArgs e)
        {
            this.Hide();
            DangNhap DN = new DangNhap();
            DN.ShowDialog();
            Properties.Settings.Default.username = "";
            Properties.Settings.Default.password = "";
        }

        private void mnuQLS_Click(object sender, EventArgs e)
        {
            this.Hide();
            QuanLySach QLS = new QuanLySach();
            QLS.ShowDialog();
            QLS = null;
            this.Show();
        }

        private void mnuQLSV_Click(object sender, EventArgs e)
        {
            this.Hide();
            QuanLySinhVien QLSV = new QuanLySinhVien();
            QLSV.ShowDialog();
            QLSV = null;
            this.Show();
        }

        private void mnuQLMT_Click(object sender, EventArgs e)
        {
            this.Hide();
            QuanLyMuonSach QLMS = new QuanLyMuonSach();
            QLMS.ShowDialog();
            QLMS = null;
            this.Show();
        }
    }
}
