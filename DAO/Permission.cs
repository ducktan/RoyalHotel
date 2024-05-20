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
    public enum Role
    {
        Admin,
        Manager, // quan ly
        Employee // le tan
    }

    public class Permission
    {
        private Firebase.Database.FirebaseClient firebaseClient;

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
       /*
        public static async Task<bool> HasPermission(string userId, string pageUrl, Role role)
        {
            var userPermissions = await firebaseClient.Child($"permissions/{userId}").GetAsync();
            var permissions = userPermissions.ResultAs<Dictionary<string, bool>>();

            if (permissions != null && permissions.ContainsKey(pageUrl))
            {
                return permissions[pageUrl];
            }

            // Nếu không tìm thấy quyền riêng cho người dùng, kiểm tra quyền mặc định của vai trò
            var rolePermissions = await firebaseClient.Child($"roles/{role.ToString().ToLower()}/permissions").GetAsync();
            permissions = rolePermissions.ResultAs<Dictionary<string, bool>>();

            return permissions != null && permissions.ContainsKey(pageUrl) && permissions[pageUrl];
        }
       */
    }
    


}
