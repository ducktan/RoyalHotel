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



        private async void LoadCTHDFromBill(string billID)
        {
            try
            {
                // Lấy danh sách chi tiết hóa đơn từ Firebase Realtime Database
                List<BillDetailDAO> billDetails = await new BillDetailDAO().SearchBillDetailByMaHD(billID);
                dataGridViewDV.Rows.Clear();
                // Kiểm tra xem có dữ liệu hay không
                if (billDetails.Count > 0)
                {
                    // Xóa dữ liệu hiện có trong DataGridView (tùy chọn)
                    

                    // Thêm dữ liệu chi tiết hóa đơn vào DataGridView
                    foreach (var item in billDetails)
                    {
                        ServiceDAO s = new ServiceDAO();
                        List<ServiceDAO> serviceList = await s.SearchSeTypeById(item.MADV);

                        // Lấy đối tượng ServiceDAO đầu tiên trong danh sách
                        ServiceDAO newItem = serviceList.FirstOrDefault();

                        dataGridViewDV.Rows.Add(
                            item.MACTHD,
                            newItem?.seName ?? "N/A",
                            newItem?.sePrice ?? 0,
                            item.SLG_DV
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


        public async void LoadInfoFromMaKH(string makh)
        {
            try
            {
               


                CustomerDAO result = new CustomerDAO();
                CustomerDAO customer = await result.SearchRoomById(makh);


                tenKH.Text = customer.HOTEN;
                diaChi.Text = customer.DIACHI;
                cmnd.Text = customer.CCCD;
                soDT.Text = customer.SDT;
                
              
                DAO.CustomerType customerType = new DAO.CustomerType();
                DAO.CustomerType result1 = await customerType.SearchRoomById("LKH01");
                //MessageBox.Show(result1.ToString());


                //LoaiKH.Text = result1.TEN_LKH;
                LoaiKH.Text = result1.TEN_LOAIKH;
                //MessageBox.Show(result1.TEN_LKH);
                quocTich.Text = customer.QUOCTICH;

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
                    maKhachhang = billSnapshot.First().Object.ID_KH.ToString().Trim();
                    maPhong= billSnapshot.First().Object.MAPHONG.ToString();
                }
               
                LoadInfoFromMaKH(maKhachhang);
                LoadCTHDFromBill(maHDBox.Text);
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ nếu có
                MessageBox.Show("Lỗi khi tải dữ liệu từ Firebase Realtime Database: " + ex.Message);
            }
        }

        private void kryptonButton5_Click(object sender, EventArgs e)
        {
            try
            {
                // Parse the date from the TextBox
                string dateFormat = "dddd, MMMM dd, yyyy";
                if (!DateTime.TryParseExact(ngLap.Text, dateFormat, null, System.Globalization.DateTimeStyles.None, out DateTime selectedDate))
                {
                    MessageBox.Show("Invalid date format. Please use 'dddd, MMMM dd, yyyy'.");
                    return;
                }

                int month = selectedDate.Month;
                int year = selectedDate.Year;

                string roomType = loaiPhong.Text;


                if (!int.TryParse(thanhTien.Text, out int totalRevenue))
                {
                    MessageBox.Show("Invalid total revenue");
                    return;
                }

                ReportDAO report = new ReportDAO
                {
                    Month = month,
                    Year = year,
                    RoomType = roomType,

                    TotalRevenue = totalRevenue
                };

                // Create an instance of ReportDAO to access the AddReport method
                ReportDAO reportDAO = new ReportDAO();

                // Call the AddReport method
                reportDAO.AddReport(report);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        private void kryptonButton6_Click(object sender, EventArgs e)
        {
            this.Close();
            
           
        }
    }
}
