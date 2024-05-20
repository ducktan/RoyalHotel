using Royal.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Royal
{
    public partial class ManageStaffType : Form
    {
        private Firebase.Database.FirebaseClient firebaseClient;
        public ManageStaffType()
        {
            InitializeComponent();
            // Khởi tạo FirebaseClient với chuỗi kết nối đến Firebase Realtime Database
            firebaseClient = FirebaseManage.GetFirebaseClient();
            StaffType st = new StaffType();
            st.LoadStaffType(dataGridStaffType);
            dataGridStaffType.CellClick += dataGridBill_CellClick;
        }

        private void dataGridStaff_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private async void kryptonButton1_Click(object sender, EventArgs e)
        {
            // Get the current row count for the "Bill" table
            var bills = await firebaseClient
                .Child("StaffType")
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
                maHoaDon = $"LNV{formattedNumber}";

                // Check if the ID is unique
                isUnique = !bills.Any(b => (b.Object as dynamic).MAHD == maHoaDon);

                if (!isUnique)
                {
                    newNumber++;
                }
            } while (!isUnique);


            StaffType newst = new StaffType()
            {
                stID = maHoaDon, 
                stName = nameBox.Text,
                number = Int32.Parse(numBox.Text.Trim()),
                stSalary = Int32.Parse(salaryBox.Text.Trim()) 
            };

            newst.AddStaffType(newst);
        }

        private void dataGridBill_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Kiểm tra xem có hàng nào được chọn không
            {
                DataGridViewRow selectedRow = dataGridStaffType.Rows[e.RowIndex];

                // Kiểm tra nếu hàng không rỗng
                if (!selectedRow.IsNewRow)
                {

                    // Trích xuất dữ liệu từ hàng được chọn và hiển thị lên form
                    maLNVBox.Text = (string)selectedRow.Cells[0].Value;
                    nameBox.Text = (string)selectedRow.Cells[1].Value;
                    numBox.Text = selectedRow.Cells[2].Value.ToString();
                    salaryBox.Text = selectedRow.Cells[3].Value.ToString();

                }
            }
        }

        private void kryptonButton4_Click(object sender, EventArgs e)
        {
            StaffType st = new StaffType();
            st.LoadStaffType(dataGridStaffType);
        }

        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            string id = maLNVBox.Text;
            StaffType a = new StaffType(); 
            a.DeleteStaffType(id);
        }

        private void kryptonButton3_Click(object sender, EventArgs e)
        {
            string id = maLNVBox.Text.ToUpper();
            string name = nameBox.Text; 
            int num = Int32.Parse(numBox.Text);
            int salary = Int32.Parse(salaryBox.Text);

            StaffType a = new StaffType(); 
            a.UpdateStaffType(id, name, num, salary);   
        }

        private async void kryptonButton5_Click(object sender, EventArgs e)
        {
           
        // Get the search criteria from the UI controls
        string searchText1 = search.Text.Trim(); // Assuming txtSearch is a textbox for search text

        if (string.IsNullOrEmpty(searchText1))
        {
            MessageBox.Show("Please enter the search text.");
            return;
        }
        // Call the appropriate search function based on the selected type
        Royal.DAO.StaffType billFun = new Royal.DAO.StaffType(); // Assuming you have an instance

        // Call the appropriate search function based on the selected type
        List<Royal.DAO.StaffType> searchResults = new List<Royal.DAO.StaffType>(); // Initialize empty list


        try
        {

            Royal.DAO.StaffType searchResult = await billFun.SearchSTypeById(searchText1);
            searchResults.Add(searchResult);


            // Prepare UI results (assuming you want to display MALPH, TENLPH, SLNG, GIA)
            List<string[]> uiResults = searchResults.Select(bill => new string[] { bill.stID, bill.stName, bill.number.ToString(), bill.stSalary.ToString() }).ToList();

            // Update UI elements on the UI thread
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() =>
                {
                    dataGridStaffType.Rows.Clear();
                    foreach (string[] rowData in uiResults)
                    {
                        dataGridStaffType.Rows.Add(rowData);
                    }
                }));
            }
            else
            {
                    dataGridStaffType.Rows.Clear();
                foreach (string[] rowData in uiResults)
                {
                        dataGridStaffType.Rows.Add(rowData);
                }
            }

            // Handle no search results (optional)
            if (searchResults.Count == 0)
            {

                MessageBox.Show($"No staff type found");
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error searching staff type: {ex.Message}");
        }
            
        }
    }
}
