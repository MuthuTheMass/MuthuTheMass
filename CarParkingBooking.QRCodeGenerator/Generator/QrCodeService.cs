using System.Drawing.Imaging;
using System.Drawing;
using ZXing.Common;
using ZXing;

namespace CarParkingBooking.QRCodeGenerator.Generator
{
    public interface IQrCodeService
    {
        string GenerateQrCode(string text);
    }

    public class QrCodeService : IQrCodeService
    {
        public string GenerateQrCode(string text)
        {
            var writer = new BarcodeWriter<Bitmap>
            {
                Format = BarcodeFormat.QR_CODE,
                Options = new EncodingOptions
                {
                    Height = 500,
                    Width = 500,
                    Margin = 1
                },
                Renderer = new ImageSharpRenderer()
            };

            using (Bitmap bitmap = writer.Write(text))
            {
                string filePath = "MyQR.png";
                bitmap.Save(filePath, ImageFormat.Png);
                Console.WriteLine($"QR Code saved as {filePath}");
            }
            return string.Empty;
        }
    }
}
