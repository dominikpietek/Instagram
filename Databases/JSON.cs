using Instagram.JSONModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
    public class JSON<JSONModel>
    {
        private string _fileName;
        private string _hardPath = @"C:\Programs\Instagram\Instagram\Databases\";
        private string _path;
        public JSON(string fileName)
        {
            _fileName = fileName;
            _path = $"{_hardPath}{_fileName}.json";
        }
        public JSONModel Get<JSONModel>()
        {
            return JsonConvert.DeserializeObject<JSONModel>(File.ReadAllText(_path));
        }
        public void Save(JSONModel model)
        {
            string serializedModel = JsonConvert.SerializeObject(model);
            File.WriteAllText(_path, serializedModel);
        }
    }
}
