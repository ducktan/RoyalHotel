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
    public partial class RoomType : Form
    {
        private Firebase.Database.FirebaseClient firebaseClient;
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
            string searchText = txtSearch.Text; // Assuming txtSearch is a textbox for search text
            Royal.DAO.RoomType roomType = new Royal.DAO.RoomType();

            // Validate search criteria
            if (string.IsNullOrEmpty(searchText))
            {
                MessageBox.Show("Please enter the search text.");
                return;
            }

            // Determine the search option based on the selected type
            string searchOption;
            switch (type)
            {
                case "Mã loại phòng": // Search by room type ID (MALPH)
                    searchOption = "MALPH";
                    break;
                case "Tên loại phòng": // Search by room type name (TENLPH)
                    searchOption = "TENLPH";
                    break;
                case "Số lượng người": // Search by number of occupants (SLNG)
                    searchOption = "SLNG";
                    // Validate search text for integer value (optional)
                    if (!int.TryParse(searchText, out int slngValue))
                    {
                        MessageBox.Show("Invalid value for number of occupants. Please enter an integer.");
                        return;
                    }
                    searchText = slngValue.ToString();  // Convert to string for search
                    break;
                case "Giá": // Search by price (GIA)
                    searchOption = "GIA";
                    // Validate search text for integer value (optional)
                    if (!int.TryParse(searchText, out int giaValue))
                    {
                        MessageBox.Show("Invalid value for price. Please enter an integer.");
                        return;
                    }
                    searchText = giaValue.ToString();  // Convert to string for search
                    break;
                default:
                    MessageBox.Show("Invalid search type selected.");
                    return;
            }
            // Call the search function with the appropriate option and value
            try
            {


                List<Royal.DAO.RoomType> searchResults = await roomType.SearchRoomType(searchOption, searchText);

                // Prepare UI results (assuming you want to display MALPH, TENLPH, SLNG, GIA)
                List<string[]> uiResults = new List<string[]>();
                foreach (Royal.DAO.RoomType foundRoom in searchResults)
                {
                    uiResults.Add(new string[] { foundRoom.MALPH, foundRoom.TENLPH, foundRoom.SLNG.ToString(), foundRoom.GIA.ToString() });
                }

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
