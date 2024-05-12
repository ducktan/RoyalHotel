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
    public class Parameter
    {
        private Firebase.Database.FirebaseClient firebaseClient;
        public string ParaID { get; set; }
        public string ParaContent { get; set; }
        

        public FirebConfig config = new FirebConfig();
        public IFirebaseClient Client { get; private set; } // Make client accessible only within the class

        public Parameter()
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

       


    }
}
