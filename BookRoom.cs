using System;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;
using Firebase.Database.Query;
using FireSharp.Extensions;
using FireSharp.Response;
using MetroFramework.Controls;
using Newtonsoft.Json;
using Royal.DAO;
using static System.Windows.Forms.AxHost;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
namespace Royal
{
    public partial class BookRoom : Form
    {
        private Firebase.Database.FirebaseClient firebaseClient;

        public BookRoom()
        {
            InitializeComponent();
            firebaseClient = FirebaseManage.GetFirebaseClient();
            bookingroomDAO newBook = new bookingroomDAO();
            newBook.LoadBookingRooms(dataGridViewParameter);

            LoadMaLPFromDatabase();
            // Set the MinDate properties to prevent selecting past dates
            kryptonDateTimePicker2.MinDate = DateTime.Today;
            kryptonDateTimePicker1.MinDate = DateTime.Today.AddDays(1);

            // Set event handlers for date validation and difference calculation
            kryptonDateTimePicker2.ValueChanged += kryptonDateTimePicker2_ValueChanged_1;
            kryptonDateTimePicker1.ValueChanged += kryptonDateTimePicker1_ValueChanged_1;
        }

        private async void LoadMaLPFromDatabase()
        {
            try
            {
                // Truy vấn Firebase Realtime Database để lấy danh sách loại phòng
                cbRoomType.Items.Clear();

                var typeRoomList = await firebaseClient
            .Child("RoomType")
            .OnceAsync<Royal.DAO.RoomType>();

                foreach (var roomType in typeRoomList)
                {
                    // Combine MALPH for display
                    string roomTypeDisplayText = roomType.Object.MALPH;
                    cbRoomType.Items.Add(roomTypeDisplayText);
                }
                LoadRoomIDs();
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ nếu có
                MessageBox.Show("Lỗi khi tải dữ liệu từ Firebase Realtime Database: " + ex.Message);
            }
        }


