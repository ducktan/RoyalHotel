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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Royal
{
    public partial class Parameter : Form
    {
        private Firebase.Database.FirebaseClient firebaseClient;
        private Authen authen;
        private Permission permission;
        public Parameter()
        {
            InitializeComponent();
            authen = new Authen();
            permission = new Permission();
            // Khởi tạo FirebaseClient với chuỗi kết nối đến Firebase Realtime Database
            firebaseClient = FirebaseManage.GetFirebaseClient();
            ParameterDAO demo = new ParameterDAO();
            demo.LoadPara(dataGridViewParameter);
            demo.LoadPara(dataGridView1);
            dataGridViewParameter.CellClick += dataGridBill_CellClick;
            dataGridView1.CellClick += dataGridBill_CellClick1;

            LoadMaNVFromDatabase();
        }

        private async void kryptonButton1_Click(object sender, EventArgs e)
        {

            if (permission.HasAccess(User.Role, "Giám Đốc") || permission.HasAccess(User.Role, "Quản lý"))
            {
                try
                {
                    // Get the current row count for the "Bill" table
                    var bills = await firebaseClient
                        .Child("Parameters")
                        .OnceAsync<Royal.DAO.ParameterDAO>();

                    // Tìm mã phòng lớn nhất hiện có
                    int maxRoomNumber = 0;
                    foreach (var roomData in bills)
                    {
                        int roomNumber = int.Parse(roomData.Object.pID.Substring(2));
                        if (roomNumber > maxRoomNumber)
                        {
                            maxRoomNumber = roomNumber;
                        }
                    }

                    string newRoomNumber = "NQ" + (maxRoomNumber + 1).ToString("D3");
                    ParameterDAO p1 = new ParameterDAO()
                    {
                        pID = newRoomNumber,
                        pName = tenP.Text,
                        pContent = content.Text,
                        pValue = Int32.Parse(value.Text)
                    };

                    // Thực hiện các thao tác với đối tượng p1 (ví dụ: lưu vào Firebase Realtime Database)
                    // firebaseClient.Child("Parameters").PostAsync(p1);

                    await p1.AddPara(p1);
                    p1.LoadPara(dataGridViewParameter);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Bạn không có quyền truy cập vào mục này!");
            }

        }

        private void dataGridViewParameter_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridBill_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Kiểm tra xem có hàng nào được chọn không
            {
                DataGridViewRow selectedRow = dataGridViewParameter.Rows[e.RowIndex];

                // Kiểm tra nếu hàng không rỗng
                if (!selectedRow.IsNewRow)
                {
                    maP.Text = (string)selectedRow.Cells[0].Value;
                    tenP.Text = (string)(selectedRow.Cells[1].Value);
                    value.Text = selectedRow.Cells[2].Value.ToString();
                    content.Text = (string)(selectedRow.Cells[3].Value);



                }
            }
        }
        private void dataGridBill_CellClick1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Kiểm tra xem có hàng nào được chọn không
            {
                DataGridViewRow selectedRow = dataGridViewParameter.Rows[e.RowIndex];

                // Kiểm tra nếu hàng không rỗng
                if (!selectedRow.IsNewRow)
                {
                    Va.Text = selectedRow.Cells[2].Value.ToString();



                }
            }
        }

        private void kryptonButton4_Click(object sender, EventArgs e)
        {
            ParameterDAO p = new ParameterDAO(); 
            p.LoadPara(dataGridViewParameter);
        }

        private async void kryptonButton2_Click(object sender, EventArgs e)
        {
            if (permission.HasAccess(User.Role, "Giám Đốc") || permission.HasAccess(User.Role, "Quản lý"))
            {
                string pIDD = maP.Text;
                ParameterDAO p2 = new ParameterDAO();
                await p2.DeletePara(pIDD);
                p2.LoadPara(dataGridViewParameter);
            }
            else
            {
                MessageBox.Show("Bạn không có quyền truy cập vào mục này!");
            }

            }

            private async void kryptonButton3_Click(object sender, EventArgs e)
        {
            if (permission.HasAccess(User.Role, "Giám Đốc") || permission.HasAccess(User.Role, "Quản lý"))
            {
                string pIDD = maP.Text; 
            string tenPP = tenP.Text;
            int valueP = Int32.Parse(value.Text);
            string motaP = content.Text;

            ParameterDAO p = new ParameterDAO();
            await p.UpdateParameter(pIDD, tenPP, valueP, motaP);
                p.LoadPara(dataGridViewParameter);
            }
            else
            {
                MessageBox.Show("Bạn không có quyền truy cập vào mục này!");
            }
        }

        private async void kryptonButton5_Click(object sender, EventArgs e)
        {
            // Get the search criteria from the UI controls
            string searchText1 = searchText.Text.Trim(); // Assuming txtSearch is a textbox for search text

            if (string.IsNullOrEmpty(searchText1))
            {
                MessageBox.Show("Please enter the search text.");
                return;
            }
            // Call the appropriate search function based on the selected type
            Royal.DAO.ParameterDAO billFun = new Royal.DAO.ParameterDAO(); // Assuming you have an instance

            // Call the appropriate search function based on the selected type
            List<Royal.DAO.ParameterDAO> searchResults = new List<Royal.DAO.ParameterDAO>(); // Initialize empty list


            try
            {                

                Royal.DAO.ParameterDAO searchResult = await billFun.SearchParaTypeById(searchText1);
                searchResults.Add(searchResult);
               

                // Prepare UI results (assuming you want to display MALPH, TENLPH, SLNG, GIA)
                List<string[]> uiResults = searchResults.Select(bill => new string[] { bill.pID, bill.pName, bill.pValue.ToString(), bill.pContent }).ToList();

                // Update UI elements on the UI thread
                if (this.InvokeRequired)
                {
                    this.Invoke(new Action(() =>
                    {
                        dataGridViewParameter.Rows.Clear();
                        foreach (string[] rowData in uiResults)
                        {
                            dataGridViewParameter.Rows.Add(rowData);
                        }
                    }));
                }
                else
                {
                    dataGridViewParameter.Rows.Clear();
                    foreach (string[] rowData in uiResults)
                    {
                        dataGridViewParameter.Rows.Add(rowData);
                    }
                }

                // Handle no search results (optional)
                if (searchResults.Count == 0)
                {
                    
                    MessageBox.Show($"No parameter found");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error searching parameter: {ex.Message}");
            }
        }
        public async void LoadMaNVFromDatabase()
        {
            try
            {
                // Truy vấn Firebase Realtime Database để lấy danh sách khách hàng
                var customerList = await firebaseClient
                    .Child("Staff")
                    .OnceAsync<StaffDAO>();

                // Xóa các mục hiện có trong ComboBox
                maNV.Items.Clear();

                // Thêm ID_KH từ các bản ghi khách hàng vào ComboBox
                foreach (var customer in customerList)
                {
                    string customerDisplayText = $"{customer.Object.StaffID}";
                    maNV.Items.Add(customerDisplayText);
                }
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ nếu có
                MessageBox.Show("Lỗi khi tải dữ liệu từ Firebase Realtime Database: " + ex.Message);
            }
        }

        public async void LoadInfoStaffFromID()
        {
            try
            {
                // Lấy danh sách chi tiết hóa đơn từ Firebase Realtime Database
                StaffDAO billDetails = await new StaffDAO().SearchStaffbyID(maNV.Text);


                // Kiểm tra xem có dữ liệu hay không
                if (billDetails != null)
                {
                  
                        Luong.Text = billDetails.luongNV?.staffSalary.ToString() ?? "0";
                        SoLanVP.Text = billDetails.luongNV?.countVP.ToString() ?? "0";
                        tenNV.Text = billDetails.staffName.ToString();
                    ngdilam.Text = billDetails.luongNV?.workingDay.ToString() ?? "0";
                    
                }
                else
                {
                    // Nếu không tìm thấy dữ liệu, hiển thị thông báo
                    MessageBox.Show("Không tìm thấy dữ liệu nhân viên.");
                }
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ nếu có
                MessageBox.Show("Lỗi khi tải dữ liệu từ Firebase Realtime Database: " + ex.Message);
            }
        }
        private void maNV_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadInfoStaffFromID();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (permission.HasAccess(User.Role, "Giám Đốc") || permission.HasAccess(User.Role, "Quản lý"))
            {
                ManageStaff s = new ManageStaff();
                s.Show();

            }
            else
            {
                MessageBox.Show("Bạn không có quyền truy cập vào mục này!");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            LoadMaNVFromDatabase();
            ParameterDAO p = new ParameterDAO();
            p.LoadPara(dataGridView1);
            ngdilam.Clear();
            maNV.Text = ""; 
            SoLanVP.Clear();
            Luong.Clear();
            tenNV.Clear();  
            MessageBox.Show("Refresh successfully!");
        }

        private StaffDAO currentStaff;
        private async void addBut_Click(object sender, EventArgs e)
        {
            if (permission.HasAccess(User.Role, "Giám Đốc") || permission.HasAccess(User.Role, "Quản lý"))
            {
                string maLNV;
                try
                {
                    // Lấy danh sách chi tiết hóa đơn từ Firebase Realtime Database
                    currentStaff = await new StaffDAO().SearchStaffbyID(maNV.Text);
                    maLNV = currentStaff.staffType.ToString();

                    StaffType a = await new StaffType().SearchSTypeById(maLNV);

                    // Kiểm tra xem có dữ liệu hay không
                    int value;
                    if (int.TryParse(Va.Text, out value))
                    {
                        // Kiểm tra nếu currentStaff.luongNV là null thì khởi tạo
                        if (currentStaff.luongNV == null)
                        {
                            currentStaff.luongNV = new Salary();
                            await currentStaff.luongNV.InitSalary(currentStaff.StaffID);
                        }

                        if (value == 1)
                        {
                            currentStaff.luongNV.workingDay++;
                            if (currentStaff.luongNV.workingDay > 26)
                            {
                                currentStaff.luongNV.staffSalary += a.stSalary;
                                MessageBox.Show("Đủ lương rồi nè!");
                            }
                        }
                        else
                        {
                            currentStaff.luongNV.staffSalary += value;
                            if (value < 0)
                            {
                                currentStaff.luongNV.countVP++;
                            }
                        }

                        // Cập nhật dữ liệu
                        await currentStaff.luongNV.UpdateSalary(currentStaff.StaffID, currentStaff.luongNV.staffSalary, currentStaff.luongNV.countVP, currentStaff.luongNV.workingDay);
                    }
                    LoadInfoStaffFromID();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi cập nhật dữ liệu: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Bạn không có quyền truy cập vào mục này!");
            }
        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();


            saveFileDialog.Filter = "PDF Files (*.pdf)|*.pdf";
            saveFileDialog.DefaultExt = "pdf";
            saveFileDialog.AddExtension = true;



            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                ExportToPdf.Export(dataGridViewParameter, saveFileDialog.FileName);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Dashboard d = new Dashboard();
            d.Show();
            this.Hide();
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }
    }
}
