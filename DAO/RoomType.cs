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
    public class RoomType
    {
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
        public async Task<List<RoomType>> SearchRoomType(string searchBy, string value = "")
        {
            // Create an empty list to store search results
            List<RoomType> searchResults = new List<RoomType>();

            // Construct the base Firebase query path
            string queryPath = "RoomType/";

            // Validate search option
            if (string.IsNullOrEmpty(searchBy) || !(searchBy == "MALPH" || searchBy == "TENLPH" || searchBy == "SLNG" || searchBy == "GIA"))
            {
                throw new ArgumentException("Invalid search option. Supported options: MALPH, TENLPH, SLNG, GIA");
            }

            // Build the query based on provided parameters
            bool hasQuery = true;

            if (searchBy == "MALPH")
            {
                queryPath += $"{value}";
            }
            else if (searchBy == "TENLPH")
            {
                queryPath += $"?orderByChild=TENLPH&equalTo={value}";
            }
            else if (searchBy == "SLNG")
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("SLNG search requires a value");
                }
                queryPath += $"?orderByChild=SLNG&equalTo={value}";
            }
            else if (searchBy == "GIA")
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("GIA search requires a value");
                }
                int gia;
                if (!int.TryParse(value, out gia))
                {
                    throw new ArgumentException("Invalid value for GIA search. Please provide a valid integer.");
                }
                queryPath += $"?orderByChild=GIA&startAt={gia}";
            }

            try
            {
                // Execute the Firebase query
                FirebaseResponse response = await Client.GetAsync(queryPath);

                // Get the search results
                Dictionary<string, RoomType> rooms = response.ResultAs<Dictionary<string, RoomType>>();

                // Add each RoomType to the search results list
                foreach (var room in rooms)
                {
                    searchResults.Add(room.Value);
                }

                return searchResults;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error searching room types: {ex.Message}");
                return searchResults;
            }
        }

    }
}
