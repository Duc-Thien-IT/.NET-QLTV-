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
    public partial class QuanLySach : Form
    {
        public string conStr = Properties.Settings.Default.conStr;
        SqlDataAdapter da_dsSach;
        DataTable dt_dsSach;

        public QuanLySach()
        {
            InitializeComponent();
        }

        private void QuanLySach_Load(object sender, EventArgs e)
        {
            txtMaS.Enabled = txtTenS.Enabled  = txtNamXB.Enabled = txtTacG.Enabled = cbbTenTL.Enabled = cbbTenNXB.Enabled = false;
            btnSave.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            dsTheLoai();
            layDSSach();
            dsNXB();

        }
        
        public void dsTheLoai()
        {
            using (SqlConnection conn = new SqlConnection(conStr))
            {
                conn.Open();
                string sql = "dsTheLoai";
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                cbbTenTL.DataSource = dt;
                cbbTenTL.DisplayMember = "TenTL";
                cbbTenTL.ValueMember = "MaTL";
                cbbTenTL.Text = "Tên thể loại";
            }
        }
        public void dsNXB()
        {
            using (SqlConnection conn = new SqlConnection(conStr))
            {
                conn.Open();
                string sql = "dsNXB";
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                cbbTenNXB.DataSource = dt;
                cbbTenNXB.DisplayMember = "TenNXB";
                cbbTenNXB.ValueMember = "MaNXB";
                cbbTenNXB.Text = "Tên NXB";
            }
        }

        public void capnhatDSSach(DataTable dt)
        {
            dsSach.DataSource = dt;
        }
        public void layDSSach()
        {
            string sql = "dsSach"; // Thay đổi câu truy vấn SQL phù hợp với yêu cầu của bạn
            using (SqlConnection con = new SqlConnection(conStr))
            {
                da_dsSach = new SqlDataAdapter();
                dt_dsSach = new DataTable();
                da_dsSach.SelectCommand = new SqlCommand(sql, con);

                try
                {
                    con.Open();
                    da_dsSach.Fill(dt_dsSach);

                    DataColumn[] key = new DataColumn[1];
                    key[0] = dt_dsSach.Columns[0];
                    dt_dsSach.PrimaryKey = key;

                    dsSach.DataSource = dt_dsSach; // Thay đổi DataSource phù hợp với DataGridView của bạn

                    // Duyệt qua từng hàng của DataTable
                    foreach (DataRow row in dt_dsSach.Rows)
                    {
                        // Duyệt qua từng cột của hàng
                        foreach (DataColumn column in dt_dsSach.Columns)
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
            if (string.IsNullOrEmpty(txtMaS.Text))
            {
                MessageBox.Show("Vui lòng nhập Mã Sách", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaS.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtTenS.Text))
            {
                MessageBox.Show("Vui lòng nhập Tên Sách", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTenS.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtNamXB.Text))
            {
                MessageBox.Show("Vui lòng nhập Năm Xuất Bản", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtNamXB.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtTacG.Text))
            {
                MessageBox.Show("Vui lòng nhập Tác Giả", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTacG.Focus();
                return false;
            }
            if (cbbTenNXB.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn Nhà Xuất Bản", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbbTenNXB.Focus();
                return false;
            }
            if (cbbTenTL.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn Thể Loại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbbTenTL.Focus();
                return false;
            }
            return true;
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn đóng?", "Xác nhận", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            if (txtMaS.Text != string.Empty)
            {
              
                txtMaS.Enabled = txtTenS.Enabled = txtNamXB.Enabled = txtTacG.Enabled = cbbTenTL.Enabled = cbbTenNXB.Enabled = true;
                btnSave.Enabled = true;
                txtMaS.Focus();
                txtMaS.Clear();
                txtTenS.Clear();
                txtNamXB.Clear();
                txtTacG.Clear();
                cbbTenTL.Text = "Tên thể loại";
                cbbTenNXB.Text = "Tên NXB";
            }    
            else
            {
                btnSave.Enabled = true;
                txtMaS.Enabled = txtTenS.Enabled = txtNamXB.Enabled = txtTacG.Enabled = cbbTenTL.Enabled = cbbTenNXB.Enabled = true;
            }    
        }

        private void dsSach_SelectionChanged(object sender, EventArgs e)
        {
    
            if (dsSach.SelectedRows.Count > 0)
            {
                txtMaS.Enabled = txtTenS.Enabled = txtNamXB.Enabled = txtTacG.Enabled = cbbTenTL.Enabled = cbbTenNXB.Enabled = false;
                btnSua.Enabled = true;
                btnXoa.Enabled = true;
                txtMaS.Enabled = false;
                // Get data from the first selected row
                DataGridViewRow row = dsSach.SelectedRows[0];

                // Update TextBoxes and ComboBoxes
                txtMaS.Text = row.Cells["colMaS"].Value.ToString();
                txtTenS.Text = row.Cells["colTenS"].Value.ToString();
                txtNamXB.Text = row.Cells["colNamXB"].Value.ToString();
                txtTacG.Text = row.Cells["colTacG"].Value.ToString();
                cbbTenNXB.Text = row.Cells["colTenNXB"].Value.ToString();
                cbbTenTL.Text = row.Cells["colTenTL"].Value.ToString();
            }
        }

    
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (InforCheck())
            {
                string sql = txtMaS.Enabled == true ? "themSach" : "suaSach";
                using (SqlConnection con = new SqlConnection(conStr))
                {
                    try
                    {
                        SqlCommand cmd = new SqlCommand(sql, con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@MaSach", SqlDbType.VarChar).Value = txtMaS.Text;
                        cmd.Parameters.Add("@TenSach", SqlDbType.NVarChar).Value = txtTenS.Text;
                        cmd.Parameters.Add("@NamXB", SqlDbType.Int).Value = txtNamXB.Text;
                        cmd.Parameters.Add("@TenTG", SqlDbType.NVarChar).Value = txtTacG.Text;
                        cmd.Parameters.Add("@MaTL", SqlDbType.NVarChar).Value = cbbTenTL.SelectedValue.ToString();
                        cmd.Parameters.Add("@MaNXB", SqlDbType.NVarChar).Value = cbbTenNXB.SelectedValue.ToString();

                        con.Open();
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Thành công!!");
                        txtMaS.Clear();
                        txtTenS.Clear();
                        txtNamXB.Clear();
                        txtTacG.Clear();
                        cbbTenTL.Text = "Tên thể loại";
                        cbbTenNXB.Text = "Tên NXB";
                        layDSSach();
                    }
                    catch (SqlException ex)
                    {
                        switch (ex.Number)
                        {
                            // Unique constraint error
                            case 2627:
                                {
                                    MessageBox.Show("Tên sách hoạc mã sách đã tồn tại. Vui lòng nhập lại");
                                    txtMaS.Focus();
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
        }


        private void btnXoa_Click(object sender, EventArgs e)
        {
            SqlConnection con = null;
            try
            {
                string sql = "xoaSach";
                con = new SqlConnection(conStr);
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@MaS", SqlDbType.VarChar).Value = txtMaS.Text;
                DialogResult dg = MessageBox.Show("Bạn có chắn chắn muốn xóa?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dg == DialogResult.Yes)
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    layDSSach();
                    MessageBox.Show("Đã xóa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtMaS.Clear();
                    txtTenS.Clear();
                    txtNamXB.Clear();
                    txtTacG.Clear();
                    cbbTenTL.Text = "Tên thể loại";
                    cbbTenNXB.Text = "Tên NXB";
                }
            }
            catch (SqlException ex)
            {
                if (ex.Number == 547)
                {
                    MessageBox.Show("Dữ liệu không thể xóa");
                }
                else
                {
                    MessageBox.Show(ex.Message);
                }
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
            txtMaS.Enabled = false;
            txtTenS.Enabled = txtNamXB.Enabled = txtTacG.Enabled = cbbTenTL.Enabled = cbbTenNXB.Enabled = true;
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

        private void mnuFind_Click(object sender, EventArgs e)
        {
            TimKiemSach tks = new TimKiemSach(this);
            tks.Show();
        }

      

        private void mnuReset_Click(object sender, EventArgs e)
        {
            layDSSach();
            dsNXB();
            dsTheLoai();
        }
    }
}
