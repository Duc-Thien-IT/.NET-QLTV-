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
using System.Globalization;

namespace Nhom7
{
    public partial class QuanLyMuonSach : Form
    {
        public string conStr = Properties.Settings.Default.conStr;

        public QuanLyMuonSach()
        {
            InitializeComponent();
        }
        private void QuanLyMuonSach_Load(object sender, EventArgs e)
        {
            layDSPhieuThue();
            btnTaoTTSV.Enabled = false;
            mnReset.Visible = false;
            txtMaPT.ReadOnly = true;
            monthCalendar1.Visible = false;
            txtMaSV_CT.Enabled = false;
            txtMaPT_CT.Enabled = false;
            txtNMuon.Enabled = false;
            cboMaSach.Enabled = false;
            txtTenSach.Enabled = false;
            btnThem.Enabled = false;
            btnXoa.Enabled = false;
            btnLuu.Enabled = false;

        }
        public void layDSPhieuThue()
        {
            string sql = "dsPhieuThue";
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlDataAdapter da_dsPhieuThue = new SqlDataAdapter();
                DataTable dt_dsPhieuThue = new DataTable();
                da_dsPhieuThue.SelectCommand = new SqlCommand(sql, con);

                try
                {
                    con.Open();
                    da_dsPhieuThue.Fill(dt_dsPhieuThue);

                    DataColumn[] key = new DataColumn[2];
                    key[0] = dt_dsPhieuThue.Columns["MaPT"];
                    key[1] = dt_dsPhieuThue.Columns["MaSV"];
                    dt_dsPhieuThue.PrimaryKey = key;

                    dsPhieuThue.DataSource = dt_dsPhieuThue;


                    foreach (DataRow row in dt_dsPhieuThue.Rows)
                    {
                        // Duyệt qua từng cột của hàng
                        foreach (DataColumn column in dt_dsPhieuThue.Columns)
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

        public void dsSach()
        {
            using (SqlConnection conn = new SqlConnection(conStr))
            {
                conn.Open();
                string sql = "sachCM";
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                cboMaSach.DataSource = dt;
                cboMaSach.DisplayMember = "MaSach";
                cboMaSach.ValueMember = "MaSach";
                cboMaSach.Text = "Mã Sách";
            }
        }

        public bool InforCheck()
        {
            if (txtNMuon.Text == "")
            {
                MessageBox.Show("Vui lòng nhập Ngày mượn", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaSV.Focus();
                return false;
            }
            if (txtTenSach.Text == "")
            {
                MessageBox.Show("Vui lòng chọn sách", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaPT.Focus();
                return false;
            }
            return true;
        }
        private void cboMaSach_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataRowView drv = (DataRowView)cboMaSach.SelectedItem;

            // Lấy TenSach từ DataRowView
            string tenSach = drv["TenSach"].ToString();

            // Hiển thị TenSach trong txtTenSach  
            txtTenSach.Text = tenSach;
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            string sql = "timSV_PT"; // Sử dụng stored procedure bạn vừa tạo
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@masv", SqlDbType.VarChar).Value = txtMaSV.Text; // Thay "txtMaSV.Text" bằng mã sinh viên bạn muốn tìm

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                try
                {
                    da.Fill(dt);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tìm kiếm phiếu thuê: " + ex.Message);
                    return;
                }

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("Sinh viên chưa có bất kỳ phiếu thuê nào hoặc chưa có thông tin sinh viên");
                    txtMaSV.Clear();
                    btnTaoTTSV.Enabled = true; // Kích hoạt nút btnTaoTTSV
                    return;
                }
                mnReset.Visible = true;

                // Cập nhật danh sách phiếu thuê
                dsPhieuThue.DataSource = dt; // Thay "dsPhieuThue" bằng DataGridView của bạn
            }
            txtMaSV.Clear();
        }

        private void mnReset_Click(object sender, EventArgs e)
        {
            layDSPhieuThue();
            mnReset.Visible = false;
        }

        private void btnLapPT_Click(object sender, EventArgs e)
        {
            if (txtMaPT.Text == "" || txtMaSV.Text == "")
            {
                MessageBox.Show("Không được để trống Mã sinh viên hoặc Mã phiếu thuê");
                txtMaSV.Focus();
                return;
            }

            try
            {
                thutucLapPT(txtMaSV.Text, txtMaPT.Text);
                MessageBox.Show("Phiếu thuê mới đã được tạo thành công.");
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Lỗi khi tạo phiếu thuê mới: " + ex.Message);
                return;
            }

            txtMaPT.Clear();
            txtMaSV.Clear();
            layDSPhieuThue();
        }
        public void thutucLapPT(string maSV, string maPT)
        {
            string sql = "lapPT"; // Sử dụng stored procedure bạn vừa tạo
            using (SqlConnection con = new SqlConnection(conStr))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@MaSV", SqlDbType.VarChar).Value = maSV;
                    cmd.Parameters.Add("@MaPT", SqlDbType.VarChar).Value = maPT;
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void btnTaoTTSV_MouseEnter(object sender, EventArgs e)
        {
            btnTaoTTSV.Cursor = Cursors.Hand;
            btnTaoTTSV.BackColor = System.Drawing.Color.DodgerBlue;
        }

        private void btnTaoTTSV_MouseLeave(object sender, EventArgs e)
        {
            btnTaoTTSV.Cursor = Cursors.Hand;
            btnTaoTTSV.BackColor = System.Drawing.Color.DeepSkyBlue;
        }

        private void btnTaoTTSV_Click(object sender, EventArgs e)
        {
            QuanLySinhVien qlsv = new QuanLySinhVien();
            qlsv.ShowDialog();
        }

        private void txtMaPT_MouseEnter(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null)
            {
                textBox.ReadOnly = false;
            }
        }

        private void txtMaPT_MouseLeave(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null)
            {
                textBox.ReadOnly = true;
            }
        }

        private void dsPhieuThue_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Lấy hàng được chọn
                DataGridViewRow row = this.dsPhieuThue.Rows[e.RowIndex];
                // Lấy MaPT và MaSV từ hàng được chọn
                // Kiểm tra xem dòng có chứa giá trị hay không
                if (row.Cells.Cast<DataGridViewCell>().Any(cell => cell.Value == null || string.IsNullOrEmpty(cell.Value.ToString())))
                {
                    // Nếu dòng không chứa giá trị, thoát khỏi phương thức
                    MessageBox.Show("Không có dữ liệu phiếu thuê!!");
                    btnLapPT.Enabled = true;
                    txtMaSV.Focus();
                    return;
                }
                string maPT = row.Cells["colMaPT"].Value.ToString();
                string maSV = row.Cells["colMaSV"].Value.ToString();

                // Thực hiện thủ tục lưu trữ và lấy kết quả
                DataTable dt = ExecuteStoredProcedure(maPT, maSV);

                //Hiển thị kết quả trong dsCTPT
                dsCTPT.DataSource = dt;
                dsSach();
                txtTenSach.Text = "Tên Sách";

                //int soLuong = Convert.ToInt32(row.Cells["colSLSach"].Value);

                //// Kiểm tra giá trị của SoLuong
                //if (soLuong == 3)
                //{
                //    // Nếu SoLuong > 3, vô hiệu hóa btnLapPhieu
                //    btnLapPT.Enabled = false;
                //}
                //else
                //{
                //    // Nếu không, kích hoạt btnLapPhieu
                //    btnLapPT.Enabled = true;
                //}
            }
        }
        public DataTable ExecuteStoredProcedure(string maPT, string maSV)
        {
            using (SqlConnection conn = new SqlConnection(conStr))
            {
                using (SqlCommand cmd = new SqlCommand("sp_GetCTPTDetails", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@MaPT", maPT));
                    cmd.Parameters.Add(new SqlParameter("@MaSV", maSV));

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    return dt;
                }
            }
        }
        private void txtNMuon_Click(object sender, EventArgs e)
        {
            monthCalendar1.Visible = true;
        }
        private void monthCalendar1_DateSelected(object sender, DateRangeEventArgs e)
        {
            var monthCalendar = sender as MonthCalendar;
            txtNMuon.Text = monthCalendar.SelectionStart.ToString("dd/MM/yyyy");
            monthCalendar.Visible = false;
        }



        private void txtNMuon_TextChanged(object sender, EventArgs e)
        {
            monthCalendar1.Visible = false;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            cboMaSach.Enabled = true;
            txtNMuon.Enabled = true;
            btnLuu.Enabled = true;
        }

        private void dsCTPT_SelectionChanged(object sender, EventArgs e)
        {
            if (dsCTPT.SelectedRows.Count > 0)
            {

                // Get data from the first selected row
                DataGridViewRow row = dsCTPT.SelectedRows[0];
                if (row.Cells["colNgayMuon"].Value.ToString() != "")
                {
                    DateTime ngayMuon = Convert.ToDateTime(row.Cells["colNgayMuon"].Value);
                    txtNMuon.Text = ngayMuon.ToString("dd/MM/yyyy");
                    btnThem.Enabled = false;
                    btnXoa.Enabled = true;
                }
                else
                {
                    btnThem.Enabled = true;
                    txtNMuon.Clear();
                }

                if (row.Cells["colNgayTra"].Value.ToString() != "")
                {
                    btnXoa.Enabled = false;
                    btnTra.Enabled = false;
                }
                else
                {
                    btnTra.Enabled = true;
                }
                txtMaPT_CT.Text = row.Cells["colMaPT_CTPT"].Value.ToString();
                txtMaSV_CT.Text = row.Cells["colMaSV_CTPT"].Value.ToString();


                if (row.Cells["colMaS"].Value.ToString() != "" || row.Cells["colTenS"].Value.ToString() != "")
                {
                    txtTenSach.Text = row.Cells["colTenS"].Value.ToString();
                    cboMaSach.Text = row.Cells["colMaS"].Value.ToString();

                }
                else
                {
                    txtTenSach.Text = "Tên Sách";
                    cboMaSach.Text = "Mã Sách";

                }


            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(conStr))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("taoCTPT", conn))
                    {
                        DateTime ngayMuon;
                        if (DateTime.TryParseExact(txtNMuon.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out ngayMuon))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            // Lấy giá trị từ các control và thêm vào tham số của thủ tục
                            cmd.Parameters.Add("@mapt", SqlDbType.VarChar).Value = txtMaPT_CT.Text;
                            cmd.Parameters.Add("@masv", SqlDbType.VarChar).Value = txtMaSV_CT.Text;

                            // Lấy giá trị MaCTPT từ cột ẩn trong DataGridView
                            DataGridViewRow row = dsCTPT.SelectedRows[0];
                            int maCTPT = int.Parse(row.Cells["colMaCTPT"].Value.ToString());
                            cmd.Parameters.Add("@mactpt", SqlDbType.Int).Value = maCTPT;

                            cmd.Parameters.Add("@masach", SqlDbType.VarChar).Value = cboMaSach.SelectedValue.ToString();
                            cmd.Parameters.Add("@ngaymuon", SqlDbType.Date).Value = ngayMuon;

                            // Thực thi thủ tục
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Thành công!!");
                            // Cập nhật DataGridView
                            DataTable dt = ExecuteStoredProcedure(txtMaPT.Text, txtMaSV.Text);
                            dsCTPT.DataSource = dt;
                            dsSach();
                            btnThem.Enabled = false;
                            cboMaSach.Text = "Mã Sách";
                            txtTenSach.Text = "Tên Sách";
                            txtNMuon.Clear();
                            layDSPhieuThue();
                        }
                        else
                        {
                            // Hiển thị thông báo lỗi nếu không thể chuyển đổi chuỗi thành DateTime
                            MessageBox.Show("Ngày mượn không hợp lệ. Vui lòng nhập ngày theo định dạng dd/MM/yyyy.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Hiển thị thông báo lỗi
                MessageBox.Show("Có lỗi xảy ra: " + ex.Message);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            txtNMuon.Enabled = true;
            cboMaSach.Enabled = true;
            btnLuu.Enabled = false;
            DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Xác nhận xóa", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(conStr))
                    {
                        conn.Open();
                        using (SqlCommand cmd = new SqlCommand("xoaCTPT", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            // Lấy giá trị từ các control và thêm vào tham số của thủ tục
                            cmd.Parameters.Add("@mapt", SqlDbType.VarChar).Value = txtMaPT_CT.Text;
                            cmd.Parameters.Add("@masv", SqlDbType.VarChar).Value = txtMaSV_CT.Text;

                            // Lấy giá trị MaCTPT từ cột ẩn trong DataGridView
                            DataGridViewRow row = dsCTPT.SelectedRows[0];
                            int maCTPT = int.Parse(row.Cells["colMaCTPT"].Value.ToString());
                            cmd.Parameters.Add("@mactpt", SqlDbType.Int).Value = maCTPT;

                            // Lấy giá trị MaSach từ cột colMaSach trong DataGridView
                            string maSach = row.Cells["colMaS"].Value.ToString();
                            cmd.Parameters.Add("@masach", SqlDbType.VarChar).Value = maSach;

                            // Thực thi thủ tục
                            cmd.ExecuteNonQuery();

                            // Cập nhật DataGridView
                            DataTable dt = ExecuteStoredProcedure(txtMaPT.Text, txtMaSV.Text);
                            dsCTPT.DataSource = dt;
                            dsSach();
                            layDSPhieuThue();
                            txtNMuon.Clear();
                            cboMaSach.Text = "Mã Sách";
                            txtTenSach.Text = "Tên Sách";
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Hiển thị thông báo lỗi
                    MessageBox.Show("Có lỗi xảy ra: " + ex.Message);
                }
            }
        }

        private void btnTra_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Bạn có muốn thực hiện trả sách?", "Xác nhận trả sách", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(conStr))
                    {
                        conn.Open();
                        using (SqlCommand cmd = new SqlCommand("traSach", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            // Lấy giá trị từ các control và thêm vào tham số của thủ tục
                            cmd.Parameters.Add("@mapt", SqlDbType.VarChar).Value = txtMaPT_CT.Text;
                            cmd.Parameters.Add("@masv", SqlDbType.VarChar).Value = txtMaSV_CT.Text;

                            // Lấy giá trị MaCTPT từ cột ẩn trong DataGridView
                            DataGridViewRow row = dsCTPT.SelectedRows[0];
                            int maCTPT = int.Parse(row.Cells["colMaCTPT"].Value.ToString());
                            cmd.Parameters.Add("@mactpt", SqlDbType.Int).Value = maCTPT;

                            // Lấy giá trị MaSach từ cột colMaSach trong DataGridView
                            string maSach = row.Cells["colMaS"].Value.ToString();
                            cmd.Parameters.Add("@masach", SqlDbType.VarChar).Value = maSach;

                            // Thực thi thủ tục
                            cmd.ExecuteNonQuery();

                            // Cập nhật DataGridView
                            DataTable dt = ExecuteStoredProcedure(txtMaPT.Text, txtMaSV.Text);
                            dsCTPT.DataSource = dt;
                            dsSach();
                            layDSPhieuThue();
                            txtNMuon.Clear();
                            cboMaSach.Text = "Mã Sách";
                            txtTenSach.Text = "Tên Sách";
                            btnTra.Enabled = false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Hiển thị thông báo lỗi
                    MessageBox.Show("Có lỗi xảy ra: " + ex.Message);
                }
            }
        }
    }
}
