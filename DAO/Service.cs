using FireSharp.Interfaces;
using FireSharp.Response;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Google.Apis.Requests.BatchRequest;

namespace Royal.DAO
{
    public class ServiceDAO
    {
        private Firebase.Database.FirebaseClient firebaseClient;
        public string seID { get; set; }
        public string seName { get; set; }
        public int sePrice { get; set; }
        public string seDetail { get; set; }




        public FirebConfig config = new FirebConfig();
        public IFirebaseClient Client { get; private set; } // Make client accessible only within the class

        public ServiceDAO()
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

        public async void AddService(ServiceDAO s)
        {
            var stData = new
            {
                s.seID,
                s.seName,
                s.sePrice, 
                s.seDetail

            };
            FirebaseResponse response = await Client.SetAsync("Service/" + s.seID, stData);
            MessageBox.Show("Add service success");
        }

        public async void LoadService(DataGridView v)
        {
            // Fetch data from Firebase
            FirebaseResponse response = await Client.GetAsync("Service/");

            // Check for successful response
            if (response != null && !string.IsNullOrEmpty(response.Body))
            {
                // Cast response as Dictionary<string, ParameterDAO>
                Dictionary<string, ServiceDAO> getStaff = response.ResultAs<Dictionary<string, ServiceDAO>>();

                // Check if getPara is not null
                if (getStaff != null)
                {
                    // Clear the DataGridView before loading new data (optional)
                    v.Rows.Clear();

                    foreach (var item in getStaff)
                    {
                        ServiceDAO staff = item.Value; // Access the ParameterDAO object

                        // Add a new row to the DataGridView
                        v.Rows.Add(
                            staff.seID,
                            staff.seName,
                            staff.sePrice,
                            staff.seDetail
                        );
                    }
                }

            }

        }

        public async void DeleteSeervice(string stID)
        {
            // Confirmation prompt (optional)
            if (MessageBox.Show("Are you sure you want to delete this service?", "Delete Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    // Delete the bill from Firebase
                    await Client.DeleteAsync($"Service/{stID}");

                    MessageBox.Show("Service deleted successfully!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting service: {ex.Message}");
                }
            }
        }


        public async void UpdateService(string sID, string sName, int num, string de)
        {
            // Get the updated bill information from the selected row
            ServiceDAO s = new ServiceDAO()
            {
                seID = sID,
                seName = sName,
                sePrice = num,
                seDetail = de

            };


            // Confirmation prompt (modified)
            if (MessageBox.Show("Are you sure you want to update this service?", "Update Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    // Update the bill in Firebase
                    await Client.SetAsync($"Service/{sID}", s);

                    // Refresh the DataGridView (optional)
                    // v.Refresh(); // You might want to refresh only the updated row

                    MessageBox.Show("Service's information updated successfully!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error updating service: {ex.Message}");
                }
            }

        }

        public async Task<List<ServiceDAO>> SearchSeTypeById(string id)
        {
            try
            {
                var billList = await firebaseClient
                .Child("Service")
                .OnceAsync<Royal.DAO.ServiceDAO>();
                    // Initialize an empty list to store matching rooms
                    List<ServiceDAO> matchingBill = new List<ServiceDAO>();
                    foreach (var bill in billList)
                    {
                        // Extract room information
                        ServiceDAO billA = bill.Object;

                        // Check if room capacity matches the search criteria
                        if (billA.seID == id)
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
                MessageBox.Show($"Error searching service by id: {ex.Message}");
                return new List<ServiceDAO>(); // Return empty list on error
            }

        }







    }
}
