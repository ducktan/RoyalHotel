using Firebase.Auth;
using Org.BouncyCastle.Asn1.X509;
using Royal.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Royal
{
    public partial class Chat : Form
    {
        private Firebase.Database.FirebaseClient firebaseClient;
        private ChatDAO chatDAO;
        public Chat()
        {
            InitializeComponent();
            firebaseClient = FirebaseManage.GetFirebaseClient();
            chatDAO = new ChatDAO();
        }

        private void cbID_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void kryptonButton5_Click(object sender, EventArgs e)
        {

        }

        private void RoleBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void chatRTB_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private async void kryptonButton1_Click(object sender, EventArgs e)
        {
            string content = textBox3.Text;
            string receiverID = cbID.SelectedItem?.ToString();
            string senderID = Royal.DAO.User.Id;
            string dateTime = DateTime.Now.ToString("HH:mm:ss ddd,dd-MM-yyyy");

            if (string.IsNullOrEmpty(receiverID))
            {
                MessageBox.Show("Please select a receiver.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ChatDAO chat = new ChatDAO
            {
                SenderID = senderID,
                ReceiverID = receiverID,
                Content = content,
                DateTime = dateTime
            };

            await chatDAO.AddChat(chat);
            MessageBox.Show("Message sent successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            string formattedMessage = $"{dateTime} - To {receiverID}: {content}\n";
            chatRTB.AppendText(formattedMessage);

            // Clear the input textbox after sending the message
            textBox3.Clear();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private async void Chat_Load(object sender, EventArgs e)
        {
            StaffDAO staff = new StaffDAO();
            await Royal.DAO.User.LoadUsersToComboBox(cbID);
            string displayText = $"{Royal.DAO.User.Role} ({Royal.DAO.User.Id})";
            await chatDAO.LoadChat(displayText, chatRTB,displayText);

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                byte[] iArray = File.ReadAllBytes(filePath);
                string imageUrl = Convert.ToBase64String(iArray);
                string receiverID = cbID.SelectedItem?.ToString();
                string senderID = Royal.DAO.User.Id;
                string dateTime = DateTime.Now.ToString("HH:mm:ss ddd,dd-MM-yyyy");

                if (string.IsNullOrEmpty(receiverID))
                {
                    MessageBox.Show("Please select a receiver.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                ChatDAO chat = new ChatDAO
                {
                    SenderID = senderID,
                    ReceiverID = receiverID,
                    Content = "[Image]",
                    DateTime = dateTime,
                    ImageURL = imageUrl // Set ImageURL in chat
                };

                await chatDAO.AddChat(chat);
                MessageBox.Show("Image sent successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                string formattedMessage = $"{dateTime} - To {receiverID}: [Image]\n";
                chatRTB.AppendText(formattedMessage);
                chatDAO.LoadImageFromURL(imageUrl, chatRTB);
            }
        }
    }
}
