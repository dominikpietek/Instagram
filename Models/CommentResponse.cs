using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instagram.Models
{
    public class CommentResponse : ModelBase
    {
        [Key]
        public int Id { get; set; }
        public int AuthorId { get; set; }
        public Comment Comment { get; set; }
        [ForeignKey(nameof(Comment))]
        public int CommentId { get; set; }
        public string Content { get; set; }
        public int Likes { get; set; }
        public DateTime PublicationDate { get; set; }
    }
}
