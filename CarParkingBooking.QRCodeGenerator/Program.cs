// See https://aka.ms/new-console-template for more information
using CarParkingBooking.QRCodeGenerator.Generator;

Console.WriteLine("Hello, World!");

IQrCodeService qrCodeService = new QrCodeService();
var G =qrCodeService.GenerateQrCode("hello world").GetAwaiter().GetResult();
Console.WriteLine(G);
