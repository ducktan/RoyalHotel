using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using System;
using System.IO;
using System.Collections.Generic;
using System.Drawing;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;
using Firebase.Database;
using ComponentFactory.Krypton.Toolkit;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.Linq;
using iTextSharp.text.pdf.codec;

namespace Royal.DAO
{
    public class ChatDAO
    {
        private Firebase.Database.FirebaseClient firebaseClient;
        public string SenderID { get; set; }
        public string ReceiverID { get; set; }
        public string Content { get; set; }
        public string DateTime { get; set; }
        public string ImageURL { get; set; } // New property for image URL
        private readonly FirebConfig firebaseConfig = new FirebConfig();
        public IFirebaseClient Client { get; private set; }

        public ChatDAO()
        {
            try
            {
                Client = new FireSharp.FirebaseClient(firebaseConfig.Config);
                firebaseClient = FirebaseManage.GetFirebaseClient();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Connection Fail");
            }
        }

        public async Task AddChat(ChatDAO chat)
        {
            var ChatData = new
            {
                chat.SenderID,
                chat.ReceiverID,
                chat.Content,
                chat.DateTime,
                chat.ImageURL
            };

            FirebaseResponse response = await Client.SetAsync("Chat/" + chat.ReceiverID + "/" + chat.SenderID + "/" + chat.DateTime, ChatData);
        }

        public async Task LoadChat(string receiverID, RichTextBox richTextBox, string senderID)
        {
            try
            {
                FirebaseResponse response = await Client.GetAsync("Chat/" + receiverID + "/");
                if (response.Body != "null")
                {
                    var allSenderChats = response.ResultAs<Dictionary<string, Dictionary<string, ChatDAO>>>();

                    foreach (var senderChats in allSenderChats)
                    {
                        foreach (var chatEntry in senderChats.Value)
                        {
                            var chat = chatEntry.Value;
                            string formattedMessage = $"{chat.DateTime} - {chat.SenderID}: {chat.Content}\n";
                            richTextBox.AppendText(formattedMessage);
                            if (!string.IsNullOrEmpty(chat.ImageURL))
                            {
                                // Display image from URL
                                LoadImageFromURL(chat.ImageURL, richTextBox);
                            }
                        }
                    }

                    var allReceiverChats = response.ResultAs<Dictionary<string, Dictionary<string, Dictionary<string, ChatDAO>>>>();

                    foreach (var receiverChats in allReceiverChats)
                    {
                        if (receiverChats.Value.ContainsKey(senderID))
                        {
                            var senderChats = receiverChats.Value[senderID];
                            foreach (var chatEntry in senderChats)
                            {

                                var chat = chatEntry.Value;

                                string formattedMessage = $"{chat.DateTime} - To {receiverChats.Key}: {chat.Content}\n";
                                richTextBox.AppendText(formattedMessage);
                                if (!string.IsNullOrEmpty(chat.ImageURL))
                                {
                                    // Display image from URL
                                    LoadImageFromURL(chat.ImageURL, richTextBox);
                                    richTextBox.AppendText("\n");
                                }
                            }
                        }
                    }
                }
                else
                {
                    richTextBox.AppendText("No chat history found.");
                }
            }
            catch (Exception ex)
            {
                
            }
        }


        public void LoadImageFromURL(string imageURL, RichTextBox richTextBox)
        {
            try
            {
                    byte[] imageBytes = Convert.FromBase64String(imageURL);
                    using (System.IO.MemoryStream ms = new System.IO.MemoryStream(imageBytes))
                    {
                        Image img = Image.FromStream(ms);
                        Clipboard.SetImage(img);
                        richTextBox.Paste();
                    }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading the image: {ex.Message}", "Load Image Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

       
    }
}
