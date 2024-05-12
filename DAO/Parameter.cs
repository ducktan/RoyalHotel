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

        

        public FirebConfig config = new FirebConfig();
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

        public async void AddPara(ParameterDAO p)
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




    }
}
