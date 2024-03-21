using Instagram.Models;
using Instagram.ViewModels;
using System.Windows.Controls;

namespace Instagram.Views
{
    public partial class MessageView : UserControl
    {
        public MessageView(Message message)
        {
            this.DataContext = new MessageViewModel(message);
            InitializeComponent();
        }
    }
}
