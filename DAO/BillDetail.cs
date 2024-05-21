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
using Firebase.Database;
using System.Globalization;


namespace Royal.DAO
{
    public class BillDetailDAO
    {
        private Firebase.Database.FirebaseClient firebaseClient;
        public string MAHD { get; set; }
        public string MADV { get; set; }
        public int SLG_DV { get; set; } 
        public int THANHTIEN { get; set; }



        public FirebConfig config = new FirebConfig();
        public IFirebaseClient Client { get; private set; } // Make client accessible only within the class


        public BillDetailDAO()
        {
            try
            {
                Client = new FireSharp.FirebaseClient(config.Config);
                firebaseClient = FirebaseManage.GetFirebaseClient();

            }
            catch
            {
                MessageBox.Show("Connection fail!");
            }
            // Initialize client upon object creation


        }
        public async void AddBDetail(BillDetailDAO bill)
        {


            // Tạo một đối tượng chứa các thuộc tính không bao gồm cấu hình
            var billData = new
            {
                bill.MAHD, 
                bill.MADV, 
                bill.SLG_DV, 
                bill.THANHTIEN
            };

            // Set data to Firebase RTDB
            FirebaseResponse response = await Client.SetAsync("BillDetail/" + bill.MAHD + bill.MADV, billData);
            MessageBox.Show("Add a bill detail");
        }

        public async void LoadBillDetail(DataGridView v, string dongia, string makh)
        {

            // Fetch data from Firebase
            FirebaseResponse response = await Client.GetAsync("BillDetail/");

            // Check for successful response

            if (response != null && !string.IsNullOrEmpty(response.Body))
            {
                // Cast response as Dictionary<string, Bill> (assuming 'Bill' class exists)
                Dictionary<string, BillDetailDAO> getBill = response.ResultAs<Dictionary<string, BillDetailDAO>>();

                // Clear the DataGridView before loading new data (optional)
                v.Rows.Clear();

                if (getBill != null)
                {
                    foreach (var item in getBill)
                    {
                        BillDetailDAO bill = item.Value; // Access the Bill object

                        // Add a new row to the DataGridView
                        v.Rows.Add(
                            bill.MAHD,
                            bill.MADV,
                            bill.SLG_DV,
                            dongia,
                            makh,
                            bill.THANHTIEN
                        );
                    }
                }

            }
        }

        public async void DeleteBillDetail(string mahd, string madv)
        {
            // Confirmation prompt (optional)
            if (MessageBox.Show("Are you sure you want to delete this bill detail?", "Delete Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    // Delete the bill from Firebase
                    await Client.DeleteAsync($"BillDetail/{mahd+madv}");

                    MessageBox.Show("Bill detail deleted successfully!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting bill detail: {ex.Message}");
                }
            }



        }

        public async void UpdateBill(string mahd, string madv, int sl, int thanhtien)
        {



            // Get the updated bill information from the selected row
            BillDetailDAO updatedBill = new BillDetailDAO
            {
                MAHD = mahd, 
                MADV = madv,
                SLG_DV = sl,
                THANHTIEN = thanhtien
            };


            // Confirmation prompt (modified)
            if (MessageBox.Show("Are you sure you want to update this bill detail?", "Update Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    // Update the bill in Firebase
                    await Client.SetAsync($"BillDetail/{MAHD+MADV}", updatedBill);

                    // Refresh the DataGridView (optional)
                    // v.Refresh(); // You might want to refresh only the updated row

                    MessageBox.Show("Bill detail's information updated successfully!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error updating bill detail: {ex.Message}");
                }
            }

        }






    }
}