        private async void LoadRoomIDs()
        {
            try
            {
                string selectedRoomType = cbRoomType.SelectedItem != null ? cbRoomType.SelectedItem.ToString() : "";

                var roomListSnapshot = await firebaseClient.Child("Room")
                                                          .OrderBy("LoaiPhong")
                                                          .EqualTo(selectedRoomType)
                                                          .OnceSingleAsync<Dictionary<string, Room>>();


                metroComboBox2.Items.Clear();

                if (roomListSnapshot != null)
                {
                    foreach (var room in roomListSnapshot)
                    {
                        string roomID = room.Key;
                        if (room.Value.LoaiPhong == selectedRoomType)
                        {
                            // Lấy start và end date từ các controls trong form của bạn
                            DateTime startDate = kryptonDateTimePicker2.Value;
                            DateTime endDate = kryptonDateTimePicker1.Value;
                            bookingroomDAO newBook = new bookingroomDAO();
                            // Kiểm tra tính khả dụng của phòng cho thời gian đã chọn
                            bool isRoomAvailable = await newBook.IsRoomAvailableInTimePeriod(roomID, startDate, endDate);

                            if (isRoomAvailable)
                            {
                                // Kiểm tra xem mã phòng đã tồn tại trong danh sách chưa
                                if (!metroComboBox2.Items.Contains(roomID))
                                {
                                    metroComboBox2.Items.Add(roomID);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading room IDs: {ex.Message}");
            }
        }






        private async void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                // Get the search criteria from the UI controls
                string searchText = textBox2.Text.Trim();

                if (string.IsNullOrEmpty(searchText))
                {
                    MessageBox.Show("Please enter the search text.");
                    return;
                }

                // Initialize Firebase client
                Firebase.Database.FirebaseClient firebaseClient = FirebaseManage.GetFirebaseClient();

                // Query Firebase Realtime Database to find customers with matching CCCD
                var customerList = await firebaseClient
                    .Child("Customer")
                    .OnceAsync<CustomerDAO>();

                // Filter the list of customers based on the search criteria
                var matchingCustomers = customerList
                    .Select(c => c.Object)
                    .Where(c => c.CCCD == searchText)
                    .ToList();

                // Check if any matching customer found
                if (matchingCustomers.Count > 0)
                {
                    // Get the first matching customer (assuming CCCD is unique)
                    CustomerDAO matchingCustomer = matchingCustomers.First();

                    // Update UI controls with customer information
                    textBox12.Text = matchingCustomer.MAKH;
                    textBox3.Text = matchingCustomer.HOTEN;
                    textBox4.Text = matchingCustomer.CCCD;
                    textBox5.Text = matchingCustomer.SDT;
                    textBox11.Text = matchingCustomer.EMAIL;
                    kryptonDateTimePicker3.Value = DateTime.Parse(matchingCustomer.NGSINH);
                    textBox6.Text = matchingCustomer.DIACHI;
                    cbSex.SelectedItem = matchingCustomer.GIOITINH;
                    cbNationality.SelectedItem = matchingCustomer.QUOCTICH;

                    // Update cbCustomerType directly with the ID_LOAIKH
                    textBox1.Text = matchingCustomer.ID_LOAIKH;
                }
                else
                {
                    MessageBox.Show("No customer found", "Search Results");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error searching customer: {ex.Message}");
            }

        }

        private async void button2_Click(object sender, EventArgs e)
        {
            try
            {
                // Initialize Firebase client
                Firebase.Database.FirebaseClient firebaseClient = FirebaseManage.GetFirebaseClient();

                // Get the current rooms from the "ReservedRoom" table
                var currentRooms = await firebaseClient
                    .Child("ReservedRoom")
                    .OnceAsync<bookingroomDAO>();




                int maxRoomNumber = 0;
                foreach (var roomData in currentRooms)
                {
                    int roomNumber = int.Parse(roomData.Object.ID_DATPHONG.Substring(4));
                    if (roomNumber > maxRoomNumber)
                    {
                        maxRoomNumber = roomNumber;
                    }
                }
                string newID_DATPHONG = "IDDP" + (maxRoomNumber + 1).ToString("D3");

                // Get the form values
                string selectedRoomType = cbRoomType.SelectedItem != null ? cbRoomType.SelectedItem.ToString() : "";
                string maphong = metroComboBox2.SelectedItem != null ? metroComboBox2.Text : "";
                int maxCapacity = int.Parse(textBox9.Text);

                // Parse the value from metroComboBox1 to get the number of guests
                int numberOfGuests = 0;
                int.TryParse(metroComboBox1.SelectedItem != null ? metroComboBox1.SelectedItem.ToString() : "", out numberOfGuests);

                // Check if the number of guests exceeds the maximum capacity
                if (numberOfGuests > maxCapacity)
                {
                    MessageBox.Show("Number of guests exceeds maximum capacity.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    DateTime ngayDat = kryptonDateTimePicker2.Value;
                    DateTime ngayTra = kryptonDateTimePicker1.Value;

                    // Calculate the number of days between the booking dates
                    int numberOfDays = (ngayTra - ngayDat).Days;

                    // Get the daily rate from textBox10
                    int dailyRate = int.Parse(textBox10.Text);

                    // Calculate the total cost
                    int totalCost = dailyRate * numberOfDays;

                    // Calculate the deposit (30% of the total cost)
                    int deposit = totalCost * 30 / 100;

                    // Create a new bookingroomDAO object with form control values
                    bookingroomDAO bookingroom = new bookingroomDAO()
                    {
                        CCCD_KH = textBox2.Text,
                        ID_DATPHONG = newID_DATPHONG,
                        MAPHONG = maphong,
                        ID_LOAIPHONG = selectedRoomType, // Use the selectedRoomType variable
                        NGAYDAT = ngayDat.ToString("yyyy-MM-dd"),
                        NGAYTRA = ngayTra.ToString("yyyy-MM-dd"),
                        SONGUOI = numberOfGuests, // Use the numberOfGuests variable
                        TIENCOC = deposit
                    };

                    // Call the AddBookingRoom method to store the booking room object in the Firebase database
                    await bookingroom.AddBookingRoom(bookingroom);

                    // Refresh the DataGridView with the latest data
                    bookingroom.LoadBookingRooms(dataGridViewParameter);
                    ClearInputFields();
                    if (checkBox1.Checked)
                    {
                        ReiceiveRoom reiceive = new ReiceiveRoom();
                        reiceive.Show();
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding booking room: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void ClearInputFields()
        {
            textBox2.Clear();
            textBox12.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox11.Clear();
            textBox6.Clear();
            textBox7.Clear();
            textBox8.Clear();
            textBox9.Clear();
            textBox10.Clear();
            kryptonDateTimePicker2.Value = DateTime.Now;
            kryptonDateTimePicker1.Value = DateTime.Now;
            cbRoomType.SelectedIndex = -1;
            metroComboBox2.SelectedIndex = -1;
            metroComboBox1.SelectedIndex = -1;
            cbSex.SelectedIndex = -1;
            cbNationality.SelectedIndex = -1;
            textBox1.Clear();
        }







        private void UpdateDateDifference()
        {
            // Calculate the difference in days between the check-in and check-out dates
            TimeSpan difference = kryptonDateTimePicker1.Value - kryptonDateTimePicker2.Value;
            int differenceInDays = (int)difference.TotalDays;

            if (differenceInDays < 0)
            {
                // If the difference is negative, prompt the user to choose the dates again
                MessageBox.Show("Please choose a check-out date that is after the check-in date.", "Invalid Dates", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Perform additional actions based on date difference
            LoadRoomIDs();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ClearInputFields();
        }

        private async void cbRoomType_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            try
            {
                if (cbRoomType.SelectedItem == null)
                {
                    return; // Exit if no room type is selected
                }

                // Get the selected room type code from the ComboBox
                string selectedRoomTypeCode = cbRoomType.SelectedItem.ToString();

                // Initialize Firebase client
                Firebase.Database.FirebaseClient firebaseClient = FirebaseManage.GetFirebaseClient();

                // Query Firebase Realtime Database to find the room type details
                var roomTypeResponse = await firebaseClient
                    .Child("RoomType")
                    .Child(selectedRoomTypeCode)
                    .OnceSingleAsync<DAO.RoomType>();

                // Check if room type details found
                if (roomTypeResponse != null)
                {
                    // Update UI controls with room type details
                    // Assuming textBox7 for MALPH, textBox8 for TENLPH, textBox9 for SLNG, textBox10 for GIA
                    textBox7.Text = roomTypeResponse.MALPH;
                    textBox8.Text = roomTypeResponse.TENLPH;
                    textBox9.Text = roomTypeResponse.SLNG != null ? roomTypeResponse.SLNG.ToString() : string.Empty;
                    textBox10.Text = roomTypeResponse.GIA != null ? roomTypeResponse.GIA.ToString() : string.Empty;
                }
                else
                {
                    MessageBox.Show("Room type details not found", "Error");
                }
                LoadRoomIDs();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error retrieving room type details: {ex.Message}", "Error");
            }

        }

        private void kryptonDateTimePicker2_ValueChanged_1(object sender, EventArgs e)
        {
            if (kryptonDateTimePicker2.Value > kryptonDateTimePicker1.Value)
            {
                kryptonDateTimePicker1.Value = kryptonDateTimePicker2.Value.AddDays(1);
            }
            kryptonDateTimePicker1.MinDate = kryptonDateTimePicker2.Value;

            // Update date difference
            UpdateDateDifference();
        }

        private void kryptonDateTimePicker1_ValueChanged_1(object sender, EventArgs e)
        {
            // Ensure the check-out date is not before the check-in date
            if (kryptonDateTimePicker1.Value < kryptonDateTimePicker2.Value)
            {
                kryptonDateTimePicker1.Value = kryptonDateTimePicker2.Value.AddDays(1);
            }

            // Update date difference
            UpdateDateDifference();
        }

        private void metroComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();


            saveFileDialog.Filter = "PDF Files (*.pdf)|*.pdf";
            saveFileDialog.DefaultExt = "pdf";
            saveFileDialog.AddExtension = true;



            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                ExportToPdf.Export(dataGridViewParameter, saveFileDialog.FileName);
            }
        }

        private void label23_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label20_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
