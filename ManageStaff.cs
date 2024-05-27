using Royal.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Royal
{
    public partial class ManageStaff : Form
    {
        private Firebase.Database.FirebaseClient firebaseClient;
        public ManageStaff()
        {
            InitializeComponent();
            // Khởi tạo FirebaseClient với chuỗi kết nối đến Firebase Realtime Database
            firebaseClient = FirebaseManage.GetFirebaseClient();
            StaffDAO staff = new StaffDAO();
            staff.LoadStaff(dataGridStaff);
            dataGridStaff.CellClick += dataGridBill_CellClick;
            LoadRoomTypeNameFromDB();
        }

        private async void kryptonButton1_Click(object sender, EventArgs e)
        {
            // Get the current row count for the "Bill" table
            var bills = await firebaseClient
                .Child("Staff")
                .OnceAsync<DAO.StaffDAO>();

            // Tìm mã phòng lớn nhất hiện có
            int maxRoomNumber = 0;
            foreach (var roomData in bills)
            {
                int roomNumber = int.Parse(roomData.Object.StaffID.Substring(3));
                if (roomNumber > maxRoomNumber)
                {
                    maxRoomNumber = roomNumber;
                }
            }

            string newRoomNumber = "NV" + (maxRoomNumber + 1).ToString("D3");



            // Create a new Bill object with form control values
            StaffDAO newStaff = new StaffDAO()
            {
                StaffID = newRoomNumber,
                staffName = name.Text,
                staffCCCD = idcc.Text,
                staffType = loaiNV.Text,
                staffPhone = soDT.Text,
                staffEmail = mail.Text,
                staffBirth = dateBirth.Text,
                staffAdd = address.Text,
                staffGender = comboBoxSex.Text,
                staffDateIn = dateIn.Text
            };

            await newStaff.AddStaff(newStaff);
            newStaff.LoadStaff(dataGridStaff);
        }

        private void dataGridBill_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Kiểm tra xem có hàng nào được chọn không
            {
                DataGridViewRow selectedRow = dataGridStaff.Rows[e.RowIndex];

                // Kiểm tra nếu hàng không rỗng
                if (!selectedRow.IsNewRow)
                {
                    // Trích xuất dữ liệu từ hàng được chọn và hiển thị lên form
                    manv.Text = (string)selectedRow.Cells[0].Value;
                    name.Text = (string)selectedRow.Cells[1].Value;
                    idcc.Text = (string)selectedRow.Cells[2].Value;
                    loaiNV.Text = (string)selectedRow.Cells[3].Value;
                    soDT.Text = (string)selectedRow.Cells[4].Value;
                    mail.Text = (string)selectedRow.Cells[5].Value;
                    dateBirth.Text = (string)selectedRow.Cells[6].Value;
                    address.Text = (string)selectedRow.Cells[7].Value;
                    comboBoxSex.Text = (string)selectedRow.Cells[8].Value;
                    dateIn.Text = (string)selectedRow.Cells[9].Value;

                }
            }
        }

        private async void LoadRoomTypeNameFromDB()
        {
            try
            {
                var typeRoomList = await firebaseClient
                    .Child("StaffType")
                    .OnceAsync<Royal.DAO.StaffType>();
                loaiNV.Items.Clear();

                foreach (var roomType in typeRoomList)
                {
                    loaiNV.Items.Add(roomType.Object.stID);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu từ Firebase Realtime Database: " + ex.Message);
            }

        }



        private void kryptonButton4_Click(object sender, EventArgs e)
        {
            StaffDAO a = new StaffDAO();
            a.LoadStaff(dataGridStaff);
        }

        private async void kryptonButton2_Click(object sender, EventArgs e)
        {
            string id = manv.Text;
            StaffDAO a = new StaffDAO();
            await a.DeleteStaff(id);

            a.LoadStaff(dataGridStaff);
        }

        private async void kryptonButton3_Click(object sender, EventArgs e)
        {
            StaffDAO a = new StaffDAO();
            await a.UpdateStaff(manv.Text, name.Text, idcc.Text, loaiNV.Text, soDT.Text, mail.Text, dateBirth.Value.ToString(), address.Text, comboBoxSex.Text, dateIn.Value.ToString());
            a.LoadStaff(dataGridStaff);
        }



        private async void kryptonButton5_Click(object sender, EventArgs e)
        {
            // Get the search criteria from the UI controls
            string type = cboTypeSearch.Text;
            string searchText1 = searchText.Text.Trim(); // Assuming txtSearch is a textbox for search text

            if (string.IsNullOrEmpty(searchText1))
            {
                MessageBox.Show("Please enter the search text.");
                return;
            }
            // Call the appropriate search function based on the selected type
            Royal.DAO.StaffDAO billFun = new Royal.DAO.StaffDAO(); // Assuming you have an instance

            // Call the appropriate search function based on the selected type
            List<Royal.DAO.StaffDAO> searchResults = new List<Royal.DAO.StaffDAO>(); // Initialize empty list


            try
            {
                if(type=="Mã nhân viên")
                {
                    Royal.DAO.StaffDAO searchResult = await billFun.SearchStaffbyID(searchText1);
                    searchResults.Add(searchResult);
                                       

                }
                else if(type =="Loại nhân viên"){
                    searchResults = await billFun.SearchRoomByType(searchText1);
                }
                else if(type =="Giới tính")
                {
                    searchResults = await billFun.SearchStaffByGender(searchText1);
                }
                else
                {
                    MessageBox.Show("Invalid search type selected.");
                    return;
                }



                List<string[]> uiResults = searchResults.Select(bill => new string[] { bill.StaffID, bill.staffName, bill.staffCCCD, bill.staffType, bill.staffPhone, bill.staffEmail, bill.staffBirth, bill.staffAdd, bill.staffGender, bill.staffDateIn }).ToList();

                // Update UI elements on the UI thread
                if (this.InvokeRequired)
                {
                    this.Invoke(new Action(() =>
                    {
                        dataGridStaff.Rows.Clear();
                        foreach (string[] rowData in uiResults)
                        {
                            dataGridStaff.Rows.Add(rowData);
                        }
                    }));
                }
                else
                {
                    dataGridStaff.Rows.Clear();
                    foreach (string[] rowData in uiResults)
                    {
                        dataGridStaff.Rows.Add(rowData);
                    }
                }

                // Handle no search results (optional)
                if (searchResults.Count == 0)
                {
                    MessageBox.Show("No staff");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error searching staff: {ex.Message}");
            }
        }

        private void kryptonButton6_Click(object sender, EventArgs e)
        {
            ManageStaffType s = new ManageStaffType();
            s.Show();
        }
    }
}
