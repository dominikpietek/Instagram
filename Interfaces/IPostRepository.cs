using Instagram.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instagram.Interfaces
{
    public interface IPostRepository
    {
        Task<List<Post>> GetAllPostsWithAllDataToShowAsync();
        Task<Post> GetPostWithAllDataAsync(int id);
        Task<bool> UpdatePostAsync(Post post);
        Task<bool> RemovePostByIdAsync(int postId);
        Task<bool> AddPostAsync(Post post);
    }
}
