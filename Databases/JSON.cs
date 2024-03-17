using Instagram.JSONModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;

namespace Instagram.Databases
{
    public class JSON<JSONModel> where JSONModel : class
    {
        private string _fileName;
        private readonly string _hardPath;
        private string _path;
        public JSON(string fileName)
        {
            _fileName = fileName;
            _hardPath = ConfigurationManager.AppSettings.Get("DatabasesPath")!;
            _path = $"{_hardPath}{_fileName}.json";
        }
        public JSONModel Get<JSONModel>()
        {
            if (!File.Exists(_path))
            {
                File.Create(_path).Dispose();
            }
            return JsonConvert.DeserializeObject<JSONModel>(File.ReadAllText(_path))!;
        }

        public void Save(JSONModel model)
        {
            string serializedModel = JsonConvert.SerializeObject(model);
            File.WriteAllText(_path, serializedModel);
        }
        public bool GetDarkMode()
        {
            var jsonModel = Get<UserDataModel>();
            return jsonModel.DarkMode;
        }
    }
}
