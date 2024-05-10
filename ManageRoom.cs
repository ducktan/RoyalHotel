using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FireSharp;
using Firebase.Database;
using Firebase.Database.Query;
using Royal.DAO;



namespace Royal
{
    public partial class ManageRoom : Form
    { 
       private Firebase.Database.FirebaseClient firebaseClient;
    
        public ManageRoom()
        {
            InitializeComponent();

            firebaseClient = FirebaseManage.GetFirebaseClient();
            LoadRoomTypeNameFromDB();
        }


        private async void LoadRoomTypeNameFromDB()
        {
            try
            {
                var typeRoomList = await firebaseClient
                    .Child("RoomType")
                    .OnceAsync<Royal.DAO.RoomType>();
                cboRoomType.Items.Clear();

                foreach (var roomType in typeRoomList)
                {
                    cboRoomType .Items.Add(roomType.Object.TENLPH);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu từ Firebase Realtime Database: " + ex.Message);
            }

        }


        private void kryptonButton6_Click(object sender, EventArgs e)
        {
            RoomType room = new RoomType();
            room.Show();
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            Royal.DAO.Room room = new Royal.DAO.Room()
            {
                MAPH = txtRoomID.Text,
                TenPhong = txtRoomName.Text,
                LoaiPhong = cboRoomType.Text,
                TrangThai = cboStatusRoom.Text
            };

            room.AddRoom(room);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Royal.DAO.Room room = new Royal.DAO.Room();
            room.LoadRoom(dataGridRoom);
        }

        private void btnDeleteRoom_Click(object sender, EventArgs e)
        {
            Royal.DAO.Room room = new Royal.DAO.Room();
            room.DeleteRoom(dataGridRoom);
        }

        private async void kryptonButton5_Click(object sender, EventArgs e)
        {
            // Get the search criteria from the UI controls
            string type = cboTypeSearch.Text;
            string searchText = txtSearch.Text.Trim(); // Assuming txtSearch is a textbox for search text

            // Validate search criteria
            if (string.IsNullOrEmpty(searchText))
            {
                MessageBox.Show("Please enter the search text.");
                return;
            }

            // Call the appropriate search function based on the selected type
            Royal.DAO.Room room = new Royal.DAO.Room(); // Assuming you have an instance

            // Call the appropriate search function based on the selected type
            List<Royal.DAO.Room> searchResults = new List<Royal.DAO.Room>(); // Initialize empty list

//            Mã phòng
//Loại phòng
//Tên phòng
//Trạng thái

            try
            {
                if (type == "Mã phòng") // Search by room type ID (MALPH)
                {

                    Royal.DAO.Room searchResult = await room.SearchRoomById(searchText);
                    searchResults.Add(searchResult);
                }
                else
                if (type == "Loại phòng") // Search by room type name (TENLPH)
                {
                    searchResults = await room.SearchRoomByType(searchText);
                }
                else if (type == "Tên phòng") // Search by number of occupants (SLNG)
                {

                    searchResults = await room.SearchRoomByName(searchText);
                }
                else if (type == "Trạng thái") // Search by price (GIA)
                {
                    searchResults = await room.SearchRoomByStatus(searchText);
                }
                else
                {
                    MessageBox.Show("Invalid search type selected.");
                    return;
                }



        // Prepare UI results (assuming you want to display MALPH, TENLPH, SLNG, GIA)
        List<string[]> uiResults = searchResults.Select(rooms => new string[] { rooms.MAPH, rooms.TenPhong, rooms.LoaiPhong, rooms.TrangThai }).ToList();

                // Update UI elements on the UI thread
                if (this.InvokeRequired)
                {
                    this.Invoke(new Action(() =>
                    {
                        dataGridRoom.Rows.Clear();
                        foreach (string[] rowData in uiResults)
                        {
                            dataGridRoom.Rows.Add(rowData);
                        }
                    }));
                }
                else
                {
                    dataGridRoom.Rows.Clear();
                    foreach (string[] rowData in uiResults)
                    {
                        dataGridRoom.Rows.Add(rowData);
                    }
                }

                // Handle no search results (optional)
                if (searchResults.Count == 0)
                {
                    string searchCriteria = $"Search by: {type}";
                    MessageBox.Show($"No room types found with {searchCriteria}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error searching room types: {ex.Message}");
            }
        }
    }
}
