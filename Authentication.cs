using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Royal.DAO;

namespace Royal
{
    public partial class Authentication : Form
    {
        private Firebase.Database.FirebaseClient firebaseClient;
        private Dictionary<string, List<string>> rolePermissions;
        public Authentication()
        {

            InitializeComponent();
            firebaseClient = FirebaseManage.GetFirebaseClient();
            LoadRoomTypeNameFromDB();
            InitializeRolePermissions();
            cbbStaffType.SelectedIndexChanged += ComboBoxRoles_SelectedIndexChanged;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cbbStaffType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void InitializeRolePermissions()
        {
            rolePermissions = new Dictionary<string, List<string>>()
            {
                { "Giám đốc", new List<string> { "Quản lý nhân viên", "Quản lý tài chính", "Quản lý khách hàng", "Đặt phòng", "Nhận phòng", "Quản lý hóa đơn", "Quản lý dịch vụ", "Quản lý quy định", "Thống kê, báo cáo","Chat" } },
                { "Quản lý", new List<string> { "Quản lý nhân viên", "Quản lý khách hàng", "Đặt phòng", "Nhận phòng", "Quản lý hóa đơn", "Quản lý dịch vụ", "Thống kê, báo cáo","Chat" } },

                { "Kế toán", new List<string> { "Quản lý tài chính", "Quản lý hóa đơn", "Thống kê, báo cáo", "Chat" } },
                { "Lễ tân", new List<string> { "Đặt phòng", "Nhận phòng", "Quản lý hóa đơn", "Quản lý khách hàng", "Quản lý phòng",  "Chat"} },
                { "Nhân viên dọn dẹp", new List<string> { "Chat"} },
                 { "Nhân viên IT", new List<string> { "Chat"} },
                  { "Nhân viên bảo vệ", new List<string> { "Chat"} },



                // Thêm các vai trò và quyền khác nếu cần
            };
        }

        private async void LoadRoomTypeNameFromDB()
        {
            try
            {
                var typeRoomList = await firebaseClient
                    .Child("StaffType")
                    .OnceAsync<Royal.DAO.StaffType>();
                cbbStaffType.Items.Clear();

                foreach (var roomType in typeRoomList)
                {
                    cbbStaffType.Items.Add(roomType.Object.stName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu từ Firebase Realtime Database: " + ex.Message);
            }

        }

        private void ComboBoxRoles_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Lấy vai trò được chọn
            string selectedRole = cbbStaffType.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(selectedRole))
                return;

            // Lấy danh sách quyền cho vai trò được chọn
            var currentPermissions = rolePermissions.ContainsKey(selectedRole) ? rolePermissions[selectedRole] : new List<string>();
            var allPermissions = new List<string> { "Quản lý nhân viên", "Quản lý tài chính", "Quản lý khách hàng", "Đặt phòng", "Nhận phòng", "Quản lý hóa đơn", "Quản lý dịch vụ", "Quản lý quy định", "Thống kê, báo cáo", "Chat" };

            // Tính toán quyền còn lại
            var remainingPermissions = new List<string>(allPermissions);
            remainingPermissions.RemoveAll(permission => currentPermissions.Contains(permission));

            // Cập nhật các ListBox
            listBoxCurrentPermission.Items.Clear();
            listBoxRemainPermission.Items.Clear();

            foreach (var permission in currentPermissions)
            {
                listBoxCurrentPermission.Items.Add(permission);
            }

            foreach (var permission in remainingPermissions)
            {
                listBoxRemainPermission.Items.Add(permission);
            }
        }



    }
}
