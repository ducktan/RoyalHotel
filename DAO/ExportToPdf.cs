using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Packaging;
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

        public static void ExportLine(DataGridView dichvu, string filename, string mahd, string nglap, string nv, string tenKH, string cmnd, string loaiKH, string diachi, string quoctich, string tenphong, string loaiphong, string dongiaPhong,string ngden, string sodem, string soNguoi, string tienPhong, string tienDV, string giamGia, string thanhTien)
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

          

                // Create a paragraph and add content
                Paragraph paragraph = new Paragraph("THE 10 ROYAL HOTEL");
                paragraph.Alignment = Element.ALIGN_CENTER; // Center align
                document.Add(paragraph);

                paragraph = new Paragraph("▶ Address: Ben Nghe Ward, District 1, Ho Chi Minh City.");
                paragraph.SpacingBefore = 5f; // Add spacing
                document.Add(paragraph);

                paragraph = new Paragraph("▶ Information bill: ");
                paragraph.SpacingBefore = 5f; // Add spacing
                document.Add(paragraph);

                paragraph = new Paragraph("1. Bill ID: " + mahd);
                paragraph.SpacingBefore = 5f; // Add spacing
                document.Add(paragraph);
                paragraph = new Paragraph("2. Date to create: " + nglap);
                paragraph.SpacingBefore = 5f; // Add spacing
                document.Add(paragraph);
                paragraph = new Paragraph("3. Staff: " + nv);
                paragraph.SpacingBefore = 5f; // Add spacing
                document.Add(paragraph);
                paragraph = new Paragraph("4. INFORMATION OF CUSTOMER:");
                paragraph.SpacingBefore = 5f; // Add spacing
                document.Add(paragraph);
                paragraph = new Paragraph("- Name: " + tenKH);
                paragraph.SpacingBefore = 5f; // Add spacing
                document.Add(paragraph);
                paragraph = new Paragraph("- CCCD: " + cmnd);
                paragraph.SpacingBefore = 5f; // Add spacing
                document.Add(paragraph);

                paragraph = new Paragraph("- Customer Type: " + loaiKH);
                paragraph.SpacingBefore = 5f; // Add spacing
                document.Add(paragraph);

                paragraph = new Paragraph("- Address: " + diachi);
                paragraph.SpacingBefore = 5f; // Add spacing
                document.Add(paragraph);

                paragraph = new Paragraph("- Nationality: " + quoctich);
                paragraph.SpacingBefore = 5f; // Add spacing
                document.Add(paragraph);

                paragraph = new Paragraph("5. INFORMATION OF ROOM: ");
                paragraph.SpacingBefore = 5f; // Add spacing
                document.Add(paragraph);

                paragraph = new Paragraph("- Room name: " + tenphong);
                paragraph.SpacingBefore = 10f; // Add spacing
                document.Add(paragraph);

                paragraph = new Paragraph("- Room type: " + loaiphong);
                paragraph.SpacingBefore = 5f; // Add spacing
                document.Add(paragraph);

                paragraph = new Paragraph("- Prices: " + dongiaPhong);
                paragraph.SpacingBefore = 5f; // Add spacing
                document.Add(paragraph);

                paragraph = new Paragraph("- Date to check in: " + ngden);
                paragraph.SpacingBefore = 5f; // Add spacing
                document.Add(paragraph);

                paragraph = new Paragraph("- Nights: " + sodem);
                paragraph.SpacingBefore = 5f; // Add spacing
                document.Add(paragraph);

                paragraph = new Paragraph("- Numbers of people: " + soNguoi);
                paragraph.SpacingBefore = 5f; // Add spacing
                document.Add(paragraph);

                paragraph = new Paragraph("6. INFORMATION OF SERVICES: ");
                paragraph.SpacingBefore = 20f; // Add spacing
                document.Add(paragraph);
                // fix sau

                PdfPTable table = new PdfPTable(dichvu.ColumnCount);
                table.WidthPercentage = 100;

                // Thêm tiêu đề cột vào bảng
                foreach (DataGridViewColumn column in dichvu.Columns)
                {
                    PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText, font));
                    table.AddCell(cell);
                }

                // Thêm dữ liệu vào bảng
                foreach (DataGridViewRow row in dichvu.Rows)
                {
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        table.AddCell(new Phrase(cell.Value?.ToString(), font));
                    }
                }

                document.Add(table);


                paragraph = new Paragraph("7. TOTAL: ");
                paragraph.SpacingBefore = 5f; // Add spacing
                document.Add(paragraph);

                paragraph = new Paragraph("==> Room fee: " + tienPhong);
                paragraph.SpacingBefore = 5f; // Add spacing
                document.Add(paragraph);
                paragraph = new Paragraph("==> Services fee: " + tienDV);
                paragraph.SpacingBefore = 5f; // Add spacing
                document.Add(paragraph);
                paragraph = new Paragraph("==> Discount: " + giamGia);
                paragraph.SpacingBefore = 5f; // Add spacing
                document.Add(paragraph);
                paragraph = new Paragraph("==> All fees: " + thanhTien);
                paragraph.SpacingBefore = 5f; // Add spacing
                document.Add(paragraph);







                document.Close(); // Close the document

                // Open the PDF using an external program (optional)
                System.Diagnostics.Process.Start("HoaDon.pdf");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
