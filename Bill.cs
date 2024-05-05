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
            string firebaseConnectionString = "https://royal-9807e-default-rtdb.firebaseio.com/";
            firebaseClient = new Firebase.Database.FirebaseClient(firebaseConnectionString);

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
            maKHBox.Items.Add(customer.Object.MAKH);
        }
        }
         catch (Exception ex)
        {
            // Xử lý ngoại lệ nếu có
            MessageBox.Show("Lỗi khi tải dữ liệu từ Firebase Realtime Database: " + ex.Message);
        }
}


        private void kryptonButton3_Click(object sender, EventArgs e)
        {
            // Tạo số ngẫu nhiên từ 0 đến 999999
            Random random = new Random();
            int randomNumber = random.Next(0, 999999);

            // Tạo mã hóa đơn
            string maHoaDon = "HD" + randomNumber.ToString("D6");

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
    }
}