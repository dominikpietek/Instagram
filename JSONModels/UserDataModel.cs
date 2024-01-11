using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instagram.JSONModels
{
    public class UserDataModel
    {
        public string RememberedEmailNickname { get; set; }
        public DateTime LastLogin { get; set; }
        public bool DarkMode { get; set; }
    }
}
