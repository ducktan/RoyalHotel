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
    public class ParameterDAO
    {
        private Firebase.Database.FirebaseClient firebaseClient;
        public string pID { get; set; }
        public string pContent { get; set; }

        public int pValue { get; set; }

        // nhân viên làm thêm giờ: + // nhân viên đi trễ: - 

        public string pName { get; set; }

        

        private readonly FirebConfig config = new FirebConfig();
        public IFirebaseClient Client { get; private set; } // Make client accessible only within the class

        public ParameterDAO()
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

        public async Task AddPara(ParameterDAO p)
        {


            // Tạo một đối tượng chứa các thuộc tính không bao gồm cấu hình
            var paraData = new
            {
                p.pID, 
                p.pName, 
                p.pContent, 
                p.pValue
            };

            // Set data to Firebase RTDB
            FirebaseResponse response = await Client.SetAsync("Parameters/" + p.pID, paraData);
            MessageBox.Show("Add a parameter successfully!");
        }

        public async void LoadPara(DataGridView v)
        {
            // Fetch data from Firebase
            FirebaseResponse response = await Client.GetAsync("Parameters/");

            // Check for successful response
            if (response != null && !string.IsNullOrEmpty(response.Body))
            {
                // Cast response as Dictionary<string, ParameterDAO>
                Dictionary<string, ParameterDAO> getPara = response.ResultAs<Dictionary<string, ParameterDAO>>();

                // Check if getPara is not null
                if (getPara != null)
                {
                    // Clear the DataGridView before loading new data (optional)
                    v.Rows.Clear();

                    foreach (var item in getPara)
                    {
                        ParameterDAO para = item.Value; // Access the ParameterDAO object

                        // Add a new row to the DataGridView
                        v.Rows.Add(
                            para.pID,
                            para.pName,
                            para.pValue,
                            para.pContent
                        );
                    }
                }
                
            }
            
        }

        public async Task DeletePara(string paraId)
        {
            // Confirmation prompt (optional)
            if (MessageBox.Show("Are you sure you want to delete this parameter?", "Delete Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    // Delete the bill from Firebase
                    await Client.DeleteAsync($"Parameters/{paraId}");

                    MessageBox.Show("Parameters deleted successfully!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting parameter: {ex.Message}");
                }
            }



        }

        public async Task UpdateParameter(string paraID, string tenPara, int value, string mota)
        {



            // Get the updated bill information from the selected row
            ParameterDAO updatedPara = new ParameterDAO
            {
                pID = paraID, 
                pName = tenPara,
                pValue = value,
                pContent = mota
            };


            // Confirmation prompt (modified)
            if (MessageBox.Show("Are you sure you want to update this parameter?", "Update Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    // Update the bill in Firebase
                    await Client.SetAsync($"Parameters/{paraID}", updatedPara);

                    // Refresh the DataGridView (optional)
                    // v.Refresh(); // You might want to refresh only the updated row

                    MessageBox.Show("Parameter information updated successfully!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error updating parameter: {ex.Message}");
                }
            }

        }

        public async Task<ParameterDAO> SearchParaTypeById(string id)
        {
            string queryPath = $"Parameters/{id}";

            try
            {
                FirebaseResponse response = await Client.GetAsync(queryPath);
                return response.ResultAs<ParameterDAO>();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error searching parameter by ID: {ex.Message}");
                return null; // Return null on error
            }
        }





    }
}
