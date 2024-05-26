using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FireSharp.Interfaces;
using FireSharp.Response;

namespace Royal.DAO
{
    public class CustomerType
    {
        private Firebase.Database.FirebaseClient firebaseClient;
        public string ID_LOAIKH { get; set; }
        public string TEN_LOAIKH { get; set; }
        public int DISCOUNT { get; set; }

        private readonly FirebConfig config = new FirebConfig();
        public IFirebaseClient Client { get; private set; }

        public CustomerType()
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

        public async void AddCustomerType(CustomerType CustomerType)
        {
            var CustomerData = new
            {
                CustomerType.ID_LOAIKH,
                CustomerType.TEN_LOAIKH,
                CustomerType.DISCOUNT
            };
            FirebaseResponse response = await Client.SetAsync("CustomerType/" + CustomerType.ID_LOAIKH, CustomerData);
            MessageBox.Show("Add customer type successfully");

        }

        public async void LoadCustomerType(DataGridView v)
        {

            FirebaseResponse response = await Client.GetAsync("CustomerType/");
            Dictionary<string, Royal.DAO.CustomerType> getCustomer = response.ResultAs<Dictionary<string, Royal.DAO.CustomerType>>();
            v.Rows.Clear();
            foreach (var r in getCustomer)
            {
                Royal.DAO.CustomerType Customer = r.Value;
                v.Rows.Add(
                    Customer.ID_LOAIKH,
                    Customer.TEN_LOAIKH,
                    Customer.DISCOUNT

                );
            }
        }


        public async void DeleteCustomerType(string stID)
        {
            // Confirmation prompt (optional)
            if (MessageBox.Show("Are you sure you want to delete this CustomerType?", "Delete Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    // Delete the bill from Firebase
                    await Client.DeleteAsync($"CustomerType/{stID}");

                    MessageBox.Show("CustomerType deleted successfully!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting service: {ex.Message}");
                }
            }
        }


        public async void UpdateCustomerType(string id, string name, int num)
        {
            // Get the updated Customer information from the selected row
            CustomerType updatedCustomerType = new CustomerType
            {
                ID_LOAIKH = id, // Assuming Customer ID remains unchanged
                TEN_LOAIKH = name,
                DISCOUNT = num

            };


            // Confirmation prompt (modified)
            if (MessageBox.Show("Are you sure you want to update this CustomerType?", "Update Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    // Update the Customer in Firebase
                    await Client.SetAsync($"CustomerType/{id}", updatedCustomerType);

                    // Refresh the DataGridView (optional)
                    // v.Refresh(); // You might want to refresh only the updated row

                    MessageBox.Show("Customer information updated successfully!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error updating Customer: {ex.Message}");
                }
            }



        }

        public async Task<CustomerType> SearchCustomerTypeById(string id)
        {
            string queryPath = $"CustomerType/{id}";

            try
            {
                FirebaseResponse response = await Client.GetAsync(queryPath);
                return response.ResultAs<CustomerType>();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error searching Customer by ID: {ex.Message}");
                return null; // Return null on error
            }
        }

        public async Task<List<CustomerType>> SearchCustomerTypeByName(string name)
        {
            try
            {
                var typeCustomerList = await firebaseClient
            .Child("CustomerType")
            .OnceAsync<Royal.DAO.CustomerType>();
                // Initialize an empty list to store matching Customers
                List<CustomerType> matchingCustomers = new List<CustomerType>();
                foreach (var CustomerType in typeCustomerList)
                {
                    // Extract Customer information
                    CustomerType Customer = CustomerType.Object;

                    // Check if Customer capacity matches the search criteria
                    if (Customer.TEN_LOAIKH == name)
                    {
                        // Add matching Customer to the list
                        matchingCustomers.Add(Customer);
                    }
                }

                // Return the list of matching Customers
                return matchingCustomers;
            }
            catch (Exception ex)
            {
                // Handle exceptions (logging, throwing specific exceptions, etc.)
                Console.WriteLine($"Error searching Customer by capacity: {ex.Message}");
                return new List<CustomerType>(); // Return empty list on error
            }

        }

        public async Task<List<CustomerType>> SearchCustomerTypeByCapacity(int capacity)
        {
            try
            {
                // Retrieve all Customer data from "CustomerType" node
                var typeCustomerList = await firebaseClient
                    .Child("CustomerType")
                    .OnceAsync<Royal.DAO.CustomerType>();

                // Initialize an empty list to store matching Customers
                List<CustomerType> matchingCustomers = new List<CustomerType>();

                // Iterate through retrieved Customer data
                foreach (var CustomerType in typeCustomerList)
                {
                    // Extract Customer information
                    CustomerType Customer = CustomerType.Object;

                    // Check if Customer capacity matches the search criteria
                    if (Customer.DISCOUNT == capacity)
                    {
                        // Add matching Customer to the list
                        matchingCustomers.Add(Customer);
                    }
                }

                // Return the list of matching Customers
                return matchingCustomers;
            }
            catch (Exception ex)
            {
                // Handle exceptions (logging, throwing specific exceptions, etc.)
                Console.WriteLine($"Error searching Customer by capacity: {ex.Message}");
                return new List<CustomerType>(); // Return empty list on error
            }
        }

        public async Task<CustomerType> SearchRoomById(string id)
        {
            string queryPath = $"CustomerType/{id}";

            try
            {
                FirebaseResponse response = await Client.GetAsync(queryPath);
                return response.ResultAs<CustomerType>();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error searching room by ID: {ex.Message}");
                return null; // Return null on error
            }
        }


    }
}
