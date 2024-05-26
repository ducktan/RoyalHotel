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


namespace Royal.DAO
{
    public class CustomerDAO
    {
        private Firebase.Database.FirebaseClient firebaseClient;
        public string MAKH { get; set; }
        public string HOTEN { get; set; }
        public string GIOITINH { get; set; }
        public string EMAIL { get; set; }
        public string SDT { get; set; }
        public string NGSINH { get; set; }
        public string DIACHI { get; set; }
        public string QUOCTICH { get; set; }
        public string CCCD { get; set; }
        public string ID_LOAIKH { get; set; }


        // Set up Firebase configuration
        public FirebConfig config = new FirebConfig();

        // Initialize Firebase client
        public IFirebaseClient Client { get; private set; } // Make client accessible only within the class


        public CustomerDAO()
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
        public async void AddCustomer(CustomerDAO customer)
        {


            // Tạo một đối tượng chứa các thuộc tính không bao gồm cấu hình
            var customerData = new
            {
                customer.HOTEN, 
                customer.GIOITINH,
                customer.EMAIL,
                customer.SDT,
                customer.CCCD, 
                customer.ID_LOAIKH, 
                customer.DIACHI,
                customer.QUOCTICH,
                customer.NGSINH, 
                customer.MAKH
            };

            // Set data to Firebase RTDB
            FirebaseResponse response = await Client.SetAsync("Customer/" + customer.MAKH, customerData);
            MessageBox.Show("Add Customer");
        }

        public async void LoadCustomer(DataGridView v)
        {

            // Fetch data from Firebase
            FirebaseResponse response = await Client.GetAsync("Customer/");

            // Check for successful response
            // Check for successful response
            if (response != null && !string.IsNullOrEmpty(response.Body))
            {
                // Cast response as Dictionary<string, Bill> (assuming 'Bill' class exists)
                Dictionary<string, CustomerDAO> getBill = response.ResultAs<Dictionary<string, CustomerDAO>>();

                // Clear the DataGridView before loading new data (optional)
                v.Rows.Clear();
                if (getBill != null)
                {
                    foreach (var item in getBill)
                    {
                        CustomerDAO bill = item.Value; // Access the Bill object

                        // Add a new row to the DataGridView
                        v.Rows.Add(
                           bill.MAKH,
                           bill.HOTEN,
                           bill.CCCD,
                           bill.NGSINH,
                           bill.DIACHI,
                           bill.ID_LOAIKH,
                           bill.GIOITINH,
                           bill.SDT, 
                           bill.QUOCTICH, 
                           bill.EMAIL
                        );
                    }
                }
                    
            }

            




        }

        public async void DeleteCus(string cusId)
        {
            // Confirmation prompt (optional)
            if (MessageBox.Show("Are you sure you want to delete this customer?", "Delete Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    // Delete the bill from Firebase
                    await Client.DeleteAsync($"Customer/{cusId}");

                    MessageBox.Show("Customer deleted successfully!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting customer: {ex.Message}");
                }
            }



        }

        public async void UpdateCustomer(string cusID, string hoten, string cccd, string ngsinh, string diachi, string loaikh, string gender, string sdt, string qt, string email)
        {



            // Get the updated bill information from the selected row
            CustomerDAO updatedCustomer = new CustomerDAO
            {
                MAKH = cusID,
                HOTEN = hoten,
                CCCD = cccd,
                NGSINH = ngsinh,
                DIACHI = diachi,
                ID_LOAIKH = loaikh,
                GIOITINH = gender,
                SDT = sdt,
                QUOCTICH = qt,
                EMAIL = email
            };


            // Confirmation prompt (modified)
            if (MessageBox.Show("Are you sure you want to update this customer?", "Update Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    // Update the bill in Firebase
                    await Client.SetAsync($"Customer/{cusID}", updatedCustomer);

                    // Refresh the DataGridView (optional)
                    // v.Refresh(); // You might want to refresh only the updated row

                    MessageBox.Show("Customer information updated successfully!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error updating customer: {ex.Message}");
                }
            }

        }

        public async Task<List<CustomerDAO>> SearchCusByCCCD(string state)
        {
            try
            {
                var billList = await firebaseClient
                .Child("Customer")
                .OnceAsync<Royal.DAO.CustomerDAO>();
                // Initialize an empty list to store matching rooms
                List<CustomerDAO> matchingBill = new List<CustomerDAO>();
                foreach (var bill in billList)
                {
                    // Extract room information
                    CustomerDAO billA = bill.Object;

                    // Check if room capacity matches the search criteria
                    if (billA.CCCD == state)
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
                MessageBox.Show($"Error searching {ex.Message}");
                return new List<CustomerDAO>(); // Return empty list on error
            }

        }

        public async Task<CustomerDAO> SearchRoomById(string id)
        {
            string queryPath = $"Customer/{id}";

            try
            {
                FirebaseResponse response = await Client.GetAsync(queryPath);
                return response.ResultAs<CustomerDAO>();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error searching room by ID: {ex.Message}");
                return null; // Return null on error
            }
        }


        public async Task<List<CustomerDAO>> SearchRoomByType(string type)
        {
            try
            {
                // Retrieve all room data from "Room" node
                var typeRoomList = await firebaseClient
                    .Child("Customer")
                    .OnceAsync<Royal.DAO.CustomerDAO>();

                // Initialize an empty list to store matching rooms
                List<CustomerDAO> matchingRooms = new List<CustomerDAO>();

                // Iterate through retrieved room data
                foreach (var Room in typeRoomList)
                {
                    // Extract room information
                    CustomerDAO room = Room.Object;

                    // Check if room capacity matches the search criteria
                    if (room.ID_LOAIKH == type)
                    {
                        // Add matching room to the list
                        matchingRooms.Add(room);
                    }
                }

                // Return the list of matching rooms
                return matchingRooms;
            }
            catch (Exception ex)
            {
                // Handle exceptions (logging, throwing specific exceptions, etc.)
                Console.WriteLine($"Error searching room by capacity: {ex.Message}");
                return new List<CustomerDAO>(); // Return empty list on error
            }
        }




        public async Task<List<CustomerDAO>> SearchCustomerByGender(string type)
        {
            try
            {
                // Retrieve all room data from "Room" node
                var typeRoomList = await firebaseClient
                    .Child("Customer")
                    .OnceAsync<Royal.DAO.CustomerDAO>();

                // Initialize an empty list to store matching rooms
                List<CustomerDAO> matchingRooms = new List<CustomerDAO>();

                // Iterate through retrieved room data
                foreach (var Room in typeRoomList)
                {
                    // Extract room information
                    CustomerDAO room = Room.Object;

                    // Check if room capacity matches the search criteria
                    if (room.GIOITINH == type)
                    {
                        // Add matching room to the list
                        matchingRooms.Add(room);
                    }
                }

                // Return the list of matching rooms
                return matchingRooms;
            }
            catch (Exception ex)
            {
                // Handle exceptions (logging, throwing specific exceptions, etc.)
                Console.WriteLine($"Error searching room by capacity: {ex.Message}");
                return new List<CustomerDAO>(); // Return empty list on error
            }
        }


    }




}

