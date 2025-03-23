// See https://aka.ms/new-console-template for more information
using CarParkingBooking.QRCodeGenerator.Generator;

Console.WriteLine("Hello, World!");

IQrCodeService qrCodeService = new QrCodeService();
qrCodeService.GenerateQrCode("hello world");
