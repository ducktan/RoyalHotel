using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using FirebaseAdmin.Auth;
using FireSharp.Interfaces;
using FireSharp.Response;

namespace Royal.DAO
{


    public static class User
    {
        private static Firebase.Database.FirebaseClient firebaseClient;
        public static string Id { get; set; }
        public static string UserId { get; set; }
        public static string Email { get; set; }
        public static string Role { get; set; }
        public static string ProfilePictureBase64 { get; set; }

        private static readonly IFirebaseClient Client;

        static User()
        {
            var config = new FirebConfig(); // Assuming you have this configuration class
            Client = new FireSharp.FirebaseClient(config.Config);
            firebaseClient = FirebaseManage.GetFirebaseClient();
        }

        public static async Task<bool> UploadProfilePicture(string filePath)
        {
            try
            {
                byte[] imageArray = File.ReadAllBytes(filePath);
                ProfilePictureBase64 = Convert.ToBase64String(imageArray);

                // Find user ID by email
                var sanitizedEmail = Email.Replace(".", "_").Replace("@", "_");
                FirebaseResponse response = await Client.GetAsync("Users");
                var allUsers = response.ResultAs<Dictionary<string, dynamic>>();
                var userId = allUsers?.FirstOrDefault(u => u.Value.Email == Email).Key;

                if (userId != null)
                {
                    var update = new Dictionary<string, object>
                    {
                        { "ProfilePictureBase64", ProfilePictureBase64 }
                    };

                    await Client.UpdateAsync($"Users/{userId}", update);

                    return true;
                }
                else
                {
                    MessageBox.Show("User not found in the database.", "Upload Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while uploading the profile picture: {ex.Message}", "Upload Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static async Task<Image> GetProfilePicture()
        {
            try
            {
                // Find user ID by email
                var sanitizedEmail = Email.Replace(".", "_").Replace("@", "_");
                FirebaseResponse response = await Client.GetAsync("Users");
                var allUsers = response.ResultAs<Dictionary<string, dynamic>>();
                var userId = allUsers?.FirstOrDefault(u => u.Value.Email == Email).Key;

                if (userId != null)
                {
                    FirebaseResponse picResponse = await Client.GetAsync($"Users/{userId}/ProfilePictureBase64");
                    ProfilePictureBase64 = picResponse.ResultAs<string>();

                    if (!string.IsNullOrEmpty(ProfilePictureBase64))
                    {
                        byte[] imageArray = Convert.FromBase64String(ProfilePictureBase64);
                        using (MemoryStream ms = new MemoryStream(imageArray))
                        {
                            return Image.FromStream(ms);
                        }
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while retrieving the profile picture: {ex.Message}", "Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }



        public static async Task<bool> CreateUser(string email, string role)
        {
            try
            {
                FirebaseResponse response = await Client.GetAsync("Users");
                var allUsers = response.ResultAs<Dictionary<string, dynamic>>();

                int maxUserId = 0;
                if (allUsers != null)
                {
                    foreach (var user in allUsers)
                    {
                        string userId = user.Key;
                        if (userId.StartsWith("US"))
                        {
                            if (int.TryParse(userId.Substring(2), out int idNumber))
                            {
                                if (idNumber > maxUserId)
                                {
                                    maxUserId = idNumber;
                                }
                            }
                        }
                    }
                }

                string newUserId = "US" + (maxUserId + 1).ToString("D3");

                var newUser = new
                {
                    UserId = newUserId,
                    Email = email,
                    Role = role,
                    ProfilePictureBase64 = ""
                };

                SetResponse setResponse = await Client.SetAsync($"Users/{newUserId}", newUser);

                if (setResponse.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    MessageBox.Show("User created successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return true;
                }
                else
                {
                    MessageBox.Show("Failed to create user.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while creating the user: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }


        public static async Task<bool> DeleteUser(string email)
        {
            try
            {
                FirebaseResponse response = await Client.GetAsync("Users");
                var allUsers = response.ResultAs<Dictionary<string, dynamic>>();
                var userId = allUsers?.FirstOrDefault(u => u.Value.Email == email).Key;

                if (userId != null)
                {
                    FirebaseResponse deleteResponse = await Client.DeleteAsync($"Users/{userId}");

                    if (deleteResponse.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        MessageBox.Show("User deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("Failed to delete user.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
                else
                {
                    MessageBox.Show("User not found in the database.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while deleting the user: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static async void LoadUser(DataGridView gridView)
        {
            try
            {
                FirebaseResponse response = await Client.GetAsync("Users/");
                var allUsers = response.ResultAs<Dictionary<string, dynamic>>();

                gridView.Rows.Clear();
                foreach (var user in allUsers)
                {
                    gridView.Rows.Add(
                        user.Value.Email,
                        user.Value.Role
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading users: {ex.Message}", "Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static async Task LoadUsersToComboBox(ComboBox comboBox)
        {
            try
            {
                FirebaseResponse response = await Client.GetAsync("Users/");
                var allUsers = response.ResultAs<Dictionary<string, dynamic>>();

                comboBox.Items.Clear();
                foreach (var user in allUsers)
                {
                    if (user.Value.UserID != User.Id)
                    {
                        string displayText = $"{user.Value.Role} ({user.Value.UserID})";
                        comboBox.Items.Add(displayText);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading users: {ex.Message}", "Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static async Task<string> GetUserIdByEmail(string email)
        {
            try
            {
                var sanitizedEmail = email.Replace(".", "_").Replace("@", "_");
                FirebaseResponse response = await Client.GetAsync("Users");
                var allUsers = response.ResultAs<Dictionary<string, dynamic>>();
                var userId = allUsers?.FirstOrDefault(u => u.Value.Email == email).Key;

                if (userId != null)
                {
                    return userId;
                }
                else
                {
                    MessageBox.Show("User not found in the database.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while retrieving the user ID: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
    }
}
