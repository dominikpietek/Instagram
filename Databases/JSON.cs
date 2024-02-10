﻿using Instagram.JSONModels;
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
    public class JSON<JSONModel>
    {
        private string _fileName;
        private readonly string _hardPath;
        private string _path;
        public JSON(string fileName)
        {
            _fileName = fileName;
            _hardPath = ConfigurationManager.AppSettings.Get("DatabasesPath");
            _path = $"{_hardPath}{_fileName}.json";
        }
        public async Task<JSONModel> GetAsync<JSONModel>()
        {
            return JsonConvert.DeserializeObject<JSONModel>(File.ReadAllText(_path));
        }
        public async Task SaveAsync(JSONModel model)
        {
            string serializedModel = JsonConvert.SerializeObject(model);
            File.WriteAllText(_path, serializedModel);
        }
        public async Task<bool> GetDarkModeAsync()
        {
            var jsonModel = await GetAsync<UserDataModel>();
            return jsonModel.DarkMode;
        }
    }
}
