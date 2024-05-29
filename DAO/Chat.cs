using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Firebase.Database;
using Firebase.Database.Query;

namespace Royal.DAO
{
    public class ChatDAO
    {
        private Firebase.Database.FirebaseClient firebaseClient;
        public string SenderID { get; set; }
        public string ReceiverID { get; set; }
        public string Content { get; set; }
        public string DateTime { get; set; }
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
                chat.DateTime
            };

            FirebaseResponse response = await Client.SetAsync("Chat/" + chat.ReceiverID + "/" + chat.SenderID + "/" + chat.DateTime, ChatData);
        }

        public async Task LoadChat(string receiverID, RichTextBox richTextBox, string senderID)
        {
            try
            {
                FirebaseResponse response = await Client.GetAsync("Chat/" + receiverID + "/");
                if (richTextBox.Text != null)
                {
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
                MessageBox.Show($"An error occurred while loading the chat: {ex.Message}", "Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public async Task LoadChatForSender(string senderID, RichTextBox richTextBox)
        {
            try
            {
                FirebaseResponse response = await Client.GetAsync("Chat/");
                if (richTextBox.Text != null)
                {
                    if (response.Body != "null")
                    {
                        
                    }
                }
                else
                {
                    richTextBox.AppendText("No chat history found.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading the chat: {ex.Message}", "Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void SetupRealTimeListener(string userID, RichTextBox richTextBox)
        {
            firebaseClient
                .Child("Chat")
                .Child(userID)
                .AsObservable<ChatDAO>()
                .Subscribe(d =>
                {
                    if (d.Object != null)
                    {
                        var chat = d.Object;
                        string formattedMessage = $"{chat.DateTime} - {chat.SenderID}: {chat.Content}\n";
                        richTextBox.Invoke(new Action(() =>
                        {
                            richTextBox.AppendText(formattedMessage);
                        }));
                    }
                });
        }

    }
}
