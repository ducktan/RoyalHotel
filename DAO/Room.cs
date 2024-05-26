using FireSharp.Interfaces;
using FireSharp.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Royal.DAO
{
    public class Room
    {
        private Firebase.Database.FirebaseClient firebaseClient;
        public string MAPH { get; set; }
        public string LoaiPhong { get; set; }
        public string TenPhong { get; set; }
        public string TrangThai { get; set; }

        // Set up Firebase configuration
        private readonly FirebConfig config = new FirebConfig();

        // Initialize Firebase client
        public IFirebaseClient Client { get; private set; } // Make client accessible only within the class

        public Room()
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
            // Initialize client upon object creation
        }

        public async void LoadRoom(DataGridView v)
        {
            try
            {
                FirebaseResponse response = await Client.GetAsync("Room/");
                Dictionary<string, Room> getRoom = response.ResultAs<Dictionary<string, Room>>();
                v.Rows.Clear();
                foreach (var r in getRoom)
                {
                    Room room = r.Value;
                    v.Rows.Add(
                        room.MAPH,
                        room.TenPhong,
                        room.LoaiPhong,
                        room.TrangThai
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading rooms: {ex.Message}");
            }
        }

        public async void DeleteRoom(string id)
        {
            if (MessageBox.Show("Are you sure you want to delete this Room?", "Delete Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes) // Check if any row is selected
            {
                try
                {
                    await Client.DeleteAsync($"Room/{id}");
                    MessageBox.Show("Room deleted successfully!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting room: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Please select a room to delete.");
            }
        }

        public async void UpdateRoom(string sId, string sName, string LoaiPhong, string TrangThai)
        {
            var room = new Room()
            {
                MAPH = sId,
                TenPhong = sName,
                LoaiPhong = LoaiPhong,
                TrangThai = TrangThai
            };

            if (MessageBox.Show("Are you sure you want to update this room?", "Update Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    await Client.SetAsync($"Room/{sId}", room);
                    MessageBox.Show("Room's information updated successfully!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error updating room: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Please select a room to update.");
            }
        }

        public async void AddRoom(Room room)
        {
            try
            {
                var roomData = new
                {
                    room.MAPH,
                    room.TenPhong,
                    room.LoaiPhong,
                    room.TrangThai
                };
                await Client.SetAsync("Room/" + room.MAPH, roomData);
                MessageBox.Show("Room added successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding room: {ex.Message}");
            }
        }

        public async Task<Room> SearchRoomById(string id)
        {
            string queryPath = $"Room/{id}";

            try
            {
                FirebaseResponse response = await Client.GetAsync(queryPath);
                return response.ResultAs<Room>();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error searching room by ID: {ex.Message}");
                return null; // Return null on error
            }
        }

        public async Task<List<Room>> SearchRoomByName(string name)
        {
            try
            {
                var typeRoomList = await firebaseClient
                    .Child("Room")
                    .OnceAsync<Royal.DAO.Room>();

                List<Room> matchingRooms = new List<Room>();
                foreach (var Room in typeRoomList)
                {
                    Room room = Room.Object;
                    if (room.TenPhong == name)
                    {
                        matchingRooms.Add(room);
                    }
                }

                return matchingRooms;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error searching room by name: {ex.Message}");
                return new List<Room>(); // Return empty list on error
            }
        }

        public async Task<List<Room>> SearchRoomByType(string type)
        {
            try
            {
                var typeRoomList = await firebaseClient
                    .Child("Room")
                    .OnceAsync<Royal.DAO.Room>();

                List<Room> matchingRooms = new List<Room>();

                foreach (var Room in typeRoomList)
                {
                    Room room = Room.Object;
                    if (room.LoaiPhong == type)
                    {
                        matchingRooms.Add(room);
                    }
                }

                return matchingRooms;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error searching room by type: {ex.Message}");
                return new List<Room>(); // Return empty list on error
            }
        }

        public async Task<List<Room>> SearchRoomByStatus(string status)
        {
            try
            {
                var typeRoomList = await firebaseClient
                    .Child("Room")
                    .OnceAsync<Royal.DAO.Room>();

                List<Room> matchingRooms = new List<Room>();

                foreach (var Room in typeRoomList)
                {
                    Room room = Room.Object;
                    if (room.TrangThai == status)
                    {
                        matchingRooms.Add(room);
                    }
                }

                return matchingRooms;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error searching room by status: {ex.Message}");
                return new List<Room>(); // Return empty list on error
            }
        }
    }
}
