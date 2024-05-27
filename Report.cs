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
using System.Windows.Forms.DataVisualization.Charting;

namespace Royal
{
    public partial class Report : Form
    {
        public Report()
        {
            InitializeComponent();
            doanhthu_lv.Font = new System.Drawing.Font("Segoe UI", 9.75F);
        }
        private async void DrawChart(int month, int year)
        {
            ReportDAO reportDAO = new ReportDAO();
            DataTable table = new DataTable();
            if (month == 0)
            {
                table = await reportDAO.GetReportAYear(year);
            } // Loại bỏ việc khai báo biến cục bộ ở đây
            else table = await reportDAO.GetReport(month, year);                                                          // Clear the existing series in the chart
            resChart.Series.Clear();

            // Add a new series for the chart
            Series series = new Series("Doanh Thu");
            series.ChartType = SeriesChartType.Column;

            // Add data points to the series
            foreach (DataRow row in table.Rows)
            {
                string roomType = row["name"].ToString();
                int revenue = Convert.ToInt32(row["value"]);
                series.Points.AddXY(roomType, revenue);
            }

            // Add the series to the chart
            resChart.Series.Add(series);

            // Set axis labels
            resChart.ChartAreas[0].AxisX.Title = "Room Type";
            resChart.ChartAreas[0].AxisY.Title = "Revenue";
        }
        private void result_but_Click(object sender, EventArgs e)
        {
            ReportDAO reportDAO = new ReportDAO();
            int month;
            if (comboBoxMonth.Text == "Cả năm")
            {
                reportDAO.LoadReportAYear(doanhthu_lv, (int)(numericYear.Value));
                DrawChart(0, (int)(numericYear.Value));
            }
            else
            {
                reportDAO.LoadReport(doanhthu_lv, int.Parse(comboBoxMonth.Text), (int)(numericYear.Value));
                DrawChart(int.Parse(comboBoxMonth.Text), (int)(numericYear.Value));
            }
        }

        private void Report_Load(object sender, EventArgs e)
        {
            ReportDAO reportDAO = new ReportDAO();
            reportDAO.LoadReport(doanhthu_lv, DateTime.Now.Month, DateTime.Now.Year);
            DrawChart(DateTime.Now.Month, DateTime.Now.Year);
            comboBoxMonth.Text = DateTime.Now.Month.ToString();
            numericYear.Text = DateTime.Now.Year.ToString();
        }

        private void comboBoxMonth_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
