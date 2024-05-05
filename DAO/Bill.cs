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

        public async void DeleteBill(DataGridView v)
        {
            if (v.SelectedRows.Count > 0) // Check if any row is selected
            {
                // Get the selected row
                DataGridViewRow selectedRow = v.SelectedRows[0];

                if (selectedRow != null)
                {
                    // Extract the bill ID from the selected row (assuming it's in column 0)
                    string billId = (string)selectedRow.Cells[0].Value;

                    // Confirmation prompt (optional)
                    if (MessageBox.Show("Are you sure you want to delete this bill?", "Delete Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        try
                        {
                            // Delete the bill from Firebase
                            await Client.DeleteAsync($"Bill/{billId}");

                            // Remove the row from the DataGridView
                            v.Rows.Remove(selectedRow);

                            MessageBox.Show("Bill deleted successfully!");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Error deleting bill: {ex.Message}");
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a bill to delete.");
            }
        }

        public async void UpdateBill(DataGridView v)
        {
            if (v.SelectedRows.Count > 0) // Check if any row is selected
            {
                // Get the selected row
                DataGridViewRow selectedRow = v.SelectedRows[0];

                if (selectedRow != null)
                {
                    // Extract the bill ID from the selected row (assuming it's in column 0)
                    string billId = (string)selectedRow.Cells[0].Value;

                    // Get the updated bill information from the selected row
                    BillDAO updatedBill = new BillDAO
                    {
                        MAHD = (string)selectedRow.Cells[0].Value, // Assuming bill ID remains unchanged
                        MAPHONG = (string)selectedRow.Cells[1].Value,
                        TRANGTHAI = (string)selectedRow.Cells[2].Value,
                        ID_KH = (string)selectedRow.Cells[3].Value,
                        ID_NV = (string)selectedRow.Cells[4].Value,

                        NGLAP = (string)selectedRow.Cells[5].Value, // Remove extra space

                        DONGIA = (int)selectedRow.Cells[6].Value,
                        DISCOUNT = (int)selectedRow.Cells[7].Value,
                        THANHTIEN = (int)selectedRow.Cells[8].Value,
                    };


                    // Confirmation prompt (modified)
                    if (MessageBox.Show("Are you sure you want to update this bill?", "Update Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        try
                        {
                            // Update the bill in Firebase
                            await Client.SetAsync($"Bill/{billId}", updatedBill);

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
            }
            else
            {
                MessageBox.Show("Please select a bill to update.");
            }
        }





    }




}

