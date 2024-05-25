using FireSharp.Interfaces;
using FireSharp.Response;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Google.Apis.Requests.BatchRequest;

namespace Royal.DAO
{
    public class RoomType
    {
        private Firebase.Database.FirebaseClient firebaseClient;
        public string MALPH { get; set; }
        public string TENLPH { get; set; }
        public int SLNG { get; set; }

        public int GIA { get; set; }

        public FirebConfig config = new FirebConfig();
        public IFirebaseClient Client { get; private set; } // Make client accessible only within the class

        public RoomType() 
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

        public async void AddRoomType(RoomType roomType)
        {
            var roomData = new
            {
                roomType.MALPH,
                roomType.TENLPH,
                roomType.SLNG,
                roomType.GIA
            };
            FirebaseResponse response = await Client.SetAsync("RoomType/" + roomType.MALPH, roomData);
            MessageBox.Show("Add room success");
        }

        public async void LoadRoomType(DataGridView v)
        {

            FirebaseResponse response = await Client.GetAsync("RoomType/");
            Dictionary<string, Royal.DAO.RoomType> getRoom = response.ResultAs<Dictionary<string, Royal.DAO.RoomType>>();
            v.Rows.Clear();
            foreach (var r in getRoom)
            {
                Royal.DAO.RoomType room = r.Value;
                v.Rows.Add(
                    room.MALPH,
                    room.TENLPH,
                    room.SLNG,
                    room.GIA
                );
            }
        }



        public async void DeleteRoomType(DataGridView v)
        {
            if (v.SelectedRows.Count > 0) // Check if any row is selected
            {
                // Get the selected row
                DataGridViewRow selectedRow = v.SelectedRows[0];

                if (selectedRow != null)
                {
                    // Extract the Room ID from the selected row (assuming it's in column 0)
                    string roomTypeId = (string)selectedRow.Cells[0].Value;

                    // Confirmation prompt (optional)
                    if (MessageBox.Show("Are you sure you want to delete this room type?", "Delete Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        try
                        {
                            // Delete the Room from Firebase
                            await Client.DeleteAsync($"RoomType/{roomTypeId}");

                            // Remove the row from the DataGridView
                            v.Rows.Remove(selectedRow);

                            MessageBox.Show("Room type deleted successfully!");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Error deleting room type: {ex.Message}");
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a room type to delete.");
            }
        }


        public async void UpdateRoomType(DataGridView v)
        {
            if (v.SelectedRows.Count > 0) // Check if any row is selected
            {
                // Get the selected row
                DataGridViewRow selectedRow = v.SelectedRows[0];

                if (selectedRow != null)
                {
                    // Extract the Room ID from the selected row (assuming it's in column 0)
                    string roomTypeId = (string)selectedRow.Cells[0].Value;

                    // Get the updated Room information from the selected row
                    RoomType updatedRoomType = new RoomType
                    {
                        MALPH = (string)selectedRow.Cells[0].Value, // Assuming Room ID remains unchanged
                        TENLPH = (string)selectedRow.Cells[1].Value,
                        SLNG = (int)selectedRow.Cells[2].Value,
                        GIA = (int)selectedRow.Cells[3].Value
                    };


                    // Confirmation prompt (modified)
                    if (MessageBox.Show("Are you sure you want to update this RoomType?", "Update Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        try
                        {
                            // Update the Room in Firebase
                            await Client.SetAsync($"RoomType/{roomTypeId}", updatedRoomType);

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
                MessageBox.Show("Please select a RoomType to update.");
            }
        }
        public async Task<RoomType> SearchRoomTypeById(string id)
        {
            string queryPath = $"RoomType/{id}";

            try
            {
                FirebaseResponse response = await Client.GetAsync(queryPath);
                return response.ResultAs<RoomType>();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error searching room by ID: {ex.Message}");
                return null; // Return null on error
            }
        }

        public async Task<List<RoomType>> SearchRoomTypeByName(string name)
        {
            try
            {
                var typeRoomList = await firebaseClient
            .Child("RoomType")
            .OnceAsync<Royal.DAO.RoomType>();
                // Initialize an empty list to store matching rooms
                List<RoomType> matchingRooms = new List<RoomType>();
                foreach (var roomType in typeRoomList)
                {
                    // Extract room information
                    RoomType room = roomType.Object;

                    // Check if room capacity matches the search criteria
                    if (room.TENLPH == name)
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
                return new List<RoomType>(); // Return empty list on error
            }
       
        }

        public async Task<List<RoomType>> SearchRoomTypeByCapacity(int capacity)
        {
            try
            {
                // Retrieve all room data from "RoomType" node
                var typeRoomList = await firebaseClient
                    .Child("RoomType")
                    .OnceAsync<Royal.DAO.RoomType>();

                // Initialize an empty list to store matching rooms
                List<RoomType> matchingRooms = new List<RoomType>();

                // Iterate through retrieved room data
                foreach (var roomType in typeRoomList)
                {
                    // Extract room information
                    RoomType room = roomType.Object;

                    // Check if room capacity matches the search criteria
                    if (room.SLNG == capacity)
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
                return new List<RoomType>(); // Return empty list on error
            }
        }



        public async Task<List<RoomType>> SearchRoomTypeByPrice(int minPrice)
        {
            try
            {
                // Retrieve all room data from "RoomType" node
                var typeRoomList = await firebaseClient
                    .Child("RoomType")
                    .OnceAsync<Royal.DAO.RoomType>();

                // Initialize an empty list to store matching rooms
                List<RoomType> matchingRooms = new List<RoomType>();

                // Iterate through retrieved room data
                foreach (var roomType in typeRoomList)
                {
                    // Extract room information
                    RoomType room = roomType.Object;

                    // Check if room price falls within the specified range
                    if (room.GIA >= minPrice)
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
                return new List<RoomType>(); // Return empty list on error
            }
        }


    }
}
