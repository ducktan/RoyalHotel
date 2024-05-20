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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Royal
{
    public partial class CustomerType : Form
    {
        private Firebase.Database.FirebaseClient firebaseClient;
        public CustomerType()
        {
            InitializeComponent();
            // Khởi tạo FirebaseClient với chuỗi kết nối đến Firebase Realtime Database
            firebaseClient = FirebaseManage.GetFirebaseClient();
            CustomerTypeDAO a = new CustomerTypeDAO();
            a.LoadCustomerType(dataGridStaff);
            dataGridStaff.CellClick += dataGridBill_CellClick;
        }

        private async void kryptonButton1_Click(object sender, EventArgs e)
        {
            // Get the current row count for the "Bill" table
            var bills = await firebaseClient
                .Child("CustomerType")
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
                maHoaDon = $"LKH{formattedNumber}";

                // Check if the ID is unique
                isUnique = !bills.Any(b => (b.Object as dynamic).MAHD == maHoaDon);

                if (!isUnique)
                {
                    newNumber++;
                }
            } while (!isUnique);

            // Create a new Bill object with form control values
            CustomerTypeDAO newBill = new CustomerTypeDAO()
            {
                ID_LKH = maHoaDon,
                TEN_LKH = tenLKH.Text,
                DISCOUNT = Int32.Parse(giamgia.Text.Trim())
            };

            newBill.AddCustomerType(newBill);
        }

        private void kryptonButton3_Click(object sender, EventArgs e)
        {
            CustomerTypeDAO a = new CustomerTypeDAO();
            a.LoadCustomerType(dataGridStaff);
        }

        private void dataGridBill_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Kiểm tra xem có hàng nào được chọn không
            {
                DataGridViewRow selectedRow = dataGridStaff.Rows[e.RowIndex];

                // Kiểm tra nếu hàng không rỗng
                if (!selectedRow.IsNewRow)
                {
                    idLKH.Text = (string)selectedRow.Cells[0].Value;
                    tenLKH.Text = (string)selectedRow.Cells[1].Value; 
                    giamgia.Text = selectedRow.Cells[2].Value.ToString();
                }
            }
        }

        private void kryptonButton4_Click(object sender, EventArgs e)
        {
            string id = idLKH.Text;
            CustomerTypeDAO a = new CustomerTypeDAO(); 
            a.DeleteCustomerType(id);
        }

        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            string id = idLKH.Text; 
            string name = tenLKH.Text;
            int discount = Int32.Parse(giamgia.Text); 
            CustomerTypeDAO a = new CustomerTypeDAO();  
            a.UpdateCustomerType(id, name, discount);
        }

        private async void kryptonButton5_Click(object sender, EventArgs e)
        {
         
            string searchText1 = searchText.Text.Trim(); // Assuming txtSearch is a textbox for search text

            if (string.IsNullOrEmpty(searchText1))
            {
                MessageBox.Show("Please enter the search text.");
                return;
            }
            // Call the appropriate search function based on the selected type
            Royal.DAO.CustomerTypeDAO billFun = new Royal.DAO.CustomerTypeDAO(); // Assuming you have an instance

            // Call the appropriate search function based on the selected type
            List<Royal.DAO.CustomerTypeDAO> searchResults = new List<Royal.DAO.CustomerTypeDAO>(); // Initialize empty list


            try
            {
                
                

                    searchResults = await billFun.SearchCSTypeByID(searchText1);
               

                // Prepare UI results (assuming you want to display MALPH, TENLPH, SLNG, GIA)
                List<string[]> uiResults = searchResults.Select(bill => new string[] { bill.ID_LKH, bill.TEN_LKH, bill.DISCOUNT.ToString() }).ToList();

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
                    MessageBox.Show($"No customer type found with id");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error searching customer type: {ex.Message}");
            }
        }
    }
}
