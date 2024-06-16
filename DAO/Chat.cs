using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Drawing.Imaging;

namespace ConsoleChatServer
{
    // Đây là code của chat server, em đã build nó ra file.exe để chạy nên code này chỉ để tham khảo
    class Program
    {
        const int maxWidth = 200;
        const int maxHeight = 150;
        static bool serverRunning = false;
        static TcpListener tcpServer;
        static Thread listenThread;
        static Dictionary<string, TcpClient> dic_clients = new Dictionary<string, TcpClient>();
        static bool listening = true;
        static IPAddress ipServer = IPAddress.Any;

        static void Main(string[] args)
        {
            Console.WriteLine("Chat Server");
            StartServer();
            string command;
            do
            {
                command = Console.ReadLine();
                if (command == "//connections")
                {
                    Console.WriteLine($"Connections: {dic_clients.Count}");
                }
                else
                {
                    Broadcast("Admin", command);
                }
            } while (command != "//quit");
            StopServer();
        }

        static void AddMessage(string username, string data)
        {
            if (username == null)
            {
                Console.WriteLine(data);
            }
            else
            {
                Console.WriteLine($"{username}: {data}");
            }
        }

        static void Listen()
        {
            try
            {
                while (listening)
                {
                    TcpClient client = tcpServer.AcceptTcpClient();
                    IPEndPoint remoteIpEndPoint = (IPEndPoint)client.Client.RemoteEndPoint;
                    string clientIp = remoteIpEndPoint.Address.ToString();
                    int clientPort = remoteIpEndPoint.Port;

                    NetworkStream net_stream = client.GetStream();
                    byte[] data = new byte[1024 * 5000];
                    int byte_count = net_stream.Read(data, 0, data.Length);
                    string username = Encoding.UTF8.GetString(data, 0, byte_count);

                    if (username == "Admin" || username == "Administrator")
                    {
                        byte[] response = Encoding.UTF8.GetBytes("This username is reserved!");
                        net_stream.Write(response, 0, response.Length);
                        net_stream.Flush();
                        client.Close();
                    }
                    else if (dic_clients.ContainsKey(username))
                    {
                        byte[] response = Encoding.UTF8.GetBytes("This username has already existed!");
                        net_stream.Write(response, 0, response.Length);
                        net_stream.Flush();
                        client.Close();
                    }
                    else
                    {
                        AddMessage("Message", $"User {username} from ({clientIp}:{clientPort}) has connected successfully");
                        dic_clients.Add(username, client);
                        Thread receiveThread = new Thread(Receive)
                        {
                            IsBackground = true
                        };
                        receiveThread.Start(username);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        static void Broadcast(string username, string message, TcpClient except_this_client = null)
        {
            byte[] final = Encoding.UTF8.GetBytes($"<Message>💀{username}: {message}");
            foreach (TcpClient client in dic_clients.Values)
            {
                if (client != except_this_client)
                {
                    NetworkStream net_stream = client.GetStream();
                    net_stream.Write(final, 0, final.Length);
                    net_stream.Flush();
                }
            }
        }

        static void Receive(object obj)
        {
            string username = obj.ToString();
            TcpClient client = dic_clients[username];
            NetworkStream net_stream = client.GetStream();
            byte[] data = new byte[1024 * 10000];
            try
            {
                while (listening)
                {
                    int byte_count = net_stream.Read(data, 0, data.Length);
                    if (byte_count == 0)
                    {
                        dic_clients.Remove(username);
                        client.Close();
                        AddMessage(null, $"Client {username} disconnected.");
                        break;
                    }

                    if (IsImageData(data))
                    {
                        using (MemoryStream ms = new MemoryStream(data, 0, byte_count))
                        {
                            Image image = Image.FromStream(ms);
                            AddImageToChat(username, image);
                            SendImageBroadcast(image, client);
                        }
                    }
                    else
                    {
                        string message = Encoding.UTF8.GetString(data, 0, byte_count);
                        string[] msg = Split_Message(message);
                        if (msg[0] == "<Message>")
                        {
                            AddMessage(username, msg[1]);
                            Broadcast(username, msg[1], client);
                        }
                        else if (msg[0] == "<FileText>")
                        {
                            AddMessage(null, "Received a file");
                            ReceiveFile(msg[1]);
                            byte[] final = Encoding.UTF8.GetBytes(message);
                            foreach (TcpClient clients in dic_clients.Values)
                            {
                                if (clients != client)
                                {
                                    NetworkStream newnet_stream = clients.GetStream();
                                    newnet_stream.Write(final, 0, final.Length);
                                    newnet_stream.Flush();
                                }
                            }
                        }
                    }
                    Array.Clear(data, 0, data.Length);
                }
            }
            catch (IOException ex)
            {
                dic_clients.Remove(username);
                client.Close();
                AddMessage("Server", $"Error: Client {username} connection lost. {ex.Message}");
            }
        }

        static string[] Split_Message(string message)
        {
            string separator = "💀";
            string[] msg = message.Contains(separator) ? message.Split(new string[] { separator }, 2, StringSplitOptions.None) : new string[] { message };
            return msg;
        }

        public static bool IsPortInUse(int port)
        {
            bool isAvailable = true;
            IPGlobalProperties ipGlobalProperties = IPGlobalProperties.GetIPGlobalProperties();
            TcpConnectionInformation[] tcpConnInfoArray = ipGlobalProperties.GetActiveTcpConnections();

            foreach (TcpConnectionInformation tcpi in tcpConnInfoArray)
            {
                if (tcpi.LocalEndPoint.Port == port)
                {
                    isAvailable = false;
                    break;
                }
            }

            if (isAvailable)
            {
                IPEndPoint[] listeners = ipGlobalProperties.GetActiveTcpListeners();
                foreach (IPEndPoint listener in listeners)
                {
                    if (listener.Port == port)
                    {
                        isAvailable = false;
                        break;
                    }
                }
            }

            return !isAvailable;
        }

        static void StartServer()
        {
            int port = 8080;

            if (!serverRunning)
            {
                if (!IsPortInUse(port))
                {
                    try
                    {
                        tcpServer = new TcpListener(ipServer, port);
                        tcpServer.Start();
                        serverRunning = true;

                        AddMessage("Admin", $"{ipServer} - Waiting for connections...");
                        listenThread = new Thread(new ThreadStart(Listen));
                        listenThread.IsBackground = true;
                        listenThread.Start();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error starting server: {ex.Message}");
                    }
                }
                else
                {
                    Console.WriteLine($"Port {port} is already in use. Server is already running.");
                }
            }
        }

        static void StopServer()
        {
            if (serverRunning)
            {
                try
                {
                    listening = false;

                    foreach (TcpClient client in dic_clients.Values)
                    {
                        client.Close();
                    }

                    dic_clients.Clear();
                    tcpServer.Stop();

                    serverRunning = false;

                    AddMessage("Admin", "Server stopped.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error stopping server: {ex.Message}");
                }
            }
        }

        #region Image
        static bool IsImageData(byte[] data)
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

        static Image ResizeImage(Image image, int maxWidth, int maxHeight)
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

        static void SendImageBroadcast(Image image, TcpClient except_this_client = null)
        {
            image = ResizeImage(image, maxWidth, maxHeight);
            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, ImageFormat.Jpeg);
                byte[] imageData = ms.ToArray();

                foreach (TcpClient client in dic_clients.Values)
                {
                    if (client != except_this_client)
                    {
                        NetworkStream netStream = client.GetStream();
                        netStream.Write(imageData, 0, imageData.Length);
                        netStream.Flush();
                    }
                }
            }
        }

        static void AddImageToChat(string username, Image image)
        {
            Console.WriteLine($"{username} sent an image:");
            image = ResizeImage(image, maxWidth, maxHeight);
            string tempPath = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}.jpg");
            image.Save(tempPath, ImageFormat.Jpeg);
            Console.WriteLine($"Image saved to {tempPath}");
        }
        #endregion

        #region File
        static void SendFile(string filePath)
        {
            try
            {
                string fileName = Path.GetFileName(filePath);
                string fileContent = File.ReadAllText(filePath);

                string file = $"<FileText>💀{fileName}💀{fileContent}";

                byte[] final = Encoding.UTF8.GetBytes(file);

                foreach (TcpClient client in dic_clients.Values)
                {
                    NetworkStream netStream = client.GetStream();
                    netStream.Write(final, 0, final.Length);
                    netStream.Flush();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending file: {ex.Message}");
            }
        }

        static void ReceiveFile(string message)
        {
            string[] file = Split_Message(message);
            string fileName = file[0];
            string fileContent = file[1];

            Console.WriteLine($"Received file: {fileName}");
            Console.WriteLine("Enter the path to save the file:");
            string filePath = Console.ReadLine();

            try
            {
                File.WriteAllText(filePath, fileContent);
                Console.WriteLine("File saved successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving file: {ex.Message}");
            }
        }
        #endregion
    }
}
