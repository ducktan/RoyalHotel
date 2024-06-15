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
using System.Linq.Expressions;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Interop;

namespace Royal
{
    public partial class Chat : Form
    {
        TcpClient client = null;
        NetworkStream stream = null;
        Random rand = new Random();
        public Chat()
        {
            InitializeComponent();
        }
        #region Global
        TcpClient tcpClient = null;
        NetworkStream net_stream = null;
        Thread clientThread = null;
        const int maxWidth = 200;
        const int maxHeight = 150;
        bool isConnected = false;
        bool connecting = true;
        #endregion

        private void AddMessage(string username, string data)
        {
            if(textBox3.InvokeRequired)
            {
                var method = new Action<string, string>(AddMessage);
                textBox3.Invoke(method, new object[] {username, data});
            }
            else
            {
                if(username == null)
                {
                    chatRTB.AppendText(data + Environment.NewLine);
                }
                else
                {
                    chatRTB.AppendText($"{username}: {data}" + Environment.NewLine);
                }
                ScrollToBottom();
            }
        }

        private void ScrollToBottom()
        {
            chatRTB.SelectionStart = chatRTB.Text.Length;
            chatRTB.ScrollToCaret();
        }

        private bool IsImageData(byte[] data, int byte_count)
        {
            try
            {
                using (var ms = new MemoryStream(data, 0, byte_count))
                {
                    Image.FromStream(ms);
                    return true;
                }
            }
            catch { return false; }
        }

        private void Receive()
        {
            net_stream = tcpClient.GetStream();
            byte[] data = new byte[1024 * 10000];
            try
            {
                while (connecting && tcpClient.Connected)
                {
                    int byte_count = net_stream.Read(data, 0, data.Length);

                    if (byte_count == 0)
                    {
                        tcpClient.Close();
                        connecting = false;
                        isConnected = false;
                        AddMessage(null, "Server disconnected");

                        this.Invoke((MethodInvoker)delegate
                        {
                            ConnectBTN.Enabled = true;
                            ConnectBTN.Text = "Connect";
                        });
                        break;
                    }

                    if (byte_count > 0)
                    {
                        if (IsImageData(data, byte_count))
                        {
                            byte[] imageData = new byte[byte_count];
                            Array.Copy(data, 0, imageData, 0, byte_count);

                            using (var ms = new MemoryStream(data, 0, byte_count))
                            {
                                this.Invoke((MethodInvoker)delegate
                                {
                                    Image image = Image.FromStream(ms);
                                    AddImageToChat("Admin", image);
                                });
                            }
                        }
                        else
                        {
                            string message = Encoding.UTF8.GetString(data, 0, byte_count);
                            string[] msg = Split_Message(message);
                            if (msg[0] == "This username is reserved!" || msg[0] == "This username has already existed!")
                            {
                                AddMessage(null, message);
                                tcpClient.Close();
                                this.Invoke((MethodInvoker)delegate
                                {
                                    ConnectBTN.Enabled = true;
                                    textBox3.Focus();
                                });
                                isConnected = false;
                            }
                            else
                            {
                                if (msg[0] == "<Message>")
                                {
                                    AddMessage(null, msg[1]);
                                }
                            }
                        }
                    }
                    Array.Clear(data, 0, data.Length);
                }
            }
            catch
            {
                connecting = false;
                isConnected = false;
                AddMessage(null, $"Disconnected from server");

                this.Invoke((MethodInvoker)delegate
                {
                    ConnectBTN.Enabled = true;
                });
            }
        }


        private string[] Split_Message(string message)
        {
            string separator = "💀";
            string[] msg = message.Contains(separator) ? message.Split(new string[] { separator }, 2, StringSplitOptions.None) : new string[] { message };
            return msg;
        }

