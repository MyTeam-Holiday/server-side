using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCrypt_Decryptor
{
   
    internal class Program
    {
        public static string key;
        public static string iv;
        static void Main(string[] args)
        {
            DataEncryptor.GenerateKey();
            DataEncryptor.GenerateIV();
            string jsonData = File.ReadAllText("appsettings.json");
            key = "your_secret_key";
             iv = "your_initialization_vector";

            DataEncryptor dataEncryptor = new DataEncryptor(key, iv);

            // Путь к файлу appsettings.json , реальное название хз какое будет, поправьте, кто увидет, там же файл после билда будет или типо того, ну вы поняли
            string appSettingsPath = "appsettings.json";

            if (File.Exists(appSettingsPath))
            {
                // Чтение данных из файла
                string jsonDataFromFile = File.ReadAllText(appSettingsPath);

                // Проверка наличия зашифрованных данных
                bool isEncrypted = jsonDataFromFile.Contains("Encrypted:");

                if (!isEncrypted)
                {
                    // Шифрование данных
                    string encryptedData = dataEncryptor.Encrypt(jsonData);

                    // Добавление пометки о зашифрованных данных
                    jsonDataFromFile = $"Encrypted: {encryptedData}";

                    // Сохранение обратно в файл
                    File.WriteAllText(appSettingsPath, jsonDataFromFile);
                    Console.WriteLine("Данные зашифрованы и сохранены в appsettings.json.");
                }
                else
                {
                    // Разбираем пометку для получения зашифрованных данных
                    int startIndex = jsonDataFromFile.IndexOf(": ") + 2;
                    string encryptedData = jsonDataFromFile.Substring(startIndex);

                    // Дешифрование данных
                    string decryptedData = dataEncryptor.Decrypt(encryptedData);
                    jsonDataFromFile = $"Decrypted: {encryptedData}";
                    File.WriteAllText(appSettingsPath, jsonDataFromFile);
                    Console.WriteLine("Данные расшифрованы и сохранены в appsettings.json.");

                }
            }
            else
            {
                Console.WriteLine("Файл appsettings.json не найден.");
            }
        }
    }
}
