using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OrderTask.Model;
using OrderTask.Service.ServiceInterface;

namespace OrderTask.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = BuildWebHost(args);
            //初始化数据库
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var logService = services.GetRequiredService<ILogService<Program>>();
                try
                {
                    var codeFrameContext = services.GetRequiredService<OrderTaskContext>();
                    OrderTaskContextSend.SeedAsync(codeFrameContext)
                        .Wait();

                }
                catch (Exception ex)
                {
                    logService.Error("An error occurred seeding the DB", ex);
                }
            }
            host.Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
