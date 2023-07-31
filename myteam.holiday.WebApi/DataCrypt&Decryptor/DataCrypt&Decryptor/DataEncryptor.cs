using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;

namespace DataCrypt_Decryptor
{
    internal class DataEncryptor
    {
        private string key;
        private string iv;

        public DataEncryptor(string key, string iv)
        {
            this.key = key;
            this.iv = iv;
        }

        // Метод для шифрования данных
        public string Encrypt(string data)
        {
            using Aes aesAlg = Aes.Create();
            aesAlg.Key = Encoding.UTF8.GetBytes(key);
            aesAlg.IV = Encoding.UTF8.GetBytes(iv);

            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

            using MemoryStream msEncrypt = new MemoryStream();
            using CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write);
            using StreamWriter swEncrypt = new StreamWriter(csEncrypt);

            swEncrypt.Write(data);

            return Convert.ToBase64String(msEncrypt.ToArray());
        }

        // Метод для дешифрования данных
        public string Decrypt(string encryptedData)
        {
            byte[] cipherText = Convert.FromBase64String(encryptedData);
            using Aes aesAlg = Aes.Create();
            aesAlg.Key = Encoding.UTF8.GetBytes(key);
            aesAlg.IV = Encoding.UTF8.GetBytes(iv);

            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

            using MemoryStream msDecrypt = new MemoryStream(cipherText);
            using CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
            using StreamReader srDecrypt = new StreamReader(csDecrypt);

            return srDecrypt.ReadToEnd();
        }
        public static byte[] GenerateKey()
        {
            using (AesManaged aes = new AesManaged())
            {
                aes.GenerateKey();
                return aes.Key;
            }
        }

        public static byte[] GenerateIV()
        {
            using (AesManaged aes = new AesManaged())
            {
                aes.GenerateIV();
                return aes.IV;
            }
        }
    }
}
