﻿using Instagram.ComponentsViewModels;
using Instagram.Databases;
using Instagram.Interfaces;
using Instagram.JSONModels;
using Instagram.Models;
using Instagram.Repositories;
using Instagram.Services;
using Instagram.StartupHelpers;
using Instagram.Views;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Instagram.Components
{
    public partial class HomeUserControl : UserControl
    {
        public HomeUserControl(InstagramDbContext db, IAbstractFactory<PostView> postFactory)
        {
            ChangeHomeTheme();
            DataContext = new HomeViewModel(db, postFactory, ChangeHomeTheme);
            InitializeComponent();
        }

        public async Task ChangeHomeTheme()
        {
            await ChangeTheme.ChangeAsync(this.Resources);
        }
    }
}
