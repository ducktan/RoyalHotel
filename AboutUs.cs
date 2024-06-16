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
    public partial class AboutUs : Form
    {
        private System.Windows.Forms.Timer updateTimer;
        public AboutUs()
        {
            InitializeComponent();
            updateTimer = new System.Windows.Forms.Timer();
            updateTimer.Interval = (30 * 24 * 60 *6* 1000); // 30 days in milliseconds (for monthly update)
            updateTimer.Tick += UpdateTimer_Tick;
            updateTimer.Start();
        }

        private async void UpdateTimer_Tick(object sender, EventArgs e)
        {
            // Dừng Timer trước khi thực hiện công việc để tránh chồng lấn
            updateTimer.Stop();

            try
            {
                // Gọi hàm cập nhật loại khách hàng dựa trên số tiền hóa đơn
                await new CustomerDAO().UpdateCustomerTypeByBillAmount();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating customer types: {ex.Message}");
            }
            finally
            {
                // Sau khi hoàn thành công việc, khởi động lại Timer
                updateTimer.Start();
            }
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Hide();
        }
    }
}
