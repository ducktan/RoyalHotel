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

    }
}
