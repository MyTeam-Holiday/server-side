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

            string[] jsonFilePaths = new string[]
            {
                "appsettings.dev.json",
                "пappsettings.prod.json",
                "appsettings.uat.json",
            
            };
            
            foreach (string jsonFilePath in jsonFilePaths)
            {
                if (!IsEncrypted(jsonFilePath))
                {
                    Crypt.EncryptFile(jsonFilePath);
                }
            }
            
        }
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
        
    }
    
}
