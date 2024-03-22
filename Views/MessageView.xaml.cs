using Instagram.Models;
using Instagram.ViewModels;
using System.Windows.Controls;

namespace Instagram.Views
{
    public partial class MessageView : UserControl
    {
        public MessageView(Message message, bool isTurnChanged)
        {
            this.DataContext = new MessageViewModel(message, isTurnChanged);
            InitializeComponent();
        }
    }
}
