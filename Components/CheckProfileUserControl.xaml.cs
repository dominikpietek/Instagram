using Instagram.ComponentsViewModels;
using Instagram.Databases;
using Instagram.Services;
using Instagram.StartupHelpers;
using Instagram.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Instagram.Components
{
    public partial class CheckProfileUserControl : UserControl
    {
        private readonly InstagramDbContext _db;
        private readonly IAbstractFactory<PostView> _postFactory;

        public CheckProfileUserControl(InstagramDbContext db, IAbstractFactory<PostView> postFactory)
        {
            InitializeComponent();
            _db = db;
            _postFactory = postFactory;
        }

        public void SetDataContext(int profileId)
        {
            this.DataContext = new CheckProfileViewModel(_db, _postFactory, profileId, ChangeProfileTheme);
        }

        public async Task ChangeProfileTheme()
        {
            await ChangeTheme.ChangeAsync(this.Resources);
        }
    }
}
