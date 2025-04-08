using CarParkingSystem.Application.Dtos.Booking;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace CarParkingBooking.QRCodeGenerator.PDFGenerator;

public interface IGeneratePdf
{
    Task<byte[]> BookingConfirmation(CarBookingDetailDto booking);
}

public class GeneratePdf : IGeneratePdf
{
    public async Task<byte[]> BookingConfirmation(CarBookingDetailDto booking)
    {
        using var ms = new MemoryStream();
        var document = new Document(PageSize.A4, 40, 40, 60, 40);
        PdfWriter.GetInstance(document, ms);
        document.Open();

        // Fonts
        var titleFont = FontFactory.GetFont("Helvetica", 18, Font.BOLD, new BaseColor(255, 255, 255));
        var labelFont = FontFactory.GetFont("Helvetica", 12, Font.BOLD);
        var valueFont = FontFactory.GetFont("Helvetica", 12);

        // Header
        var headerTable = new PdfPTable(1) { WidthPercentage = 100 };
        var headerCell = new PdfPCell(new Phrase("Zenpark Booking Confirmation", titleFont))
        {
            BackgroundColor = new BaseColor(0, 102, 204),
            HorizontalAlignment = Element.ALIGN_CENTER,
            Padding = 12,
            Border = Rectangle.NO_BORDER
        };
        headerTable.AddCell(headerCell);
        document.Add(headerTable);

        // Spacer
        document.Add(new Paragraph("\n"));

        // Booking Details Table
        var table = new PdfPTable(2) { WidthPercentage = 100 };
        table.SetWidths(new float[] { 30f, 70f });

        void AddRow(string label, string value)
        {
            table.AddCell(new PdfPCell(new Phrase(label, labelFont)) { Border = Rectangle.NO_BORDER, Padding = 5 });
            table.AddCell(new PdfPCell(new Phrase(value, valueFont)) { Border = Rectangle.NO_BORDER, Padding = 5 });
        }

        AddRow("Booking ID:", booking.BookingId);
        AddRow("Full Name:", booking.CustomerName);
        AddRow("Email:", booking.CustomerEmail);
        AddRow("Mobile Number:", booking.CustomerPhoneNumber);
        AddRow("Vehicle Number:", booking.VehicleNumber);
        AddRow("Vehicle Model:", booking.VehicleModel);
        AddRow("Booking Date:", booking.BookingFromDate.ToString());
        AddRow("Alloted Slot:", booking.AllottedSlots);
        AddRow("Advance Amount:", booking.AdvanceAmount);

        document.Add(table);

        // Spacer before QR
        document.Add(new Paragraph("\n"));

        // ✅ Add QR Code if available
        if (!string.IsNullOrWhiteSpace(booking.QrCode))
        {
            string base64Qr = booking.QrCode;

            // Remove data URI prefix if present
            if (base64Qr.StartsWith("data:image"))
            {
                base64Qr = base64Qr.Substring(base64Qr.IndexOf(',') + 1);
            }

            byte[] qrBytes = Convert.FromBase64String(base64Qr);
            var qrImage = iTextSharp.text.Image.GetInstance(qrBytes);
            qrImage.ScaleAbsolute(100f, 100f); // Resize QR
            qrImage.Alignment = Element.ALIGN_CENTER;
            document.Add(qrImage);
        }

        // Footer
        document.Add(new Paragraph("\n\nThank you for your booking!", valueFont)
        {
            Alignment = Element.ALIGN_CENTER
        });

        document.Close();

        return await Task.FromResult(ms.ToArray());
    }
}