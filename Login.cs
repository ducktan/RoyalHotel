using System;
using System.Text.RegularExpressions;
using System.Net.NetworkInformation;
using System.Net;
using System.Windows.Forms;
using Firebase.Auth;
using Royal.DAO;
using Lab3_Bai6;
using System.Diagnostics;

namespace Royal
{
    public partial class Login : Form
    {
        private Authen authen;

        public Login()
        {
            InitializeComponent();
            authen = new Authen();
            label3.Visible = false;
            label5.Visible = false;

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
                    Lab3_Bai6.Server server = new Server();
                    //server.Show();
                        if(!IsPortInUse(8080))
                    {
                        StartConsoleApp();
                    }

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
        public static bool IsPortInUse(int port)
        {
            bool isAvailable = true;

            // Đánh giá các kết nối TCP hiện tại trong hệ thống
            IPGlobalProperties ipGlobalProperties = IPGlobalProperties.GetIPGlobalProperties();
            TcpConnectionInformation[] tcpConnInfoArray = ipGlobalProperties.GetActiveTcpConnections();

            foreach (TcpConnectionInformation tcpi in tcpConnInfoArray)
            {
                if (tcpi.LocalEndPoint.Port == port)
                {
                    isAvailable = false;
                    break;
                }
            }

            if (isAvailable)
            {
                // Kiểm tra các listener TCP hiện tại trong hệ thống
                IPEndPoint[] listeners = ipGlobalProperties.GetActiveTcpListeners();
                foreach (IPEndPoint listener in listeners)
                {
                    if (listener.Port == port)
                    {
                        isAvailable = false;
                        break;
                    }
                }
            }

            return !isAvailable;
        }

        private void StartConsoleApp()
        {
            string exePath = @"C:\Users\ADMIN\Downloads\RoyalHotel-main\RoyalHotel-main\ChatServer.exe";

            ProcessStartInfo processStartInfo = new ProcessStartInfo
            {
                FileName = exePath,
                UseShellExecute = true,
                RedirectStandardOutput = false,
                RedirectStandardError = false,
                CreateNoWindow = false // Hiển thị cửa sổ console
            };

            try
            {
                Process consoleProcess = new Process
                {
                    StartInfo = processStartInfo
                };

                consoleProcess.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error starting console app: {ex.Message}");
            }
        }
        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            txtPassword.PasswordChar = '*';
            string password = txtPassword.Text;

            if (string.IsNullOrEmpty(password) || password.Length >= 8)
            {
                label5.Visible = false;
            }
            else
            {
                label5.Visible = true;
            }
        }


        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            ForgotPass forgot = new ForgotPass(); 
            forgot.Show();
            
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {

            string email = txtEmail.Text;
            if (!IsValidEmail(email))
            {
                label3.Visible = true;
            }
            else
            {
                label3.Visible = false;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {
            
        }

        private bool IsValidEmail(string email)
        {
            // Biểu thức chính quy để kiểm tra định dạng email
            string pattern = @"^[^\s@]+@[^\s@]+\.[^\s@]+$";

            // Kiểm tra xem email có khớp với biểu thức chính quy hay không
            return Regex.IsMatch(email, pattern);
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
