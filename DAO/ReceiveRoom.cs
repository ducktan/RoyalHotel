﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Windows.Forms;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Firebase.Database;
using iTextSharp.text;

namespace Royal.DAO
{
    public class receiveroomDAO
    {
        private Firebase.Database.FirebaseClient firebaseClient;
        public string ReceivedRoom { get; set; }
        public string CCCD_KH { get; set; }

        public string MAPHONG { get; set; }
        public string TRANGTHAI { get; set; }
        public int SONGUOI { get; set; }
        public string NGAYNHAN { get; set; }
        public string NGAYTRA { get; set; }
        public string HoTen { get; set; }
        public int TIENCOC { get; set; }

        public string MAHD { get; set; }
        // Set up Firebase configuration
        private readonly FirebConfig config = new FirebConfig();

        // Initialize Firebase client
        public IFirebaseClient Client { get; private set; }
        public receiveroomDAO()
        {
            try
            {
                Client = new FireSharp.FirebaseClient(config.Config);
                firebaseClient = FirebaseManage.GetFirebaseClient();
            }
            catch
            {
                MessageBox.Show("Connection fail!");
            }
        }
        public async Task AddReceiveRoom(receiveroomDAO receiveRoom)
        {

            var receiveRoomData = new
            {
                receiveRoom.ReceivedRoom,
                receiveRoom.CCCD_KH,
                receiveRoom.HoTen,
                receiveRoom.MAPHONG,
                receiveRoom.TRANGTHAI,
                receiveRoom.NGAYNHAN,
                receiveRoom.NGAYTRA,
                receiveRoom.SONGUOI,
                receiveRoom.TIENCOC,
                receiveRoom.MAHD
            };
            // Get the current row count for the "Bill" table


            FirebaseResponse response = await Client.SetAsync("ReceiveRoom/" + receiveRoom.ReceivedRoom, receiveRoomData);
            MessageBox.Show("Added receive room");
        }

