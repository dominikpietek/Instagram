using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instagram.Models
{
    public class Comment : ModelBase
    {
        public int Id { get; set; }
        public int AuthorId { get; set; }
        public Post Post { get; set; }
        public int PostId { get; set; }
        public string Content { get; set; }
        public int Likes { get; set; }
        public DateTime PublicationDate { get; set; }
        public List<CommentResponse> CommentResponses { get; set; } = new List<CommentResponse>();
    }
}
