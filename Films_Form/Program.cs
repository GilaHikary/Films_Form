using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting.WindowsServices;

namespace Films_Form
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //{
            //    CreateWebHostBuilder(args).Build().Run();
            //}

            //public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            //    WebHost.CreateDefaultBuilder(args)
            //        .UseStartup<Startup>();

            // получаем путь к файлу 
            var pathToExe = Process.GetCurrentProcess().MainModule.FileName;
            // путь к каталогу проекта
            var pathToContentRoot = Path.GetDirectoryName(pathToExe);
            // создаем хост
            var host = WebHost.CreateDefaultBuilder(args)
                    .UseContentRoot(pathToContentRoot)
                    .UseStartup<Startup>()
                    .Build();
            // запускаем в виде службы
            host.RunAsService();

        }
    }
}
