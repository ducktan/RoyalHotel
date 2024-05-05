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

namespace Royal
{
    public partial class Bill : Form
    {
        

        public Bill()
        {
            InitializeComponent();
        }

        private void kryptonButton3_Click(object sender, EventArgs e)
        {
            // Create a new Bill object with form control values
            BillDAO newBill = new BillDAO()
            {
                MAHD = mahoadon.Text,
                ID_NV = nhanvien.Text,
                MAPHONG = maphong.Text,
                NGLAP = date.Text,
                ID_KH = khachhang.Text,
                THANHTIEN = Int32.Parse(total.Text),
                DISCOUNT = Int32.Parse(discount.Text),
                TRANGTHAI = status.Text,
                DONGIA = Int32.Parse(giaDon.Text),
            };
           
            newBill.AddBill(newBill);
        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            BillDAO bill1 = new BillDAO();
            bill1.LoadBill(dataGridBill);
        }

        private void kryptonButton5_Click(object sender, EventArgs e)
        {
            BillDAO bill1 = new BillDAO();
            bill1.DeleteBill(dataGridBill);
            
        }

        private void kryptonButton4_Click(object sender, EventArgs e)
        {
            BillDAO bill1 = new BillDAO();
            bill1.UpdateBill(dataGridBill);
            
           
        }
    }
}