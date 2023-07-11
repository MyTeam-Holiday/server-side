using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using myteam.holiday.Domain.Models;
using System.IO;
using myteam.holiday.WebApi;

namespace myteam.holiday.init
{
    public partial class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
            // Получаем ключ шифрования
        key = GetEncryptionKey();

      string appSettingsFileName = "appsettings.json";
    string appSettingsFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, appSettingsFileName);


        // Зашифровываем appsettings.json, если он не зашифрован
        if (!IsEncrypted(appSettingsFilePath))
        {
            EncryptAppSettings(appSettingsFilePath);
        }

        // Расшифровываем appsettings.json и выполняем остальную логику приложения
        DecryptAppSettings(appSettingsFilePath);
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

private static byte[] GetEncryptionKey()
    {
        string keyString = Environment.GetEnvironmentVariable(KeyEnvironmentVariableName);
        if (string.IsNullOrEmpty(keyString))
        {
            // Если переменная среды не установлена, генерируем новый ключ
            byte[] generatedKey = GenerateKey();
            Environment.SetEnvironmentVariable(KeyEnvironmentVariableName, Convert.ToBase64String(generatedKey));
            return generatedKey;
        }
        else
        {
            // Если переменная среды установлена, используем ее значение
            return Convert.FromBase64String(keyString);
        }
    }

    private static byte[] GenerateKey()
    {
        using (AesManaged aes = new AesManaged())
        {
            aes.GenerateKey();
            return aes.Key;
        }
    }

    private static bool IsEncrypted(string filePath)
    {
        string fileContent = File.ReadAllText(filePath);
        return !string.IsNullOrWhiteSpace(fileContent) && fileContent.TrimStart().StartsWith("encrypted:");
    }

    private static void EncryptAppSettings(string filePath)
    {
        string plainText = File.ReadAllText(filePath);

        byte[] encryptedBytes;
        using (AesManaged aes = new AesManaged())
        {
            aes.Key = key;
            aes.IV = aes.Key;
            ICryptoTransform encryptor = aes.CreateEncryptor();

            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                {
                    cs.Write(plainTextBytes, 0, plainTextBytes.Length);
                }
                encryptedBytes = ms.ToArray();
            }
        }

        string encryptedText = Convert.ToBase64String(encryptedBytes);
        File.WriteAllText(filePath, "encrypted:" + encryptedText);
    }

    private static void DecryptAppSettings(string filePath)
    {
        string fileContent = File.ReadAllText(filePath);
        if (fileContent.StartsWith("encrypted:"))
        {
            string encryptedText = fileContent.Substring("encrypted:".Length);

            byte[] encryptedBytes = Convert.FromBase64String(encryptedText);
            string decryptedText;
            using (AesManaged aes = new AesManaged())
            {
                aes.Key = key;
                aes.IV = aes.Key;
                ICryptoTransform decryptor = aes.CreateDecryptor();

                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Write))
                    {
                        cs.Write(encryptedBytes, 0, encryptedBytes.Length);
                    }
                    byte[] decryptedBytes = ms.ToArray();
                    decryptedText = Encoding.UTF8.GetString(decryptedBytes);
                }
            }

            File.WriteAllText(filePath, decryptedText);
        }


        
    }
}
