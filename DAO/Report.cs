using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Database.Query;
using System.Windows.Forms;
using FireSharp.Interfaces;
using FireSharp.Response;

namespace Royal.DAO
{
    public class ReportDAO
    {

        public string ReportID { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public string RoomType { get; set; } // Add this propert
        public int TotalRevenue { get; set; }

        public FirebConfig config = new FirebConfig();
        public IFirebaseClient Client { get; private set; }
        private FirebaseClient firebaseClient;

        public ReportDAO()
        {
            try
            {
                Client = new FireSharp.FirebaseClient(config.Config);
                firebaseClient = FirebaseManage.GetFirebaseClient();

            }
            catch
            {
                MessageBox.Show("Connection fail!");
            }
        }
        public async void LoadReport(DataGridView v, int month, int year)
        {

            // Fetch data from Firebase
            FirebaseResponse response = await Client.GetAsync("Report/");

            // Check for successful response

            if (response != null && !string.IsNullOrEmpty(response.Body))
            {
                // Cast response as Dictionary<string, Bill> (assuming 'Bill' class exists)
                Dictionary<string, ReportDAO> getReport = response.ResultAs<Dictionary<string, ReportDAO>>();

                // Clear the DataGridView before loading new data (optional)
                v.Rows.Clear();

                if (getReport != null)
                {
                    foreach (var item in getReport)
                    {
                        ReportDAO report = item.Value; // Access the report object

                        // Add a new row to the DataGridView
                        if (report.Month == month && report.Year == year)
                        {
                            v.Rows.Add(
                            report.RoomType,
                            report.TotalRevenue
                        );
                        }
                    }
                }

            }
        }
        public async Task<DataTable> GetReport(int month, int year)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("name", typeof(string));
            dataTable.Columns.Add("value", typeof(double));

            // Fetch data from Firebase
            FirebaseResponse response = await Client.GetAsync("Report/");

            // Check for successful response
            if (response != null && !string.IsNullOrEmpty(response.Body))
            {
                // Cast response as Dictionary<string, Bill> (assuming 'Bill' class exists)
                Dictionary<string, ReportDAO> getReport = response.ResultAs<Dictionary<string, ReportDAO>>();

                if (getReport != null)
                {
                    foreach (var item in getReport)
                    {
                        ReportDAO report = item.Value; // Access the report object

                        // Add a new row to the DataTable
                        if (report.Month == month && report.Year == year)
                        {
                            DataRow row = dataTable.NewRow();
                            row["name"] = report.RoomType;
                            row["value"] = report.TotalRevenue;
                            dataTable.Rows.Add(row);
                        }
                    }
                }
            }

            return dataTable; // Return the DataTable
        }

        public async void AddReport(ReportDAO report)
        {
            // Retrieve the list of existing reports
            var reportList = await firebaseClient
                .Child("Report")
                .OnceAsync<ReportDAO>();

            // Find the existing report with the same month, year, and room type
            var existingReport = reportList
                .Select(r => r.Object)
                .FirstOrDefault(r => r.Month == report.Month && r.Year == report.Year && r.RoomType == report.RoomType);

            if (existingReport != null)
            {
                // If the report exists, update its TotalRevenue
                existingReport.TotalRevenue += report.TotalRevenue;

                var updatedReport = new
                {
                    existingReport.Month,
                    existingReport.Year,
                    existingReport.RoomType,
                    existingReport.TotalRevenue
                };

                // Update the existing report in Firebase
                await firebaseClient
                    .Child("Report")
                    .Child(existingReport.ReportID)
                    .PutAsync(updatedReport);

                MessageBox.Show("Updated existing report with new total revenue.");
            }
            else
            {
                // If the report doesn't exist, create a new one


                var newReport = new
                {
                    report.Month,
                    report.Year,
                    report.RoomType,
                    report.TotalRevenue
                };

                // Set data to Firebase RTDB
                await firebaseClient
                    .Child("Report")
                    .Child(report.ReportID)
                    .PutAsync(newReport);

                MessageBox.Show("Added new report.");
            }
        }
        public async void LoadReportAYear(DataGridView v, int year)
        {
            // Fetch data from Firebase
            FirebaseResponse response = await Client.GetAsync("Report/");

            // Check for a successful response
            if (response != null && !string.IsNullOrEmpty(response.Body))
            {
                // Cast response as Dictionary<string, ReportDAO>
                Dictionary<string, ReportDAO> getReport = response.ResultAs<Dictionary<string, ReportDAO>>();

                // Clear the DataGridView before loading new data
                v.Rows.Clear();

                if (getReport != null)
                {
                    // Use a dictionary to aggregate total revenue by room type
                    var reportData = new Dictionary<string, double>();

                    foreach (var item in getReport)
                    {
                        ReportDAO report = item.Value; // Access the report object

                        // Aggregate data for the specified year
                        if (report.Year == year)
                        {
                            if (reportData.ContainsKey(report.RoomType))
                            {
                                reportData[report.RoomType] += report.TotalRevenue;
                            }
                            else
                            {
                                reportData[report.RoomType] = report.TotalRevenue;
                            }
                        }
                    }

                    // Populate the DataGridView with aggregated data
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

            // Fetch data from Firebase
            FirebaseResponse response = await Client.GetAsync("Report/");

            // Check for a successful response
            if (response != null && !string.IsNullOrEmpty(response.Body))
            {
                // Cast response as Dictionary<string, ReportDAO>
                Dictionary<string, ReportDAO> getReport = response.ResultAs<Dictionary<string, ReportDAO>>();

                if (getReport != null)
                {
                    // Use a dictionary to aggregate total revenue by room type
                    var reportData = new Dictionary<string, double>();

                    foreach (var item in getReport)
                    {
                        ReportDAO report = item.Value; // Access the report object

                        // Aggregate data for the specified year
                        if (report.Year == year)
                        {
                            if (reportData.ContainsKey(report.RoomType))
                            {
                                reportData[report.RoomType] += report.TotalRevenue;
                            }
                            else
                            {
                                reportData[report.RoomType] = report.TotalRevenue;
                            }
                        }
                    }

                    // Populate the DataTable with aggregated data
                    foreach (var entry in reportData)
                    {
                        DataRow row = dataTable.NewRow();
                        row["name"] = entry.Key;
                        row["value"] = entry.Value;
                        dataTable.Rows.Add(row);
                    }
                }
            }

            return dataTable; // Return the DataTable
        }

    }
}
