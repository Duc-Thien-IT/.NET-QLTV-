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
    public partial class TimKiemSach : Form
    {
        public string conStr = Properties.Settings.Default.conStr;
        private QuanLySach qls;
        public TimKiemSach(QuanLySach qls)
        {
            InitializeComponent();
            this.qls = qls;
        }

        private void btnTimK_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                string sql = "TimKiemSachTheoTen";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@TenSach", SqlDbType.NVarChar).Value = txtTimKiem.Text;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                try
                {
                    da.Fill(dt);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tìm kiếm sách: " + ex.Message);
                    return;
                }

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy sách nào có tên khớp.");
                    return;
                }

                qls.capnhatDSSach(dt);
                this.Hide();
            }
        }

        private void TimKiemSach_Load(object sender, EventArgs e)
        {
            btnTimK.Focus();
        }
    }
}
