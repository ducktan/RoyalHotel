using System;
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

namespace Royal.DAO
{
    public class receiveroomDAO
    {
        private Firebase.Database.FirebaseClient firebaseClient;
        public string ReceivedRoom { get; set; }
        public string CCCD_KH { get; set; }
        public string ID_PHIEUNHAN { get; set; }
        public string ID_LOAIPHONG { get; set; }
        public string MAPHONG { get; set; }
        public string TRANGTHAI { get; set; }
        public int SONGUOI { get; set; }
        public string NGAYNHAN { get; set; }
        public string NGAYTRA { get; set; }
        public string HoTen { get; set; }
        public int TIENCOC { get; set; }

        // Set up Firebase configuration
        public FirebConfig config = new FirebConfig();

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
                receiveRoom.TIENCOC
            };

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
        public async Task<receiveroomDAO> SearchReceiveRoomByID(string idPhieuNhan)
        {
            try
            {
                var receiveRoomList = await firebaseClient
                    .Child("ReceiveRoom")
                    .OnceAsync<receiveroomDAO>();

                foreach (var receiveRoomSnapshot in receiveRoomList)
                {
                    var receiveRoom = receiveRoomSnapshot.Object;

                    // Check if ID_PHIEUNHAN of receive room matches the search criteria
                    if (receiveRoom.ReceivedRoom == idPhieuNhan)
                    {
                        // Return the matching receive room
                        return receiveRoom;
                    }
                }

                // Return null if no matching receive room is found
                return null;
            }
            catch (Exception ex)
            {
                // Handle exceptions (logging, throwing specific exceptions, etc.)
                MessageBox.Show($"Error searching receive room: {ex.Message}");
                return null; // Return null on error
            }
        }
        public async Task UpdateReceiveRoom(receiveroomDAO receiveRoom)
        {
            if (MessageBox.Show("Are you sure you want to update this receive room?", "Update Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
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
}
