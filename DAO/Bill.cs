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
        public string MAHD { get; set; }
        public string MAPHONG { get; set; }
        public string ID_NV { get; set; }
        public string ID_KH { get; set; }
        public string NGLAP { get; set; }
        public string TRANGTHAI { get; set; }
        public int THANHTIEN { get; set; }
        public int DISCOUNT { get; set; }
        public int SL_DICHVU { get; set; }
        public int DONGIA { get; set; }


        // Set up Firebase configuration
        public FirebConfig config = new FirebConfig();

        // Initialize Firebase client
        public IFirebaseClient Client { get; private set; } // Make client accessible only within the class

        
        public BillDAO()
        {   
            try
            {
                Client = new FireSharp.FirebaseClient(config.Config);
                
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
                bill.MAHD,
                bill.MAPHONG,
                bill.ID_NV,
                bill.ID_KH,
                bill.NGLAP,
                bill.TRANGTHAI,
                bill.THANHTIEN,
                bill.DISCOUNT,
                bill.SL_DICHVU, 
                bill.DONGIA
            };

            // Set data to Firebase RTDB
            FirebaseResponse response = await Client.SetAsync("Bill/" + bill.MAHD, billData);
            MessageBox.Show("Add bill");
        }

        public async void LoadBill(DataGridView v)
        {
           
                // Fetch data from Firebase
                FirebaseResponse response = await Client.GetAsync("Bill/");

                // Check for successful response
               
                
                    // Cast response as Dictionary<string, Bill> (assuming 'Bill' class exists)
                    Dictionary<string, BillDAO> getBill = response.ResultAs<Dictionary<string, BillDAO>>();

                    // Clear the DataGridView before loading new data (optional)
                    v.Rows.Clear();

                    foreach (var item in getBill)
                    {
                        BillDAO bill = item.Value; // Access the Bill object

                        // Add a new row to the DataGridView
                        v.Rows.Add(
                            bill.MAHD,
                            bill.MAPHONG,
                            bill.TRANGTHAI,
                            bill.ID_KH,
                            bill.ID_NV,
                            bill.NGLAP,
                            bill.DONGIA,
                            bill.DISCOUNT,
                            bill.THANHTIEN // Assuming THANHTIEN is the correct property

                        );
                    }
                
               
            }

        public async void DeleteBill(string billId)
        {         
                    // Confirmation prompt (optional)
                    if (MessageBox.Show("Are you sure you want to delete this bill?", "Delete Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        try
                        {
                            // Delete the bill from Firebase
                            await Client.DeleteAsync($"Bill/{billId}");                           

                            MessageBox.Show("Bill deleted successfully!");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Error deleting bill: {ex.Message}");
                        }
                    }
                
            
           
        }


        public async void UpdateBill(string billID, string maphong, string trangthai, string idkh, string idnv, string nglap, int dongia, int giamgia, int tongtien)
        {
            
                

                    // Get the updated bill information from the selected row
                    BillDAO updatedBill = new BillDAO
                    {
                        MAHD = billID, // Assuming bill ID remains unchanged
                        MAPHONG = maphong,
                        TRANGTHAI = trangthai,
                        ID_KH = idkh,
                        ID_NV = idnv,

                        NGLAP = nglap, // Remove extra space

                        DONGIA = dongia,
                        DISCOUNT = giamgia,
                        THANHTIEN = tongtien,
                    };


                    // Confirmation prompt (modified)
                    if (MessageBox.Show("Are you sure you want to update this bill?", "Update Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        try
                        {
                            // Update the bill in Firebase
                            await Client.SetAsync($"Bill/{billID}", updatedBill);

                            // Refresh the DataGridView (optional)
                            // v.Refresh(); // You might want to refresh only the updated row

                            MessageBox.Show("Bill information updated successfully!");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Error updating bill: {ex.Message}");
                        }
                    }
                
            }



        public async Task<BillDAO> SearchBillTypeById(string id)
        {
            string queryPath = $"Bill/{id}";

            try
            {
                FirebaseResponse response = await Client.GetAsync(queryPath);
                return response.ResultAs<BillDAO>();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error searching bill by ID: {ex.Message}");
                return null; // Return null on error
            }
        }



    }
    




}

