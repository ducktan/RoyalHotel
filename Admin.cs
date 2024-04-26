using Royal.DAO;
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
using System.Text.RegularExpressions;

namespace Royal
{
    public partial class Admin : Form
    {
        private Authen authen;
        private CheckPass checkPass;

        public Admin()
        {
            InitializeComponent();
            authen = new Authen();
            checkPass = new CheckPass();
            label3.Visible = false;
            label5.Visible = false;
        }

        private void IndexBut_Click(object sender, EventArgs e)
        {
            Dashboard d = new Dashboard();
            d.Show();
            this.Close();
        }

        private async void Create_But_Click(object sender, EventArgs e)
        {
            string email = Username.Text;
            string pass = Password.Text;

            try
            {
                bool userExists = await authen.CheckUserExists(email);
                if (userExists)
                {
                    MessageBox.Show("Email đã tồn tại trong hệ thống. Vui lòng sử dụng một địa chỉ email khác.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Thực hiện đăng ký tài khoản mới
                await authen.Signup(email, pass);


                // Hiển thị thông báo khi tạo tài khoản thành công
                MessageBox.Show("Tài khoản đã được tạo thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (FirebaseAuthException ex)
            {
                // Xử lý lỗi khi tạo tài khoản
                MessageBox.Show(ex.Message, "Lỗi khi tạo tài khoản", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Password_TextChanged(object sender, EventArgs e)
        {
            Password.PasswordChar = '*';
            string password = Password.Text;

            if (string.IsNullOrEmpty(password) || password.Length < 8)
            {
                label5.Visible = true;
                return;
            }

            // Kiểm tra mật khẩu có đủ mạnh không
            if (!checkPass.IsStrongPassword(password))
            {
                label5.Text = "Mật khẩu cần chứa ít nhất một ký tự \nin hoa, một ký tự thường, một số và\nmột ký tự đặc biệt.";
                label5.Height = 30;
                Create_But.Location = new Point(15, 205);
                label5.Visible = true;
            }
            else
            {
                label5.Visible = false;
            }
        }

        private void Username_TextChanged(object sender, EventArgs e)
        {
            string email = Username.Text;
            if (!IsValidEmail(email))
            {
                label3.Visible = true;
            }
            else
            {
                label3.Visible = false;
            }
        }

        private bool IsValidEmail(string email)
        {
            // Biểu thức chính quy để kiểm tra định dạng email
            string pattern = @"^[^\s@]+@[^\s@]+\.[^\s@]+$";

            // Kiểm tra xem email có khớp với biểu thức chính quy hay không
            return Regex.IsMatch(email, pattern);
        }


    }
}