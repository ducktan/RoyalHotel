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
    public class StaffType
    {
        private Firebase.Database.FirebaseClient firebaseClient;
        public string stID { get; set; }
        public string stName { get; set; }
        public int number { get; set; }
        public int stSalary { get; set; }
          



        public FirebConfig config = new FirebConfig();
        public IFirebaseClient Client { get; private set; } // Make client accessible only within the class

        public StaffType()
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

        public async void AddStaffType(StaffType s)
        {
            var stData = new
            {
                s.stID, 
                s.stName,
                s.number,
                s.stSalary 
                
            };
            FirebaseResponse response = await Client.SetAsync("StaffType/" + s.stID, stData);
            MessageBox.Show("Add staff success");
        }

        public async void LoadStaffType(DataGridView v)
        {
            // Fetch data from Firebase
            FirebaseResponse response = await Client.GetAsync("StaffType/");

            // Check for successful response
            if (response != null && !string.IsNullOrEmpty(response.Body))
            {
                // Cast response as Dictionary<string, ParameterDAO>
                Dictionary<string, StaffType> getStaff = response.ResultAs<Dictionary<string, StaffType>>();

                // Check if getPara is not null
                if (getStaff != null)
                {
                    // Clear the DataGridView before loading new data (optional)
                    v.Rows.Clear();

                    foreach (var item in getStaff)
                    {
                        StaffType staff = item.Value; // Access the ParameterDAO object

                        // Add a new row to the DataGridView
                        v.Rows.Add(
                            staff.stID, 
                            staff.stName, 
                            staff.number, 
                            staff.stSalary
                        );
                    }
                }

            }

        }

        public async void DeleteStaffType(string stID)
        {
            // Confirmation prompt (optional)
            if (MessageBox.Show("Are you sure you want to delete this staff type?", "Delete Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    // Delete the bill from Firebase
                    await Client.DeleteAsync($"StaffType/{stID}");

                    MessageBox.Show("Staff type deleted successfully!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting staff type: {ex.Message}");
                }
            }
        }


        public async void UpdateStaffType(string sID, string sName, int num, int salary)
        {   
            // Get the updated bill information from the selected row
            StaffType s = new StaffType()
            {
                stID = sID, 
                stName = sName, 
                number = num, 
                stSalary = salary

            };


            // Confirmation prompt (modified)
            if (MessageBox.Show("Are you sure you want to update this staff type?", "Update Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    // Update the bill in Firebase
                    await Client.SetAsync($"StaffType/{sID}", s);

                    // Refresh the DataGridView (optional)
                    // v.Refresh(); // You might want to refresh only the updated row

                    MessageBox.Show("Staff type's information updated successfully!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error updating staff type: {ex.Message}");
                }
            }

        }

        public async Task<StaffType> SearchSTypeById(string id)
        {
            string queryPath = $"StaffType/{id}";

            try
            {
                FirebaseResponse response = await Client.GetAsync(queryPath);
                return response.ResultAs<StaffType>();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error searching staff type by ID: {ex.Message}");
                return null; // Return null on error
            }
        }







    }
}
