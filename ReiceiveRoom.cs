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
using System.Windows.Forms.DataVisualization.Charting;

namespace Royal
{
    public partial class ReiceiveRoom : Form
    {
        private Firebase.Database.FirebaseClient firebaseClient;
        private List<receiveroomDAO> searchResults; // Store search results here
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
            // Bind the CellClick event handler
            dataGridViewParameter.CellClick += dataGridBill_CellClick;
            comboBoxCustomerType.SelectedItem = -1;
            kryptonRichTextBox1.Text = "";
            kryptonRichTextBox3.Text = "";
            kryptonRichTextBox2.Text = "";
            kryptonRichTextBox4.Text = "";
            kryptonRichTextBox5.Text = "";
            comboBoxCustomerType.Text = "";
            kryptonRichTextBox6.Text = "";
        }

        private void dataGridBill_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && e.RowIndex < dataGridViewParameter.Rows.Count)
                {
                    // Get the selected row
                    DataGridViewRow selectedRow = dataGridViewParameter.Rows[e.RowIndex];

                    // Populate form controls with the values from the selected row
                    comboBoxCustomerType.SelectedItem = selectedRow.Cells["TRANGTHAI"].Value?.ToString();
                    kryptonRichTextBox1.Text = selectedRow.Cells["CCCD"].Value?.ToString();
                    kryptonRichTextBox3.Text = selectedRow.Cells["HoTen"].Value?.ToString();
                    kryptonRichTextBox2.Text = selectedRow.Cells["MAPHONG"].Value?.ToString();
                    kryptonRichTextBox5.Text = selectedRow.Cells["SONGUOI"].Value?.ToString();
                    comboBoxCustomerType.Text = selectedRow.Cells["TRANGTHAI"].Value?.ToString();
                    kryptonRichTextBox6.Text = selectedRow.Cells["TIENCOC"].Value?.ToString();

                    // Parse and set the dates if available
                    if (DateTime.TryParse(selectedRow.Cells["NGAYNHAN"].Value?.ToString(), out DateTime ngayNhan))
                    {
                        kryptonDateTimePicker2.Value = ngayNhan;
                    }
                    if (DateTime.TryParse(selectedRow.Cells["NGAYTRA"].Value?.ToString(), out DateTime ngayTra))
                    {
                        kryptonDateTimePicker1.Value = ngayTra;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private async void kryptonButton5_Click(object sender, EventArgs e)
        {
            string idDatPhong = kryptonRichTextBox4.Text.Trim(); // Assume you have a TextBox to input the search ID
            if (!string.IsNullOrEmpty(idDatPhong))
            {
                dataGridViewParameter.Rows.Clear();
                receiveroomDAO receiveroomDAO = new receiveroomDAO();
                searchResults = await receiveroomDAO.SearchReceiveRoomByID(idDatPhong);

                if (searchResults != null && searchResults.Any())
                {
                    foreach (var result in searchResults)
                    {
                        // Populate the DataGridView with the search results
                        var row = new DataGridViewRow();
                        row.CreateCells(dataGridViewParameter);
                        row.Cells[0].Value = result.ReceivedRoom;
                        row.Cells[1].Value = result.HoTen;
                        row.Cells[2].Value = result.CCCD_KH;
                        row.Cells[3].Value = result.MAPHONG;
                        row.Cells[4].Value = result.TRANGTHAI;
                        row.Cells[5].Value = result.NGAYNHAN;
                        row.Cells[6].Value = result.NGAYTRA;
                        row.Cells[7].Value = result.SONGUOI;
                        row.Cells[8].Value = result.TIENCOC;
                        dataGridViewParameter.Rows.Add(row);
                    }

                    // Bind the CellClick event handler
                    dataGridViewParameter.CellClick += dataGridBill_CellClick;
                    comboBoxCustomerType.SelectedItem = -1;
                    kryptonRichTextBox1.Text = "";
                    kryptonRichTextBox3.Text = "";
                    kryptonRichTextBox2.Text = "";
                    kryptonRichTextBox5.Text = "";
                    comboBoxCustomerType.Text = "";
                    kryptonRichTextBox6.Text = "";

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

              
                string cccdKh = kryptonRichTextBox1.Text.Trim();
                string hoTen = kryptonRichTextBox3.Text.Trim();
                string maPhong = kryptonRichTextBox2.Text.Trim();
                int soNguoi = Int32.Parse(kryptonRichTextBox5.Text.Trim());
                string trangThai = comboBoxCustomerType.Text.Trim();
                int tienCoc = Int32.Parse(kryptonRichTextBox6.Text.Trim());
                string ngayNhan = kryptonDateTimePicker2.Value.ToString("yyyy-MM-dd");
                string ngayTra = kryptonDateTimePicker1.Value.ToString("yyyy-MM-dd");
           
                receiveroomDAO result = await receiveroomDAO.SearchReceiveRoomByIDDP(cccdKh, maPhong, ngayNhan, ngayTra);
                string receivedRoom = result.ReceivedRoom.Trim();
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
               
                // Lấy thông tin từ các ô nhập liệu
                string cccdKh = kryptonRichTextBox1.Text.Trim();
                string hoTen = kryptonRichTextBox3.Text.Trim();
                string maPhong = kryptonRichTextBox2.Text.Trim();
                int soNguoi = Int32.Parse(kryptonRichTextBox5.Text.Trim());
                string trangThai = comboBoxCustomerType.Text.Trim();
                int tienCoc = Int32.Parse(kryptonRichTextBox6.Text.Trim());
                string ngayNhan = kryptonDateTimePicker2.Value.ToString("yyyy-MM-dd");
                string ngayTra = kryptonDateTimePicker1.Value.ToString("yyyy-MM-dd");

                receiveroomDAO receiveroomDAO = new receiveroomDAO();
                receiveroomDAO result = await receiveroomDAO.SearchReceiveRoomByIDDP(cccdKh, maPhong, ngayNhan, ngayTra);
                string receivedRoom = result.ReceivedRoom.Trim();

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
                    MessageBox.Show("Trạng thái phải là 'Đã nhận phòng' mới có thể cập nhật.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }



            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating receive room: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void kryptonRichTextBox4_TextChanged(object sender, EventArgs e)
        {

        }
    }
}