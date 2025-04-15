using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace CarParkingBooking.QRCodeGenerator.PDFGenerator.UserPDF;

public class GenerateUserBookingPDF : IDocument
{
    public string BookingId { get; set; }
    public string CustomerName { get; set; }
    public string CustomerAddress { get; set; }

    public string PostalCode
    {
        get; set;
    }
    public string Country { get; set; }
    public string Date { get; set; }

    public string DealerName { get; set; }
    public string DealerAddress { get; set; }
    public string DealerCity { get; set; }
    public string DealerEmail { get; set; }

    public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

    public void Compose(IDocumentContainer container)
    {
        container.Page(page =>
        {
            page.Margin(40);
            page.DefaultTextStyle(x => x.FontSize(12));
            page.Content()
                .Column(column =>
                {
                    column.Item().Row(row =>
                    {
                        row.RelativeItem().Text("ParkZone").FontSize(20).SemiBold();
                        row.ConstantItem(200).AlignRight().Text("Invoice").FontSize(18).Bold().FontColor(Colors.Blue.Medium);
                    });

                    column.Item().PaddingVertical(10).LineHorizontal(1);

                    column.Item().Row(row =>
                    {
                        row.RelativeItem().Text(txt =>
                        {
                            txt.Span("Date: ").SemiBold();
                            txt.Span(Date);
                        });

                        row.ConstantItem(200).AlignRight().Text(txt =>
                        {
                            txt.Span("Booking Id: ").SemiBold();
                            txt.Span(BookingId);
                        });
                    });

                    column.Item().PaddingVertical(10).LineHorizontal(1);

                    column.Item().Row(row =>
                    {
                        row.RelativeItem().Column(col =>
                        {
                            col.Item().Text("Vechile Details:").Bold();
                            col.Item().Text(CustomerName);
                            col.Item().Text(CustomerAddress);
                            col.Item().Text(PostalCode);
                            col.Item().Text(Country);
                        });

                        row.ConstantItem(250).AlignRight().Column(col =>
                        {
                            col.Item().Text("Dealer Details:").Bold();
                            col.Item().Text(DealerName);
                            col.Item().Text(DealerAddress);
                            col.Item().Text(DealerCity);
                            col.Item().Text(DealerEmail);
                        });
                    });
                });
        });
    }
}