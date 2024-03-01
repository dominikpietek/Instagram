using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instagram.Models
{
    public class CommentResponse : ModelBase
    {
        public int Id { get; set; }
        public int AuthorId { get; set; }
        public Comment Comment { get; set; }
        public int CommentId { get; set; }
        public string Content { get; set; }
        public int Likes { get; set; }
        public DateTime PublicationDate { get; set; }
    }
}
