using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instagram.Models
{
    public class Post
    {
        public int Id { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public PostImage Image { get; set; }
        public List<Comment> Comments { get; set; } = new List<Comment>();
        public int Likes { get; set; }
        public DateTime PublicationDate { get; set; }
        public new List<Tag> Tags { get; set; } = new List<Tag>();
     }
}
