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
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Royal
{
    public partial class Chat : Form
    {
        TcpClient client = null;
        NetworkStream stream = null;
        Thread clientThread = null;
        public Chat()
        {
            InitializeComponent();
        }

        private void ConnectToServer()
        {
            try
            {
                string UserName = $"{Royal.DAO.User.Id} {Royal.DAO.User.Role}";
                client = new TcpClient("127.0.0.1", 8080);
                stream = client.GetStream();
                byte[] usernameBuffer = Encoding.ASCII.GetBytes(UserName);
                stream.Write(usernameBuffer, 0, usernameBuffer.Length);

                while (true)
                {
                    byte[] buffer = new byte[client.ReceiveBufferSize];
                    int bytesRead = stream.Read(buffer, 0, buffer.Length);
                    string header = Encoding.UTF8.GetString(buffer, 0, 4);

                    if (header == "TEXT")
                    {
                        string message = Encoding.UTF8.GetString(buffer, 4, bytesRead - 4);
                        DisplayMessage(message);
                    }
                    else if (header == "IMAG")
                    {
                        using (MemoryStream ms = new MemoryStream(buffer, 4, bytesRead - 4))
                        {
                            Image image = Image.FromStream(ms);
                            DisplayIMG(image);
                        }
                    }
                }
            }
            catch (SocketException ex)
            {
                DisplayMessage("SocketException: " + ex.Message);
            }
            catch (Exception ex)
            {
                DisplayMessage("Exception: " + ex.Message);
            }
        }

        private void DisplayMessage(string message)
        {
            if (chatRTB.InvokeRequired)
            {
                chatRTB.Invoke(new Action<string>(DisplayMessage), new object[] { message });
            }
            else
            {
                chatRTB.AppendText(message + Environment.NewLine);
            }
        }

        private void DisplayIMG(Image img)
        {
            if(InvokeRequired)
            {
                this.Invoke(new Action<Image>(DisplayIMG), new object[] { img });
                return;
            }
            InsertImage(img);
        }

        private void SendMessage(string message)
        {
            if (stream != null)
            {
                string fullMessage = $"{Royal.DAO.User.Id}: {textBox3.Text}";
                byte[] messageBytes = Encoding.UTF8.GetBytes(message);
                byte[] buffer = new byte[messageBytes.Length + 4];
                Buffer.BlockCopy(Encoding.ASCII.GetBytes("TEXT"), 0, buffer, 0, 4);
                Buffer.BlockCopy(messageBytes, 0, buffer, 4,messageBytes.Length);

                stream.Write(buffer, 0, buffer.Length);
                DisplayMessage("You: " + fullMessage + message);
            }
        }

        private void SendIMG(string filePath)
        {
            if (stream != null)
            {
                try
                {
                    byte[] imageBytes = File.ReadAllBytes(filePath);
                    string fullMessage = $"{Royal.DAO.User.Id} sent an image";
                    byte[] messageBytes = Encoding.ASCII.GetBytes(fullMessage);
                    byte[] buffer = new byte[4 + messageBytes.Length + imageBytes.Length];

                    Buffer.BlockCopy(Encoding.ASCII.GetBytes("IMAG"), 0, buffer, 0, 4);
                    Buffer.BlockCopy(messageBytes, 0, buffer, 4, messageBytes.Length);
                    Buffer.BlockCopy(imageBytes, 0, buffer, 4 + messageBytes.Length, imageBytes.Length);

                    stream.Write(buffer, 0, buffer.Length);
                    DisplayMessage(fullMessage);
                    InsertImage(Image.FromFile(filePath));
                }
                catch (Exception ex)
                {
                    DisplayMessage("Error sending image: " + ex.Message);
                }
            }
        }

        private void InsertImage(Image image)
        {
            Clipboard.SetImage(image);
            chatRTB.ReadOnly = false;
            int start = chatRTB.TextLength;
            chatRTB.Paste();
            chatRTB.Select(start, chatRTB.TextLength - start);
            chatRTB.SelectionAlignment = HorizontalAlignment.Left;
            chatRTB.AppendText(Environment.NewLine);
            chatRTB.ReadOnly = true;
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
            string message = textBox3.Text;
            if(!string.IsNullOrEmpty(message))
            {
                SendMessage(message);
                textBox3.Clear();
            }
        }

        private async void Chat_Load(object sender, EventArgs e)
        {
            

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";

            if(ofd.ShowDialog() == DialogResult.OK)
            {
                SendIMG(ofd.FileName);
            }

        }

        private void ConnectBTN_Click(object sender, EventArgs e)
        {
            string username = $"{Royal.DAO.User.Id} {Royal.DAO.User.Role}";
            if(string.IsNullOrEmpty(username) )
            {
                MessageBox.Show("name is invalid");
                return;
            }

          clientThread = new Thread(new ThreadStart(ConnectToServer));
            clientThread.Start();
        }
    }
}
