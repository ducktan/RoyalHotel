using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Royal.DAO;

namespace Royal
{

    public partial class ForgotPass : Form
    {
        private Authen authen;
        public ForgotPass()
        {
            InitializeComponent();
            authen = new Authen();
            label3.Visible = false;
        }

        private void ForgotPass_Load(object sender, EventArgs e)
        {

        }

        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show(); 
            this.Close();
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text;
            authen.ForgotPass(email);
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

        private bool IsValidEmail(string email)
        {
            // Biểu thức chính quy để kiểm tra định dạng email
            string pattern = @"^[^\s@]+@[^\s@]+\.[^\s@]+$";

            // Kiểm tra xem email có khớp với biểu thức chính quy hay không
            return Regex.IsMatch(email, pattern);
        }
    }
}
