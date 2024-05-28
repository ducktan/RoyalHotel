using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FireSharp;
using Firebase.Database;
using Firebase.Database.Query;
using Royal.DAO;
using System.Globalization;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

namespace Royal
{
    public partial class Bill : Form
    {
        private Firebase.Database.FirebaseClient firebaseClient;

        public Bill()
        {
            InitializeComponent();
            // Khởi tạo FirebaseClient với chuỗi kết nối đến Firebase Realtime Database
            firebaseClient = FirebaseManage.GetFirebaseClient();
            BillDAO billDAO = new BillDAO();
            billDAO.LoadBill(dataGridBill);
            dataGridBill.CellClick += dataGridBill_CellClick;
            LoadMaKHFromDatabase();
            LoadMaNVFromDatabase();
            LoadMaPhongFromDatabase();
        }
        public Bill(string id)
        {
            InitializeComponent();
            // Khởi tạo FirebaseClient với chuỗi kết nối đến Firebase Realtime Database
            firebaseClient = FirebaseManage.GetFirebaseClient();
            BillDAO billDAO = new BillDAO();
            billDAO.LoadBill(dataGridBill);
            dataGridBill.CellClick += dataGridBill_CellClick;
            LoadMaKHFromDatabase();
            LoadMaNVFromDatabase();
            LoadMaPhongFromDatabase();

            maKHBox.Text = id;
            LoadInfoBillFromMaKH(maKHBox.Text);

        }

