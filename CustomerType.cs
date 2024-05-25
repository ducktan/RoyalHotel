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
using System.Text.RegularExpressions;
namespace Royal
{
    public partial class CustomerType : Form
    {
        private Firebase.Database.FirebaseClient firebaseClient;

        public CustomerType()
        {
            InitializeComponent();
            firebaseClient = FirebaseManage.GetFirebaseClient();
            // Assuming dataGridCustomerType is a DataGridView control on your form
        }


        private void kryptonButton5_Click_1(object sender, EventArgs e)
        {

        }

        private void kryptonButton3_Click_1(object sender, EventArgs e)
        {
            // Assuming dataGridCustomerType is a DataGridView control on your form
            Royal.DAO.CustomerType CustomerType = new Royal.DAO.CustomerType();
            CustomerType.LoadCustomerType(dataGridCusType);
        }



        private void kryptonButton4_Click_1(object sender, EventArgs e)
        {
            try
            {
                string id = txtCusTypeId.Text;
                Royal.DAO.CustomerType CustomerType = new Royal.DAO.CustomerType(); // Assuming you have an instance
                CustomerType.DeleteCustomerType(id);
                CustomerType.LoadCustomerType(dataGridCusType);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridCustomerType_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Kiểm tra xem có hàng nào được chọn không
            {
                DataGridViewRow selectedRow = dataGridCusType.Rows[e.RowIndex];

                // Kiểm tra nếu hàng không rỗng
                if (!selectedRow.IsNewRow)
                {
                    txtCusTypeId.Text = (string)selectedRow.Cells[0].Value;
                    cboCusTypeName.Text = (string)selectedRow.Cells[1].Value;
                    cboDiscount.Text = selectedRow.Cells[3].Value.ToString();
                }
            }
        }

        private async void kryptonButton1_Click(object sender, EventArgs e)
        {
            try
            {
                var CustomerTypes = await firebaseClient
    .Child("CustomerType")
    .OnceAsync<object>();
                int newNumber = CustomerTypes.Count + 1;
                string maHoaDon;
                bool isUnique;
                do
                {
                    // Format the number with leading zeros (001, 002, ...)
                    string formattedNumber = newNumber.ToString("D3");

                    // Create the MAHD with your preferred prefix (e.g., "HD")
                    maHoaDon = $"LKH{formattedNumber}";

                    // Check if the ID is unique
                    isUnique = !CustomerTypes.Any(b => (b.Object as dynamic).MAHD == maHoaDon);

                    if (!isUnique)
                    {
                        newNumber++;
                    }
                } while (!isUnique);

                Royal.DAO.CustomerType CustomerType = new Royal.DAO.CustomerType()
                {
                    ID_LOAIKH = maHoaDon,
                    TEN_LOAIKH = txtCusTypeId.Text,
                    DISCOUNT = Int32.Parse(cboCusTypeName.Text)

                };

                CustomerType.AddCustomerType(CustomerType);
                CustomerType.LoadCustomerType(dataGridCusType);
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
