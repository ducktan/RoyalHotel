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
        Permission permission;
        public Service()
        {
            InitializeComponent();
            // Khởi tạo FirebaseClient với chuỗi kết nối đến Firebase Realtime Database
            firebaseClient = FirebaseManage.GetFirebaseClient();
            ServiceDAO newCus = new ServiceDAO();
            newCus.LoadService(dataGridViewService);
            dataGridViewService.CellClick += dataGridBill_CellClick;
            permission = new Permission();

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

        

     

        private void kryptonButton5_Click_1(object sender, EventArgs e)
        {
            ServiceDAO s = new ServiceDAO();
            s.LoadService(dataGridViewService);
        }

        private async void kryptonButton4_Click_1(object sender, EventArgs e)
        {
            if(permission.HasAccess(User.Role, ""))
            {
                string id = maDVBox.Text.Trim();
                string name = tenDVbox.Text;
                int pri = Int32.Parse(giaDVBox.Text);
                string de = motaBox.Text;

                ServiceDAO s = new ServiceDAO();
                await s.UpdateService(id, name, pri, de);
                s.LoadService(dataGridViewService);
            }
            else
            {
                MessageBox.Show("Bạn không có quyền truy cập vào mục này");
            }

        }

        private async void kryptonButton2_Click_1(object sender, EventArgs e)
        {
            if(permission.HasAccess(User.Role, ""))
            {
                try
                {
                    var bills = await firebaseClient
                    .Child("Service")
                    .OnceAsync<Royal.DAO.ServiceDAO>();

                    // Increment by 1 to get the new sequential number
                    // Tìm mã phòng lớn nhất hiện có
                    int maxRoomNumber = 0;
                    foreach (var roomData in bills)
                    {
                        int roomNumber = int.Parse(roomData.Object.seID.Substring(2));
                        if (roomNumber > maxRoomNumber)
                        {
                            maxRoomNumber = roomNumber;
                        }
                    }

                    string newRoomNumber = "DV" + (maxRoomNumber + 1).ToString("D3");


                    // Create a new CustomerDAO object with form control values
                    ServiceDAO service = new ServiceDAO()
                    {
                        seID = newRoomNumber,
                        seName = tenDVbox.Text,
                        sePrice = Int32.Parse(giaDVBox.Text),
                        seDetail = motaBox.Text
                    };

                    // Call the AddCustomer method to store the customer object in the Firebase database
                   await service.AddService(service);
                    service.LoadService(dataGridViewService);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }


            }
            // Get the current row count for the "Bill" table
            else
            {
                MessageBox.Show("Bạn không có quyền truy cập vào mục này");
            }

        }

        private async void kryptonButton3_Click_1(object sender, EventArgs e)
        {
            if(permission.HasAccess(User.Role,""))
            {
                string id = maDVBox.Text;
                ServiceDAO s = new ServiceDAO();
                await s.DeleteSeervice(id);
                s.LoadService(dataGridViewService);
            }
            else
            {
                MessageBox.Show("Bạn không có quyền truy cập vào mục này");

            }
        }

        private async void kryptonButton1_Click_1(object sender, EventArgs e)
        {
            string searchText1 = search.Text.Trim();
            int price = Int32.Parse(searchText1);
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

                searchResults = await billFun.SearchRoomTypeByPrice(price);


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

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();


            saveFileDialog.Filter = "PDF Files (*.pdf)|*.pdf";
            saveFileDialog.DefaultExt = "pdf";
            saveFileDialog.AddExtension = true;



            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                ExportToPdf.Export(dataGridViewService, saveFileDialog.FileName);
            }
        }
    }
}
