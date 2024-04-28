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
        private BillDAO bill; // Khai báo biến bill ở đây

        public Bill()
        {
            InitializeComponent();
        }

        private void kryptonButton3_Click(object sender, EventArgs e)
        {
            // Create a new Bill object with form control values
            BillDAO newBill = new BillDAO()
            {
                MaHD = mahoadon.Text,
                NvTao = nhanvien.Text,
                MaPhong = maphong.Text,
                Date = date.Text,
                KhachHang = khachhang.Text,
                Total = total.Text,
                Discount = discount.Text,
                TrangThai = status.Text
            };

           
            newBill.AddBill(newBill);
        }
    }
}