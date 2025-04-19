using CarParkingSystem.Infrastructure.Database.CosmosDatabase.Entities;
using iTextSharp.text.pdf;
using iTextSharp.text;

namespace CarParkingBooking.QRCodeGenerator.PDFGenerator;

public class GenerateUserBookingPdf
{
    public async Task<byte[]> GeneratePdfAsync(CarBooking booking)
    {
        return await Task.Run(() =>
        {
            using var memoryStream = new MemoryStream();
            using (Document document = new Document(PageSize.A4))
            {
                using (PdfWriter writer = PdfWriter.GetInstance(document, memoryStream))
                {
                    document.Open();

                    document.Add(new Paragraph("Car Booking Confirmation", new Font(Font.HELVETICA, 20, Font.BOLD))
                        { Alignment = Element.ALIGN_CENTER });

                    var bookingTable = new PdfPTable(2) { WidthPercentage = 100 };
                    bookingTable.AddCell(new PdfPCell(new Phrase("Booking ID:")) { Border = Rectangle.NO_BORDER });
                    bookingTable.AddCell(new PdfPCell(new Phrase(booking.id ?? "N/A"))
                        { Border = Rectangle.NO_BORDER });
                    bookingTable.AddCell(new PdfPCell(new Phrase("Encrypted Booking ID:"))
                        { Border = Rectangle.NO_BORDER });
                    bookingTable.AddCell(new PdfPCell(new Phrase(booking.EncryptedBookingId ?? "N/A"))
                        { Border = Rectangle.NO_BORDER });
                    bookingTable.AddCell(new PdfPCell(new Phrase("Dealer ID:")) { Border = Rectangle.NO_BORDER });
                    bookingTable.AddCell(new PdfPCell(new Phrase(booking.DealerId ?? "N/A"))
                        { Border = Rectangle.NO_BORDER });
                    bookingTable.AddCell(new PdfPCell(new Phrase("Booking Source:")) { Border = Rectangle.NO_BORDER });
                    bookingTable.AddCell(new PdfPCell(new Phrase(booking.BookingSource ?? "N/A"))
                        { Border = Rectangle.NO_BORDER });
                    bookingTable.AddCell(new PdfPCell(new Phrase("Created Date:")) { Border = Rectangle.NO_BORDER });
                    bookingTable.AddCell(
                        new PdfPCell(new Phrase(booking?.CreatedDate?.ToString("dd MMM yyyy HH:mm") ?? "N/A"))
                            { Border = Rectangle.NO_BORDER });
                    document.Add(bookingTable);

                    var customerTable = new PdfPTable(2) { WidthPercentage = 100 };
                    customerTable.AddCell(new PdfPCell(new Phrase("Customer Details:"))
                        { Border = Rectangle.NO_BORDER, Colspan = 2, HorizontalAlignment = Element.ALIGN_LEFT });
                    customerTable.AddCell(new PdfPCell(new Phrase("Name:")) { Border = Rectangle.NO_BORDER });
                    customerTable.AddCell(new PdfPCell(new Phrase(booking.CustomerData.CustomerName ?? "N/A"))
                        { Border = Rectangle.NO_BORDER });
                    customerTable.AddCell(new PdfPCell(new Phrase("Email:")) { Border = Rectangle.NO_BORDER });
                    customerTable.AddCell(new PdfPCell(new Phrase(booking.CustomerData.CustomerEmail ?? "N/A"))
                        { Border = Rectangle.NO_BORDER });
                    customerTable.AddCell(new PdfPCell(new Phrase("Mobile:")) { Border = Rectangle.NO_BORDER });
                    customerTable.AddCell(new PdfPCell(new Phrase(booking.CustomerData.CustomerMobileNumber ?? "N/A"))
                        { Border = Rectangle.NO_BORDER });
                    customerTable.AddCell(new PdfPCell(new Phrase("Address:")) { Border = Rectangle.NO_BORDER });
                    customerTable.AddCell(new PdfPCell(new Phrase(booking.CustomerData.CustomerAddress ?? "N/A"))
                        { Border = Rectangle.NO_BORDER });
                    document.Add(customerTable);

                    var vehicleTable = new PdfPTable(2) { WidthPercentage = 100 };
                    vehicleTable.AddCell(new PdfPCell(new Phrase("Vehicle Details:"))
                        { Border = Rectangle.NO_BORDER, Colspan = 2, HorizontalAlignment = Element.ALIGN_LEFT });
                    vehicleTable.AddCell(new PdfPCell(new Phrase("Vehicle ID:")) { Border = Rectangle.NO_BORDER });
                    vehicleTable.AddCell(new PdfPCell(new Phrase(booking.VehicleInfo.VehicleId ?? "N/A"))
                        { Border = Rectangle.NO_BORDER });
                    vehicleTable.AddCell(new PdfPCell(new Phrase("Vehicle Number:")) { Border = Rectangle.NO_BORDER });
                    vehicleTable.AddCell(new PdfPCell(new Phrase(booking.VehicleInfo.VehicleNumber ?? "N/A"))
                        { Border = Rectangle.NO_BORDER });
                    vehicleTable.AddCell(new PdfPCell(new Phrase("Vehicle Model:")) { Border = Rectangle.NO_BORDER });
                    vehicleTable.AddCell(new PdfPCell(new Phrase(booking.VehicleInfo.VehicleModel ?? "N/A"))
                        { Border = Rectangle.NO_BORDER });
                    document.Add(vehicleTable);

                    var datesTable = new PdfPTable(2) { WidthPercentage = 100 };
                    datesTable.AddCell(new PdfPCell(new Phrase("Booking Dates:"))
                        { Border = Rectangle.NO_BORDER, Colspan = 2, HorizontalAlignment = Element.ALIGN_LEFT });
                    datesTable.AddCell(new PdfPCell(new Phrase("User Booking Date:")) { Border = Rectangle.NO_BORDER });
                    datesTable.AddCell(
                        new PdfPCell(new Phrase(booking.BookingDate.UserBookingDate?.ToString("dd MMM yyyy") ?? "N/A"))
                            { Border = Rectangle.NO_BORDER });
                    datesTable.AddCell(new PdfPCell(new Phrase("From:")) { Border = Rectangle.NO_BORDER });
                    datesTable.AddCell(
                        new PdfPCell(new Phrase(booking.BookingDate.From?.ToString("dd MMM yyyy HH:mm") ?? "N/A"))
                            { Border = Rectangle.NO_BORDER });
                    datesTable.AddCell(new PdfPCell(new Phrase("To:")) { Border = Rectangle.NO_BORDER });
                    datesTable.AddCell(
                        new PdfPCell(new Phrase(booking.BookingDate.To?.ToString("dd MMM yyyy HH:mm") ?? "N/A"))
                            { Border = Rectangle.NO_BORDER });
                    document.Add(datesTable);

                    if (booking.Payment != null)
                    {
                        var paymentTable = new PdfPTable(2) { WidthPercentage = 100 };
                        paymentTable.AddCell(new PdfPCell(new Phrase("Payment Info:"))
                            { Border = Rectangle.NO_BORDER, Colspan = 2, HorizontalAlignment = Element.ALIGN_LEFT });
                        paymentTable.AddCell(new PdfPCell(new Phrase("Transaction ID:"))
                            { Border = Rectangle.NO_BORDER });
                        paymentTable.AddCell(new PdfPCell(new Phrase(booking.Payment.TransactionId ?? "N/A"))
                            { Border = Rectangle.NO_BORDER });
                        paymentTable.AddCell(new PdfPCell(new Phrase("Advance:")) { Border = Rectangle.NO_BORDER });
                        paymentTable.AddCell(new PdfPCell(new Phrase(booking.Payment.AdvanceAmount ?? "N/A"))
                            { Border = Rectangle.NO_BORDER });
                        paymentTable.AddCell(new PdfPCell(new Phrase("Due:")) { Border = Rectangle.NO_BORDER });
                        paymentTable.AddCell(new PdfPCell(new Phrase(booking.Payment.Due_Amount ?? "N/A"))
                            { Border = Rectangle.NO_BORDER });
                        paymentTable.AddCell(new PdfPCell(new Phrase("Final Amount:"))
                            { Border = Rectangle.NO_BORDER });
                        paymentTable.AddCell(new PdfPCell(new Phrase(booking.Payment.Final_Amount ?? "N/A"))
                            { Border = Rectangle.NO_BORDER });
                        paymentTable.AddCell(new PdfPCell(new Phrase("Currency:")) { Border = Rectangle.NO_BORDER });
                        paymentTable.AddCell(new PdfPCell(new Phrase(booking.Payment.CurrencyMode.ToString() ?? "N/A"))
                            { Border = Rectangle.NO_BORDER });
                        paymentTable.AddCell(new PdfPCell(new Phrase("Payment Method:"))
                            { Border = Rectangle.NO_BORDER });
                        paymentTable.AddCell(new PdfPCell(new Phrase(booking.Payment.PaymentMethod.ToString() ?? "N/A"))
                            { Border = Rectangle.NO_BORDER });
                        paymentTable.AddCell(new PdfPCell(new Phrase("Payment Status:"))
                            { Border = Rectangle.NO_BORDER });
                        paymentTable.AddCell(new PdfPCell(new Phrase(booking.Payment.status.ToString() ?? "N/A"))
                            { Border = Rectangle.NO_BORDER });
                        document.Add(paymentTable);
                    }

                    var statusTable = new PdfPTable(2) { WidthPercentage = 100 };
                    statusTable.AddCell(new PdfPCell(new Phrase("Booking Status:"))
                    {
                        Border = Rectangle.NO_BORDER, Colspan = 2, HorizontalAlignment = Element.ALIGN_LEFT
                    });
                    statusTable.AddCell(new PdfPCell(new Phrase("State:")) { Border = Rectangle.NO_BORDER });
                    statusTable.AddCell(new PdfPCell(new Phrase(booking.BookingStatus.State.ToString() ?? "N/A"))
                        { Border = Rectangle.NO_BORDER });
                    statusTable.AddCell(new PdfPCell(new Phrase("Reason:")) { Border = Rectangle.NO_BORDER });
                    statusTable.AddCell(new PdfPCell(new Phrase(booking.BookingStatus.Reason ?? "N/A"))
                        { Border = Rectangle.NO_BORDER });
                    document.Add(statusTable);

                    if (booking.GeneratedQrCode != null)
                    {
                        var qrImage = Image.GetInstance(booking.GeneratedQrCode);
                        qrImage.ScaleToFit(100f, 100f);
                        qrImage.Alignment = Element.ALIGN_CENTER;
                        document.Add(new Paragraph("Scan QR Code"));
                        document.Add(qrImage);
                    }

                    document.Close();
                }
            }

            return memoryStream.ToArray();
        });
    }
}

