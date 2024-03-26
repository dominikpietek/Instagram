using Instagram.Components;
using Instagram.Databases;
using Instagram.GenerateFakeData;
using Instagram.Interfaces;
using Instagram.Models;
using Instagram.Repositories;
using Instagram.Services;
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
            AppHost = Host.CreateDefaultBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddTransient<InstagramDbContext>(db => new InstagramDbContext("FakeDataDb"));
                    services.AddSingleton<LoginOrRegisterWindowView>();
                    services.AddFormFactory<CreateAccountWindowView>();
                    services.AddFormFactory<CreateNewPostWindowView>();
                    services.AddFormFactory<LoginOrRegisterWindowView>();
                    services.AddFormFactory<HomeUserControl>();
                    services.AddFormFactory<FeedView>();
                    services.AddFormFactory<PostView>();
                    services.AddFormFactory<CommentView>();
                    services.AddFormFactory<ReplyCommentView>();
                    services.AddFormFactory<StoryUserView>();
                    services.AddFormFactory<StoryView>();
                    services.AddFormFactory<CreateNewStoryView>();
                    services.AddFormFactory<ProfileUserControl>();
                    services.AddFormFactory<MessengerUserControl>();
                    services.AddFormFactory<MaybeFriendView>();
                    services.AddFormFactory<FriendRequestView>();
                    services.AddFormFactory<CheckProfileUserControl>();
                    services.AddFormFactory<SearchedUserView>();
                    services.AddFormFactory<FriendInMessengerView>();
                })
                .Build();
        }
        protected override async void OnStartup(StartupEventArgs e)
        {
            CreateFakeData fakeData = new CreateFakeData();

            await AppHost!.StartAsync();

            InstagramDbContext dbContext = new InstagramDbContext("FakeDataDb");
            IUserRepository userRepository = new UserRepository(dbContext);
            try
            {
                User user = await GetUser.FromDbAndFileAsync(userRepository);
                IsInDatabaseRepository isInDatabase = new IsInDatabaseRepository(userRepository, user.Nickname);
                if (await isInDatabase.CheckLoginAsync("Email or Nickname doesn't exist!"))
                {
                    var startupForm = AppHost.Services.GetRequiredService<FeedView>();
                    startupForm.Show();
                    base.OnStartup(e);
                }
            }
            catch (Exception ex)
            {
                var startupForm = AppHost.Services.GetRequiredService<LoginOrRegisterWindowView>();
                startupForm.Show();
                base.OnStartup(e);
            }
        }
        protected override async void OnExit(ExitEventArgs e)
        {
            await AppHost!.StopAsync();
            base.OnExit(e);
        }
    }
}
