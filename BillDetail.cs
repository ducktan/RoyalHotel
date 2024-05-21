using Royal.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Royal
{
    public partial class BillDetail : Form
    {
        private Firebase.Database.FirebaseClient firebaseClient;
        public BillDetail()
        {
            InitializeComponent();
            // Khởi tạo FirebaseClient với chuỗi kết nối đến Firebase Realtime Database
            firebaseClient = FirebaseManage.GetFirebaseClient();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Service s = new Service();
            s.Show();
        }

        private async void addBut_Click(object sender, EventArgs e)
        {
            // Get the current row count for the "Bill" table
            var bills = await firebaseClient
                .Child("BillDetail")
                .OnceAsync<object>();


            // Create a new Bill object with form control values
            BillDetailDAO newBill = new BillDetailDAO()
            {
                MAHD = maHDBox.Text,
                MADV = maDV.Text,
                SLG_DV = Int32.Parse(SLG_DV.Text),
                THANHTIEN = Int32.Parse(thanhtien.Text)
            };

            newBill.AddBDetail(newBill);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            BillDetailDAO a = new BillDetailDAO();
            a.LoadBillDetail(dataGridViewBillDetail, "NULL", "NULL");
        }
    }
}
