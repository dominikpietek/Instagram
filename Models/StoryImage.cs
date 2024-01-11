using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instagram.Models
{
    public class StoryImage
    {
        public int Id { get; set; }
        public byte[] ImageBytes { get; set; } = new byte[0];
        public Story Story { get; set; }
        public int StoryId { get; set; }
    }
}
