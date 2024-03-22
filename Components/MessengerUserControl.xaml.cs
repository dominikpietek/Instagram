using Instagram.ComponentsViewModels;
using Instagram.Databases;
using Instagram.Services;
using Instagram.StartupHelpers;
using Instagram.Views;
using System;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Instagram.Components
{
    public partial class MessengerUserControl : UserControl
    {
        private readonly InstagramDbContext _db;
        private readonly IAbstractFactory<FriendInMessengerView> _friendFactory;

        public MessengerUserControl(InstagramDbContext db, IAbstractFactory<FriendInMessengerView> friendFactory)
        {
            ChangeMessengerTheme();
            InitializeComponent();
            _db = db;
            _friendFactory = friendFactory;
        }

        public void SetDataContext(Action ScrollToBottom)
        {
            this.DataContext = new MessengerViewModel(_db, _friendFactory, ScrollToBottom);
        }

        public async Task ChangeMessengerTheme()
        {
            await ChangeTheme.ChangeAsync(this.Resources);
        }
    }
}
