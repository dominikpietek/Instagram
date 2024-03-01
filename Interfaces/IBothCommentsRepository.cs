using Instagram.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instagram.Interfaces
{
    public interface IBothCommentsRepository<T>
    {
        Task<bool> AddCommentAsync(T comment);
        Task<bool> DeleteCommentAsync(int commentId);
        Task<T> GetCommentAsync(int commentId);
        Task<Comment> GetCommentWithResponsesAsync(int commentId);
        Task<bool> UpdateCommentAsync(T comment);
    }
}
