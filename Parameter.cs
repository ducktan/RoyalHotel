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
        public Parameter()
        {
            InitializeComponent();
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
            // Get the current row count for the "Bill" table
            var bills = await firebaseClient
                .Child("Parameters")
                .OnceAsync<object>();

            // Increment by 1 to get the new sequential number
            int newNumber = bills.Count + 1;
            string maHoaDon;
            bool isUnique;

            do
            {
                // Format the number with leading zeros (001, 002, ...)
                string formattedNumber = newNumber.ToString("D3");

                // Create the MAHD with your preferred prefix (e.g., "HD")
                maHoaDon = $"NQ{formattedNumber}";

                // Check if the ID is unique
                isUnique = !bills.Any(b => (b.Object as dynamic).MAHD == maHoaDon);

                if (!isUnique)
                {
                    newNumber++;
                }
            } while (!isUnique);

            ParameterDAO p1 = new ParameterDAO()
            {
                pID = maHoaDon,
                pName = tenP.Text,
                pContent = content.Text,
                pValue = Int32.Parse(value.Text)
            };

            // Thực hiện các thao tác với đối tượng p1 (ví dụ: lưu vào Firebase Realtime Database)
            // firebaseClient.Child("Parameters").PostAsync(p1);

            p1.AddPara(p1);
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

        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            string pIDD = maP.Text;
            ParameterDAO p2 = new ParameterDAO();
            p2.DeletePara(pIDD);
        }

        private void kryptonButton3_Click(object sender, EventArgs e)
        {
            string pIDD = maP.Text; 
            string tenPP = tenP.Text;
            int valueP = Int32.Parse(value.Text);
            string motaP = content.Text;

            ParameterDAO p = new ParameterDAO();
            p.UpdateParameter(pIDD, tenPP, valueP, motaP);
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
        private async void addBut_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy danh sách chi tiết hóa đơn từ Firebase Realtime Database
                List<StaffDAO> billDetails = await new StaffDAO().SearchStaffbyIDStaff(maNV.Text);


                // Kiểm tra xem có dữ liệu hay không
                if (billDetails.Count > 0)
                {
                    // Thêm dữ liệu chi tiết hóa đơn vào DataGridView
                    foreach (var item in billDetails)
                    {
                        int value = Int32.Parse(Va.Text);
                        item.staffSalary += value;
                        if (value < 0)
                        {
                            item.countVP++; 
                           
                        }
                        item.UpdateSalary(item.StaffID, item.staffSalary, item.countVP);

                    }
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
        public async void LoadInfoStaffFromID()
        {
            try
            {
                // Lấy danh sách chi tiết hóa đơn từ Firebase Realtime Database
                List<StaffDAO> billDetails = await new StaffDAO().SearchStaffbyIDStaff(maNV.Text);
            

                // Kiểm tra xem có dữ liệu hay không
                if (billDetails.Count > 0)
                {
                    // Thêm dữ liệu chi tiết hóa đơn vào DataGridView
                    foreach (var item in billDetails)
                    {
                        Luong.Text = item.staffSalary.ToString() ?? "0";
                        SoLanVP.Text = item.countVP.ToString() ?? "0";
                        tenNV.Text = item.staffName.ToString();
                    }
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
            ManageStaff s = new ManageStaff();
            s.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            LoadInfoStaffFromID();
            MessageBox.Show("Refresh successfully!");
        }
    }
}
