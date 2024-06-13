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
        Permission permission;

        public CustomerType()
        {
            InitializeComponent();
            firebaseClient = FirebaseManage.GetFirebaseClient();
            // Assuming dataGridCustomerType is a DataGridView control on your form
            DAO.CustomerType customerType = new DAO.CustomerType();
       
            customerType.LoadCustomerType(dataGridCusType);
            dataGridCusType.CellClick += dataGridCustomer_CellClick;
            permission = new Permission();
        }




        private async void LoadCustomerDiscountFromDB()
        {
            try
            {
                var typeCustomerList = await firebaseClient
                    .Child("CustomerType")
                    .OnceAsync<Royal.DAO.CustomerType>();
                cboDiscount.Items.Clear();

                foreach (var CustomerType in typeCustomerList)
                {
                    cboDiscount.Items.Add(CustomerType.Object.DISCOUNT.ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu từ Firebase Realtime Database: " + ex.Message);
            }

        }


        private void kryptonButton3_Click_1(object sender, EventArgs e)
        {
            // Assuming dataGridCustomerType is a DataGridView control on your form
            Royal.DAO.CustomerType CustomerType = new Royal.DAO.CustomerType();
            CustomerType.LoadCustomerType(dataGridCusType);
        }

        private void dataGridCustomer_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Kiểm tra xem có hàng nào được chọn không
            {
                DataGridViewRow selectedRow = dataGridCusType.Rows[e.RowIndex];

                // Kiểm tra nếu hàng không rỗng
                if (!selectedRow.IsNewRow)
                {
                    txtCusTypeId.Text = (string)selectedRow.Cells[0].Value;
                    txtName.Text = (string)selectedRow.Cells[1].Value;
                    cboDiscount.Text = cboDiscount.Text = selectedRow.Cells[2].Value.ToString();
                    ;

                }
            }
        }

        private async void kryptonButton4_Click_1(object sender, EventArgs e)
        {
            if(permission.HasAccess(User.Role, ""))
            {
                try
                {
                    string id = txtCusTypeId.Text;
                    Royal.DAO.CustomerType CustomerType = new Royal.DAO.CustomerType(); // Assuming you have an instance
                    await CustomerType.DeleteCustomerType(id);
                    CustomerType.LoadCustomerType(dataGridCusType);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
            else
            {
                MessageBox.Show("Bạn không có quyền truy cập vào mục này");
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
                    txtName.Text = (string)selectedRow.Cells[1].Value;
                    cboDiscount.Text = selectedRow.Cells[3].Value.ToString();
                }
            }
        }

        private async void kryptonButton1_Click(object sender, EventArgs e)
        {
            if(permission.HasAccess(User.Role, ""))
            {
                var CustomerTypes = await firebaseClient
.Child("CustomerType")
.OnceAsync<DAO.CustomerType>();

                int maxCustomerNumber = 0;
                foreach (var CustomerData in CustomerTypes)
                {
                    int CustomerNumber = int.Parse(CustomerData.Object.ID_LOAIKH.Substring(3));
                    if (CustomerNumber > maxCustomerNumber)
                    {
                        maxCustomerNumber = CustomerNumber;
                    }
                }

                string newCustomerNumber = "LKH" + (maxCustomerNumber + 1).ToString("D2");


                Royal.DAO.CustomerType CustomerType = new Royal.DAO.CustomerType()
                {
                    ID_LOAIKH = newCustomerNumber,
                    TEN_LOAIKH = txtName.Text,
                    DISCOUNT = Int32.Parse(cboDiscount.Text)

                };

                await CustomerType.AddCustomerType(CustomerType);
                CustomerType.LoadCustomerType(dataGridCusType);
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
                    string id = txtCusTypeId.Text;
                    string name = txtName.Text;
                    int discount = Int32.Parse(cboDiscount.Text);
                    DAO.CustomerType a = new DAO.CustomerType();
                    await a.UpdateCustomerType(id, name, discount);
                    a.LoadCustomerType(dataGridCusType);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            else
            {
                MessageBox.Show("Bạn không có quyền truy cập vào mục này");
            }


        }
    }
}
