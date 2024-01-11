using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Instagram.Commands
{
    public class OpenImageButtonCommand : CommandBase
    {
        private Action<string> _OnLoadingImage { get; set; }
        public OpenImageButtonCommand(Action<string> OnLoadingImage)
        {
            _OnLoadingImage = OnLoadingImage;
        }
        public override void Execute(object parameter)
        {
            OpenFileDialog openFile = new OpenFileDialog() 
            { 
                Filter = "Files|*.jpg;*.jpeg;*.png", 
                Multiselect = false 
            };
            if (openFile.ShowDialog() == true)
            {
                _OnLoadingImage.Invoke(openFile.FileName);
            }
        }
    }
}
