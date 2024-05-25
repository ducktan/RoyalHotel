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
    public class StaffDAO
    {
        private Firebase.Database.FirebaseClient firebaseClient;
        public string StaffID { get; set; }
        public string staffName { get; set; }   
        public string staffCCCD { get; set; }
        public string staffAdd {  get; set; }
        public string staffGender { get; set; }
        public string staffBirth {  get; set; }
        public string staffEmail { get; set; }
        public string staffPhone { get; set; }
        public string staffDateIn { get; set; }
        public string staffType { get; set; }   

        
 

        public FirebConfig config = new FirebConfig();
        public IFirebaseClient Client { get; private set; } // Make client accessible only within the class

        public StaffDAO()
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

        public async void AddStaff(StaffDAO staff)
        {
            var staffData = new
            {
                staff.StaffID, 
                staff.staffName, 
                staff.staffCCCD,
                staff.staffType, 
                staff.staffPhone, 
                staff.staffEmail, 
                staff.staffBirth, 
                staff.staffAdd, 
                staff.staffGender, 
                staff.staffDateIn
            };
            FirebaseResponse response = await Client.SetAsync("Staff/" + staff.StaffID, staffData);
            MessageBox.Show("Add staff success");
        }

        public async void LoadStaff(DataGridView v)
        {
            // Fetch data from Firebase
            FirebaseResponse response = await Client.GetAsync("Staff/");

            // Check for successful response
            if (response != null && !string.IsNullOrEmpty(response.Body))
            {
                // Cast response as Dictionary<string, ParameterDAO>
                Dictionary<string, StaffDAO> getStaff = response.ResultAs<Dictionary<string, StaffDAO>>();

                // Check if getPara is not null
                if (getStaff != null)
                {
                    // Clear the DataGridView before loading new data (optional)
                    v.Rows.Clear();

                    foreach (var item in getStaff)
                    {
                        StaffDAO staff = item.Value; // Access the ParameterDAO object

                        // Add a new row to the DataGridView
                        v.Rows.Add(
                            staff.StaffID,
                            staff.staffName,
                            staff.staffCCCD,
                            staff.staffType,
                            staff.staffPhone,
                            staff.staffEmail,
                            staff.staffBirth,
                            staff.staffAdd,
                            staff.staffGender,
                            staff.staffDateIn
                        );
                    }
                }

            }

        }

        public async void DeleteStaff(string staffId)
        {
            // Confirmation prompt (optional)
            if (MessageBox.Show("Are you sure you want to delete this staff?", "Delete Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    // Delete the bill from Firebase
                    await Client.DeleteAsync($"Staff/{staffId}");

                    MessageBox.Show("Staffs deleted successfully!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting staff: {ex.Message}");
                }
            }



        }

        public async void UpdateStaff(string sID, string sName, string sCCCD, string sType, string sPhone, string sEmail, string  sNgsinh, string sAdd, string sGender, string sDatein)
        {



            // Get the updated bill information from the selected row
            StaffDAO s = new StaffDAO()
            {
                StaffID = sID,
                staffName = sName,
                staffCCCD = sCCCD,
                staffType = sType,
                staffPhone = sPhone,
                staffEmail = sEmail,
                staffBirth = sNgsinh,
                staffAdd = sAdd,
                staffGender = sGender,
                staffDateIn = sDatein

            };


            // Confirmation prompt (modified)
            if (MessageBox.Show("Are you sure you want to update this staff?", "Update Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    // Update the bill in Firebase
                    await Client.SetAsync($"Staff/{sID}", s);

                    // Refresh the DataGridView (optional)
                    // v.Refresh(); // You might want to refresh only the updated row

                    MessageBox.Show("Staff information updated successfully!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error updating staff: {ex.Message}");
                }
            }

        }

        public async Task<List<StaffDAO>> SearchStaffbyIDStaff(string idnv)
        {
            try
            {
                var billList = await firebaseClient
            .Child("Staff")
            .OnceAsync<Royal.DAO.StaffDAO>();
                // Initialize an empty list to store matching rooms
                List<StaffDAO> matchingBill = new List<StaffDAO>();
                foreach (var bill in billList)
                {
                    // Extract room information
                    StaffDAO billA = bill.Object;

                    // Check if room capacity matches the search criteria
                    if (billA.StaffID == idnv)
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
                MessageBox.Show($"Error searching staff by id staff: {ex.Message}");
                return new List<StaffDAO>(); // Return empty list on error
            }

        }





    }
}
