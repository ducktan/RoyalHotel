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

        private void kryptonButton5_Click(object sender, EventArgs e)
        {
            BillDAO billDAO = new BillDAO();
            billDAO.DeleteBill(dataGridBill);
        }

        private void kryptonButton4_Click(object sender, EventArgs e)
        {
            BillDAO billDAO = new BillDAO();
            billDAO.UpdateBill(dataGridBill);
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {

        }
    }
}