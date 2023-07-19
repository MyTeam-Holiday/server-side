using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

public class Crypt
{
    private static readonly byte[] key = KeyGenerator.GenerateKey();
    private static readonly byte[] iv = KeyGenerator.GenerateIV();

    public static void EncryptFile(string filePath)
    {
        try
        {
            string fileContent = File.ReadAllText(filePath);
            byte[] encryptedBytes = EncryptStringToBytes(fileContent);

            string encryptedString = Convert.ToBase64String(encryptedBytes);
            File.WriteAllText(filePath, encryptedString);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error encrypting file: {ex.Message}");
        }
    }



    public static void DecryptFile(string filePath)
    {
        try
        {
            string encryptedString = File.ReadAllText(filePath);
            byte[] encryptedBytes = Convert.FromBase64String(encryptedString);

            string decryptedString = DecryptBytesToString(encryptedBytes);
            File.WriteAllText(filePath, decryptedString);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error decrypting file: {ex.Message}");
        }
    }

    private static byte[] EncryptStringToBytes(string plainText)
    {
        using (AesManaged aes = new AesManaged())
        {
            aes.Key = key;
            aes.IV = iv;

            ICryptoTransform encryptor = aes.CreateEncryptor();
            byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);

            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                {
                    cs.Write(plainBytes, 0, plainBytes.Length);
                }
                return ms.ToArray();
            }
        }
    }

    private static string DecryptBytesToString(byte[] encryptedBytes)
    {
        using (AesManaged aes = new AesManaged())
        {
            aes.Key = key;
            aes.IV = iv;

            ICryptoTransform decryptor = aes.CreateDecryptor();

            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Write))
                {
                    cs.Write(encryptedBytes, 0, encryptedBytes.Length);
                }
                byte[] decryptedBytes = ms.ToArray();
                return Encoding.UTF8.GetString(decryptedBytes);
            }
        }
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
