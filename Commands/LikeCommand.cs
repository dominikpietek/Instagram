using Instagram.Databases;
using Instagram.Enums;
using Instagram.Interfaces;
using Instagram.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Instagram.Commands
{
    public class LikeCommand : CommandBase
    {
        private readonly LikedThingsEnum _LikedThing;
        private readonly int _userId;
        private readonly int _likedThingId;
        private readonly IUserLikedRepository _userLikedRepository;
        private readonly Action<bool> _UpdateLikes;
        public LikeCommand(LikedThingsEnum LikedThing, int userId, int likedThingId, Action<bool> UpdateLikes, IUserLikedRepository userLikedRepository)
        {
            _LikedThing = LikedThing;
            _userId = userId;
            _likedThingId = likedThingId;
            _userLikedRepository = userLikedRepository;
            _UpdateLikes = UpdateLikes;
        }
        public async override void Execute(object parameter)
        {
            if(await _userLikedRepository.IsLikedBy(_userId, _LikedThing, _likedThingId))
            {
                await _userLikedRepository.RemoveLikeAsync(_userId, _LikedThing, _likedThingId);
                _UpdateLikes.Invoke(false);
            }
            else
            {
                UserLiked userLiked = new UserLiked() { LikedThing = _LikedThing, LikedThingId = _likedThingId, UserThatLikedId = _userId };
                await _userLikedRepository.AddLikeAsync(userLiked);
                _UpdateLikes.Invoke(true);
            }
        }
    }
}
