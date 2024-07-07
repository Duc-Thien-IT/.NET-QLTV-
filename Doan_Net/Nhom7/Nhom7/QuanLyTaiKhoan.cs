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
    public partial class QuanLyTaiKhoan : Form
    {
        public string conStr = Properties.Settings.Default.conStr;
        SqlDataAdapter da_dsU;
        DataTable dt_dsU ;

        public QuanLyTaiKhoan()
        {
            InitializeComponent();
        }

        private void QuanLyTaiKhoan_Load(object sender, EventArgs e)
        {
           


            layDSU();
            txtQLHT.Enabled = txtQLMK.Enabled = txtQLTK.Enabled = txtQLSDT.Enabled = false;
            btnSua.Enabled = btnXoa.Enabled = btnSave.Enabled = false;
        }

        
        public void layDSU()
        {
            string sql = "dsU";
            using (SqlConnection con = new SqlConnection(conStr))
            {
                da_dsU = new SqlDataAdapter();
                dt_dsU = new DataTable();
                da_dsU.SelectCommand = new SqlCommand(sql, con);

                try
                {
                    con.Open();
                    da_dsU.Fill(dt_dsU);

                    DataColumn[] key = new DataColumn[1];
                    key[0] = dt_dsU.Columns[0];
                    dt_dsU.PrimaryKey = key;

                    dsUser.DataSource = dt_dsU;

                    // Duyệt qua từng hàng của DataTable
                    foreach (DataRow row in dt_dsU.Rows)
                    {
                        // Duyệt qua từng cột của hàng
                        foreach (DataColumn column in dt_dsU.Columns)
                        {
                            Console.WriteLine(row[column]);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred: " + ex.Message);
                }
            }
        }

        public bool InforCheck()
        {
            if ((txtQLTK.Text.Length < 4))
            {
                MessageBox.Show("Vui lòng nhập lại username", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtQLTK.Focus();
                return false;
            }
            if (txtQLMK.Text == "")
            {
                MessageBox.Show("Vui lòng nhập PassWord", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtQLMK.Focus();
                return false;
            }
            if (txtQLHT.Text == "")
            {
                MessageBox.Show("Vui lòng nhập Tên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtQLHT.Focus();
                return false;
            }
            if (txtQLSDT.Text == "" || txtQLSDT.Text.Length < 10)
            {
                MessageBox.Show("Vui lòng nhập lại số điện thoại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtQLHT.Focus();
                return false;
            }
            if (rdioUser.Checked == false && rdioAdmin.Checked == false)
            {
                MessageBox.Show("Vui lòng chọn quyền", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                rdioAdmin.Focus();
                return false;
            }
            return true;
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            txtQLHT.Enabled = txtQLMK.Enabled = txtQLTK.Enabled = txtQLSDT.Enabled = true;
            btnSave.Enabled = true;
            txtQLTK.Focus();
            txtQLHT.Clear();
            txtQLMK.Clear();
            txtQLSDT.Clear();
            txtQLTK.Clear();
        }


        private void btnDong_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn đóng?", "Xác nhận", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void dsUser_SelectionChanged(object sender, EventArgs e)
        {
            btnSua.Enabled = btnXoa.Enabled = true;
            if (dsUser.SelectedRows.Count > 0)
            {
                // Get data from the first selected row
                DataGridViewRow row = dsUser.SelectedRows[0];

                // Update TextBoxes
                txtQLTK.Text = row.Cells["colTK"].Value.ToString();
                txtQLSDT.Text = row.Cells["colPhone"].Value.ToString();
                txtQLHT.Text = row.Cells["colTenU"].Value.ToString();

                // Update password TextBox
                txtQLMK.Text = row.Cells["colMK"].Value.ToString(); // Assuming "MatKhau" is the hidden column

                // Update RadioButtons
                string role = row.Cells["colRole"].Value.ToString();
                if (role == "Admin")
                {
                    rdioAdmin.Checked = true;
                    rdioUser.Checked = false;
                }
                else if (role == "User")
                {
                    rdioAdmin.Checked = false;
                    rdioUser.Checked = true;
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (InforCheck())
            {
                SqlConnection con = new SqlConnection(conStr);
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;

                if (txtQLTK.Enabled == true)
                {
                    cmd.CommandText = "themU";
                    if (rdioAdmin.Checked == true)
                        cmd.Parameters.Add("@role", SqlDbType.VarChar).Value = rdioAdmin.Text;
                    else
                        cmd.Parameters.Add("@role", SqlDbType.VarChar).Value = rdioUser.Text;
                }
                else
                {
                    cmd.CommandText = "suaU";
                }

                cmd.Parameters.Add("@tk", SqlDbType.VarChar).Value = txtQLTK.Text;
                cmd.Parameters.Add("@mk", SqlDbType.VarChar).Value = txtQLMK.Text;
                cmd.Parameters.Add("@ht", SqlDbType.NVarChar).Value = txtQLHT.Text;
                cmd.Parameters.Add("@mk_md5", SqlDbType.VarChar).Value = Password.create_MD5(txtQLMK.Text);
                cmd.Parameters.Add("@sdt", SqlDbType.VarChar).Value = txtQLSDT.Text;

                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Thành công!");
                    layDSU();
                }
                catch (SqlException ex)
                {
                    switch (ex.Number)
                    {
                        // Unique constraint error
                        case 2627:
                            {
                                MessageBox.Show("Tên tài khoản đã tồn tại. Vui lòng nhập lại");
                                txtQLTK.Focus();
                                break;
                            }

                        case 547:   // Constraint check violation
                        case 2601: // Duplicated key row error
                        default:
                            // A custom error message
                            MessageBox.Show("Đã xảy ra lỗi khi thao tác với cơ sở dữ liệu: " + ex.Message);
                            break;
                    }
                }
            }
        }
        private void btnXoa_Click(object sender, EventArgs e)
        {
            SqlConnection con = null;
            try
            {
                if (Properties.Settings.Default.username == txtQLTK.Text)
                {
                    MessageBox.Show("Dữ liệu không thể xóa");
                    return;
                }
                else
                {
                    string sql = "xoaU";
                    con = new SqlConnection(conStr);
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@tk", SqlDbType.VarChar).Value = txtQLTK.Text;
                    DialogResult dg = MessageBox.Show("Bạn có chắn chắn muốn xóa?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dg == DialogResult.Yes)
                    {
                        con.Open();
                        cmd.ExecuteNonQuery();
                        layDSU();
                        MessageBox.Show("Đã xóa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtQLHT.Clear();
                        txtQLMK.Clear();
                        txtQLSDT.Clear();
                        txtQLTK.Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }
        private void btnSua_Click(object sender, EventArgs e)
        {
            txtQLTK.Enabled = false;
            btnXoa.Enabled = false;
            txtQLMK.Enabled = txtQLSDT.Enabled = txtQLHT.Enabled = true;
            btnSave.Enabled = true;
        }

        private void btnSua_MouseEnter(object sender, EventArgs e)
        {
            btnSua.Cursor = Cursors.Hand;
            btnSua.BackColor = System.Drawing.Color.RoyalBlue;
        }

        private void btnSua_MouseLeave(object sender, EventArgs e)
        {
            btnSua.BackColor = System.Drawing.Color.CornflowerBlue;
        }

    

        private void btnXoa_MouseLeave(object sender, EventArgs e)
        {
            btnXoa.BackColor = System.Drawing.Color.Lime;

        }

        private void btnXoa_MouseEnter(object sender, EventArgs e)
        {
            btnXoa.Cursor = Cursors.Hand;
            btnXoa.BackColor = System.Drawing.Color.ForestGreen;
        }

        private void btnThem_MouseEnter(object sender, EventArgs e)
        {
            btnThem.Cursor = Cursors.Hand;
            btnThem.BackColor = System.Drawing.Color.Brown;
        }

        private void btnThem_MouseLeave(object sender, EventArgs e)
        {
            btnThem.BackColor = System.Drawing.Color.LightCoral;
        }

        private void btnSave_MouseEnter(object sender, EventArgs e)
        {
            btnSave.Cursor = Cursors.Hand;
            btnSave.BackColor = System.Drawing.Color.Gray;
        }

        private void btnSave_MouseLeave(object sender, EventArgs e)
        {
            btnSave.BackColor = System.Drawing.Color.Silver;
        }

        private void btnDong_MouseEnter(object sender, EventArgs e)
        {
            btnDong.Cursor = Cursors.Hand;
            btnDong.BackColor = System.Drawing.Color.DarkRed;
        }

        private void btnDong_MouseLeave(object sender, EventArgs e)
        {
            btnDong.BackColor = System.Drawing.Color.Red;
        }

        private void dsUser_MouseEnter(object sender, EventArgs e)
        {
            dsUser.Cursor = Cursors.Hand;
        }

        private void rdioUser_MouseEnter(object sender, EventArgs e)
        {
            rdioUser.Cursor = Cursors.Hand;
        }

        private void rdioAdmin_MouseEnter(object sender, EventArgs e)
        {
            rdioAdmin.Cursor = Cursors.Hand;
        }

     
    }
}
