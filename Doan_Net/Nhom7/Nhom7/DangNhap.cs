using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Nhom7.Utilities;

namespace Nhom7
{
    public partial class DangNhap : Form
    {
        public string conStr = Properties.Settings.Default.conStr;
        public DangNhap()
        {
            InitializeComponent();
            txtTaiKhoan.Text = Properties.Settings.Default.username;
            txtMatKhau.Text = Properties.Settings.Default.password;
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            if (rdioRemember.Checked)
            {
                Properties.Settings.Default.username = txtTaiKhoan.Text;
                Properties.Settings.Default.password = txtMatKhau.Text;
                Properties.Settings.Default.Save();
            }

            string pw1 = txtMatKhau.Text;
            string pw2 = Password.create_MD5(pw1);
            string pw3 = Password.Create_SHA1(pw1);

            SqlConnection con = new SqlConnection(conStr);
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "select * from users where taikhoan = @u and mk_md5 = @pmd5";
            cmd.Parameters.AddWithValue("@u", txtTaiKhoan.Text);
            cmd.Parameters.AddWithValue("@pmd5", Password.create_MD5(txtMatKhau.Text));
            SqlDataReader rdr = cmd.ExecuteReader();
            if (!rdr.HasRows)
            {
                MessageBox.Show("Sai...");
                return;
            }
            rdr.Read();
            Login.taikhoan = txtTaiKhoan.Text;
            Login.name = rdr["tenuser"].ToString();
            Login.role = rdr["role"].ToString();
            Login.phone = rdr["phone"].ToString();
            this.Hide();
            GiaoDienTong gdt = new GiaoDienTong();
            gdt.ShowDialog();
            this.Close();
           
        }

        private void btnDangNhap_MouseEnter(object sender, EventArgs e)
        {
            btnDangNhap.Cursor = Cursors.Hand;
            btnDangNhap.BackColor = System.Drawing.Color.LightSeaGreen;
        }

        private void btnDangNhap_MouseLeave(object sender, EventArgs e)
        {
            btnDangNhap.BackColor = System.Drawing.Color.Aquamarine;

        }
    }
}
