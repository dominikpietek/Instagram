using Instagram.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instagram.Interfaces
{
    public interface ICommentResponseRepository
    {
        Task<bool> AddCommentResponseAsync(CommentResponse commentResponse);
        Task<bool> DeleteCommentResponseAsync(int commentResponseId);
        Task<bool> SaveChangesAsync();
    }
}
