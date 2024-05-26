using Firebase.Database.Query;
using Royal.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Royal
{
    public partial class ReiceiveRoom : Form
    {
        private Firebase.Database.FirebaseClient firebaseClient;
        public ReiceiveRoom()
        {
            InitializeComponent();
            firebaseClient = FirebaseManage.GetFirebaseClient();
            receiveroomDAO receiveroomDAO = new receiveroomDAO();
            receiveroomDAO.LoadReceiveRooms(dataGridViewParameter);

            dataGridViewParameter.CellClick += dataGridBill_CellClick;
        }

        private void kryptonButton3_Click(object sender, EventArgs e)
        {
            receiveroomDAO receiveroomDAO = new receiveroomDAO();
            receiveroomDAO.LoadReceiveRooms(dataGridViewParameter);
        }
        
        private void dataGridBill_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    // Get the selected row
                    DataGridViewRow selectedRow = dataGridViewParameter.Rows[e.RowIndex];

                    // Ensure the row is not a new row
                    if (!selectedRow.IsNewRow)
                    {
                        // Populate form controls with the values from the selected row
                        ID.HeaderText = selectedRow.Cells["ReceivedRoom"].Value?.ToString() ?? string.Empty;
                        ten.HeaderText = selectedRow.Cells["HoTen"].Value?.ToString() ?? string.Empty;
                        CCCD.HeaderText = selectedRow.Cells["CCCD_KH"].Value?.ToString() ?? string.Empty;
                        MaPhong.HeaderText = selectedRow.Cells["MAPHONG"].Value?.ToString() ?? string.Empty;
                        TrangThai.HeaderText = selectedRow.Cells["TRANGTHAI"].Value?.ToString() ?? string.Empty;
                        NgayNhan.HeaderText = selectedRow.Cells["NGAYNHAN"].Value?.ToString() ?? string.Empty;
                        NgayTra.HeaderText = selectedRow.Cells["NGAYTRA"].Value?.ToString() ?? string.Empty;
                        SoNguoi.HeaderText = selectedRow.Cells["SONGUOI"].Value?.ToString() ?? string.Empty;
                        TienCoc.HeaderText = selectedRow.Cells["TIENCOC"].Value?.ToString() ?? string.Empty;
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Errors: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            // Check if a valid row index is clicked

        }
            

        private async void kryptonButton5_Click(object sender, EventArgs e)
        {
            string idDatPhong = kryptonRichTextBox4.Text.Trim(); // Assume you have a TextBox to input the search ID
            if (!string.IsNullOrEmpty(idDatPhong))
            {
                receiveroomDAO receiveroomDAO = new receiveroomDAO();
                receiveroomDAO result = await receiveroomDAO.SearchReceiveRoomByID(idDatPhong);

                if (result != null)
                {
                    // Populate the comboBoxCustomerType with the TRANGTHAI
                    comboBoxCustomerType.SelectedItem = result.TRANGTHAI;

                    // Populate other UI elements with the received room details
                    kryptonRichTextBox1.Text = result.CCCD_KH;
                    kryptonRichTextBox3.Text = result.HoTen;
                    kryptonRichTextBox2.Text = result.MAPHONG;
                    kryptonRichTextBox5.Text = result.SONGUOI.ToString();
                    comboBoxCustomerType.Text = result.TRANGTHAI;
                    kryptonRichTextBox6.Text = result.TIENCOC.ToString();
                    // You may need to parse the dates as needed
                    if (DateTime.TryParse(result.NGAYNHAN, out DateTime ngayNhan))
                    {
                        kryptonDateTimePicker2.Value = ngayNhan;
                    }
                    if (DateTime.TryParse(result.NGAYTRA, out DateTime ngayTra))
                    {
                        kryptonDateTimePicker1.Value = ngayTra;
                    }
                }
                else
                {
                    MessageBox.Show("No receive room found with the specified ID.", "Search Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

        }

        private void dataGridViewParameter_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private async void kryptonButton1_Click(object sender, EventArgs e)
        {
            try
            {
                receiveroomDAO receiveroomDAO = new receiveroomDAO();

                // Lấy thông tin từ các ô nhập liệu
                string receivedRoom = kryptonRichTextBox4.Text.Trim();
                string cccdKh = kryptonRichTextBox1.Text.Trim();
                string hoTen = kryptonRichTextBox3.Text.Trim();
                string maPhong = kryptonRichTextBox2.Text.Trim();
                int soNguoi = Int32.Parse(kryptonRichTextBox5.Text.Trim());
                string trangThai = comboBoxCustomerType.Text.Trim();
                int tienCoc = Int32.Parse(kryptonRichTextBox6.Text.Trim());
                string ngayNhan = kryptonDateTimePicker2.Value.ToString("yyyy-MM-dd");
                string ngayTra = kryptonDateTimePicker1.Value.ToString("yyyy-MM-dd");

                if (trangThai == "Đã Nhận Phòng")
                {
                    // Tạo đối tượng receiveroomDAO với thông tin mới
                    receiveroomDAO updatedReceiveRoom = new receiveroomDAO
                    {
                        ReceivedRoom = receivedRoom,
                        CCCD_KH = cccdKh,
                        HoTen = hoTen,
                        MAPHONG = maPhong,
                        SONGUOI = soNguoi,
                        TRANGTHAI = trangThai,
                        TIENCOC = tienCoc,
                        NGAYNHAN = ngayNhan,
                        NGAYTRA = ngayTra
                    };

                    // Gọi phương thức cập nhật
                    await receiveroomDAO.UpdateReceiveRoom(updatedReceiveRoom);
                    receiveroomDAO.LoadReceiveRooms(dataGridViewParameter);
                }
                else
                {
                    MessageBox.Show("Trạng thái phải là 'Đã nhận phòng' mới có thể cập nhật.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating receive room: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void kryptonButton2_Click(object sender, EventArgs e)
        {

            try
            {
                receiveroomDAO receiveroomDAO = new receiveroomDAO();

                // Lấy thông tin từ các ô nhập liệu
                string receivedRoom = kryptonRichTextBox4.Text.Trim();
                string cccdKh = kryptonRichTextBox1.Text.Trim();
                string hoTen = kryptonRichTextBox3.Text.Trim();
                string maPhong = kryptonRichTextBox2.Text.Trim();
                int soNguoi = Int32.Parse(kryptonRichTextBox5.Text.Trim());
                string trangThai = comboBoxCustomerType.Text.Trim();
                int tienCoc = Int32.Parse(kryptonRichTextBox6.Text.Trim());
                string ngayNhan = kryptonDateTimePicker2.Value.ToString("yyyy-MM-dd");
                string ngayTra = kryptonDateTimePicker1.Value.ToString("yyyy-MM-dd");




                if (trangThai == "Đã Trả Phòng")
                {
                    // Tạo đối tượng receiveroomDAO với thông tin mới
                    receiveroomDAO updatedReceiveRoom = new receiveroomDAO
                    {
                        ReceivedRoom = receivedRoom,
                        CCCD_KH = cccdKh,
                        HoTen = hoTen,
                        MAPHONG = maPhong,
                        SONGUOI = soNguoi,
                        TRANGTHAI = trangThai,
                        TIENCOC = tienCoc,
                        NGAYNHAN = ngayNhan,
                        NGAYTRA = ngayTra
                    };

                    // Gọi phương thức cập nhật
                    await receiveroomDAO.UpdateReceiveRoom(updatedReceiveRoom);
                    receiveroomDAO.LoadReceiveRooms(dataGridViewParameter);
                }
                else
                {
                    MessageBox.Show("Trạng thái phải là 'Đã Trả Phòng' mới có thể cập nhật.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating receive room: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
