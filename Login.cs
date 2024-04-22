using System;
using System.Windows.Forms;
using Firebase.Auth;
using Royal.DAO;

namespace Royal
{
    public partial class Login : Form
    {
        private Authen authen;

        public Login()
        {
            InitializeComponent();
            authen = new Authen();
        }

        private async void kryptonButton1_Click(object sender, EventArgs e)
        {
            try
            {
                string email = txtEmail.Text;
                string password = txtPassword.Text;

                // Kiểm tra xem người dùng có tồn tại không trước khi đăng nhập
                bool userExists = await authen.CheckUserExists(email);
                if (!userExists)
                {
                    MessageBox.Show("User not found.", "Authentication Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                UserCredential userCredential = await authen.SignIn(email, password);

                if (userCredential != null)
                {
                    MessageBox.Show($"You're signed in as {userCredential.User.Info.Email}", "Sign In Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            txtPassword.PasswordChar = '*';
        }
    }
}
