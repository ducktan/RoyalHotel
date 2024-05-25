using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Firebase.Database;

namespace Royal.DAO
{
    public class FirebaseManage
    {
        private static FirebaseClient firebaseClient;

        // Phương thức khởi tạo FirebaseClient
        public static FirebaseClient GetFirebaseClient()
        {
            if (firebaseClient == null)
            {
                // Khởi tạo FirebaseClient nếu chưa tồn tại
                string firebaseConnectionString = "https://royal-9807e-default-rtdb.firebaseio.com/";
                firebaseClient = new FirebaseClient(firebaseConnectionString);
            }

            return firebaseClient;
        }

    }
}
