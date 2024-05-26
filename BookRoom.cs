﻿using System;
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

            dataGridViewParameter.CellClick += dataGridBill_CellClick;
            LoadMaLPFromDatabase();
            // Set the MinDate properties to prevent selecting past dates
            kryptonDateTimePicker2.MinDate = DateTime.Today;
            kryptonDateTimePicker1.MinDate = DateTime.Today.AddDays(1);

            // Set event handlers for date validation and difference calculation
            kryptonDateTimePicker2.ValueChanged += kryptonDateTimePicker2_ValueChanged;
            kryptonDateTimePicker1.ValueChanged += kryptonDateTimePicker1_ValueChanged;

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

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox5_Enter(object sender, EventArgs e)
        {

        }

        private async void button1_Click(object sender, EventArgs e)
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


        private void label16_Click(object sender, EventArgs e)
        {

        }
        private void dataGridBill_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // Check if a valid row index is clicked
                if (e.RowIndex >= 0)
                {
                    // Get the selected row
                    DataGridViewRow selectedRow = dataGridViewParameter.Rows[e.RowIndex];

                    // Ensure the row is not a new row
                    if (!selectedRow.IsNewRow)
                    {
                        // Populate form controls with the values from the selected row
                        MaDP.HeaderText = selectedRow.Cells["MaDP"].Value?.ToString() ?? string.Empty;
                        NgayDP.HeaderText = selectedRow.Cells["NgayDP"].Value?.ToString() ?? string.Empty;
                        TimeDP.HeaderText = selectedRow.Cells["TimeDP"].Value?.ToString() ?? string.Empty;
                        NgayTra.HeaderText = selectedRow.Cells["NgayTra"].Value?.ToString() ?? string.Empty;
                        CCCD.HeaderText = selectedRow.Cells["CCCD"].Value?.ToString() ?? string.Empty;
                        SONGUOI.HeaderText = selectedRow.Cells["SONGUOI"].Value?.ToString() ?? string.Empty;
                        MAPHONG.HeaderText = selectedRow.Cells["MAPHONG"].Value?.ToString() ?? string.Empty;
                        TIENCOC.HeaderText = selectedRow.Cells["TIENCOC"].Value?.ToString() ?? string.Empty;
                    }
                }
            }
            catch (Exception ex)
            {
                // Display any errors that occur during the cell click handling
                MessageBox.Show($"Error processing selected cell: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                // Extract the latest ID_DATPHONG and increment it for the new booking
                int latestNumber = currentRooms
 .Select(r => int.TryParse(r.Object.ID_DATPHONG.Replace("IDDP", ""), out int number) ? number : -1) // Extract numeric part and parse to int
 .OrderByDescending(num => num)
 .FirstOrDefault();



                int newNumber = latestNumber + 1;
                string newID_DATPHONG = $"IDDP{newNumber}";

                // Get the form values
                string selectedRoomType = cbRoomType.SelectedItem != null ? cbRoomType.SelectedItem.ToString() : "";
                string maphong = metroComboBox2.SelectedItem != null ? metroComboBox2.SelectedItem.ToString() : "";
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

        private async void cbRoomType_SelectedIndexChanged(object sender, EventArgs e)
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
                    .OnceSingleAsync<RoomType>();

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
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error retrieving room type details: {ex.Message}", "Error");
            }
        }

        private void kryptonDateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            // Ensure the check-out date is after or the same as the check-in date
            if (kryptonDateTimePicker2.Value > kryptonDateTimePicker1.Value)
            {
                kryptonDateTimePicker1.Value = kryptonDateTimePicker2.Value;
            }
            kryptonDateTimePicker1.MinDate = kryptonDateTimePicker2.Value;

            // Update date difference
            UpdateDateDifference();
        }

        private void kryptonDateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            // Ensure the check-out date is not before the check-in date
            if (kryptonDateTimePicker1.Value < kryptonDateTimePicker2.Value)
            {
                kryptonDateTimePicker1.Value = kryptonDateTimePicker2.Value;
            }

            // Update date difference
            UpdateDateDifference();
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


        private void dataGridViewParameter_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            ClearInputFields();
        }
    }
}