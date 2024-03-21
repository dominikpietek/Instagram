using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instagram.Models
{
    public class Friend
    {
        public int Id { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public int FriendId { get; set; }
    }
}
