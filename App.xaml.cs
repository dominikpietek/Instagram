using Instagram.Databases;
using Instagram.Interfaces;
using Instagram.Repositories;
using Instagram.ViewModels;
using Instagram.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Instagram
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public readonly IServiceProvider serviceProvider;
        public App()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            //serviceProvider = new ServiceCollection()
            //    .AddDbContext<InstagramDbContext>(options => options.UseSqlServer(connectionString))
            //    .AddTransient<IPostRepository, PostRepository>()
            //    .AddTransient<LoginOrRegisterWindowViewModel>()
            //    .BuildServiceProvider();
        }
        protected void OnStartUp(StartupEventArgs e)
        {
            base.OnStartup(e);
            //var window = serviceProvider.GetRequiredService<LoginOrRegisterWindowViewModel>();
            
        }
    }
}