        private async void LoadInfoBillFromMaKH(string makh)
        {
            // Call the appropriate search function based on the selected type
            Royal.DAO.BillDAO billFun = new Royal.DAO.BillDAO(); // Assuming you have an instance

            // Call the appropriate search function based on the selected type
            List<Royal.DAO.BillDAO> searchResults = new List<Royal.DAO.BillDAO>(); // Initialize empty list
            searchResults = await billFun.SearchBillByIDKH(makh);

            // Prepare UI results (assuming you want to display MALPH, TENLPH, SLNG, GIA)
            List<string[]> uiResults = searchResults.Select(bill => new string[] { bill.MAHD, bill.MAPHONG, bill.TRANGTHAI, bill.ID_KH, bill.ID_NV, bill.NGLAP, bill.DONGIA.ToString(), bill.DISCOUNT.ToString(), bill.THANHTIEN.ToString() }).ToList();

            // Update UI elements on the UI thread
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() =>
                {
                    dataGridBill.Rows.Clear();
                    foreach (string[] rowData in uiResults)
                    {
                        dataGridBill.Rows.Add(rowData);
                    }
                }));
            }
            else
            {
                dataGridBill.Rows.Clear();
                foreach (string[] rowData in uiResults)
                {
                    dataGridBill.Rows.Add(rowData);
                }
            }

        }

        private async void LoadMaKHFromDatabase()
        {
            try
            {
                // Truy vấn Firebase Realtime Database để lấy danh sách khách hàng
                var customerList = await firebaseClient
                    .Child("Customer")
                    .OnceAsync<CustomerDAO>();

                // Xóa các mục hiện có trong ComboBox
                maKHBox.Items.Clear();

                // Thêm ID_KH từ các bản ghi khách hàng vào ComboBox
                foreach (var customer in customerList)
                {
                    string customerDisplayText = $"{customer.Object.MAKH}";
                    maKHBox.Items.Add(customerDisplayText);
                }
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ nếu có
                MessageBox.Show("Lỗi khi tải dữ liệu từ Firebase Realtime Database: " + ex.Message);
            }
        }

        private async void LoadMaNVFromDatabase()
        {
            try
            {
                // Truy vấn Firebase Realtime Database để lấy danh sách khách hàng
                var customerList = await firebaseClient
                    .Child("Staff")
                    .OnceAsync<StaffDAO>();

                // Xóa các mục hiện có trong ComboBox
                nhanvien.Items.Clear();

                // Thêm ID_KH từ các bản ghi khách hàng vào ComboBox
                foreach (var customer in customerList)
                {
                    // Combine MAKH and TENKH for display
                    string customerDisplayText = $"{customer.Object.StaffID}";
                    nhanvien.Items.Add(customerDisplayText);
                }
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ nếu có
                MessageBox.Show("Lỗi khi tải dữ liệu từ Firebase Realtime Database: " + ex.Message);
            }
        }
        private async void LoadMaPhongFromDatabase()
        {
            try
            {
                // Truy vấn Firebase Realtime Database để lấy danh sách khách hàng
                var customerList = await firebaseClient
                    .Child("Room")
                    .OnceAsync<Room>();

                // Xóa các mục hiện có trong ComboBox
                phong.Items.Clear();

                // Thêm ID_KH từ các bản ghi khách hàng vào ComboBox
                foreach (var customer in customerList)
                {
                    // Combine MAKH and TENKH for display
                    string customerDisplayText = customer.Object.MAPH;
                    phong.Items.Add(customerDisplayText);
                }
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ nếu có
                MessageBox.Show("Lỗi khi tải dữ liệu từ Firebase Realtime Database: " + ex.Message);
            }
        }



        private async void kryptonButton3_Click(object sender, EventArgs e)
        {
            var roomTypes = await firebaseClient
                                .Child("Bill")
                                .OnceAsync<BillDAO>();
            // Tìm mã phòng lớn nhất hiện có
            int maxRoomNumber = 0;
            foreach (var roomData in roomTypes)
            {
                int roomNumber = int.Parse(roomData.Object.MAHD.Substring(2));
                if (roomNumber > maxRoomNumber)
                {
                    maxRoomNumber = roomNumber;
                }
            }

            string newRoomNumber = "HD" + (maxRoomNumber + 1).ToString("D3");

            // Get the ID_NV from the combobox
            string nhanVienText = nhanvien.Text;
            string[] nhanVienParts = nhanVienText.Split('-');
            string id_nv = nhanVienParts[0];

            // Get the ID_KH from the combobox
            string khachHangText = maKHBox.Text;
            string[] khachHangParts = khachHangText.Split('-');
            string id_kh = khachHangParts[0];

            // Create a new Bill object with form control values
            BillDAO newBill = new BillDAO()
            {
                MAHD = newRoomNumber,
                ID_NV = id_nv,
                MAPHONG = phong.Text,
                NGLAP = date.Text,
                ID_KH = id_kh,
                THANHTIEN = Int32.Parse(total.Text),
                DISCOUNT = Int32.Parse(discount.Text),
                TRANGTHAI = status.Text,
                DONGIA = Int32.Parse(giaDon.Text),
            };

            await newBill.AddBill(newBill);
            newBill.LoadBill(dataGridBill);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            BillDAO billDAO = new BillDAO();
            billDAO.LoadBill(dataGridBill);
        }

        private void dataGridBill_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Kiểm tra xem có hàng nào được chọn không
            {
                DataGridViewRow selectedRow = dataGridBill.Rows[e.RowIndex];

                // Kiểm tra nếu hàng không rỗng
                if (!selectedRow.IsNewRow)
                {
                    // Trích xuất dữ liệu từ hàng được chọn và hiển thị lên form
                    mahoadon.Text = (string)selectedRow.Cells[0].Value;
                    phong.Text = (string)selectedRow.Cells[1].Value;
                    nhanvien.Text = (string)selectedRow.Cells[4].Value;
                    date.Text = (string)selectedRow.Cells[5].Value;
                    status.Text = (string)selectedRow.Cells[2].Value;
                    giaDon.Text = selectedRow.Cells[6].Value.ToString();
                    discount.Text = selectedRow.Cells[7].Value.ToString();
                    total.Text = selectedRow.Cells[8].Value.ToString();
                    maKHBox.Text = selectedRow.Cells[3].Value.ToString();
                }
            }
        }
        private async void kryptonButton5_Click(object sender, EventArgs e)
        {
            BillDAO billDAO = new BillDAO();

            string billID = mahoadon.Text;
            await billDAO.DeleteBill(billID);
            billDAO.LoadBill(dataGridBill);



            // Đặt các giá trị thành rỗng
            mahoadon.Text = "";
            phong.Text = "";
            nhanvien.Text = "";
            date.Text = "";
            status.Text = "";
            giaDon.Text = "";
            discount.Text = "";
            total.Text = "";



        }

        private async void kryptonButton4_Click(object sender, EventArgs e)
        {
            BillDAO billDAO = new BillDAO();
            int giaDonValue = int.Parse(giaDon.Text);
            int discountValue = int.Parse(discount.Text);
            int totalValue = int.Parse(total.Text);

            await billDAO.UpdateBill(mahoadon.Text, phong.Text, status.Text, maKHBox.Text, nhanvien.Text, date.Text, giaDonValue, discountValue, totalValue);
            billDAO.LoadBill(dataGridBill);
        }

        private async void kryptonButton1_Click(object sender, EventArgs e)
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
            Royal.DAO.BillDAO billFun = new Royal.DAO.BillDAO(); // Assuming you have an instance

            // Call the appropriate search function based on the selected type
            List<Royal.DAO.BillDAO> searchResults = new List<Royal.DAO.BillDAO>(); // Initialize empty list


            try
            {
                if (type == "Mã hoá đơn") // Search by room type ID (MALPH)
                {

                    Royal.DAO.BillDAO searchResult = await billFun.SearchBillTypeById(searchText1);
                    searchResults.Add(searchResult);
                }
                else if (type == "Mã nhân viên") // Search by room type ID (MALPH)
                {

                    searchResults = await billFun.SearchBillByIDNV(searchText1);
                }
                else if (type == "Mã khách hàng") // Search by room type ID (MALPH)
                {

                    searchResults = await billFun.SearchBillByIDKH(searchText1);
                }
                else if (type == "Ngày lập HĐ") // Search by room type ID (MALPH)
                {

                    searchResults = await billFun.SearchBillByDate(searchText1);
                }
                else if (type == "Trạng thái HĐ") // Search by room type ID (MALPH)
                {

                    searchResults = await billFun.SearchBillByState(searchText1);
                }

                else
                {
                    MessageBox.Show("Invalid search type selected.");
                    return;
                }

                // Prepare UI results (assuming you want to display MALPH, TENLPH, SLNG, GIA)
                List<string[]> uiResults = searchResults.Select(bill => new string[] { bill.MAHD, bill.MAPHONG, bill.TRANGTHAI, bill.ID_KH, bill.ID_NV, bill.NGLAP, bill.DONGIA.ToString(), bill.DISCOUNT.ToString(), bill.THANHTIEN.ToString() }).ToList();

                // Update UI elements on the UI thread
                if (this.InvokeRequired)
                {
                    this.Invoke(new Action(() =>
                    {
                        dataGridBill.Rows.Clear();
                        foreach (string[] rowData in uiResults)
                        {
                            dataGridBill.Rows.Add(rowData);
                        }
                    }));
                }
                else
                {
                    dataGridBill.Rows.Clear();
                    foreach (string[] rowData in uiResults)
                    {
                        dataGridBill.Rows.Add(rowData);
                    }
                }

                // Handle no search results (optional)
                if (searchResults.Count == 0)
                {
                    string searchCriteria = $"Search by: {type}";
                    MessageBox.Show($"No bill found with {searchCriteria}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error searching bill: {ex.Message}");
            }
        }



        private void CalculateTotal()
        {
            if (int.TryParse(giaDon.Text, out int giaDonValue) && int.TryParse(discount.Text, out int discountValue))
            {
                int thanhtien = giaDonValue;
                int discountAmount = (giaDonValue * discountValue) / 100;
                int tongTien = thanhtien - discountAmount;
                total.Text = tongTien.ToString();
            }
            else
            {
                total.Text = ""; // Nếu không thể parse, đặt total.Text thành chuỗi rỗng
            }
        }

        private void giaDon_TextChanged_1(object sender, EventArgs e)
        {
            CalculateTotal();
        }

        private void discount_TextChanged_1(object sender, EventArgs e)
        {
            CalculateTotal();
        }

        private void dataGridBill_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {

        }

        private void maKHBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private async void kryptonButton3_Click_1(object sender, EventArgs e)
        {
            try
            {
                BillDetailDAO billDetailDAO = new BillDetailDAO();
                List<BillDetailDAO> listBillDetail = await billDetailDAO.SearchBillDetailByMaHD(mahoadon.Text);
                int sum = 0;
                BillDAO a = await new BillDAO().SearchBillTypeById(mahoadon.Text);
                if (listBillDetail.Count > 0)
                {

                    if (int.TryParse(giaDon.Text, out int dongiaPhong))
                    {
                        sum += dongiaPhong;

                        foreach (var item in listBillDetail)
                        {
                            BillDetailDAO bill1 = new BillDetailDAO();
                            bill1 = item;
                            sum += Int32.Parse(bill1.THANHTIEN.ToString());

                        }
                        giaDon.Text = sum.ToString();
                        await a.UpdateBill(a.MAHD, a.MAPHONG, a.TRANGTHAI, a.ID_KH, a.ID_NV, a.NGLAP, sum, a.DISCOUNT, a.THANHTIEN, listBillDetail.Count);

                    }
                    else
                    {
                        MessageBox.Show("Invalid room price format.");
                    }
                }
                else
                {
                    MessageBox.Show("No bill details found for the given bill ID.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DetailBut_Click(object sender, EventArgs e)
        {
            BillDetail b = new BillDetail();
            b.Show();
        }

        private void kryptonButton4_Click_1(object sender, EventArgs e)
        {
            PrintBill b = new PrintBill();
            b.Show();
        }

        private void toolStripLabel2_Click(object sender, EventArgs e)
        {


            SaveFileDialog saveFileDialog = new SaveFileDialog();


            saveFileDialog.Filter = "PDF Files (*.pdf)|*.pdf";
            saveFileDialog.DefaultExt = "pdf";
            saveFileDialog.AddExtension = true;



            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {


                ExportToPdf.Export(dataGridBill, saveFileDialog.FileName);


            }

        }

        private void toolStripLabel3_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();


            saveFileDialog.Filter = "Excel Files (*.xlsx)|*.xlsx";
            saveFileDialog.DefaultExt = "xlsx";
            saveFileDialog.AddExtension = true;



            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {


                ExportToExcel.Export(dataGridBill, saveFileDialog.FileName);


            }



        }

        private void Bill_Load(object sender, EventArgs e)
        {

        }
    }
}