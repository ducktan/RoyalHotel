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
    public partial class ManageCustomer : Form
    {
        public ManageCustomer()
        {
            InitializeComponent();
        }

        private void comboBoxCustomerType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void kryptonRichTextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void ManageCustomer_Load(object sender, EventArgs e)
        {

        }

        private void kryptonButton6_Click(object sender, EventArgs e)
        {
            CustomerType customer = new CustomerType();
            customer.Show();
        }
    }
}
