using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Firebase.Auth;

using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Firebase.Auth.Providers;

namespace Royal
{
    public partial class Login : Form

    {


        private FirebaseAuthClient client;

        public Login()
        {
            InitializeComponent();
        }

        private async void kryptonButton1_Click(object sender, EventArgs e)
        {
            try
            {
                UserCredential userCredential = await SignIn();

                if (userCredential != null)
                {
                    MessageBox.Show($"You're signed in as {userCredential.User.Info.DisplayName}", "Sign In Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Dashboard db = new Dashboard();
                    db.Show();
                    Hide();
                }
                else
                {
                    MessageBox.Show("Authentication failed. Please check your email and password.", "Authentication Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (FirebaseAuthException ex)
            {
                MessageBox.Show(ex.Message, "Authentication Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {
            var config = new FirebaseAuthConfig
            {
                ApiKey = "AIzaSyBHZYh9tSMeSEYpZIRgdK7etYcQZUJj4vU",
                AuthDomain = "fir-c569c.firebaseapp.com",
                Providers = new Firebase.Auth.Providers.FirebaseAuthProvider[]
              {
                    new EmailProvider()
              }
            };

            client = new FirebaseAuthClient(config);
        }


        private async Task<UserCredential> SignIn()
        {
            string email = txtEmail.Text;

            // Kiểm tra xem người dùng có tồn tại không
            var result = await client.FetchSignInMethodsForEmailAsync(email);

            if (result.UserExists)
            {
                // Nhập mật khẩu
                string password = txtPassword.Text;

                try
                {
                    // Đăng nhập người dùng đã tồn tại
                    var credential = EmailProvider.GetCredential(email, password);
                    return await client.SignInWithCredentialAsync(credential);
                }
                catch (FirebaseAuthException ex)
                {
                    MessageBox.Show("Incorrect password. Please try again.", "Authentication Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
            }
            else
            {
                MessageBox.Show("User not found.", "Authentication Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            txtPassword.PasswordChar = '*';
        }
    }
}
