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
        }

        private async void kryptonButton1_Click(object sender, EventArgs e)
        {
            // Get the current row count for the "Bill" table
            int currentRowCount = await firebaseClient
                .Child("Staff")
                .OnceAsync<object>()
                .ContinueWith(task => task.Result.Count);

            // Increment by 1 to get the new sequential number
            int newNumber = currentRowCount + 1;

            // Format the number with leading zeros (001, 002, ...)
            string formattedNumber = newNumber.ToString("D3"); // Adjust "D3" for desired number of digits

            // Create the MAHD with your preferred prefix (e.g., "HD")
            string manv = $"NV{formattedNumber}";

            // Create a new Bill object with form control values
            StaffDAO newStaff = new StaffDAO()
            {
               StaffID = manv,
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

            newStaff.AddStaff(newStaff);
        }

        private void dataGridBill_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Kiểm tra xem có hàng nào được chọn không
            {
                DataGridViewRow selectedRow = dataGridStaff.Rows[e.RowIndex];

                // Kiểm tra nếu hàng không rỗng
                if (!selectedRow.IsNewRow)
                {

                    string dateBỉthString = (string)selectedRow.Cells[6].Value;
                    DateTime datebirth, dateInA;

                    // Sử dụng phương thức ParseExact để chuyển đổi chuỗi thành đối tượng DateTime
                    datebirth = DateTime.ParseExact(dateBỉthString, "dddd, MMMM d, yyyy", CultureInfo.InvariantCulture);

                    string dateInstring = (string)selectedRow.Cells[9].Value;
                    dateInA = DateTime.ParseExact(dateInstring, "dddd, MMMM d, yyyy", CultureInfo.InvariantCulture);



                    // Trích xuất dữ liệu từ hàng được chọn và hiển thị lên form
                    manv.Text = (string)selectedRow.Cells[0].Value;
                    name.Text = (string)selectedRow.Cells[1].Value;
                    idcc.Text = (string)selectedRow.Cells[2].Value;
                    loaiNV.Text = (string)selectedRow.Cells[3].Value;
                    soDT.Text = (string)selectedRow.Cells[4].Value;
                    mail.Text = (string)selectedRow.Cells[5].Value;
                    dateBirth.Value = datebirth;
                    address.Text = (string)selectedRow.Cells[7].Value;
                    comboBoxSex.Text = (string)selectedRow.Cells[8].Value;
                    dateIn.Value = dateInA;

                }
            }
        }

        private void kryptonButton4_Click(object sender, EventArgs e)
        {
            StaffDAO a = new StaffDAO();
            a.LoadStaff(dataGridStaff);
        }

        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            string id = manv.Text;
            StaffDAO a = new StaffDAO(); 
            a.DeleteStaff(id);
        }

        private void kryptonButton3_Click(object sender, EventArgs e)
        {
            StaffDAO a =new StaffDAO(); 
            a.UpdateStaff(manv.Text, name.Text, idcc.Text, loaiNV.Text, soDT.Text, mail.Text, dateBirth.Value.ToString(), address.Text, comboBoxSex.Text, dateIn.Value.ToString());
        }
    }
}
