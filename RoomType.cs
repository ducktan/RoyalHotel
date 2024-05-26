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
using System.Text.RegularExpressions;


namespace Royal
{
    public partial class RoomType : Form
    {
        private Firebase.Database.FirebaseClient firebaseClient;

        public string MALPH { get;  set; }
        public string TENLPH { get;  set; }
        public object SLNG { get; set; }
        public object GIA { get; set; }

        public RoomType()
        {
            InitializeComponent();
            firebaseClient = FirebaseManage.GetFirebaseClient();
            // Assuming dataGridRoomType is a DataGridView control on your form
            Royal.DAO.RoomType roomType = new Royal.DAO.RoomType();
            roomType.LoadRoomType(dataGridRoomType);
        }


        private void RoomType_Load(object sender, EventArgs e)
        {

        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            Royal.DAO.RoomType roomType = new Royal.DAO.RoomType()
            {
                MALPH = txtRoomTypeId.Text,
                TENLPH = txtRoomTypeName.Text,
                SLNG = Int32.Parse(cboPeopleNum.Text),
                GIA = Int32.Parse(txtPrice.Text)

            };

            roomType.AddRoomType(roomType);
        }

        private void btnUpdateRoomType_Click(object sender, EventArgs e)
        {
            Royal.DAO.RoomType roomType = new Royal.DAO.RoomType();
            
        }

        private void btnDisplayRoomType_Click(object sender, EventArgs e)
        {
            // Assuming dataGridRoomType is a DataGridView control on your form
            Royal.DAO.RoomType roomType = new Royal.DAO.RoomType();
            roomType.LoadRoomType(dataGridRoomType);
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
            Royal.DAO.RoomType roomType = new Royal.DAO.RoomType(); // Assuming you have an instance

            // Call the appropriate search function based on the selected type
            List<Royal.DAO.RoomType> searchResults = new List<Royal.DAO.RoomType>(); // Initialize empty list

           
            try
            {
                if (type == "Mã loại phòng") // Search by room type ID (MALPH)
                {

                    Royal.DAO.RoomType searchResult = await roomType.SearchRoomTypeById(searchText);
                    searchResults.Add(searchResult);
                }
                else
                if (type == "Tên loại phòng") // Search by room type name (TENLPH)
                {
                    searchResults = await roomType.SearchRoomTypeByName(searchText);
                }
                else if (type == "Số lượng người") // Search by number of occupants (SLNG)
                {
                    int slngValue;
                    if (!int.TryParse(searchText, out slngValue))
                    {
                        MessageBox.Show("Invalid value for number of occupants. Please enter an integer.");
                        return;
                    }
                    searchResults = await roomType.SearchRoomTypeByCapacity(slngValue);
                }
                else if (type == "Giá") // Search by price (GIA)
                {

                    string priceText = searchText; // Remove leading/trailing whitespace

                    int priceValue;
                    if (!int.TryParse(priceText, out priceValue))
                    {
                        MessageBox.Show("Invalid value for price. Please enter an integer.");
                        return;
                    }
                    searchResults = await roomType.SearchRoomTypeByPrice(priceValue);
                }
                else
                {
                    MessageBox.Show("Invalid search type selected.");
                    return;
                }

                // Prepare UI results (assuming you want to display MALPH, TENLPH, SLNG, GIA)
                List<string[]> uiResults = searchResults.Select(room => new string[] { room.MALPH, room.TENLPH, room.SLNG.ToString(), room.GIA.ToString() }).ToList();

                // Update UI elements on the UI thread
                if (this.InvokeRequired)
                {
                    this.Invoke(new Action(() =>
                    {
                        dataGridRoomType.Rows.Clear();
                        foreach (string[] rowData in uiResults)
                        {
                            dataGridRoomType.Rows.Add(rowData);
                        }
                    }));
                }
                else
                {
                    dataGridRoomType.Rows.Clear();
                    foreach (string[] rowData in uiResults)
                    {
                        dataGridRoomType.Rows.Add(rowData);
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


        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }
    }
}
