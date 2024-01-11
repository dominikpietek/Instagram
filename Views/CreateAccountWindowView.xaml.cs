using Instagram.ViewModels;
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
using System.Windows.Shapes;

namespace Instagram.Views
{
    /// <summary>
    /// Interaction logic for CreateAccountWindowView.xaml
    /// </summary>
    public partial class CreateAccountWindowView : Window
    {
        public CreateAccountWindowView()
        {
            InitializeComponent();
            DataContext = new CreateAccountWindowViewModel(CloseWindow);
        }
        public void CloseWindow()
        {
            this.Close();
        }
    }
}
