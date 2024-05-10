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

    }
}
