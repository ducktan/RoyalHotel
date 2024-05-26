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
            Royal.DAO.Room room = new Royal.DAO.Room();
            room.LoadRoom(dataGridRoom);
            dataGridRoom.CellClick += dataGridRoom_CellClick;

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
                    cboRoomType.Items.Add(roomType.Object.MALPH);
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

        private async void kryptonButton1_Click(object sender, EventArgs e)
        {
            try
            {
                var roomList = await firebaseClient
                    .Child("Room")
                    .OnceAsync<Royal.DAO.Room>();

                // Tìm mã phòng lớn nhất hiện có
                int maxRoomNumber = 0;
                foreach (var roomData in roomList)
                {
                    int roomNumber = int.Parse(roomData.Object.MAPH.Substring(2));
                    if (roomNumber > maxRoomNumber)
                    {
                        maxRoomNumber = roomNumber;
                    }
                }

                string newRoomNumber = "PH" + (maxRoomNumber + 1).ToString("D3");


                Royal.DAO.Room room = new Royal.DAO.Room()
                {
                    MAPH = newRoomNumber,
                    TenPhong = txtRoomName.Text,
                    LoaiPhong = cboRoomType.Text,
                    TrangThai = cboStatusRoom.Text
                };

                room.AddRoom(room);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Royal.DAO.Room room = new Royal.DAO.Room();
            room.LoadRoom(dataGridRoom);
        }

        private void btnDeleteRoom_Click(object sender, EventArgs e)
        {
            try
            {
                string id = txtRoomID.Text;
                Royal.DAO.Room room = new Royal.DAO.Room();
                room.DeleteRoom(id);
                room.LoadRoom(dataGridRoom);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

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

        private void btnUpdateRoom_Click(object sender, EventArgs e)
        {
            try
            {
                string id = txtRoomID.Text;
                string name = txtRoomName.Text;
                string loaiphong = cboRoomType.Text;
                string trangthai = cboStatusRoom.Text;

                Royal.DAO.Room room = new Royal.DAO.Room();
                room.UpdateRoom(id, name, loaiphong, trangthai);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error searching room types: {ex.Message}");
            }


        }

        private void dataGridRoom_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Kiểm tra xem có hàng nào được chọn không
            {
                DataGridViewRow selectedRow = dataGridRoom.Rows[e.RowIndex];

                // Kiểm tra nếu hàng không rỗng
                if (!selectedRow.IsNewRow)
                {
                    txtRoomID.Text = (string)selectedRow.Cells[0].Value;
                    txtRoomName.Text = (string)selectedRow.Cells[1].Value;
                    cboRoomType.Text = (string)selectedRow.Cells[2].Value;
                    cboStatusRoom.Text = (string)selectedRow.Cells[3].Value;

                }
            }
        }
    }
}
