using Firebase.Database.Query;
using FireSharp.Response;
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
    public partial class PrintBill : Form
    {
        private Firebase.Database.FirebaseClient firebaseClient;
        string maPhong;
        string maKhachhang;
        string CustomerType;

        public PrintBill()
        {
            InitializeComponent();
            // Khởi tạo FirebaseClient với chuỗi kết nối đến Firebase Realtime Database
            firebaseClient = FirebaseManage.GetFirebaseClient();
            LoadMaHDFromDatabase();
        }

        public async void LoadCTHDFromBill()
        {
            try
            {
                // Lấy dữ liệu chi tiết hóa đơn từ Firebase Realtime Database
                var orderDetailsSnapshot = await firebaseClient
                    .Child("BillDetail")
                    .OrderByKey()
                    .EqualTo(maHDBox.Text)
                    .OnceAsync<BillDetailDAO>();

                // Kiểm tra xem có dữ liệu hay không
                if (orderDetailsSnapshot.Any())
                {
                    // Xóa dữ liệu hiện có trong DataGridView (tùy chọn)
                    dataGridViewDV.Rows.Clear();

                    // Thêm dữ liệu chi tiết hóa đơn vào DataGridView
                    foreach (var item in orderDetailsSnapshot)
                    {
                        BillDetailDAO orderDetail = item.Object;

                        dataGridViewDV.Rows.Add(
                         
                            orderDetail.MACTHD, 
                            orderDetail.MADV, 
                            orderDetail.SLG_DV
                        );
                    }
                }
                else
                {
                    // Nếu không tìm thấy dữ liệu, hiển thị thông báo
                    MessageBox.Show("Không tìm thấy dữ liệu chi tiết hóa đơn.");
                }
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ nếu có
                MessageBox.Show("Lỗi khi tải dữ liệu từ Firebase Realtime Database: " + ex.Message);
            }
        }
       
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

        public async void LoadInfoFromMaKH()
        {
            try
            {
                MessageBox.Show(maKhachhang);
                // Lấy thông tin khách hàng từ Firebase Realtime Database dựa trên mã khách hàng (maKH)
                var customerSnapshot = await firebaseClient
                    .Child("Customer/")
                    .OrderByKey()
                    .EqualTo(maKhachhang)
                    .OnceAsync<CustomerDAO>();

                // Kiểm tra xem có dữ liệu hay không
                if (customerSnapshot.Any())
                {
                    // Lấy thông tin khách hàng từ bản ghi
                    CustomerDAO customer = customerSnapshot.First().Object;

                    // Hiển thị thông tin khách hàng
                    tenKH.Text = customer.HOTEN;
                    diaChi.Text = customer.DIACHI;
                    cmnd.Text = customer.CCCD;
                    soDT.Text = customer.SDT;
                    CustomerType = customer.ID_LOAIKH.ToString();
                    quocTich.Text = customer.QUOCTICH; 


                }
                else
                {
                    // Nếu không tìm thấy dữ liệu, hiển thị thông báo
                    MessageBox.Show("Không tìm thấy thông tin khách hàng.");
                }
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ nếu có
                MessageBox.Show("Lỗi khi tải dữ liệu từ Firebase Realtime Database: " + ex.Message);
            }
        }

        private async void maHDBox_SelectedIndexChanged(object sender, EventArgs e)
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
                    string maNV1 = billSnapshot.First().Object.ID_NV;
                    nvLap.Text = maNV1;

                    ngLap.Text = billSnapshot.First().Object.NGLAP.ToString();
                    maKhachhang = billSnapshot.First().Object.ID_KH;
                    maPhong= billSnapshot.First().Object.MAPHONG.ToString();
                }
                LoadInfoFromMaKH();
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ nếu có
                MessageBox.Show("Lỗi khi tải dữ liệu từ Firebase Realtime Database: " + ex.Message);
            }
        }
    }
}
