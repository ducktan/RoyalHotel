using FireSharp.Interfaces;
using FireSharp.Response;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Google.Apis.Requests.BatchRequest;

namespace Royal.DAO
{
    public class Salary
    {
        private Firebase.Database.FirebaseClient firebaseClient;
        public string StaffID { get; set; }
        public int staffSalary {  get; set; }    
        public int countVP { get; set; }
        public int workingDay {  get; set; }





        private readonly FirebConfig config = new FirebConfig();
        public IFirebaseClient Client { get; private set; } // Make client accessible only within the class

        public Salary()
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

       

        public async Task InitSalary(string id)
        {
            var staffSalary = new
            {
                StaffID = id,
                staffSalary = 0, 
                countVP = 0,
                workingDay = 0
            };

            FirebaseResponse response = await Client.SetAsync("Salary/" + id, staffSalary);
          
        }

        public async Task UpdateSalary(string id, int salary, int count, int workday)
        {



            // Get the updated bill information from the selected row
            Salary sa = new Salary
            {
                StaffID = id, 
                staffSalary = salary, 
                countVP = count,
                workingDay = workday
            };


            // Confirmation prompt (modified)
          
                try
                {
                    // Update the bill in Firebase
                    await Client.SetAsync($"Salary/{id}", sa);
                    await Client.SetAsync($"Staff/{id}/luongNV", sa);
                // await Client.SetAsync($"Staff/{id}", sa);



                MessageBox.Show("Chấm công thành công!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            

        }








    }
}
