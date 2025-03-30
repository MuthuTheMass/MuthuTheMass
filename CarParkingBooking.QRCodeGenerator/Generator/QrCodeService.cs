using System.Drawing;
using System.Drawing.Imaging;
using ZXing;
using ZXing.Common;

namespace CarParkingBooking.QRCodeGenerator.Generator
{
    public interface IQrCodeService
    {
        Task<byte[]> GenerateQrCode(string text);
    }

    public class QrCodeService : IQrCodeService
    {
        public async Task<byte[]> GenerateQrCode(string text)
        {
            return await Task.Run(() =>
            {
                var qrWriter = new BarcodeWriterPixelData
                {
                    Format = BarcodeFormat.QR_CODE,
                    Options = new EncodingOptions
                    {
                        Height = 250,
                        Width = 250,
                        Margin = 1
                    }
                };

                var pixelData = qrWriter.Write(text);
                using (var bitmap = new Bitmap(pixelData.Width, pixelData.Height, PixelFormat.Format32bppRgb))
                using (var ms = new MemoryStream())
                {
                    var bitmapData = bitmap.LockBits(new Rectangle(0, 0, pixelData.Width, pixelData.Height),
                        ImageLockMode.WriteOnly, PixelFormat.Format32bppRgb);
                    try
                    {
                        System.Runtime.InteropServices.Marshal.Copy(pixelData.Pixels, 0, bitmapData.Scan0,
                            pixelData.Pixels.Length);
                    }
                    finally
                    {
                        bitmap.UnlockBits(bitmapData);
                    }

                    bitmap.Save(ms, ImageFormat.Png);
                    return ms.ToArray();
                }
            });
        }
    }
}
