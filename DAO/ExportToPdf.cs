using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;


namespace Royal.DAO
{
    public class ExportToPdf
    {
        public static void Export(DataGridView dgv, string filename)
        {
            try
            {
                // Đường dẫn đến font Arial hoặc Tahoma trong thư mục dự án
                string fontPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ENV", "arial.ttf");
                BaseFont bfArialUnicode = BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                Font font = new Font(bfArialUnicode, 12, Font.NORMAL);

                Document document = new Document();
                PdfWriter.GetInstance(document, new FileStream(filename, FileMode.Create));
                document.Open();

                PdfPTable table = new PdfPTable(dgv.ColumnCount);
                table.WidthPercentage = 100;

                // Thêm tiêu đề cột vào bảng
                foreach (DataGridViewColumn column in dgv.Columns)
                {
                    PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText, font));
                    table.AddCell(cell);
                }

                // Thêm dữ liệu vào bảng
                foreach (DataGridViewRow row in dgv.Rows)
                {
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        table.AddCell(new Phrase(cell.Value?.ToString(), font));
                    }
                }

                document.Add(table);
                document.Close();
                MessageBox.Show("Export to PDF successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error exporting to PDF: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
