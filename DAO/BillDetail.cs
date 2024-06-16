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
        public string MACTHD { get; set; }
        public string MAHD { get; set; }
        public string MADV { get; set; }
        public int SLG_DV { get; set; }
        public int THANHTIEN { get; set; }



        private readonly FirebConfig config = new FirebConfig();
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
        public async Task AddBDetail(BillDetailDAO bill)
        {


            // Tạo một đối tượng chứa các thuộc tính không bao gồm cấu hình
            var billData = new
            {
                bill.MACTHD,
                bill.MAHD,
                bill.MADV,
                bill.SLG_DV,
                bill.THANHTIEN
            };

            // Set data to Firebase RTDB
            FirebaseResponse response = await Client.SetAsync("BillDetail/" + bill.MACTHD, billData);

            MessageBox.Show("Add a bill detail");
        }

        public async void LoadBillDetail(DataGridView v)
        {
            try
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
                                bill.MACTHD,
                                bill.MAHD,
                                bill.MADV,
                                bill.SLG_DV,
                                bill.THANHTIEN
                            );
                        }
                    }

                }
            }
            catch( Exception ex ) {
                MessageBox.Show("Error!");
            }
           
        }

        public async Task DeleteBillDetail(string id)
        {
            // Confirmation prompt (optional)
            if (MessageBox.Show("Are you sure you want to delete this bill detail?", "Delete Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    // Delete the bill from Firebase
                    await Client.DeleteAsync($"BillDetail/{id}");

                    MessageBox.Show("Bill detail deleted successfully!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting bill detail: {ex.Message}");
                }
            }



        }

        public async Task UpdateBill(string id, string mahd, string madv, int sl, int thanhtien)
        {



            // Get the updated bill information from the selected row
            BillDetailDAO updatedBill = new BillDetailDAO
            {
                MACTHD = id,
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
                    await Client.SetAsync($"BillDetail/{id}", updatedBill);

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

        public async Task<List<BillDetailDAO>> SearchBillDetailByMaHD(string mahd)
        {
            try
            {
                var billList = await firebaseClient
            .Child("BillDetail")
            .OnceAsync<Royal.DAO.BillDetailDAO>();
                // Initialize an empty list to store matching rooms
                List<BillDetailDAO> matchingBill = new List<BillDetailDAO>();
                foreach (var bill in billList)
                {
                    // Extract room information
                    BillDetailDAO billA = bill.Object;

                    // Check if room capacity matches the search criteria
                    if (billA.MAHD == mahd)
                    {
                        // Add matching room to the list
                        matchingBill.Add(billA);
                    }
                }

                // Return the list of matching rooms
                return matchingBill;
            }
            catch (Exception ex)
            {
                // Handle exceptions (logging, throwing specific exceptions, etc.)
                MessageBox.Show($"Error searching bill detail by id bill: {ex.Message}");
                return new List<BillDetailDAO>(); // Return empty list on error
            }

        }
        public async Task<List<BillDetailDAO>> SearchBillDetailByMaDV(string mahd)
        {
            try
            {
                var billList = await firebaseClient
            .Child("BillDetail")
            .OnceAsync<Royal.DAO.BillDetailDAO>();
                // Initialize an empty list to store matching rooms
                List<BillDetailDAO> matchingBill = new List<BillDetailDAO>();
                foreach (var bill in billList)
                {
                    // Extract room information
                    BillDetailDAO billA = bill.Object;

                    // Check if room capacity matches the search criteria
                    if (billA.MADV == mahd)
                    {
                        // Add matching room to the list
                        matchingBill.Add(billA);
                    }
                }

                // Return the list of matching rooms
                return matchingBill;
            }
            catch (Exception ex)
            {
                // Handle exceptions (logging, throwing specific exceptions, etc.)
                MessageBox.Show($"Error searching bill detail by id service: {ex.Message}");
                return new List<BillDetailDAO>(); // Return empty list on error
            }

        }






    }
}

