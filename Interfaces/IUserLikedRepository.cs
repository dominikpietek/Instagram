using Instagram.Enums;
using Instagram.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instagram.Interfaces
{
    public interface IUserLikedRepository
    {
        Task<bool> AddLikeAsync(UserLiked userLiked);
        Task<bool> RemoveLikeAsync(int userThatLikedId, LikedThingsEnum likedThing, int likedThingId);
        Task<bool> IsLikedBy(int userThatLikedId, LikedThingsEnum likedThing, int likedThingId);
    }
}
