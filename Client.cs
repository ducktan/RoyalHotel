using System;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using Royal.DAO;

namespace Lab3_Bai6
{
    public partial class Client : Form
    {
        public Client()
        {
            InitializeComponent();
        }

        #region Global
        const int maxWidth = 200;
        const int maxHeight = 150;
        private bool isConnected = false;
        private TcpClient tcpClient;
        private Thread clientThread;
        private bool connecting = true;
        NetworkStream net_stream;
        #endregion

        // Hàm thêm tin nhắn vào chatBox
        private delegate void SafeCallDelegate(string username, string data);
        private void AddMessage(string username, string data)
        {
            if (rtbChatBox.InvokeRequired)
            {
                var method = new SafeCallDelegate(AddMessage);
                rtbChatBox.Invoke(method, new object[] { username, data });
            }
            else
            {
                if (username == null)
                {
                    rtbChatBox.AppendText(data + Environment.NewLine);
                }
                else
                {
                    rtbChatBox.AppendText($"{username}: {data}" + Environment.NewLine);
                }   
                ScrollToBottom();
            }
        }
        // Button gửi tin nhắn
        private void btnSend_Click(object sender, EventArgs e)
        {
            if (isConnected == false)
            {
                return;
            }
            if (string.IsNullOrEmpty(txtMessage.Text.Trim()))
            {
                txtMessage.Clear();
                return;
            }

            NetworkStream net_stream = tcpClient.GetStream();
            byte[] message = Encoding.UTF8.GetBytes("<Message>💀" + txtMessage.Text.Trim());
            net_stream.Write(message, 0, message.Length);
            net_stream.Flush();

            AddMessage("Me", txtMessage.Text.Trim());
            txtMessage.Clear();
        }
        // Cơ bản cho enter chatBox
        private void txtMessage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSend.PerformClick();
            }
        }
        // Hàm tự động kéo xuống cùng
        private void ScrollToBottom()
        {
            rtbChatBox.SelectionStart = rtbChatBox.Text.Length;
            rtbChatBox.ScrollToCaret();
        }

        #region Client
        // Kiểm tra xem dữ liệu nhận có phải là ảnh không
        private bool IsImageData(byte[] data)
        {
            try
            {
                using (var ms = new MemoryStream(data))
                {
                    Image.FromStream(ms);
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
        // Hàm nhận dữ liệu từ luồng 
        private void Receive()
        {
            net_stream = tcpClient.GetStream();
            byte[] data = new byte[1024*10000];
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
                        AddMessage(null, "Server disconnected.");

                        this.Invoke((MethodInvoker)delegate
                        {
                            btnConnect.Enabled = true;
                            //txtServerIP.ReadOnly = txtServerPort.ReadOnly = txtUsername.ReadOnly = !txtServerIP.Enabled;
                            this.Text = "TCP Client";
                            btnConnect.Text = "Connect";
                        });

                        break;
                    }

                    if (byte_count > 0)
                    {
                        if (IsImageData(data))
                        {
                            byte[] imageData = new byte[byte_count];
                            Array.Copy(data, 0, imageData, 0, byte_count);

                            using (var ms = new MemoryStream(data, 0, byte_count))
                            {
                                this.Invoke((MethodInvoker)delegate
                                {
                                    Image image = Image.FromStream(ms);
                                    AddImageToChat(Royal.DAO.User.Role, image);
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
                                    //txtServerIP.ReadOnly = txtServerPort.ReadOnly = txtUsername.ReadOnly = !txtServerIP.Enabled;
                                    btnConnect.Enabled = true;
                                    //txtUsername.Focus();
                                });
                                isConnected = false;
                            }
                            else
                            {
                                if (msg[0] == "<Message>")
                                {
                                    AddMessage(null, msg[1]);
                                } 
                                else if (msg[0] == "<FileText>")
                                {
                                    AddMessage(null,"Đã nhận được 1 file");
                                    ReceiveFile(msg[1]);
                                    return;
                                }
                            }
                        }
                    }
                    Array.Clear(data, 0, 1024*10000);
                }
            }
            catch
            {
                connecting = false;
                isConnected = false;
                AddMessage(null, $"Đã rời khỏi server");

                this.Invoke((MethodInvoker)delegate
                {
                    btnConnect.Enabled = true;
                    //txtServerIP.ReadOnly = txtServerPort.ReadOnly = txtUsername.ReadOnly = !txtServerIP.Enabled;
                    this.Text = "TCP Client";
                });
            }
        }
        // Tách chuỗi
        private string[] Split_Message(string message)
        {
            string separator = "💀";
            string[] msg = message.Contains(separator) ? message.Split(new string[] 
            { separator }, 2, StringSplitOptions.None) : new string[] { message };
            return msg;
        }
        // Button dùng để kết nối và thoát server
        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (btnConnect.Text == "Connect")
            {
                ConnectServer();
                btnSend.Enabled = true;
                btnSendFile.Enabled = true;
                btnConnect.Text = "Exit";
            }
            else if(btnConnect.Text == "Exit")
            {
                ExitServer();
                btnConnect.Text = "Connect";
            }
        }
        // Hàm kết nối server
        private void ConnectServer()
        {
            try
            {
                // Tiến hành kết nối đến server
                IPEndPoint tcpServer = new IPEndPoint(IPAddress.Parse("127.0.0.1"), int.Parse("8080"));
                tcpClient = new TcpClient();
                tcpClient.Connect(tcpServer);

                // Gửi username đến server 
                NetworkStream net_stream = tcpClient.GetStream();
                byte[] message = Encoding.UTF8.GetBytes(Royal.DAO.User.Role);
                net_stream.Write(message, 0, message.Length);
                net_stream.Flush();

                // Khởi động tiến trình client
                clientThread = new Thread(Receive);
                clientThread.IsBackground = true;
                clientThread.Start();

                //txtServerIP.ReadOnly = txtServerPort.ReadOnly = txtUsername.ReadOnly = !txtServerIP.ReadOnly;
                isConnected = true;
                //this.Text += ": " + txtUsername.Text;
                rtbChatBox.Clear();
            }
            catch
            {
                MessageBox.Show("Connetion failed!");
            }
        }
        // Hàm thoát server
        private void ExitServer()
        {
            if (!isConnected) return;
            if (isConnected)
            {
                try
                {
                    net_stream = tcpClient.GetStream();
                    byte[] message = Encoding.UTF8.GetBytes("//disconnect");
                    net_stream.Write(message, 0, message.Length);
                    net_stream.Flush();
                }
                catch (Exception)
                {
                }
                finally
                {
                    connecting = false;

                    tcpClient.Close();
                    net_stream.Close();

                    tcpClient = null;
                    isConnected = false;
                    //txtServerIP.ReadOnly = txtServerPort.ReadOnly = txtUsername.ReadOnly = !txtServerIP.Enabled;
                    AddMessage(null, "Disconnected...");
                    this.Text = "TCP Client";
                }
            }
        }
        #endregion

        // button gửi file và ảnh
        private void btnSendFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "File|*.*";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                if (IsImageFile(filePath))
                {
                    Image image = Image.FromFile(filePath);
                    AddImageToChat("Me", image);
                    SendImageToServer(image);
                }
                else
                {
                    SendFile(filePath);
                }
            }
        }

        #region Image
        // Resize lại ảnh theo kích thước của chatBox
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
        // Thêm ảnh vào chatbox
        private void AddImageToChat(string username, Image image)
        {
            rtbChatBox.AppendText(Environment.NewLine);
            rtbChatBox.Select(rtbChatBox.Text.Length, 0);
            rtbChatBox.SelectionColor = rtbChatBox.ForeColor;
            rtbChatBox.AppendText($"{username}: ");
            rtbChatBox.Select(rtbChatBox.Text.Length, 0);

            rtbChatBox.ReadOnly = false;
            image = ResizeImage(image, maxWidth, maxHeight);

            if (rtbChatBox.InvokeRequired)
            {
                rtbChatBox.BeginInvoke((MethodInvoker)delegate {
                    Clipboard.SetImage(image);
                    rtbChatBox.Paste();
                });
            }
            else
            {
                Clipboard.SetImage(image);
                rtbChatBox.Paste();
            }
            rtbChatBox.ScrollToCaret();
            rtbChatBox.ReadOnly = true;
            rtbChatBox.AppendText(Environment.NewLine);
        }
        // Gửi ảnh cho server
        private void SendImageToServer(Image image)
        {
            Image resizedImage = ResizeImage(image, maxWidth, maxHeight);

            using (MemoryStream ms = new MemoryStream())
            {
                resizedImage.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                byte[] imageData = ms.ToArray();

                NetworkStream net_stream = tcpClient.GetStream();
                net_stream.Write(imageData, 0, imageData.Length);
                net_stream.Flush();
            }
        }
        // Check file ảnh
        private bool IsImageFile(string filePath)
        {
            try
            {
                using (var image = Image.FromFile(filePath))
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region File

        // Nhận file
        private void ReceiveFile(string message)
        {
            string[] file = Split_Message(message);
            string fileName = file[0];
            string fileContent = file[1];

            this.Invoke((MethodInvoker)delegate
            {
                Button downloadButton = new Button();
                downloadButton.Text = fileName;
                downloadButton.Tag = fileContent;
                downloadButton.AutoSize = true;

                flpFile.Controls.Add(downloadButton);

                downloadButton.Click += (sender, e) =>
                {
                    using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                    {
                        saveFileDialog.FileName = fileName;
                        saveFileDialog.Filter = "Text file|*.txt|All files|*.*";

                        if (saveFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            string filePath = saveFileDialog.FileName;

                            try
                            {
                                File.WriteAllText(filePath, downloadButton.Tag.ToString());
                                MessageBox.Show("File saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show($"Error saving file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                };
            });
        }
        // Gửi file 
        private void SendFile(string filePath)
        {
            try
            {
                // Đọc nội dung file
                string fileName = Path.GetFileName(filePath);
                string fileContent = File.ReadAllText(filePath);

                // Tạo chuỗi định dạng "tên file - nội dung file"
                string file = $"<FileText>💀{fileName}💀{fileContent}";

                byte[] final = Encoding.UTF8.GetBytes(file);

                NetworkStream netStream = tcpClient.GetStream();
                netStream.Write(final, 0, final.Length);
                netStream.Flush();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending file: {ex.Message}");
            }
        }
        #endregion

        private void Client_Load(object sender, EventArgs e)
        {
            btnSend.Enabled = false;
            btnSendFile.Enabled = false;
        }
    }
}