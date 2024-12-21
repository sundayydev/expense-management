using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace DAL.Utils
{
    public static class SecureStorage
    {
        private static readonly string filePath = "credentials.dat";
        private static readonly byte[] key = Encoding.UTF8.GetBytes("2280601102228060"); // Thay bằng key bảo mật riêng
        private static readonly byte[] iv = Encoding.UTF8.GetBytes("ngaymaitroidepdo");   // Thay bằng IV bảo mật riêng

        public static void SaveCredentials(string email, string password)
        {
            string data = $"{email}:{password}";
            byte[] encryptedData = EncryptString(data);
            File.WriteAllBytes(filePath, encryptedData);
        }

        public static (string Email, string Password) LoadCredentials()
        {
            if (!File.Exists(filePath)) return (null, null);
            byte[] encryptedData = File.ReadAllBytes(filePath);
            string decryptedData = DecryptString(encryptedData);
            string[] parts = decryptedData.Split(':');
            return (parts[0], parts[1]);
        }

        private static byte[] EncryptString(string plainText)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                aes.IV = iv;
                using (MemoryStream ms = new MemoryStream())
                using (CryptoStream cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
                    cs.Write(plainBytes, 0, plainBytes.Length);
                    cs.FlushFinalBlock();
                    return ms.ToArray();
                }
            }
        }

        private static string DecryptString(byte[] encryptedData)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                aes.IV = iv;
                using (MemoryStream ms = new MemoryStream(encryptedData))
                using (CryptoStream cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Read))
                {
                    byte[] plainBytes = new byte[encryptedData.Length];
                    int bytesRead = cs.Read(plainBytes, 0, plainBytes.Length);
                    return Encoding.UTF8.GetString(plainBytes, 0, bytesRead);
                }
            }
        }
    }
}
