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

namespace Royal
{


    


    public partial class Profile : Form
    {
        private Authen authen;
        private Firebase.Database.FirebaseClient firebaseClient;

        public Profile()
        {
            InitializeComponent();
            firebaseClient = FirebaseManage.GetFirebaseClient();
            authen = new Authen();
            LoadUserInfor();

        }

        private async void LoadUserInfor()
        {
            try
            {
                txtEmail.Text = User.Email;
                txtRole.Text = User.Role;

                StaffDAO staffDAO = new StaffDAO();
                StaffDAO result = await staffDAO.GetUserInforByEmail(txtEmail.Text);

                if (result != null)
                {
                    txtCCCD.Text = result.staffCCCD;
                    txtName.Text = result.staffName;
                    txtAdress.Text = result.staffAdd;
                    txtPhone.Text = result.staffPhone;
                    cbSex.Text = result.staffGender;

                    // Parse and display the date of birth
                    if (DateTime.TryParse(result.staffBirth, out DateTime birthDate))
                    {
                        dtBirth.Value = birthDate; // Assuming dtBirth is a DateTimePicker
                    }
                    else
                    {
                        // Handle the case where the date is not in a valid format
                        MessageBox.Show("Invalid date format for birth date.", "Date Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    if(DateTime.TryParse(result.staffDateIn, out DateTime inDate))
                    {
                        dtCome.Value = inDate;
                    }
                    else
                    {
                        // Handle the case where the date is not in a valid format
                        MessageBox.Show("Invalid date format for start date.", "Date Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                     DisplayProfilePicture();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading user information: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Profile_Load(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private async void button2_Click(object sender, EventArgs e)
        {
            try
            {

                    StaffDAO staffDAO = new StaffDAO();
                    StaffDAO result = await staffDAO.GetUserInforByEmail(txtEmail.Text);
                    await staffDAO.UpdateStaff(result.StaffID, result.staffName, txtCCCD.Text, result.staffType, txtPhone.Text, result.staffEmail,dtBirth.Value.ToString(), txtAdress.Text, cbSex.Text, dtCome.Value.ToString());

               

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

 

        private async void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtNewPass.Text != txtConfirm.Text)
                {
                    MessageBox.Show("Mật khẩu xác nhận không chính xác!", "Xác nhận mật khẩu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Show confirmation message box
                var confirmationResult = MessageBox.Show(
                    "Bạn có chắc chắn muốn đổi mật khẩu?",
                    "Xác nhận đổi mật khẩu",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (confirmationResult == DialogResult.Yes)
                {
                     await authen.ChangePassword(User.Email, txtPass.Text, txtNewPass.Text);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Get the path of specified file
                    string filePath = openFileDialog.FileName;

                    // Upload the profile picture
                    var result = await User.UploadProfilePicture(filePath);

                    if (result)
                    {
                        MessageBox.Show("Profile picture uploaded successfully.");
                        LoadUserInfor();
                    }
                }
            }
        }

        private async void DisplayProfilePicture()
        {
            Image profilePicture = await User.GetProfilePicture();
            if (profilePicture != null)
            {
                pictureBoxProfile.Image = profilePicture;
            }
            else
            {
                MessageBox.Show("No profile picture found.");
            }
        }

        private void txtPass_TextChanged(object sender, EventArgs e)
        {
            txtPass.PasswordChar = '*';
           
        }

        private void txtNewPass_TextChanged(object sender, EventArgs e)
        {
            txtNewPass.PasswordChar = '*';
        }

        private void txtConfirm_TextChanged(object sender, EventArgs e)
        {
            txtConfirm.PasswordChar = '*';
        }
    }
}
