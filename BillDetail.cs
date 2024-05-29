using Firebase.Database.Query;
using Royal.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Royal
{
    public partial class BillDetail : Form
    {
        private Firebase.Database.FirebaseClient firebaseClient;
        public BillDetail()
        {
            InitializeComponent();
            // Khởi tạo FirebaseClient với chuỗi kết nối đến Firebase Realtime Database
            firebaseClient = FirebaseManage.GetFirebaseClient();
            BillDetailDAO a = new BillDetailDAO();
            a.LoadBillDetail(dataGridViewBillDetail);
            dataGridViewBillDetail.CellClick += dataGridBill_CellClick;
            LoadMaHDFromDatabase();
            LoadMaDVFromDatabase();
           
        }

        // Load data from Firebase 
        public async void LoadMaHDFromDatabase()
        {
            try
            {
                // Truy vấn Firebase Realtime Database để lấy danh sách khách hàng
                var customerList = await firebaseClient
                    .Child("Bill")
                    .OnceAsync<BillDAO>();

                // Xóa các mục hiện có trong ComboBox
                maHDBox.Items.Clear();

                // Thêm ID_KH từ các bản ghi khách hàng vào ComboBox
                foreach (var customer in customerList)
                {
                    string customerDisplayText = $"{customer.Object.MAHD}";
                    maHDBox.Items.Add(customerDisplayText);
                }
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ nếu có
                MessageBox.Show("Lỗi khi tải dữ liệu từ Firebase Realtime Database: " + ex.Message);
            }
        }

        public async void LoadMaDVFromDatabase()
        {
            try
            {
                // Truy vấn Firebase Realtime Database để lấy danh sách khách hàng
                var customerList = await firebaseClient
                    .Child("Service")
                    .OnceAsync<ServiceDAO>();

                // Xóa các mục hiện có trong ComboBox
                maDV.Items.Clear();

                // Thêm ID_KH từ các bản ghi khách hàng vào ComboBox
                foreach (var customer in customerList)
                {
                    string customerDisplayText = $"{customer.Object.seID}";
                    maDV.Items.Add(customerDisplayText);
                }
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ nếu có
                MessageBox.Show("Lỗi khi tải dữ liệu từ Firebase Realtime Database: " + ex.Message);
            }
        }

       

        // function

        private void button3_Click(object sender, EventArgs e)
        {
            Service s = new Service();
            s.Show();
        }

        private void dataGridBill_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Kiểm tra xem có hàng nào được chọn không
            {
                DataGridViewRow selectedRow = dataGridViewBillDetail.Rows[e.RowIndex];

                // Kiểm tra nếu hàng không rỗng
                if (!selectedRow.IsNewRow)
                {
                    // Trích xuất dữ liệu từ hàng được chọn và hiển thị lên form
                    cthd.Text = (string)selectedRow.Cells[0].Value;
                    maHDBox.Text = (string)(selectedRow.Cells[1].Value);
                    maDV.Text = (string)(selectedRow.Cells[2].Value);
                    SLG_DV.Text = selectedRow.Cells[3].Value.ToString();
                    thanhtien.Text = selectedRow.Cells[4].Value.ToString();
                }
            }
        }
        private async void addBut_Click(object sender, EventArgs e)
        {


            var roomTypes = await firebaseClient
                    .Child("BillDetail")
                    .OnceAsync<BillDetailDAO>();
            // Tìm mã phòng lớn nhất hiện có
            int maxRoomNumber = 0;
            foreach (var roomData in roomTypes)
            {
                int roomNumber = int.Parse(roomData.Object.MACTHD.Substring(4));
                if (roomNumber > maxRoomNumber)
                {
                    maxRoomNumber = roomNumber;
                }
            }

          


            string newRoomNumber = "CTHD" + (maxRoomNumber + 1).ToString("D3");


            // Create a new Bill object with form control values
            BillDetailDAO newBill = new BillDetailDAO()
            {
                MACTHD = newRoomNumber,
                MAHD = maHDBox.Text,
                MADV = maDV.Text,
                SLG_DV = Int32.Parse(SLG_DV.Text),
                THANHTIEN = Int32.Parse(thanhtien.Text)
            };

            await newBill.AddBDetail(newBill);
            newBill.LoadBillDetail(dataGridViewBillDetail);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            BillDetailDAO a = new BillDetailDAO();
            a.LoadBillDetail(dataGridViewBillDetail);
        }

        private async void maHDBox_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            try
            {
                
                // Lấy mã hóa đơn đã chọn
                string selectedMaHD = maHDBox.SelectedItem.ToString();

                // Truy vấn Firebase Realtime Database để lấy thông tin hóa đơn tương ứng
                var billSnapshot = await firebaseClient
                    .Child("Bill")
                    .OrderByKey()
                    .EqualTo(selectedMaHD)
                    .OnceAsync<BillDAO>();

                // Lấy mã khách hàng từ bản ghi hóa đơn
                if (billSnapshot.Any())
                {
                    string maKH1 = billSnapshot.First().Object.ID_KH;
                    maKH.Text = maKH1;
                }
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ nếu có
                MessageBox.Show("Lỗi khi tải dữ liệu từ Firebase Realtime Database: " + ex.Message);
            }
        }

        private async void maDV_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                // Lấy mã hóa đơn đã chọn
                string selectedMaHD = maDV.SelectedItem.ToString();

                // Truy vấn Firebase Realtime Database để lấy thông tin hóa đơn tương ứng
                var billSnapshot = await firebaseClient
                    .Child("Service")
                    .OrderByKey()
                    .EqualTo(selectedMaHD)
                    .OnceAsync<ServiceDAO>();

                // Lấy mã khách hàng từ bản ghi hóa đơn
                if (billSnapshot.Any())
                {
                    string maKH1 = billSnapshot.First().Object.sePrice.ToString();
                    dongiaDV.Text = maKH1;

                    // Gọi hàm tính thành tiền
                    CalculateTotalAmount();
                }
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ nếu có
                MessageBox.Show("Lỗi khi tải dữ liệu từ Firebase Realtime Database: " + ex.Message);
            }
        }

        private void CalculateTotalAmount()
        {
            // Lấy giá và số lượng từ các control
            string donGiaText = dongiaDV.Text;
            string soLuongText = SLG_DV.Text;

            // Kiểm tra xem cả hai trường đều có giá trị hợp lệ
            if (!string.IsNullOrEmpty(donGiaText) && !string.IsNullOrEmpty(soLuongText))
            {
                // Kiểm tra xem giá và số lượng có thể chuyển đổi sang số nguyên hay không
                if (int.TryParse(donGiaText, out int donGia) && int.TryParse(soLuongText, out int soLuong))
                {
                    // Tính thành tiền
                    int thanhTien = donGia * soLuong;

                    // Cập nhật giá trị vào control thanhtien
                    thanhtien.Text = thanhTien.ToString();
                }
                else
                {
                    // Nếu giá hoặc số lượng không phải là số nguyên, reset giá trị thanhtien
                    thanhtien.Text = "0";
                }
            }
            else
            {
                // Nếu không có đủ thông tin, reset giá trị thanhtien
                thanhtien.Text = "0";
            }
        }

        private void SLG_DV_TextChanged(object sender, EventArgs e)
        {
            CalculateTotalAmount();
        }

        private async void delBut_Click(object sender, EventArgs e)
        {
            BillDetailDAO a = new BillDetailDAO();
            await a.DeleteBillDetail(cthd.Text);
            a.LoadBillDetail(dataGridViewBillDetail);
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            string id = cthd.Text;
            string mahd = maHDBox.Text;
            string madv = maDV.Text;
            int sl = Int32.Parse(SLG_DV.Text);
            int thanhtien1 = Int32.Parse(thanhtien.Text);

            BillDetailDAO a = new BillDetailDAO(); 
            await a.UpdateBill(id, mahd, madv, sl, thanhtien1 );
            a.LoadBillDetail(dataGridViewBillDetail);
        }

        private async void button8_Click(object sender, EventArgs e)
        {
           
                // Get the search criteria from the UI controls
                string type = comboBox1.Text;
                string searchText1 = searchText.Text.Trim(); // Assuming txtSearch is a textbox for search text

                if (string.IsNullOrEmpty(searchText1))
                {
                    MessageBox.Show("Please enter the search text.");
                    return;
                }
                // Call the appropriate search function based on the selected type
                Royal.DAO.BillDetailDAO billFun = new Royal.DAO.BillDetailDAO(); // Assuming you have an instance

                // Call the appropriate search function based on the selected type
                List<Royal.DAO.BillDetailDAO> searchResults = new List<Royal.DAO.BillDetailDAO>(); // Initialize empty list


                try
                {
                    if (type == "Mã hoá đơn") // Search by room type ID (MALPH)
                    {

                    searchResults = await billFun.SearchBillDetailByMaHD(searchText1);
                }
                    else if (type == "Mã dịch vụ") // Search by room type ID (MALPH)
                    {

                        searchResults = await billFun.SearchBillDetailByMaDV(searchText1);
                    }
                   

                    else
                    {
                        MessageBox.Show("Invalid search type selected.");
                        return;
                    }

                    // Prepare UI results (assuming you want to display MALPH, TENLPH, SLNG, GIA)
                    List<string[]> uiResults = searchResults.Select(bill => new string[] { bill.MACTHD, bill.MAHD, bill.MADV, bill.SLG_DV.ToString(), bill.THANHTIEN.ToString() }).ToList();

                    // Update UI elements on the UI thread
                    if (this.InvokeRequired)
                    {
                        this.Invoke(new Action(() =>
                        {
                            dataGridViewBillDetail.Rows.Clear();
                            foreach (string[] rowData in uiResults)
                            {
                                dataGridViewBillDetail.Rows.Add(rowData);
                            }
                        }));
                    }
                    else
                    {
                    dataGridViewBillDetail.Rows.Clear();
                        foreach (string[] rowData in uiResults)
                        {
                        dataGridViewBillDetail.Rows.Add(rowData);
                        }
                    }

                    // Handle no search results (optional)
                    if (searchResults.Count == 0)
                    {
                        string searchCriteria = $"Search by: {type}";
                        MessageBox.Show($"No bill detail found with {searchCriteria}");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error searching bill detail: {ex.Message}");
                }
            
        }

        private void print_Click(object sender, EventArgs e)
        {
            PrintBill p = new PrintBill();
            p.Show();
        }
    }
}