        public async void LoadReceiveRooms(DataGridView v)
        {
            try
            {
                // Get response from Firebase
                FirebaseResponse response = await Client.GetAsync("ReceiveRoom/");

                // Check if response is successful and body is not empty
                if (response != null && !string.IsNullOrEmpty(response.Body))
                {
                    // Deserialize the response to a dictionary
                    Dictionary<string, receiveroomDAO> receiveRoomsDict = JsonConvert.DeserializeObject<Dictionary<string, receiveroomDAO>>(response.Body);

                    // Clear the DataGridView before loading new data
                    v.Rows.Clear();

                    // Populate the DataGridView with the receive room data
                    foreach (var room in receiveRoomsDict.Values)
                    {
                        // Add row to DataGridView
                        v.Rows.Add(
                            room.ReceivedRoom,
                            room.HoTen,
                             room.CCCD_KH,
                             room.MAPHONG,
                            room.TRANGTHAI,
                            room.NGAYNHAN,
                            room.NGAYTRA,
                            room.SONGUOI,
                            room.TIENCOC

                        );
                    }
                }
                else
                {
                    MessageBox.Show("Empty response from Firebase", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading receive rooms: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public async void DeleteReceiveRoom(string receiveRoomId)
        {
            if (MessageBox.Show("Are you sure you want to delete this receive room?", "Delete Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    await Client.DeleteAsync($"ReceiveRoom/{receiveRoomId}");
                    MessageBox.Show("Receive room deleted successfully!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting receive room: {ex.Message}");
                }
            }
        }
        public async Task<List<receiveroomDAO>> SearchReceiveRoomByID(string idPhieuNhan)
        {
            try
            {
                var receiveRoomList = await firebaseClient
                    .Child("ReceiveRoom")
                    .OnceAsync<receiveroomDAO>();

                var matchingReceiveRooms = new List<receiveroomDAO>();

                foreach (var receiveRoomSnapshot in receiveRoomList)
                {
                    var receiveRoom = receiveRoomSnapshot.Object;

                    // Check if ID_PHIEUNHAN of receive room matches the search criteria
                    if (receiveRoom.CCCD_KH == idPhieuNhan)
                    {
                        // Add the matching receive room to the list
                        matchingReceiveRooms.Add(receiveRoom);
                    }
                }

                // Return the list of matching receive rooms
                return matchingReceiveRooms;
            }
            catch (Exception ex)
            {
                // Handle exceptions (logging, throwing specific exceptions, etc.)
                MessageBox.Show($"Error searching receive room: {ex.Message}");
                return null; // Return null on error
            }
        }
        public async Task<receiveroomDAO> SearchReceiveRoomByIDDP(string idPhieuNhan, string maphong, string ngaynhan, string ngaytra)
        {
            try
            {
                var receiveRoomList = await firebaseClient
                    .Child("ReceiveRoom")
                    .OnceAsync<receiveroomDAO>();

                var matchingReceiveRooms = new List<receiveroomDAO>();

                foreach (var receiveRoomSnapshot in receiveRoomList)
                {
                    var receiveRoom = receiveRoomSnapshot.Object;

                    // Check if ID_PHIEUNHAN of receive room matches the search criteria
                    if (receiveRoom.CCCD_KH == idPhieuNhan && (receiveRoom.MAPHONG == maphong) && (receiveRoom.NGAYNHAN == ngaynhan) && (receiveRoom.NGAYTRA == ngaytra))
                    {
                        return receiveRoom;

                    }
                }

                return null;

                // Return the list of matching receive rooms

            }
            catch (Exception ex)
            {
                // Handle exceptions (logging, throwing specific exceptions, etc.)
                MessageBox.Show($"Error searching receive room: {ex.Message}");
                return null; // Return null on error
            }
        }

        public async Task<string> FindMAKH(string cccd)
        {
            try
            {
                // Truy vấn Firebase Realtime Database để lấy danh sách khách hàng
                var customerList = await firebaseClient
                    .Child("Customer")
                    .OnceAsync<CustomerDAO>();


                // Thêm ID_KH từ các bản ghi khách hàng vào ComboBox
                foreach (var customer in customerList)
                {
                    // Combine MAKH and TENKH for display
                    string customerDisplayText = customer.Object.MAKH;
                    string cccdCus = customer.Object.CCCD;
                    if (cccd == cccdCus)
                    {
                        return customerDisplayText;
                    }
                }
                return null;
            }

            catch (Exception ex)
            {
                // Xử lý ngoại lệ nếu có
                MessageBox.Show("Lỗi khi tải dữ liệu từ Firebase Realtime Database: " + ex.Message);
                return "";
            }
        }
        public async Task<int> FindCoupon(string cccd)
        {
            try
            {
                // Truy vấn Firebase Realtime Database để lấy danh sách khách hàng
                var customerList = await firebaseClient
                    .Child("Customer")
                    .OnceAsync<CustomerDAO>();

                // Truy vấn Firebase Realtime Database để lấy danh sách loại khách hàng
                var typeCustomerList = await firebaseClient
                    .Child("CustomerType")
                    .OnceAsync<CustomerType>();

                // Tìm khách hàng có CCCD khớp
                var customer = customerList.FirstOrDefault(c => c.Object.CCCD == cccd);
                if (customer != null)
                {
                    // Lấy ID_LOAIKH từ khách hàng tìm được
                    string customerTypeId = customer.Object.ID_LOAIKH;

                    // Tìm loại khách hàng theo ID_LOAIKH
                    var customerType = typeCustomerList.FirstOrDefault(t => t.Object.ID_LOAIKH == customerTypeId);
                    if (customerType != null)
                    {
                        // Trả về discount nếu tìm thấy loại khách hàng
                        return customerType.Object.DISCOUNT;
                    }
                }

                // Trả về 0 nếu không tìm thấy khách hàng hoặc loại khách hàng phù hợp
                return 0;
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ nếu có
                MessageBox.Show("Lỗi khi tải dữ liệu từ Firebase Realtime Database: " + ex.Message);
                return 0;
            }
        }
        public async Task UpdateReceiveRoom(receiveroomDAO receiveRoom)
        {

                try
                {
          

                        await Client.UpdateAsync($"ReceiveRoom/{receiveRoom.ReceivedRoom}", receiveRoom);
                        MessageBox.Show("Receive room information updated successfully!");
                   
                    

                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error updating receive room: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            
        }

    }
}
