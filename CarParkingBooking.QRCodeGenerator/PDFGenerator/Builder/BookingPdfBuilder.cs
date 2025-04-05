using CarParkingSystem.Application.Dtos.Booking;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace CarParkingBooking.QRCodeGenerator.PDFGenerator.Builder;


public class BookingPdfBuilder 
{
   private readonly Document _document;
    private readonly CarBookingDetailDto _booking;
    private readonly Font _titleFont;
    private readonly Font _labelFont;
    private readonly Font _valueFont;

    public BookingPdfBuilder(Document document, CarBookingDetailDto booking)
    {
        _document = document;
        _booking = booking;

        _titleFont = FontFactory.GetFont("Helvetica", 18, Font.BOLD, new BaseColor(255, 255, 255));
        _labelFont = FontFactory.GetFont("Helvetica", 12, Font.BOLD);
        _valueFont = FontFactory.GetFont("Helvetica", 12);
    }

    public async Task BuildPdfAsync()
    {
        AddHeader();
        AddSpacer();
        AddBookingDetails();
        AddSpacer();
        await AddQrCodeAsync();
        AddFooter();
    }

    private void AddHeader()
    {
        var table = new PdfPTable(1) { WidthPercentage = 100 };
        var cell = new PdfPCell(new Phrase("Zenpark Booking Confirmation", _titleFont))
        {
            BackgroundColor = new BaseColor(0, 102, 204),
            HorizontalAlignment = Element.ALIGN_CENTER,
            Padding = 12,
            Border = Rectangle.NO_BORDER
        };
        table.AddCell(cell);
        _document.Add(table);
    }

    private void AddSpacer() => _document.Add(new Paragraph("\n"));

    private void AddBookingDetails()
    {
        var table = new PdfPTable(2) { WidthPercentage = 100 };
        table.SetWidths(new float[] { 30f, 70f });

        AddRow(table, "Booking ID:", _booking?.BookingId ?? "N/A");
        AddRow(table, "Full Name:", _booking?.CustomerName ?? "N/A");
        AddRow(table, "Email:", _booking?.CustomerEmail ?? "N/A");
        AddRow(table, "Mobile Number:", _booking?.CustomerPhoneNumber ?? "N/A");
        AddRow(table, "Vehicle Number:", _booking?.VehicleNumber ?? "N/A");
        AddRow(table, "Vehicle Model:", _booking?.VehicleModel ?? "N/A");
        AddRow(table, "Booking Date:", _booking?.BookingFromDate.ToString() ?? "N/A");
        AddRow(table, "Allotted Slot:", _booking?.AllottedSlots ?? "N/A");
        AddRow(table, "Advance Amount:", _booking?.AdvanceAmount ?? "N/A");

        _document.Add(table);
    }

    private void AddRow(PdfPTable table, string label, string value)
    {
        table.AddCell(new PdfPCell(new Phrase(label, _labelFont)) { Border = Rectangle.NO_BORDER, Padding = 5 });
        table.AddCell(new PdfPCell(new Phrase(value, _valueFont)) { Border = Rectangle.NO_BORDER, Padding = 5 });
    }

    private async Task AddQrCodeAsync()
    {
        if (string.IsNullOrWhiteSpace(_booking?.QrCode))
            return;

        string base64 = ExtractBase64Image(_booking.QrCode);
        byte[] imageBytes = Convert.FromBase64String(base64);

        await Task.Run(() =>
        {
            var image = iTextSharp.text.Image.GetInstance(imageBytes);
            image.ScaleAbsolute(100f, 100f);
            image.Alignment = Element.ALIGN_CENTER;

            _document.Add(image);
        });
    }

    private void AddFooter()
    {
        _document.Add(new Paragraph("\nThank you for your booking!", _valueFont)
        {
            Alignment = Element.ALIGN_CENTER
        });
    }

    private string ExtractBase64Image(string base64DataUrl)
    {
        if (base64DataUrl.StartsWith("data:image"))
        {
            return base64DataUrl.Substring(base64DataUrl.IndexOf(',') + 1);
        }

        return base64DataUrl;
    }
}