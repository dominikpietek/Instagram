using Instagram.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instagram.Services
{
    public static class ConvertTagsToString
    {
        public static string Convert(List<Tag> tags)
        {
            string converted = "";
            foreach (var tag in tags)
            {
                converted = $"{converted} #{tag.Text}";
            }
            return converted;
        }
    }
}
