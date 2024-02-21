using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Instagram.Enums;

namespace Instagram.Models
{
    public class UserLiked
    {
        public int Id { get; set; }
        public int UserThatLikedId { get; set; }
        public LikedThingsEnum LikedThing { get; set; }
        public int LikedThingId { get; set; }
    }
}
