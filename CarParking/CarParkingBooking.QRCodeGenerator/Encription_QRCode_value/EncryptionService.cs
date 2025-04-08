using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;

namespace CarParkingBooking.QRCodeGenerator.Encription_QRCode_value
{
    public interface IEncryptionService
    {
        Task<string> EncryptAsync(string text);
        Task<string> DecryptAsync(string text);
    }

    public class EncryptionService : IEncryptionService
    {
        private readonly byte[] _key; // 32 bytes for AES-256
        private readonly byte[] _iv;  // 16 bytes for AES

        public EncryptionService()
        {
            using (Aes aes = Aes.Create())
            {
                aes.KeySize = 256;
                aes.GenerateKey();
                aes.GenerateIV();

                _key = aes.Key;
                _iv = aes.IV;
            }
        }

        public async Task<string> EncryptAsync(string plainText)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = _key;
                aes.IV = _iv;

                using (MemoryStream memoryStream = new MemoryStream())
                using (CryptoStream cryptoStream = new CryptoStream(memoryStream, aes.CreateEncryptor(), CryptoStreamMode.Write))
                using (StreamWriter writer = new StreamWriter(cryptoStream, Encoding.UTF8))
                {
                    await writer.WriteAsync(plainText);
                    await writer.FlushAsync();
                    await cryptoStream.FlushFinalBlockAsync();
                    return Convert.ToBase64String(memoryStream.ToArray());
                }
            }
        }

        public async Task<string> DecryptAsync(string encryptedText)
        {
            byte[] encryptedBytes = Convert.FromBase64String(encryptedText);

            using (Aes aes = Aes.Create())
            {
                aes.Key = _key;
                aes.IV = _iv;

                using (MemoryStream memoryStream = new MemoryStream(encryptedBytes))
                using (CryptoStream cryptoStream = new CryptoStream(memoryStream, aes.CreateDecryptor(), CryptoStreamMode.Read))
                using (StreamReader reader = new StreamReader(cryptoStream, Encoding.UTF8))
                {
                    return await reader.ReadToEndAsync();
                }
            }
        }

#if DEBUG
        public string GetKeyAsBase64() => Convert.ToBase64String(_key);
        public string GetIVAsBase64() => Convert.ToBase64String(_iv);
#endif
    }
}
