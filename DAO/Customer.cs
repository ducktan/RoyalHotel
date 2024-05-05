using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using System.Windows.Forms;
using FireSharp;


namespace Royal.DAO
{
    public class CustomerDAO
    {
        public string MAKH { get; set; }
        public string HOTEN { get; set; }
        public string GIOITINH { get; set; }
        public string EMAIL { get; set; }
        public string SDT { get; set; }
        public string NGSINH { get; set; }
        public string DIACHI { get; set; }
        public string QUOCTICH { get; set; }
        public string CCCD { get; set; }
        public string ID_LOAIKH { get; set; }


        // Set up Firebase configuration
        public FirebConfig config = new FirebConfig();

        // Initialize Firebase client
        public IFirebaseClient Client { get; private set; } // Make client accessible only within the class


        public CustomerDAO()
        {
            try
            {
                Client = new FireSharp.FirebaseClient(config.Config);

            }
            catch
            {
                MessageBox.Show("Connection fail!");
            }
            // Initialize client upon object creation


        }
        public async void AddCustomer(CustomerDAO customer)
        {


            // Tạo một đối tượng chứa các thuộc tính không bao gồm cấu hình
            var customerData = new
            {
                customer.HOTEN, 
                customer.GIOITINH,
                customer.EMAIL,
                customer.SDT,
                customer.CCCD, 
                customer.ID_LOAIKH, 
                customer.DIACHI,
                customer.QUOCTICH,
                customer.NGSINH, 
                customer.MAKH
            };

            // Set data to Firebase RTDB
            FirebaseResponse response = await Client.SetAsync("Customer/" + customer.MAKH, customerData);
            MessageBox.Show("Add Customer");
        }






    }




}

