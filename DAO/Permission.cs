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
using Firebase.Database.Query;

namespace Royal.DAO
{

    public class Permission
    {
        private Firebase.Database.FirebaseClient firebaseClient;

        private readonly FirebConfig config = new FirebConfig();
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


        public async Task<string> GetUserRoleByEmail(string email)
        {
            var staffRecord = await Client.GetAsync("Staff");
            var allStaff = staffRecord.ResultAs<Dictionary<string, StaffDAO>>();

            var staff = allStaff.Values.FirstOrDefault(s => s.staffEmail == email);
            if (staff != null)
            {
                var staffTypeRecord = await Client.GetAsync($"StaffType/{staff.staffType}");
                var staffType = staffTypeRecord.ResultAs<StaffType>();

                return staffType.stName;
            }

            return null; // hoặc trả về một giá trị mặc định nếu không tìm thấy
        }

        public bool HasAccess(string userRole, string requiredRole)
        {
            if (userRole == "Giám đốc" || userRole == "Quản lý")
            {
                return true; // Giám đốc và quản lý có quyền truy cập tất cả các chức năng
            }
            if (userRole == requiredRole)
            {
                return true; // Nếu vai trò của người dùng phù hợp với yêu cầu
            }
            return false;
        }



    }



}
