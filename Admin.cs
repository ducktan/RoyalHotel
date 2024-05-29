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
            DAO.User.LoadUser(dataGridStaff);
            dataGridStaff.CellClick += dataGridRoomType_CellClick;
        }

        private void IndexBut_Click(object sender, EventArgs e)
        {
            Dashboard d = new Dashboard();
            d.Show();
            this.Close();
        }

        private void dataGridRoomType_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Kiểm tra xem có hàng nào được chọn không
            {
                DataGridViewRow selectedRow = dataGridStaff.Rows[e.RowIndex];

                // Kiểm tra nếu hàng không rỗng
                if (!selectedRow.IsNewRow)
                {
                    Username.Text = selectedRow.Cells[0].Value.ToString();
                }
            }
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

                
                Permission permission = new Permission();
                string role = await permission.GetUserRoleByEmail(email);

                if(role== null)
                {
                    MessageBox.Show("Email không thuộc nhân viên trong công ty.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    return;
                }

                bool result = await DAO.User.CreateUser(email, role);
                // Thực hiện đăng ký tài khoản mới
                await authen.Signup(email, pass);

                if (result) 

                // Hiển thị thông báo khi tạo tài khoản thành công
                MessageBox.Show("Tài khoản đã được tạo thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DAO.User.LoadUser(dataGridStaff);
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
                label5.Text = "Đặt lại mật khẩu an toàn hơn!.";
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

        private void Parameter_button_Click(object sender, EventArgs e)
        {
            Authentication p = new Authentication();
            p.Show();
        }

        private async void Del_But_Click(object sender, EventArgs e)
        {
            await authen.DeleteUserFromAuth(Username.Text, Password.Text);
            await DAO.User.DeleteUser(Username.Text);
            DAO.User.LoadUser(dataGridStaff);
        }


    }
}