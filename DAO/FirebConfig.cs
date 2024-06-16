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


namespace Royal.DAO
{
    public class FirebConfig
    {
        public FirebaseConfig Config { get; private set; }

        public FirebConfig()
        {
            Config = new FirebaseConfig
            {
                AuthSecret = "oy0VAsm7fVZFIBAk5lmVXC7u1uDfxeNsI779WMhc",
                BasePath = "https://royal-9807e-default-rtdb.firebaseio.com/"
            };
        }
    }
}
