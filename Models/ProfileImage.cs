using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instagram.Models
{
    public class ProfileImage
    {
        public int Id { get; set; }
        public byte[] ImageBytes { get; set; } = new byte[0];
        public User User { get; set; }
        public int UserId { get; set; }
    }
}
