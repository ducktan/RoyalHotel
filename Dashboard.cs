using Royal.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Royal
{
    public partial class Dashboard : Form
    {
        private Authen authen;
        private Permission permission;
        public Dashboard()
        {

            InitializeComponent();
            authen = new Authen();
            permission = new Permission();

        }

        private void panelRight_Paint(object sender, PaintEventArgs e)
        {

        }

        private void kryptonButton2_Click(object sender, EventArgs e)
        {

        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            Profile profile = new Profile();
            profile.Show();
        }

        private void kryptonPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Đ_Click(object sender, EventArgs e)
        {
            authen.SignOut();
            AboutUs about = new AboutUs();
            about.Show();
            Close();
        }

        private void metroTile17_Click(object sender, EventArgs e)
        {

            if (permission.HasAccess(User.Role, "Lễ tân"))
            {
                ManageCustomer manageCustomer = new ManageCustomer();
                manageCustomer.Show();
            }
            else
            {
                MessageBox.Show("Bạn không có quyền truy cập vào mục này!");
            }

        }

        private void metroTile16_Click(object sender, EventArgs e)
        {

            if (permission.HasAccess(User.Role, "Lễ tân") || permission.HasAccess(User.Role, "Kế toán"))
            {
                Bill bill = new Bill();
                bill.Show();
            }
            else
            {
                MessageBox.Show("Bạn không có quyền truy cập vào mục này!");
            }

        }

        private void metroTile13_Click(object sender, EventArgs e)
        {
            Parameter parameter = new Parameter();
            parameter.Show();
        }

        private void title_Click(object sender, EventArgs e)
        {

            if (permission.HasAccess(User.Role, "Kế toán"))
            {
                Report report = new Report();
                report.Show();

            }
            else
            {
                MessageBox.Show("Bạn không có quyền truy cập vào mục này!");
            }

        }

        private void titleManageRoom_Click(object sender, EventArgs e)
        {


            if (permission.HasAccess(User.Role, "Lễ tân"))
            {
                ManageRoom manageRoom = new ManageRoom();
                manageRoom.Show();
            }
            else
            {
                MessageBox.Show("Bạn không có quyền truy cập vào mục này!");
            }

        }

        private void metroTile2_Click(object sender, EventArgs e)
        {

            if (permission.HasAccess(User.Role, "Lễ tân"))
            {
                Service service = new Service();
                service.Show();
            }
            else
            {
                MessageBox.Show("Bạn không có quyền truy cập vào mục này!");
            }

        }

        private void metroTile8_Click(object sender, EventArgs e)
        {
            if (permission.HasAccess(User.Role, ""))
            {
                ManageStaff manageStaff = new ManageStaff();
                manageStaff.Show();
            }
            else
            {
                MessageBox.Show("Bạn không có quyền truy cập vào mục này!");
            }
        }

        private void titlePay_Click(object sender, EventArgs e)
        {



            if (permission.HasAccess(User.Role, "Lễ tân") || permission.HasAccess(User.Role, "Kế toán"))
            {
                PrintBill printBill = new PrintBill();
                printBill.Show();

            }
            else
            {
                MessageBox.Show("Bạn không có quyền truy cập vào mục này!");
            }

        }

        private void titleRecieveRoom_Click(object sender, EventArgs e)
        {

            if (permission.HasAccess(User.Role, "Lễ tân"))
            {
                ReiceiveRoom room = new ReiceiveRoom();
                room.Show();
            }
            else
            {
                MessageBox.Show("Bạn không có quyền truy cập vào mục này!");
            }

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void titleBookRoom_Click(object sender, EventArgs e)
        {


            if (permission.HasAccess(User.Role, "Lễ tân"))
            {
                BookRoom book = new BookRoom();
                book.Show();
            }
            else
            {
                MessageBox.Show("Bạn không có quyền truy cập vào mục này!");
            }

        }

        private void kryptonButton3_Click(object sender, EventArgs e)
        {
            AboutUs about = new AboutUs();
            about.Show();
        }

        private void metroTile1_Click(object sender, EventArgs e)
        {
            Chat chat = new Chat();
            chat.Show();
        }

        private void kryptonButton4_Click(object sender, EventArgs e)
        {
            Authentication authentication = new Authentication();
            authentication.Show();
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            if (User.Role == "Giám đốc")
            {
                btnAdmin.Visible = true;
            }
            else
            btnAdmin.Visible = false;
        }

        private void btnAdmin_Click(object sender, EventArgs e)
        {
            Admin admin = new Admin();
            admin.Show();
        }
    }
}
