using System;
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
        public ManageCustomer()
        {
            InitializeComponent();
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

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            // Tạo số ngẫu nhiên từ 0 đến 999999
            Random random = new Random();
            int randomNumber = random.Next(0, 999999);

            // Tạo mã hóa đơn
            string maKhachhang = "KH" + randomNumber.ToString("D6");

            // Create a new Bill object with form control values
            CustomerDAO newCus = new CustomerDAO()
            {
                MAKH = maKhachhang, 
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

            newCus.AddCustomer(newCus);
        }

        private void kryptonButton5_Click(object sender, EventArgs e)
        {

        }
    }
}
