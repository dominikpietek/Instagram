﻿using System;
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
    /// <summary>
    /// Interaction logic for ProfileUserControl.xaml
    /// </summary>
    public partial class ProfileUserControl : UserControl
    {
        public ProfileUserControl()
        {
            InitializeComponent();
        }
        public void ChangeProfileTheme(bool isDarkMode)
        {
            this.Resources.MergedDictionaries.Clear();
            string resourceName = isDarkMode ? "DarkModeDictionary" : "BrightModeDictionary";
            ResourceDictionary resourceDictionary = new ResourceDictionary() { Source = new Uri(string.Format("ResourceDictionaries/{0}.xaml", resourceName), UriKind.Relative) };
            this.Resources.MergedDictionaries.Add(resourceDictionary);
        }
    }
}
