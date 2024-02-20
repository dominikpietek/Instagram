using Instagram.Databases;
using Instagram.StartupHelpers;
using Instagram.ViewModels;
using Instagram.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Configuration;
using System.Windows;

namespace Instagram
{
    public partial class App : Application
    {
        public static IHost? AppHost { get; private set; }
        public App()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            AppHost = Host.CreateDefaultBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddSingleton<LoginOrRegisterWindowView>();
                    services.AddFormFactory<CreateAccountWindowView>();
                    services.AddFormFactory<LoginOrRegisterWindowView>();
                    services.AddFormFactory<FeedView>();
                    services.AddDbContext<InstagramDbContext>(options => options.UseSqlServer(connectionString));
                })
                .Build();
        }
        protected override async void OnStartup(StartupEventArgs e)
        {
            await AppHost!.StartAsync();

            var startupForm = AppHost.Services.GetRequiredService<LoginOrRegisterWindowView>();
            startupForm.Show();

            base.OnStartup(e);
        }
        protected override async void OnExit(ExitEventArgs e)
        {
            await AppHost!.StopAsync();
            base.OnExit(e);
        }
    }
}
