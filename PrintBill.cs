using Firebase.Database.Query;
using FireSharp.Response;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Royal.DAO;
//using Syncfusion.Pdf.Graphics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;// Assuming iTextSharp library


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

        public async void LoadInfoRoom(string id)
        {
            Room room = await new Room().SearchRoomById(id);
            tenPhong.Text = room.TenPhong;

            DAO.RoomType roomType = await new DAO.RoomType().SearchRoomTypeById(room.LoaiPhong);
            loaiPhong.Text = roomType.TENLPH;
            dongiaPhong.Text = roomType.GIA.ToString();
        }

        public async void LoadBookInfoFromRoom(string id)
        {
            try
            {
                bookingroomDAO b = await new bookingroomDAO().SearchBookRoombyIDPhong(id);
                ngayDen.Text = b.NGAYDAT;
                soNguoi.Text = b.SONGUOI.ToString();

                // Parse the check-in and check-out dates
                DateTime checkIn = DateTime.Parse(b.NGAYDAT);
                DateTime checkOut = DateTime.Parse(b.NGAYTRA);

                // Calculate the number of nights
                TimeSpan nights = checkOut - checkIn;
                soDem.Text = nights.Days.ToString();

            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ nếu có
                MessageBox.Show("Lỗi khi tải dữ liệu từ Firebase Realtime Database: " + ex.Message);
            }

        }

        public async void LoadFullData()
        {
            BillDetailDAO billDetailDAO = new BillDetailDAO();
            List<BillDetailDAO> listBillDetail = await billDetailDAO.SearchBillDetailByMaHD(maHDBox.Text);
            int sum = 0;
            if (listBillDetail.Count > 0)
            {

                foreach (var item in listBillDetail)
                {
                    BillDetailDAO bill1 = new BillDetailDAO();
                    bill1 = item;
                    sum += bill1.THANHTIEN;

                }

            }
            tienDV.Text = sum.ToString();
           
            //-------------------------------------

            BillDAO bill = new BillDAO();
            BillDAO res = await bill.SearchBillTypeById(maHDBox.Text);
            int tongtien = res.THANHTIEN + sum;
            tienPhong.Text = res.DONGIA.ToString();
            thanhTien.Text = tongtien.ToString();



        }


        //private void kryptonButton5_Click(object sender, EventArgs e)
        //{
        //    //try
        //    //{
        //    //    /*
        //    //    // Parse the date from the TextBox
        //    //    string dateFormat = "dddd, MMMM dd, yyyy";
        //    //    if (!DateTime.TryParseExact(ngLap.Text, dateFormat, null, System.Globalization.DateTimeStyles.None, out DateTime selectedDate))
        //    //    {
        //    //        MessageBox.Show("Invalid date format. Please use 'dddd, MMMM dd, yyyy'.");
        //    //        return;
        //    //    }

        //    //    int month = selectedDate.Month;
        //    //    int year = selectedDate.Year;

        //    //    string roomType = loaiPhong.Text;


        //    //    if (!int.TryParse(thanhTien.Text, out int totalRevenue))
        //    //    {
        //    //        MessageBox.Show("Invalid total revenue");
        //    //        return;
        //    //    }

        //    //    ReportDAO report = new ReportDAO
        //    //    {
        //    //        Month = month,
        //    //        Year = year,
        //    //        RoomType = roomType,

        //    //        TotalRevenue = totalRevenue
        //    //    };

        //    //    // Create an instance of ReportDAO to access the AddReport method
        //    //    ReportDAO reportDAO = new ReportDAO();

        //    //    // Call the AddReport method
        //    //    reportDAO.AddReport(report);*/




        //    //    // ... (other code)

        //    //    // Create a PDF document
        //    //    Document document = new Document(PageSize.A5); // Adjust page size as needed

        //    //    // Create a PdfWriter instance (specifies output file)
        //    //    PdfWriter writer = PdfWriter.GetInstance(document, new FileStream("HoaDon.pdf", FileMode.Create));

        //    //    document.Open(); // Open the document

        //    //    // Create a paragraph and add content
        //    //    Paragraph paragraph = new Paragraph("THE 10 ROYAL HOTEL");
        //    //    paragraph.Alignment = Element.ALIGN_CENTER; // Center align
        //    //    document.Add(paragraph);

        //    //    paragraph = new Paragraph("▶ Address: Ben Nghe Ward, District 1, Ho Chi Minh City.");
        //    //    paragraph.SpacingBefore = 5f; // Add spacing
        //    //    document.Add(paragraph);

        //    //    paragraph = new Paragraph("▶ Information bill: ");
        //    //    paragraph.SpacingBefore = 5f; // Add spacing
        //    //    document.Add(paragraph);

        //    //    paragraph = new Paragraph("1. Bill ID: " + maHDBox.Text);
        //    //    paragraph.SpacingBefore = 5f; // Add spacing
        //    //    document.Add(paragraph);
        //    //    paragraph = new Paragraph("2. Date to create: " + ngLap.Text);
        //    //    paragraph.SpacingBefore = 5f; // Add spacing
        //    //    document.Add(paragraph);
        //    //    paragraph = new Paragraph("3. Staff: " + nvLap.Text);
        //    //    paragraph.SpacingBefore = 5f; // Add spacing
        //    //    document.Add(paragraph);
        //    //    paragraph = new Paragraph("4. INFORMATION OF CUSTOMER:");
        //    //    paragraph.SpacingBefore = 5f; // Add spacing
        //    //    document.Add(paragraph);
        //    //    paragraph = new Paragraph("- Name: " + tenKH.Text);
        //    //    paragraph.SpacingBefore = 5f; // Add spacing
        //    //    document.Add(paragraph);
        //    //    paragraph = new Paragraph("- CCCD: " + cmnd.Text);
        //    //    paragraph.SpacingBefore = 5f; // Add spacing
        //    //    document.Add(paragraph);

        //    //    paragraph = new Paragraph("- Customer Type: " + LoaiKH.Text);
        //    //    paragraph.SpacingBefore = 5f; // Add spacing
        //    //    document.Add(paragraph);

        //    //    paragraph = new Paragraph("- Address: " + diaChi.Text);
        //    //    paragraph.SpacingBefore = 5f; // Add spacing
        //    //    document.Add(paragraph);

        //    //    paragraph = new Paragraph("- Nationality: " + quocTich.Text);
        //    //    paragraph.SpacingBefore = 5f; // Add spacing
        //    //    document.Add(paragraph);

        //    //    paragraph = new Paragraph("5. INFORMATION OF ROOM: ");
        //    //    paragraph.SpacingBefore = 5f; // Add spacing
        //    //    document.Add(paragraph);

        //    //    paragraph = new Paragraph("- Room name: " + tenPhong.Text);
        //    //    paragraph.SpacingBefore = 10f; // Add spacing
        //    //    document.Add(paragraph);

        //    //    paragraph = new Paragraph("- Room type: " + loaiPhong.Text);
        //    //    paragraph.SpacingBefore = 5f; // Add spacing
        //    //    document.Add(paragraph);

        //    //    paragraph = new Paragraph("- Prices: " + dongiaPhong.Text);
        //    //    paragraph.SpacingBefore = 5f; // Add spacing
        //    //    document.Add(paragraph);

        //    //    paragraph = new Paragraph("- Date to check in: " + ngayDen.Text);
        //    //    paragraph.SpacingBefore = 5f; // Add spacing
        //    //    document.Add(paragraph);

        //    //    paragraph = new Paragraph("- Nights: " + soDem.Text);
        //    //    paragraph.SpacingBefore = 5f; // Add spacing
        //    //    document.Add(paragraph);

        //    //    paragraph = new Paragraph("- Numbers of people: " + soNguoi.Text);
        //    //    paragraph.SpacingBefore = 5f; // Add spacing
        //    //    document.Add(paragraph);

        //    //    paragraph = new Paragraph("6. INFORMATION OF SERVICES: ");
        //    //    paragraph.SpacingBefore = 20f; // Add spacing
        //    //    document.Add(paragraph);
        //    //    // fix sau

        //    //    paragraph = new Paragraph("7. TOTAL: ");
        //    //    paragraph.SpacingBefore = 5f; // Add spacing
        //    //    document.Add(paragraph);

        //    //    paragraph = new Paragraph("==> Room fee: " + tienPhong.Text);
        //    //    paragraph.SpacingBefore = 5f; // Add spacing
        //    //    document.Add(paragraph);
        //    //    paragraph = new Paragraph("==> Services fee: " + tienDV.Text);
        //    //    paragraph.SpacingBefore = 5f; // Add spacing
        //    //    document.Add(paragraph);
        //    //    paragraph = new Paragraph("==> Discount: " + giamGia.Text);
        //    //    paragraph.SpacingBefore = 5f; // Add spacing
        //    //    document.Add(paragraph);
        //    //    paragraph = new Paragraph("==> All fees: " + thanhTien.Text);
        //    //    paragraph.SpacingBefore = 5f; // Add spacing
        //    //    document.Add(paragraph);







        //    //    document.Close(); // Close the document

        //    //    // Open the PDF using an external program (optional)
        //    //    System.Diagnostics.Process.Start("HoaDon.pdf");
        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    MessageBox.Show($"An error occurred: {ex.Message}");
        //    //}
        //}

        private void kryptonButton6_Click(object sender, EventArgs e)
        {
            this.Close();


        }

        private void kryptonButton5_Click(object sender, EventArgs e)
        {
            try
            {
                /*
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
                reportDAO.AddReport(report);*/




                // ... (other code)

               
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        private void kryptonButton6_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void kryptonButton5_Click_1(object sender, EventArgs e)
        {
            try
            {
                /*
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
                reportDAO.AddReport(report);*/


                SaveFileDialog saveFileDialog = new SaveFileDialog();


                saveFileDialog.Filter = "PDF Files (*.pdf)|*.pdf";
                saveFileDialog.DefaultExt = "pdf";
                saveFileDialog.AddExtension = true;



                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    ExportToPdf.ExportLine(dataGridViewDV, saveFileDialog.FileName, maHDBox.Text, ngLap.Text, nvLap.Text, tenKH.Text, cmnd.Text, LoaiKH.Text, diaChi.Text, quocTich.Text, tenPhong.Text, loaiPhong.Text, dongiaPhong.Text, ngayDen.Text, soDem.Text, soNguoi.Text, tienPhong.Text, tienDV.Text, giamGia.Text, thanhTien.Text);
                }



            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
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
                    string maNV1 = billSnapshot.First().Object.ID_NV;
                    nvLap.Text = maNV1;

                    ngLap.Text = billSnapshot.First().Object.NGLAP.ToString();
                    maKhachhang = billSnapshot.First().Object.ID_KH.ToString().Trim();
                    maPhong = billSnapshot.First().Object.MAPHONG.ToString();
                }

                LoadInfoFromMaKH(maKhachhang);
                LoadCTHDFromBill(maHDBox.Text);

                BillDAO test = await new BillDAO().SearchBillTypeById(maHDBox.Text);
                LoadInfoRoom(test.MAPHONG);
                LoadBookInfoFromRoom(test.MAPHONG);
                giamGia.Text = test.DISCOUNT.ToString();
                LoadFullData();






            }



            catch (Exception ex)
            {
                // Xử lý ngoại lệ nếu có
                MessageBox.Show("Lỗi khi tải dữ liệu từ Firebase Realtime Database: " + ex.Message);
            }

        }
    }
}
