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
            // Get the current row count for the "Customer" table
            int currentRowCount = await firebaseClient
                .Child("Customer")
                .OnceAsync<object>()
                .ContinueWith(task => task.Result.Count);

            // Increment by 1 to get the new sequential number
            int newNumber = currentRowCount + 1;

            // Format the number with leading zeros (00001, 00002, ...)
            string formattedNumber = newNumber.ToString("D5"); // Adjust "D5" for desired number of digits

            // Create the customer ID with the prefix "KH"
            string customerID = $"KH{formattedNumber}";
            string ngSinh = Ngsinh.Value.ToString("yyyy-MM-dd"); // Adjust the format as per your requirements

            // Create a new CustomerDAO object with form control values
            CustomerDAO newCustomer = new CustomerDAO()
            {
                MAKH = customerID,
                HOTEN = hoTen.Text,
                CCCD = cccd.Text,
                ID_LOAIKH = LoaiKH.Text,
                SDT = sdt.Text,
                GIOITINH = GT.Text,
               
                NGSINH = ngSinh,
                DIACHI = diaChi.Text,
                QUOCTICH = QuocTich.Text,
                EMAIL = Email.Text
            };

            // Call the AddCustomer method to store the customer object in the Firebase database
            newCustomer.AddCustomer(newCustomer);
        }

        private void kryptonButton5_Click(object sender, EventArgs e)
        {

        }

        private void kryptonButton3_Click(object sender, EventArgs e)
        {
            CustomerDAO newCus = new CustomerDAO();
            newCus.LoadCustomer(dataGridStaff);
        }

        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            CustomerDAO p = new CustomerDAO();
            // Convert the selected date to a string in the desired format
            string ngSinh = Ngsinh.Value.ToString("yyyy-MM-dd"); // Adjust the format as per your requirements
            p.UpdateCustomer(makh.Text, hoTen.Text, cccd.Text, ngSinh, diaChi.Text,LoaiKH.Text,GT.Text, sdt.Text, QuocTich.Text, Email.Text); 

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

        private void kryptonButton4_Click(object sender, EventArgs e)
        {
            string idkh = makh.Text;
            CustomerDAO cus = new CustomerDAO();
            cus.DeleteCus(idkh);
        }
    }
}
