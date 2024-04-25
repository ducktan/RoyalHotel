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

namespace Royal
{
    public partial class Admin : Form
    {
        private AdminDAO adminDAO;

        public Admin()
        {
            InitializeComponent();
            adminDAO = new AdminDAO();
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
                // Gọi phương thức Signup từ đối tượng AdminDAO để tạo tài khoản người dùng mới
                await adminDAO.Signup(email, pass);

                // Hiển thị thông báo khi tạo tài khoản thành công
                MessageBox.Show("Tài khoản đã được tạo thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (FirebaseAuthException ex)
            {
                // Xử lý lỗi khi tạo tài khoản
                MessageBox.Show(ex.Message, "Lỗi khi tạo tài khoản", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}