using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using FireSharp.Interfaces;
using FireSharp.Response;
using Firebase.Database;
using Firebase.Database.Query;

namespace Royal.DAO
{
    public class ReportDAO
    {
        public string ReportID { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public string RoomType { get; set; }
        public int TotalRevenue { get; set; }

        private FirebConfig config = new FirebConfig();
        private IFirebaseClient client;
        private FirebaseClient firebaseClient;
        private Room roomDAO;

        public ReportDAO()
        {
            try
            {
                client = new FireSharp.FirebaseClient(config.Config);
                firebaseClient = FirebaseManage.GetFirebaseClient();
                roomDAO = new Room(); // Initialize RoomDAO instance
            }
            catch
            {
                MessageBox.Show("Connection failed!");
            }
        }

        public async void LoadReport(DataGridView v, int month, int year)
        {
            FirebaseResponse response = await client.GetAsync("Bill/");
            if (response != null && !string.IsNullOrEmpty(response.Body))
            {
                Dictionary<string, BillDAO> getBill = response.ResultAs<Dictionary<string, BillDAO>>();
                v.Rows.Clear();

                if (getBill != null)
                {
                    var reportData = new Dictionary<string, int>();

                    foreach (var item in getBill)
                    {
                        BillDAO bill = item.Value;
                        if (bill.NGLAPDateTime.Month == month && bill.NGLAPDateTime.Year == year && bill.TRANGTHAI == "Đã thanh toán")
                        {
                            Room room = await roomDAO.SearchRoomById(bill.MAPHONG);
                            if (room != null)
                            {
                                string roomType = room.LoaiPhong;
                                if (reportData.ContainsKey(roomType))
                                {
                                    reportData[roomType] += bill.THANHTIEN;
                                }
                                else
                                {
                                    reportData[roomType] = bill.THANHTIEN;
                                }
                            }
                        }
                    }

                    foreach (var entry in reportData)
                    {
                        v.Rows.Add(entry.Key, entry.Value);
                    }
                }
            }
        }

        public async Task<DataTable> GetReport(int month, int year)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("name", typeof(string));
            dataTable.Columns.Add("value", typeof(double));

            FirebaseResponse response = await client.GetAsync("Bill/");
            if (response != null && !string.IsNullOrEmpty(response.Body))
            {
                Dictionary<string, BillDAO> getBill = response.ResultAs<Dictionary<string, BillDAO>>();
                if (getBill != null)
                {
                    var reportData = new Dictionary<string, int>();

                    foreach (var item in getBill)
                    {
                        BillDAO bill = item.Value;
                        if (bill.NGLAPDateTime.Month == month && bill.NGLAPDateTime.Year == year && bill.TRANGTHAI == "Đã thanh toán")
                        {
                            Room room = await roomDAO.SearchRoomById(bill.MAPHONG);
                            if (room != null)
                            {
                                string roomType = room.LoaiPhong;
                                if (reportData.ContainsKey(roomType))
                                {
                                    reportData[roomType] += bill.THANHTIEN;
                                }
                                else
                                {
                                    reportData[roomType] = bill.THANHTIEN;
                                }
                            }
                        }
                    }

                    foreach (var entry in reportData)
                    {
                        DataRow row = dataTable.NewRow();
                        row["name"] = entry.Key;
                        row["value"] = entry.Value;
                        dataTable.Rows.Add(row);
                    }
                }
            }

            return dataTable;
        }

        public async void AddReport(ReportDAO report)
        {
            var reportList = await firebaseClient.Child("Report").OnceAsync<ReportDAO>();
            var existingReport = reportList.Select(r => r.Object).FirstOrDefault(r => r.Month == report.Month && r.Year == report.Year && r.RoomType == report.RoomType);

            if (existingReport != null)
            {
                existingReport.TotalRevenue += report.TotalRevenue;
                await firebaseClient.Child("Report").Child(existingReport.ReportID).PutAsync(existingReport);
                MessageBox.Show("Updated existing report with new total revenue.");
            }
            else
            {
                await firebaseClient.Child("Report").Child(report.ReportID).PutAsync(report);
                MessageBox.Show("Added new report.");
            }
        }

        public async void LoadReportAYear(DataGridView v, int year)
        {
            FirebaseResponse response = await client.GetAsync("Bill/");
            if (response != null && !string.IsNullOrEmpty(response.Body))
            {
                Dictionary<string, BillDAO> getBill = response.ResultAs<Dictionary<string, BillDAO>>();
                v.Rows.Clear();

                if (getBill != null)
                {
                    var reportData = new Dictionary<string, int>();

                    foreach (var item in getBill)
                    {
                        BillDAO bill = item.Value;
                        if (bill.NGLAPDateTime.Year == year && bill.TRANGTHAI == "Đã thanh toán")
                        {
                            Room room = await roomDAO.SearchRoomById(bill.MAPHONG);
                            if (room != null)
                            {
                                string roomType = room.LoaiPhong;
                                if (reportData.ContainsKey(roomType))
                                {
                                    reportData[roomType] += bill.THANHTIEN;
                                }
                                else
                                {
                                    reportData[roomType] = bill.THANHTIEN;
                                }
                            }
                        }
                    }

                    foreach (var entry in reportData)
                    {
                        v.Rows.Add(entry.Key, entry.Value);
                    }
                }
            }
        }

        public async Task<DataTable> GetReportAYear(int year)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("name", typeof(string));
            dataTable.Columns.Add("value", typeof(double));

            FirebaseResponse response = await client.GetAsync("Bill/");
            if (response != null && !string.IsNullOrEmpty(response.Body))
            {
                Dictionary<string, BillDAO> getBill = response.ResultAs<Dictionary<string, BillDAO>>();
                if (getBill != null)
                {
                    var reportData = new Dictionary<string, int>();

                    foreach (var item in getBill)
                    {
                        BillDAO bill = item.Value;
                        if (bill.NGLAPDateTime.Year == year && bill.TRANGTHAI == "Đã thanh toán")
                        {
                            Room room = await roomDAO.SearchRoomById(bill.MAPHONG);
                            if (room != null)
                            {
                                string roomType = room.LoaiPhong;
                                if (reportData.ContainsKey(roomType))
                                {
                                    reportData[roomType] += bill.THANHTIEN;
                                }
                                else
                                {
                                    reportData[roomType] = bill.THANHTIEN;
                                }
                            }
                        }
                    }

                    foreach (var entry in reportData)
                    {
                        DataRow row = dataTable.NewRow();
                        row["name"] = entry.Key;
                        row["value"] = entry.Value;
                        dataTable.Rows.Add(row);
                    }
                }
            }

            return dataTable;
        }
    }
}
