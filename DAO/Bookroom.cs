using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Windows.Forms;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using System.Windows.Forms;
using FireSharp;
using Firebase.Database;
using System.Runtime.InteropServices.ComTypes;

namespace Royal.DAO
{
    public class bookingroomDAO
    {


        private Firebase.Database.FirebaseClient firebaseClient;

        public string ReservedRoom { get; set; }
        public string CCCD_KH { get; set; }
        public string ID_DATPHONG { get; set; }
        public string ID_LOAIPHONG { get; set; }

        public int TIENCOC { get; set; }
        public string MAPHONG { get; set; }


        public int SONGUOI { get; set; }
        public string NGAYDAT { get; set; }
        public string NGAYTRA { get; set; }

        // Set up Firebase configuration
        public FirebConfig config = new FirebConfig();

        // Initialize Firebase client
        public IFirebaseClient Client { get; set; }

        public bookingroomDAO()
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
        }
        public async Task<string> GetHoTenFromCCCD(string cccd)
        {
            try
            {
                // Thực hiện truy vấn vào cơ sở dữ liệu để lấy họ tên dựa vào số CCCD
                var billList = await firebaseClient
                 .Child("Customer")
                 .OnceAsync<CustomerDAO>();

                foreach (var childSnapshot in billList)
                {
                    var customer = childSnapshot.Object;
                    if (customer.CCCD == cccd)
                    {
                        return customer.HOTEN;
                    }
                }

                // Trả về chuỗi rỗng nếu không tìm thấy
                return "";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error getting HoTen from CCCD: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return ""; // Trả về chuỗi rỗng nếu có lỗi xảy ra
            }
        }


        public async Task AddBookingRoom(bookingroomDAO bookingRoom)
        {
            try
            {
                var bookingRoomData = new
                {
                    bookingRoom.CCCD_KH,
                    bookingRoom.ID_DATPHONG,
                    bookingRoom.ID_LOAIPHONG,
                    bookingRoom.MAPHONG,
                    bookingRoom.NGAYDAT,
                    bookingRoom.NGAYTRA,
                    bookingRoom.SONGUOI,
                    bookingRoom.TIENCOC
                };

                receiveroomDAO receiveRoom = new receiveroomDAO()
                {
                    ReceivedRoom = bookingRoom.ID_DATPHONG, // Assign ID_DATPHONG to ReceivedRoom
                    CCCD_KH = bookingRoom.CCCD_KH,
                    HoTen = await GetHoTenFromCCCD(bookingRoom.CCCD_KH), // Use the full name from booking room
                    MAPHONG = bookingRoom.MAPHONG,
                    TRANGTHAI = "Chưa nhận phòng",
                    NGAYNHAN = bookingRoom.NGAYDAT, // Set to booking date for now
                    NGAYTRA = bookingRoom.NGAYTRA,
                    SONGUOI = bookingRoom.SONGUOI,
                    TIENCOC = bookingRoom.TIENCOC
                };

                // Add the receive room
                await receiveRoom.AddReceiveRoom(receiveRoom);

                FirebaseResponse response = await Client.SetAsync("ReservedRoom/" + bookingRoom.ID_DATPHONG, bookingRoomData);
                MessageBox.Show("Added booking room");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding booking room: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        public async void LoadBookingRooms(DataGridView v)
        {
            try
            {
                // Get response from Firebase
                FirebaseResponse response = await Client.GetAsync("ReservedRoom/");

                // Check if response is successful and body is not empty
                if (response != null && !string.IsNullOrEmpty(response.Body))
                {
                    // Deserialize the response to a dictionary
                    Dictionary<string, bookingroomDAO> bookingRoomsDict = JsonConvert.DeserializeObject<Dictionary<string, bookingroomDAO>>(response.Body);

                    // Clear the DataGridView before loading new data
                    v.Rows.Clear();

                    // Populate the DataGridView with the booking room data
                    foreach (var room in bookingRoomsDict.Values)
                    {
                        // Add row to DataGridView, ensuring ID_DATPHONG is converted to string
                        v.Rows.Add(
                            room.ID_DATPHONG.ToString(), // Convert ID_DATPHONG to string
                            room.CCCD_KH,
                            room.MAPHONG,
                            room.ID_LOAIPHONG,
                            room.NGAYDAT,
                            room.NGAYTRA,
                            room.SONGUOI,
                            room.TIENCOC
                        );
                    }
                }
                else
                {
                    MessageBox.Show("Empty response from Firebase", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading booking rooms: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public async Task<bool> IsRoomAvailableInTimePeriod(string roomID, DateTime startDate, DateTime endDate)
        {
            try
            {
                FirebaseResponse response = await Client.GetAsync("ReservedRoom/");

                if (response != null && !string.IsNullOrEmpty(response.Body))
                {
                    Dictionary<string, bookingroomDAO> bookingRoomsDict = JsonConvert.DeserializeObject<Dictionary<string, bookingroomDAO>>(response.Body);

                    foreach (var room in bookingRoomsDict.Values)
                    {
                        if (room.MAPHONG == roomID && IsDatesOverlap(startDate, endDate, room.NGAYDAT, room.NGAYTRA))
                        {
                            return false;
                        }
                    }
                    return true;
                }
                else
                {
                    MessageBox.Show("Empty response from Firebase", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        private bool IsDatesOverlap(DateTime startDate, DateTime endDate, string nGAYDAT, string nGAYTRA)
        {
            DateTime bookingStartDate = DateTime.Parse(nGAYDAT);
            DateTime bookingEndDate = DateTime.Parse(nGAYTRA);

            return startDate <= bookingEndDate && endDate >= bookingStartDate;
        }


        // Function to check if two date ranges overla
        public async void UpdateBookingRoom(string bookingRoomId, string cccd, string loaiPhong, string ngayDat, string ngayTra, string reservedRoom)
        {
            bookingroomDAO updatedBookingRoom = new bookingroomDAO
            {
                ID_DATPHONG = bookingRoomId,
                CCCD_KH = cccd,
                ID_LOAIPHONG = loaiPhong,
                NGAYDAT = ngayDat,
                NGAYTRA = ngayTra,
                ReservedRoom = reservedRoom
            };

            if (MessageBox.Show("Are you sure you want to update this booking room?", "Update Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    await Client.SetAsync($"ReservedRoom/{bookingRoomId}", updatedBookingRoom);
                    MessageBox.Show("Booking room information updated successfully!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error updating booking room: {ex.Message}");
                }
            }
        }


    }
}