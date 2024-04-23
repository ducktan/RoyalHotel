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
    public class Authen
    {
        private FirebaseAuthClient client;
        public Authen()
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

        public async Task<UserCredential> SignIn(string email, string password)
        {
            try
            {
                var result = await client.SignInWithEmailAndPasswordAsync(email, password);
                return result;
            }
            catch (FirebaseAuthException ex)
            {
                MessageBox.Show(ex.Message, "Authentication Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        public async Task<bool> CheckUserExists(string email)
        {
            try
            {
                var result = await client.FetchSignInMethodsForEmailAsync(email);
                return result.UserExists;
            }
            catch (FirebaseAuthException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public void SignOut()
        {
            try
            {
                client.SignOut();
                MessageBox.Show("Signed out successfully.", "Sign Out", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (FirebaseAuthException ex)
            {
                MessageBox.Show(ex.Message, "Sign Out Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public async Task ForgotPass(string email)
        {
            try
            {
                await client.ResetEmailPasswordAsync(email);
                MessageBox.Show("Reset password email sent successfully. Please check your email to complete the process.", "Password Reset", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (FirebaseAuthException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



    }
}
