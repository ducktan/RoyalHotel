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
        public FirebConfig config = new FirebConfig();

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

        public async void AddRoom(Room room)
        {
            var roomData = new
            {
                room.MAPH,
                room.TenPhong,
                room.LoaiPhong,
                room.TrangThai
            };
            FirebaseResponse response = await Client.SetAsync("Room/" + room.MAPH, roomData);
            MessageBox.Show("Add room success");
        }

        public async void LoadRoom(DataGridView v)
        {

            FirebaseResponse response = await Client.GetAsync("Room/");
            Dictionary<string, Room > getRoom = response.ResultAs<Dictionary<string, Room>>();
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


        public async void DeleteRoom(DataGridView v)
        {
            if (v.SelectedRows.Count > 0) // Check if any row is selected
            {
                // Get the selected row
                DataGridViewRow selectedRow = v.SelectedRows[0];

                if (selectedRow != null)
                {
                    // Extract the Room ID from the selected row (assuming it's in column 0)
                    string roomId = (string)selectedRow.Cells[0].Value;

                    // Confirmation prompt (optional)
                    if (MessageBox.Show("Are you sure you want to delete this room?", "Delete Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        try
                        {
                            // Delete the Room from Firebase
                            await Client.DeleteAsync($"Room/{roomId}");

                            // Remove the row from the DataGridView
                            v.Rows.Remove(selectedRow);

                            MessageBox.Show("Room deleted successfully!");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Error deleting room: {ex.Message}");
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a room to delete.");
            }
        }

        public async void UpdateRoom(DataGridView v)
        {
            if (v.SelectedRows.Count > 0) // Check if any row is selected
            {
                // Get the selected row
                DataGridViewRow selectedRow = v.SelectedRows[0];

                if (selectedRow != null)
                {
                    // Extract the Room ID from the selected row (assuming it's in column 0)
                    string roomId = (string)selectedRow.Cells[0].Value;

                    // Get the updated Room information from the selected row
                    Room updatedRoom = new Room
                    {
                        MAPH = (string)selectedRow.Cells[0].Value, // Assuming Room ID remains unchanged
                        TenPhong = (string)selectedRow.Cells[1].Value,
                        LoaiPhong = (string)selectedRow.Cells[2].Value,
                        TrangThai = (string)selectedRow.Cells[3].Value
                    };


                    // Confirmation prompt (modified)
                    if (MessageBox.Show("Are you sure you want to update this Room?", "Update Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        try
                        {
                            // Update the Room in Firebase
                            await Client.SetAsync($"Room/{roomId}", updatedRoom);

                            // Refresh the DataGridView (optional)
                            // v.Refresh(); // You might want to refresh only the updated row

                            MessageBox.Show("Room information updated successfully!");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Error updating Room: {ex.Message}");
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a Room to update.");
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
                // Initialize an empty list to store matching rooms
                List<Room> matchingRooms = new List<Room>();
                foreach (var Room in typeRoomList)
                {
                    // Extract room information
                    Room room = Room.Object;

                    // Check if room capacity matches the search criteria
                    if (room.TenPhong == name)
                    {
                        // Add matching room to the list
                        matchingRooms.Add(room);
                    }
                }

                // Return the list of matching rooms
                return matchingRooms;
            }
            catch (Exception ex)
            {
                // Handle exceptions (logging, throwing specific exceptions, etc.)
                Console.WriteLine($"Error searching room by capacity: {ex.Message}");
                return new List<Room>(); // Return empty list on error
            }

        }

        public async Task<List<Room>> SearchRoomByType(string type)
        {
            try
            {
                // Retrieve all room data from "Room" node
                var typeRoomList = await firebaseClient
                    .Child("Room")
                    .OnceAsync<Royal.DAO.Room>();

                // Initialize an empty list to store matching rooms
                List<Room> matchingRooms = new List<Room>();

                // Iterate through retrieved room data
                foreach (var Room in typeRoomList)
                {
                    // Extract room information
                    Room room = Room.Object;

                    // Check if room capacity matches the search criteria
                    if (room.LoaiPhong == type)
                    {
                        // Add matching room to the list
                        matchingRooms.Add(room);
                    }
                }

                // Return the list of matching rooms
                return matchingRooms;
            }
            catch (Exception ex)
            {
                // Handle exceptions (logging, throwing specific exceptions, etc.)
                Console.WriteLine($"Error searching room by capacity: {ex.Message}");
                return new List<Room>(); // Return empty list on error
            }
        }



        public async Task<List<Room>> SearchRoomByStatus(string status)
        {
            try
            {
                // Retrieve all room data from "Room" node
                var typeRoomList = await firebaseClient
                    .Child("Room")
                    .OnceAsync<Royal.DAO.Room>();

                // Initialize an empty list to store matching rooms
                List<Room> matchingRooms = new List<Room>();

                // Iterate through retrieved room data
                foreach (var Room in typeRoomList)
                {
                    // Extract room information
                    Room room = Room.Object;

                    // Check if room price falls within the specified range
                    if (room.TrangThai == status)
                    {
                        // Add matching room to the list
                        matchingRooms.Add(room);
                    }
                }

                // Return the list of matching rooms
                return matchingRooms;
            }
            catch (Exception ex)
            {
                // Handle exceptions (logging, throwing specific exceptions, etc.)
                Console.WriteLine($"Error searching room by price: {ex.Message}");
                return new List<Room>(); // Return empty list on error
            }
        }



    }
}
