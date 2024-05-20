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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Royal
{
    public partial class ManageStaff : Form
    {
        private Firebase.Database.FirebaseClient firebaseClient;
        public ManageStaff()
        {
            InitializeComponent();
            // Khởi tạo FirebaseClient với chuỗi kết nối đến Firebase Realtime Database
            firebaseClient = FirebaseManage.GetFirebaseClient();
            StaffDAO staff = new StaffDAO();
            staff.LoadStaff(dataGridStaff);
            dataGridStaff.CellClick += dataGridBill_CellClick;
        }

        private async void kryptonButton1_Click(object sender, EventArgs e)
        {
            // Get the current row count for the "Bill" table
            var bills = await firebaseClient
                .Child("Staff")
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
                maHoaDon = $"NV{formattedNumber}";

                // Check if the ID is unique
                isUnique = !bills.Any(b => (b.Object as dynamic).MAHD == maHoaDon);

                if (!isUnique)
                {
                    newNumber++;
                }
            } while (!isUnique);

            // Create a new Bill object with form control values
            StaffDAO newStaff = new StaffDAO()
            {
               StaffID = maHoaDon,
               staffName = name.Text,
               staffCCCD = idcc.Text, 
               staffType = loaiNV.Text, 
               staffPhone = soDT.Text,
               staffEmail = mail.Text, 
               staffBirth = dateBirth.Text,
               staffAdd = address.Text,
               staffGender = comboBoxSex.Text,
               staffDateIn = dateIn.Text
            };

            newStaff.AddStaff(newStaff);
        }

        private void dataGridBill_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Kiểm tra xem có hàng nào được chọn không
            {
                DataGridViewRow selectedRow = dataGridStaff.Rows[e.RowIndex];

                // Kiểm tra nếu hàng không rỗng
                if (!selectedRow.IsNewRow)
                {

                  



                    // Trích xuất dữ liệu từ hàng được chọn và hiển thị lên form
                    manv.Text = (string)selectedRow.Cells[0].Value;
                    name.Text = (string)selectedRow.Cells[1].Value;
                    idcc.Text = (string)selectedRow.Cells[2].Value;
                    loaiNV.Text = (string)selectedRow.Cells[3].Value;
                    soDT.Text = (string)selectedRow.Cells[4].Value;
                    mail.Text = (string)selectedRow.Cells[5].Value;
                    dateBirth.Text = (string)selectedRow.Cells[6].Value;
                    address.Text = (string)selectedRow.Cells[7].Value;
                    comboBoxSex.Text = (string)selectedRow.Cells[8].Value;
                    dateIn.Text = (string)selectedRow.Cells[9].Value;

                }
            }
        }

        private void kryptonButton4_Click(object sender, EventArgs e)
        {
            StaffDAO a = new StaffDAO();
            a.LoadStaff(dataGridStaff);
        }

        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            string id = manv.Text;
            StaffDAO a = new StaffDAO(); 
            a.DeleteStaff(id);
        }

        private void kryptonButton3_Click(object sender, EventArgs e)
        {
            StaffDAO a =new StaffDAO(); 
            a.UpdateStaff(manv.Text, name.Text, idcc.Text, loaiNV.Text, soDT.Text, mail.Text, dateBirth.Value.ToString(), address.Text, comboBoxSex.Text, dateIn.Value.ToString());
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
            Royal.DAO.StaffDAO billFun = new Royal.DAO.StaffDAO(); // Assuming you have an instance

            // Call the appropriate search function based on the selected type
            List<Royal.DAO.StaffDAO> searchResults = new List<Royal.DAO.StaffDAO>(); // Initialize empty list


            try
            {
                searchResults = await billFun.SearchStaffbyIDStaff(searchText1);

                // Prepare UI results (assuming you want to display MALPH, TENLPH, SLNG, GIA)
                List<string[]> uiResults = searchResults.Select(bill => new string[] { bill.StaffID, bill.staffName, bill.staffCCCD, bill.staffType, bill.staffPhone, bill.staffEmail, bill.staffBirth, bill.staffAdd, bill.staffGender, bill.staffDateIn }).ToList();

                // Update UI elements on the UI thread
                if (this.InvokeRequired)
                {
                    this.Invoke(new Action(() =>
                    {
                        dataGridStaff.Rows.Clear();
                        foreach (string[] rowData in uiResults)
                        {
                            dataGridStaff.Rows.Add(rowData);
                        }
                    }));
                }
                else
                {
                    dataGridStaff.Rows.Clear();
                    foreach (string[] rowData in uiResults)
                    {
                        dataGridStaff.Rows.Add(rowData);
                    }
                }

                // Handle no search results (optional)
                if (searchResults.Count == 0)
                {
                    MessageBox.Show("No staff");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error searching staff: {ex.Message}");
            }
        }
    }
}
