using System;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

public class ExportToExcel
{
    public static void Export(DataGridView dataGridView, string filename)
    {
        try
        {
            // Tạo ứng dụng Excel
            Excel.Application excelApp = new Excel.Application();
            if (excelApp == null)
            {
                MessageBox.Show("Excel is not installed properly!");
                return;
            }

            // Tạo workbook mới
            Excel.Workbook workbook = excelApp.Workbooks.Add(Type.Missing);
            Excel.Worksheet worksheet = (Excel.Worksheet)workbook.ActiveSheet;
            worksheet.Name = "ExportedData";

            // Thêm tiêu đề cột vào worksheet
            for (int i = 0; i < dataGridView.ColumnCount; i++)
            {
                worksheet.Cells[1, i + 1] = dataGridView.Columns[i].HeaderText;
            }

            // Thêm dữ liệu hàng vào worksheet
            for (int i = 0; i < dataGridView.RowCount; i++)
            {
                for (int j = 0; j < dataGridView.ColumnCount; j++)
                {
                    worksheet.Cells[i + 2, j + 1] = dataGridView.Rows[i].Cells[j].Value?.ToString();
                }
            }

            // Lưu workbook
            workbook.SaveAs(filename);
            workbook.Close();
            excelApp.Quit();

            MessageBox.Show("Export to Excel successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error exporting to Excel: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
