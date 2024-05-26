using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;
using Royal.DAO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Royal
{
    public partial class Service : Form
    {
        private Firebase.Database.FirebaseClient firebaseClient;
        public Service()
        {
            InitializeComponent();
            // Khởi tạo FirebaseClient với chuỗi kết nối đến Firebase Realtime Database
            firebaseClient = FirebaseManage.GetFirebaseClient();
            ServiceDAO newCus = new ServiceDAO();
            newCus.LoadService(dataGridViewService);
            dataGridViewService.CellClick += dataGridBill_CellClick;

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private async void kryptonButton2_Click(object sender, EventArgs e)
        {
            // Get the current row count for the "Customer" table
            int currentRowCount = await firebaseClient
                .Child("Service")
                .OnceAsync<object>()
                .ContinueWith(task => task.Result.Count);

            // Increment by 1 to get the new sequential number
            int newNumber = currentRowCount + 1;

            // Format the number with leading zeros (00001, 00002, ...)
            string formattedNumber = newNumber.ToString("D3"); // Adjust "D5" for desired number of digits

            // Create the customer ID with the prefix "KH"
            string serviceID = $"DV{formattedNumber}";
            

            // Create a new CustomerDAO object with form control values
            ServiceDAO service = new ServiceDAO()
            {
                seID = serviceID,
                seName = tenDVbox.Text,
                sePrice = Int32.Parse(giaDVBox.Text), 
                seDetail = motaBox.Text
            };

            // Call the AddCustomer method to store the customer object in the Firebase database
            service.AddService(service);
        }

        private void dataGridBill_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Kiểm tra xem có hàng nào được chọn không
            {
                DataGridViewRow selectedRow = dataGridViewService.Rows[e.RowIndex];

                // Kiểm tra nếu hàng không rỗng
                if (!selectedRow.IsNewRow)
                {
                    maDVBox.Text = (string)selectedRow.Cells[0].Value;
                    tenDVbox.Text = (string)selectedRow.Cells[1].Value; 
                    giaDVBox.Text = selectedRow.Cells[2].Value.ToString();
                    motaBox.Text = (string)selectedRow.Cells[3].Value;

                }
            }
        }

        private async void kryptonButton1_Click(object sender, EventArgs e)
        {
            string searchText1 = search.Text.Trim();
            // Get the search criteria from the UI controls


            if (string.IsNullOrEmpty(searchText1))
            {
                MessageBox.Show("Please enter the search text.");
                return;
            }
            // Call the appropriate search function based on the selected type
            Royal.DAO.ServiceDAO billFun = new Royal.DAO.ServiceDAO(); // Assuming you have an instance

            // Call the appropriate search function based on the selected type
            List<Royal.DAO.ServiceDAO> searchResults = new List<Royal.DAO.ServiceDAO>(); // Initialize empty list


            try
            {

                searchResults = await billFun.SearchSeTypeById(searchText1);


                // Prepare UI results (assuming you want to display MALPH, TENLPH, SLNG, GIA)
                List<string[]> uiResults = searchResults.Select(bill => new string[] { bill.seID, bill.seName, bill.sePrice.ToString(), bill.seDetail }).ToList();

                // Update UI elements on the UI thread
                if (this.InvokeRequired)
                {
                    this.Invoke(new Action(() =>
                    {
                        dataGridViewService.Rows.Clear();
                        foreach (string[] rowData in uiResults)
                        {
                            dataGridViewService.Rows.Add(rowData);
                        }
                    }));
                }
                else
                {
                    dataGridViewService.Rows.Clear();
                    foreach (string[] rowData in uiResults)
                    {
                        dataGridViewService.Rows.Add(rowData);
                    }
                }

                // Handle no search results (optional)
                if (searchResults.Count == 0)
                {

                    MessageBox.Show($"No service found");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error searching service: {ex.Message}");
            }
        }

        private void kryptonButton3_Click(object sender, EventArgs e)
        {
            string id = maDVBox.Text;
            ServiceDAO s = new ServiceDAO();
            s.DeleteSeervice(id);
        }

        private void kryptonButton4_Click(object sender, EventArgs e)
        {
            string id = maDVBox.Text.Trim();
            string name = tenDVbox.Text; 
            int pri = Int32.Parse(giaDVBox.Text);
            string de = motaBox.Text;

            ServiceDAO s = new ServiceDAO(); 
            s.UpdateService(id, name, pri, de);
        }

        private void kryptonButton5_Click(object sender, EventArgs e)
        {
            ServiceDAO s = new ServiceDAO();
            s.LoadService(dataGridViewService);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {

        }
    }
}
