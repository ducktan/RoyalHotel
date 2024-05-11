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
                    // Combine MAKH and TENKH for display
                    string customerDisplayText = customer.Object.MAKH;
                    maKHBox.Items.Add(customerDisplayText);
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
            // Get the current row count for the "Bill" table
            int currentRowCount = await firebaseClient
                .Child("Bill")
                .OnceAsync<object>()
                .ContinueWith(task => task.Result.Count);

            // Increment by 1 to get the new sequential number
            int newNumber = currentRowCount + 1;

            // Format the number with leading zeros (001, 002, ...)
            string formattedNumber = newNumber.ToString("D3"); // Adjust "D3" for desired number of digits

            // Create the MAHD with your preferred prefix (e.g., "HD")
            string maHoaDon = $"HD{formattedNumber}";

            // Create a new Bill object with form control values
            BillDAO newBill = new BillDAO()
            {
                MAHD = maHoaDon,
                ID_NV = nhanvien.Text,
                MAPHONG = maphong.Text,
                NGLAP = date.Text,
                ID_KH = maKHBox.Text, // Sử dụng mã KH từ ComboBox
                THANHTIEN = Int32.Parse(total.Text),
                DISCOUNT = Int32.Parse(discount.Text),
                TRANGTHAI = status.Text,
                DONGIA = Int32.Parse(giaDon.Text),
            };

            newBill.AddBill(newBill);
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
                    maphong.Text = (string)selectedRow.Cells[1].Value;
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
        private void kryptonButton5_Click(object sender, EventArgs e)
        {
                BillDAO billDAO = new BillDAO();
            
                string billID = mahoadon.Text;
                billDAO.DeleteBill(billID);
              

                // Đặt các giá trị thành rỗng
                mahoadon.Text = "";
                maphong.Text = "";
                nhanvien.Text = "";
                date.Text = "";
                status.Text = "";
                giaDon.Text = "";
                discount.Text = "";
                total.Text = "";
                
            
            
        }

        private void kryptonButton4_Click(object sender, EventArgs e)
        {
            BillDAO billDAO = new BillDAO();
            int giaDonValue = int.Parse(giaDon.Text);
            int discountValue = int.Parse(discount.Text);
            int totalValue = int.Parse(total.Text);

            billDAO.UpdateBill(mahoadon.Text, maphong.Text, status.Text, maKHBox.Text, nhanvien.Text, date.Text, giaDonValue, discountValue, totalValue);
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
                if (type == "MAHD") // Search by room type ID (MALPH)
                {

                    Royal.DAO.BillDAO searchResult = await billFun.SearchBillTypeById(searchText1);
                    searchResults.Add(searchResult);
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
                    MessageBox.Show($"No room types found with {searchCriteria}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error searching room types: {ex.Message}");
            }
        }



        private void CalculateTotal()
        {
            if (int.TryParse(giaDon.Text, out int giaDonValue) && int.TryParse(discount.Text, out int discountValue))
            {
                double tongTien = giaDonValue * (1 - discountValue / 100.0);
                total.Text = tongTien.ToString();
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
    }
}