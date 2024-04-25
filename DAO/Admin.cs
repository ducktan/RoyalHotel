using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Firebase.Auth;

using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Firebase.Auth.Providers;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Royal.DAO
{
    public class AdminDAO
    {
        private FirebaseAuthClient client;
        public AdminDAO()
        {
            var config = new FirebaseAuthConfig
            {
                ApiKey = "AIzaSyCkQbVSzvPQfGzSZkvnRgBXFuKUOaVbLxQ",
                AuthDomain = "royal-9807e.firebaseapp.com",
                Providers = new Firebase.Auth.Providers.FirebaseAuthProvider[]
            {
                new EmailProvider()
            }
            };
            client = new FirebaseAuthClient(config);
        }

        public async Task<UserCredential> Signup(string email, string password)
        {


            // Thực hiện việc đăng ký tài khoản người dùng mới
            UserCredential credential = await client.CreateUserWithEmailAndPasswordAsync(email, password);

            return credential;
        }
    }
}
