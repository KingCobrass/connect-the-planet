using Connect.API.Infrastructure;
using Connect.API.Infrastructure.Providers;
using Connect.API.Models;
using Connect.API.Models.Configuration;
using Connect.API.Services;
using Connect.Interface.Account;
using Connect.Interface.Chat;
using Connect.Interface.Configuration;
using Connect.Interface.Database;
using Connect.Interface.Logger;
using Connect.Interface.Response;
using Connect.Interface.User;
using log4net;
using log4net.Config;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace connect.api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ILog logger = null;
            try
            {
                string logfile = Path.Combine(Directory.GetCurrentDirectory(), "log-4net.config");
                XmlConfigurator.Configure(new FileInfo(logfile));
                logger = LogManager.GetLogger("Logger");

                IConfiguration config = new ConfigurationBuilder()
                     .SetBasePath(Directory.GetCurrentDirectory())
                     .AddJsonFile("appsettings.json")
                     .Build();

                DatabaseConnectionParams dbConnectionParam = new DatabaseConnectionParams();
                config.Bind("AppSettings:DatabaseConnection", dbConnectionParam);


                IHostBuilder hostBuilder = CreateHostBuilder();

                if (hostBuilder != null)
                    hostBuilder.Build().Run();
            }
            catch (Exception ex)
            {
                logger.Error("Main Service-> Exception: " + ex.Message);
            }

        }

        public static IHostBuilder CreateHostBuilder() =>
            Host.CreateDefaultBuilder()
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseKestrel();
                webBuilder.UseStartup<Startup>();
                webBuilder.UseContentRoot(Directory.GetCurrentDirectory());
            })
            .ConfigureLogging((hostingContext, logging) =>
            {
                logging.AddLog4Net("log-4net.config");
            })
            .ConfigureServices((hostingContext, services) =>
            {
                services.Configure<AppSettings>(hostingContext.Configuration.GetSection(nameof(AppSettings)));

                services.AddTransient<IConnectGroupInfoService, ConnectGroupInfoService>();
                services.AddTransient<IConnectChatService, ConnectChatService>();
                services.AddTransient<IConnectDatabaseRepository, ConnectDatabaseRepository>();
                services.AddTransient<IUsersService, UsersService>();
                services.AddTransient<IAccountService, AccountService>();
                services.AddTransient<ICPLogger, CPLogger>();

                //services.AddSingleton<SignInManager, SignInManager<ApplicationUser>>();

                services.AddSingleton<IUserIdProvider, EmailBasedUserIdProvider>();
                services.AddSingleton<IJsonHelper, JsonHelper>();
                services.AddScoped<ValidateModelAttribute>();
            });
    }
}
