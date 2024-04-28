using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using System.Windows.Forms;
using FireSharp;

namespace Royal.DAO
{
    public class BillDAO
    {
        public string MaHD { get; set; }
        public string MaPhong { get; set; }
        public string NvTao { get; set; }
        public string KhachHang { get; set; }
        public string Date { get; set; }
        public string TrangThai { get; set; }
        public string Total { get; set; }
        public string Discount { get; set; }
        public int numofService { get; set; }


        // Set up Firebase configuration
        public FirebaseConfig Config = new FirebaseConfig
        {
            AuthSecret = "oy0VAsm7fVZFIBAk5lmVXC7u1uDfxeNsI779WMhc",
            BasePath = "https://royal-9807e-default-rtdb.firebaseio.com/"
        };

        // Initialize Firebase client
        public IFirebaseClient Client { get; private set; } // Make client accessible only within the class

        public BillDAO()
        {   
            try
            {
                Client = new FireSharp.FirebaseClient(Config);
                if (Client != null)
                {
                    MessageBox.Show("Connect Successfully!");
                }
            }
            catch {
                MessageBox.Show("Connection fail!");
            }
            // Initialize client upon object creation
           

        }

        public async void AddBill(BillDAO bill)
        {


            // Tạo một đối tượng chứa các thuộc tính không bao gồm cấu hình
            var billData = new
            {
                bill.MaHD,
                bill.MaPhong,
                bill.NvTao,
                bill.KhachHang,
                bill.Date,
                bill.TrangThai,
                bill.Total,
                bill.Discount,
                bill.numofService
            };

            // Set data to Firebase RTDB
            FirebaseResponse response = await Client.SetAsync("Bill/" + bill.MaHD, billData);
            MessageBox.Show("Add bill");
        }

    }
    
}
