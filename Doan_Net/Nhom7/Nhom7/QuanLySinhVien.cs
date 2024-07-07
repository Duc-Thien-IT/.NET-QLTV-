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
    public partial class QuanLySinhVien : Form
    {
        public string conStr = Properties.Settings.Default.conStr;
        public QuanLySinhVien()
        {
            InitializeComponent();
            monthCalendar1.MaxSelectionCount = 1;
        }

        private void QuanLySinhVien_Load(object sender, EventArgs e)
        {
            layDSSinhVien();
            txtMaSV.Enabled = txtNSinh.Enabled = txtTenSV.Enabled = false;
            btnSave.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            monthCalendar1.Visible = false;
        }

        public void layDSSinhVien()
        {
            string sql = "dsSV"; // Sử dụng stored procedure dsSV
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlDataAdapter da_dsSV = new SqlDataAdapter();
                DataTable dt_dsSV = new DataTable();
                da_dsSV.SelectCommand = new SqlCommand(sql, con);

                try
                {
                    con.Open();
                    da_dsSV.Fill(dt_dsSV);

                    DataColumn[] key = new DataColumn[1];
                    key[0] = dt_dsSV.Columns[0];
                    dt_dsSV.PrimaryKey = key;

                    dsSinhVien.DataSource = dt_dsSV; // Thay đổi DataSource phù hợp với DataGridView của bạn

                    // Duyệt qua từng hàng của DataTable
                    foreach (DataRow row in dt_dsSV.Rows)
                    {
                        // Duyệt qua từng cột của hàng
                        foreach (DataColumn column in dt_dsSV.Columns)
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

        public void capnhatDanhSach(DataTable dt)
        {
            dsSinhVien.DataSource = dt;
        }
        public bool InforCheck()
        {
            if (txtMaSV.Text == "")
            {
                MessageBox.Show("Vui lòng nhập Mã SV", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaSV.Focus();
                return false;
            }
            if (txtTenSV.Text == "")
            {
                MessageBox.Show("Vui lòng nhập Tên SV", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTenSV.Focus();
                return false;
            }
            if (txtNSinh.Text == "")
            {
                MessageBox.Show("Vui lòng nhập Ngày Sinh", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtNSinh.Focus();
                return false;
            }
            if (rdioNam.Checked == false && rdioNu.Checked == false)
            {
                MessageBox.Show("Vui lòng chọn giới tính", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                rdioNam.Focus();
                return false;
            }
            return true;
        }

        private void dsSinhVien_SelectionChanged(object sender, EventArgs e)
        {
            btnSua.Enabled = btnXoa.Enabled = true;
            if (dsSinhVien.SelectedRows.Count > 0)
            {
                // Get data from the first selected row
                DataGridViewRow row = dsSinhVien.SelectedRows[0];
                DateTime date = Convert.ToDateTime(row.Cells["colNgayS"].Value);
                // Update TextBoxes
                txtMaSV.Text = row.Cells["colMaSV"].Value.ToString();
                txtTenSV.Text = row.Cells["colTenSV"].Value.ToString();
                txtNSinh.Text = date.ToString("dd/MM/yyyy");




                // Update RadioButtons
                string role = row.Cells["colGioiT"].Value.ToString();
                if (role == "Nam")
                {
                    rdioNam.Checked = true;
                    rdioNu.Checked = false;
                }
                else if (role == "Nữ")
                {
                    rdioNam.Checked = false;
                    rdioNu.Checked = true;
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

                if (txtMaSV.Enabled == true)
                {
                    cmd.CommandText = "themSV";
                }
                else
                {
                    cmd.CommandText = "suaSV";
                }

                cmd.Parameters.Add("@masv", SqlDbType.VarChar).Value = txtMaSV.Text;
                cmd.Parameters.Add("@hoten", SqlDbType.NVarChar).Value = txtTenSV.Text;
                cmd.Parameters.Add("@ns", SqlDbType.Date).Value = DateTime.Parse(txtNSinh.Text);

                if (rdioNam.Checked == true)
                    cmd.Parameters.Add("@gt", SqlDbType.NVarChar).Value = rdioNam.Text;
                else
                    cmd.Parameters.Add("@gt", SqlDbType.NVarChar).Value = rdioNu.Text;

                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Thành công!");
                    layDSSinhVien();
                }
                catch (SqlException ex)
                {
                    switch (ex.Number)
                    {
                        // Unique constraint error
                        case 2627:
                            {
                                MessageBox.Show("Mã sinh viên đã tồn tại. Vui lòng nhập lại");
                                txtMaSV.Focus();
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
            btnThem.Enabled = false;
            btnSua.Enabled = false;
            txtMaSV.Enabled = txtNSinh.Enabled = txtTenSV.Enabled = false;
            SqlConnection con = null;
            try
            {
                string sql = "xoaSV";
                con = new SqlConnection(conStr);
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@masv", SqlDbType.VarChar).Value = txtMaSV.Text;
                DialogResult dg = MessageBox.Show("Bạn có chắn chắn muốn xóa?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dg == DialogResult.Yes)
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    layDSSinhVien();
                    MessageBox.Show("Đã xóa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtMaSV.Clear();
                    txtTenSV.Clear();
                    txtNSinh.Clear();
                    rdioNam.Checked = false;
                    rdioNu.Checked = false;
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

        private void btnThem_Click(object sender, EventArgs e)
        {
            txtMaSV.Enabled = txtTenSV.Enabled = txtNSinh.Enabled = true;
            btnSave.Enabled = true;
            txtMaSV.Focus();
            txtTenSV.Clear();
            txtMaSV.Clear();
            txtNSinh.Clear();
            btnXoa.Enabled = false;
            btnSua.Enabled = false;
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
            dsSinhVien.Cursor = Cursors.Hand;
        }

        private void rdioUser_MouseEnter(object sender, EventArgs e)
        {
            rdioNam.Cursor = Cursors.Hand;
        }

        private void rdioAdmin_MouseEnter(object sender, EventArgs e)
        {
            rdioNu.Cursor = Cursors.Hand;
        }

        private void txtNSinh_Click(object sender, EventArgs e)
        {
            monthCalendar1.Visible = true;
        }


        private void monthCalendar1_DateSelected(object sender, DateRangeEventArgs e)
        {
            var monthCalendar = sender as MonthCalendar;
            txtNSinh.Text = monthCalendar.SelectionStart.ToString("dd/MM/yyyy");
            monthCalendar.Visible = false;
        }

        private void txtNSinh_TextChanged(object sender, EventArgs e)
        {
            monthCalendar1.Visible = false;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            txtNSinh.Enabled = true;
            txtTenSV.Enabled = true;
            txtMaSV.Enabled = false;
            btnSave.Enabled = true;
            btnXoa.Enabled = false;
        }

        private void mnuFind_Click(object sender, EventArgs e)
        {
            TimKiemSV tksv = new TimKiemSV(this);
            tksv.Show();
        }

        private void mnuReset_Click(object sender, EventArgs e)
        {
            layDSSinhVien();
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn đóng?", "Xác nhận", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void mnuQLMS_Click(object sender, EventArgs e)
        {
            QuanLyMuonSach qlms = new QuanLyMuonSach();
            this.Hide();
            qlms.ShowDialog();
            this.Close();
        }
    }
}
