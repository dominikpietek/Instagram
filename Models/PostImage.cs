using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instagram.Models
{
    public class PostImage
    {
        public int Id { get; set; }
        public byte[] ImageBytes { get; set; } = new byte[0];
        public Post Post { get; set; }
        public int PostId { get; set; }
    }
}
