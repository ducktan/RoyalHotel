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
    public class Permission
    {
        private Firebase.Database.FirebaseClient firebaseClient;

        string perID {  get; set; }
        string perName { get; set; }    
        string perContent { get; set; }
        string[] perObject { get; set; }

        public FirebConfig config = new FirebConfig();
        public IFirebaseClient Client { get; private set; } // Make client accessible only within the class
        public Permission()
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
