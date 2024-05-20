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
    public class CustomerTypeDAO
    {
        private Firebase.Database.FirebaseClient firebaseClient;
        public string ID_LKH {  get; set; }
        public string TEN_LKH { get; set; }
        public int DISCOUNT { get; set; }



        public FirebConfig config = new FirebConfig();
        public IFirebaseClient Client { get; private set; } // Make client accessible only within the class


        public CustomerTypeDAO()
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
        public async void AddCustomerType(CustomerTypeDAO cusType)
        {


            // Tạo một đối tượng chứa các thuộc tính không bao gồm cấu hình
            var cusTypeData = new
            {
                cusType.ID_LKH, 
                cusType.TEN_LKH, 
                cusType.DISCOUNT
            };

            // Set data to Firebase RTDB
            FirebaseResponse response = await Client.SetAsync("CustomerType/" + cusType.ID_LKH, cusTypeData);
            MessageBox.Show("Add customer type");
        }

        public async void LoadCustomerType(DataGridView v)
        {

            // Fetch data from Firebase
            FirebaseResponse response = await Client.GetAsync("CustomerType/");

            // Check for successful response

            if (response != null && !string.IsNullOrEmpty(response.Body))
            {
                // Cast response as Dictionary<string, Bill> (assuming 'Bill' class exists)
                Dictionary<string, CustomerTypeDAO> getBill = response.ResultAs<Dictionary<string, CustomerTypeDAO>>();

                // Clear the DataGridView before loading new data (optional)
                v.Rows.Clear();

                if (getBill != null)
                {
                    foreach (var item in getBill)
                    {
                        CustomerTypeDAO bill = item.Value; // Access the Bill object

                        // Add a new row to the DataGridView
                        v.Rows.Add(
                            bill.ID_LKH, 
                            bill.TEN_LKH, 
                            bill.DISCOUNT

                        );
                    }
                }

            }



        }

        public async void DeleteCustomerType(string id)
        {
            // Confirmation prompt (optional)
            if (MessageBox.Show("Are you sure you want to delete this customer type?", "Delete Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    // Delete the bill from Firebase
                    await Client.DeleteAsync($"CustomerType/{id}");

                    MessageBox.Show("Customer Type deleted successfully!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting customer type: {ex.Message}");
                }
            }



        }

        public async void UpdateCustomerType(string id, string ten, int dis)
        {



            // Get the updated bill information from the selected row
            CustomerTypeDAO updatedBill = new CustomerTypeDAO
            {
               ID_LKH = id,
               TEN_LKH = ten,
               DISCOUNT = dis
            };


            // Confirmation prompt (modified)
            if (MessageBox.Show("Are you sure you want to update this bill?", "Update Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    // Update the bill in Firebase
                    await Client.SetAsync($"CustomerType/{id}", updatedBill);

                    // Refresh the DataGridView (optional)
                    // v.Refresh(); // You might want to refresh only the updated row

                    MessageBox.Show("Customer type's information updated successfully!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error updating customer type: {ex.Message}");
                }
            }

        }

        public async Task<List<CustomerTypeDAO>> SearchCSTypeByID(string idnv)
        {
            try
            {
                var billList = await firebaseClient
            .Child("CustomerType")
            .OnceAsync<Royal.DAO.CustomerTypeDAO>();
                // Initialize an empty list to store matching rooms
                List<CustomerTypeDAO> matchingBill = new List<CustomerTypeDAO>();
                foreach (var bill in billList)
                {
                    // Extract room information
                    CustomerTypeDAO billA = bill.Object;

                    // Check if room capacity matches the search criteria
                    if (billA.ID_LKH == idnv)
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
                MessageBox.Show($"Error searching customer type by id: {ex.Message}");
                return new List<CustomerTypeDAO>(); // Return empty list on error
            }

        }






    }





}

