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
namespace Nhom7
{
    public partial class TimKiemSV : Form
    {
        public string conStr = Properties.Settings.Default.conStr;
        QuanLySinhVien qlsv;
      

        public TimKiemSV(QuanLySinhVien qlsv)
        {
            this.qlsv = qlsv;
            InitializeComponent();
        }
        private void btnTimK_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                string sql = "timSV";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@masv", SqlDbType.VarChar).Value = txtMaSV.Text;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                try
                {
                    da.Fill(dt);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tìm kiếm sinh viên: " + ex.Message);
                    return;
                }

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy sinh viên nào có mã khớp.");
                    return;
                }

              
                qlsv.capnhatDanhSach(dt);
                this.Hide();
            }
        }
    }
}