        private void SendTextMessage(string message)
        {
            if (isConnected == false)
            {
                return;
            }
            if (string.IsNullOrEmpty(message.Trim()))
            {
                return;
            }

            NetworkStream net_stream = tcpClient.GetStream();
            byte[] data = Encoding.UTF8.GetBytes("<Message>💀" + message.Trim());
            net_stream.Write(data, 0, data.Length);
            net_stream.Flush();

            AddMessage("Me", message.Trim());
        }

        private void SendImage(Image image)
        {
            if (!isConnected)
            {
                return;
            }

            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                byte[] imageData = ms.ToArray();
                byte[] imageLengthPrefix = BitConverter.GetBytes(imageData.Length);
                byte[] messageTypePrefix = Encoding.UTF8.GetBytes("IMG");

                NetworkStream net_stream = tcpClient.GetStream();
                net_stream.Write(messageTypePrefix, 0, messageTypePrefix.Length); // Send the type prefix
                net_stream.Write(imageLengthPrefix, 0, imageLengthPrefix.Length); // Send the length prefix
                net_stream.Write(imageData, 0, imageData.Length); // Send the image data
                net_stream.Flush();
            }

            AddImageToChat("Me", image);
        }

        private void AddImageToChat(string username, Image image)
        {
            chatRTB.AppendText(Environment.NewLine);
            chatRTB.Select(chatRTB.Text.Length, 0);
            chatRTB.SelectionColor = chatRTB.ForeColor;
            chatRTB.AppendText($"{username}: ");
            chatRTB.Select(chatRTB.Text.Length, 0);

            chatRTB.ReadOnly = false;
            image = ResizeImage(image, maxWidth, maxHeight);

            if (chatRTB.InvokeRequired)
            {
                chatRTB.BeginInvoke((MethodInvoker)delegate
                {
                    Clipboard.SetImage(image);
                    chatRTB.Paste();
                });
            }
            else
            {
                Clipboard.SetImage(image);
                chatRTB.Paste();
            }
            chatRTB.ScrollToCaret();
            chatRTB.ReadOnly = true;
            chatRTB.AppendText(Environment.NewLine);
        }

        private Image ResizeImage(Image image, int maxWidth, int maxHeight)
        {
            int newWidth, newHeight;
            double aspectRatio = (double)image.Width / image.Height;

            if (image.Width > maxWidth)
            {
                newWidth = maxWidth;
                newHeight = (int)(newWidth / aspectRatio);
            }
            else if (image.Height > maxHeight)
            {
                newHeight = maxHeight;
                newWidth = (int)(newHeight * aspectRatio);
            }
            else
            {
                newWidth = image.Width;
                newHeight = image.Height;
            }

            return new Bitmap(image, newWidth, newHeight);
        }

        private void ConnectToServer()
        {
            try
            {
                IPEndPoint tcpServer = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8080); // Replace with your server IP and port
                tcpClient = new TcpClient();
                tcpClient.Connect(tcpServer);

                NetworkStream net_stream = tcpClient.GetStream();
                byte[] message = Encoding.UTF8.GetBytes($"{rand.Next(1,50)}"); // Replace with your username
                net_stream.Write(message, 0, message.Length);
                net_stream.Flush();

                clientThread = new Thread(Receive); // Use Thread directly
                clientThread.IsBackground = true;
                clientThread.Start();

                isConnected = true;
                this.Invoke((MethodInvoker)delegate
                {
                    ConnectBTN.Enabled = false;
                    textBox3.Focus();
                });
            }
            catch
            {
                MessageBox.Show("Connection failed!");
            }
        }

        private void SendIMG(string fileName)
        {
            Image image = Image.FromFile(fileName);
            SendImage(image);
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
            SendTextMessage(textBox3.Text);
        }

        private async void Chat_Load(object sender, EventArgs e)
        {
            

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                SendIMG(ofd.FileName);
            }
        }

        private void ConnectBTN_Click(object sender, EventArgs e)
        {
            ConnectToServer();
        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            
        }
    }
}
