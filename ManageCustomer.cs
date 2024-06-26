﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Royal.DAO;

using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Royal
{
    public partial class ManageCustomer : Form
    {
        private Firebase.Database.FirebaseClient firebaseClient;
        public ManageCustomer()
        {
            InitializeComponent();
            // Khởi tạo FirebaseClient với chuỗi kết nối đến Firebase Realtime Database
            firebaseClient = FirebaseManage.GetFirebaseClient();
            CustomerDAO newCus = new CustomerDAO();
            newCus.LoadCustomer(dataGridStaff);
            dataGridStaff.CellClick += dataGridBill_CellClick;

        }

        private void comboBoxCustomerType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void kryptonRichTextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void ManageCustomer_Load(object sender, EventArgs e)
        {

        }

        private void kryptonButton6_Click(object sender, EventArgs e)
        {
            CustomerType customer = new CustomerType();
            customer.Show();
        }

        private async void kryptonButton1_Click(object sender, EventArgs e)
        {
            // Get the current row count for the "Bill" table
            var bills = await firebaseClient
                .Child("Customer")
                .OnceAsync<Royal.DAO.CustomerDAO>();

            // Tìm mã phòng lớn nhất hiện có
            int maxRoomNumber = 0;
            foreach (var roomData in bills)
            {
                int roomNumber = int.Parse(roomData.Object.MAKH.Substring(2));
                if (roomNumber > maxRoomNumber)
                {
                    maxRoomNumber = roomNumber;
                }
            }

            string newRoomNumber = "KH" + (maxRoomNumber + 1).ToString("D3");


            // Create a new CustomerDAO object with form control values
            CustomerDAO newCustomer = new CustomerDAO()
            {
                MAKH = newRoomNumber,
                HOTEN = hoTen.Text,
                CCCD = cccd.Text,
                ID_LOAIKH = LoaiKH.Text,
                SDT = sdt.Text,
                GIOITINH = GT.Text,
               
                NGSINH = Ngsinh.Text,
                DIACHI = diaChi.Text,
                QUOCTICH = QuocTich.Text,
                EMAIL = Email.Text
            };

            // Call the AddCustomer method to store the customer object in the Firebase database
            await newCustomer.AddCustomer(newCustomer);
            newCustomer.LoadCustomer(dataGridStaff);
        }

        private async void kryptonButton5_Click(object sender, EventArgs e)
        {
            string type = cboTypeSearch.Text;

            string searchText1 = kryptonRichTextBox4.Text.Trim();
            // Get the search criteria from the UI controls


            if (string.IsNullOrEmpty(searchText1))
            {
                MessageBox.Show("Please enter the search text.");
                return;
            }
            // Call the appropriate search function based on the selected type
            Royal.DAO.CustomerDAO billFun = new Royal.DAO.CustomerDAO(); // Assuming you have an instance

            // Call the appropriate search function based on the selected type
            List<Royal.DAO.CustomerDAO> searchResults = new List<Royal.DAO.CustomerDAO>(); // Initialize empty list


            try
            {
                if(type=="Mã khách hàng")
                {
                    Royal.DAO.CustomerDAO searchResult = await billFun.SearchRoomById(searchText1);
                    searchResults.Add(searchResult);
                }    
                else if (type == "CCCD")
                {
                    Royal.DAO.CustomerDAO searchResult = await billFun.SearchCusByCCCD(searchText1);
                    searchResults.Add(searchResult);

                }
                else if( type == "Giới tính")
                {
                    searchResults = await billFun.SearchCustomerByGender(searchText1);
                }    
                else if(type=="Loại khách hàng")
                {
                    searchResults = await billFun.SearchRoomByType(searchText1);    
                }    
                

                // Prepare UI results (assuming you want to display MALPH, TENLPH, SLNG, GIA)
                List<string[]> uiResults = searchResults.Select(bill => new string[] { bill.MAKH, bill.HOTEN, bill.CCCD, bill.NGSINH, bill.DIACHI, bill.ID_LOAIKH, bill.GIOITINH, bill.SDT, bill.QUOCTICH, bill.EMAIL }).ToList();

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

                    MessageBox.Show($"No customer found");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error searching customer: {ex.Message}");
            }


        }

        private void kryptonButton3_Click(object sender, EventArgs e)
        {
            CustomerDAO newCus = new CustomerDAO();
            newCus.LoadCustomer(dataGridStaff);
        }

        private async void kryptonButton2_Click(object sender, EventArgs e)
        {
            CustomerDAO p = new CustomerDAO();
            // Convert the selected date to a string in the desired format
            string ngSinh = Ngsinh.Value.ToString("yyyy-MM-dd"); // Adjust the format as per your requirements
            await p.UpdateCustomer(makh.Text, hoTen.Text, cccd.Text, ngSinh, diaChi.Text,LoaiKH.Text,GT.Text, sdt.Text, QuocTich.Text, Email.Text);
            p.LoadCustomer(dataGridStaff);

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
                    makh.Text = (string)selectedRow.Cells[0].Value;
                    hoTen.Text = (string)selectedRow.Cells[1].Value;
                    cccd.Text = (string)selectedRow.Cells[2].Value;
                    Ngsinh.Text = (string)selectedRow.Cells[3].Value;
                    diaChi.Text = (string)selectedRow.Cells[4].Value;
                    LoaiKH.Text = (string)selectedRow.Cells[5].Value.ToString();
                    GT.Text = (string)selectedRow.Cells[6].Value.ToString();
                    sdt.Text = (string)selectedRow.Cells[7].Value.ToString();
                    QuocTich.Text = (string)(selectedRow.Cells[8].Value);
                    Email.Text = (string)(selectedRow.Cells[9].Value);
                    
                }
            }
        }

        private async void kryptonButton4_Click(object sender, EventArgs e)
        {
            string idkh = makh.Text;
            CustomerDAO cus = new CustomerDAO();
            await cus.DeleteCus(idkh);
            cus.LoadCustomer(dataGridStaff);
        }

        private void toolStripLabel2_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();


            saveFileDialog.Filter = "PDF Files (*.pdf)|*.pdf";
            saveFileDialog.DefaultExt = "pdf";
            saveFileDialog.AddExtension = true;



            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                ExportToPdf.Export(dataGridStaff, saveFileDialog.FileName);
            }
        }

        private void toolStripLabel3_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();


            saveFileDialog.Filter = "Excel Files (*.xlsx)|*.xlsx";
            saveFileDialog.DefaultExt = "xlsx";
            saveFileDialog.AddExtension = true;



            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {

                ExportToExcel.Export(dataGridStaff, saveFileDialog.FileName);
            }
        }
    }
}
