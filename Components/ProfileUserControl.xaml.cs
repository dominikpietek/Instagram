using Instagram.ComponentsViewModels;
using Instagram.Databases;
using Instagram.Services;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Instagram.Components
{
    public partial class ProfileUserControl : UserControl
    {
        public ProfileUserControl(InstagramDbContext db)
        {
            ChangeProfileTheme();
            this.DataContext = new ProfileViewModel(db);
            InitializeComponent();
        }

        public async Task ChangeProfileTheme()
        {
            await ChangeTheme.ChangeAsync(this.Resources);
        }
    }
}
