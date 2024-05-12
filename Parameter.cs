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
using System.Globalization;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

namespace Royal
{
    public partial class Parameter : Form
    {
        private Firebase.Database.FirebaseClient firebaseClient;
        public Parameter()
        {
            InitializeComponent();
            // Khởi tạo FirebaseClient với chuỗi kết nối đến Firebase Realtime Database
            firebaseClient = FirebaseManage.GetFirebaseClient();
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            ParameterDAO p1 = new ParameterDAO()
            {
                pID = maP.Text,
                pName = tenP.Text,
                pContent = content.Text,
                pValue = Int32.Parse(value.Text)
            };

            // Thực hiện các thao tác với đối tượng p1 (ví dụ: lưu vào Firebase Realtime Database)
            // firebaseClient.Child("parameters").PostAsync(p1);

            p1.AddPara(p1);
        }


    }
}
