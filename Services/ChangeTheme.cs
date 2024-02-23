using Instagram.Databases;
using Instagram.JSONModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Instagram.Services
{
    public static class ChangeTheme
    {
        private static async Task<bool> GetMode()
        {
            JSON<UserDataModel> json = new JSON<UserDataModel>("UserData");
            return await json.GetDarkModeAsync();
        }

        public static async Task Change(ResourceDictionary resource)
        {
            string resourceName = await GetMode() ? "DarkModeDictionary" : "BrightModeDictionary";
            resource.MergedDictionaries.Clear();
            ResourceDictionary resourceDictionary = new ResourceDictionary() { Source = new Uri(string.Format("ResourceDictionaries/{0}.xaml", resourceName), UriKind.Relative) };
            resource.MergedDictionaries.Add(resourceDictionary);
        }

        public static string ChangeLogo(string path, bool isDarkMode)
        {
            if (isDarkMode)
            {
                return $"{path}darkLogo.png";
            }
            else
            {
                return $"{path}logo.png";
            }
        }
    }
}
