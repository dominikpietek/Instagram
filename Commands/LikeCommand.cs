using Instagram.Databases;
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
        private LikedThingsEnum _LikedThing;
        private int _userId;
        private int _likedThingId;
        private Action<int> _UpdateLikesNumber;
        private Action<int> _ChangeIsUserLiked;
        public LikeCommand(LikedThingsEnum LikedThing, int userId, int likedThingId, Action<int> UpdateLikesNumber, Action<int> ChangeIsUserLiked)
        {
            _LikedThing = LikedThing;
            _userId = userId;
            _likedThingId = likedThingId;
            _UpdateLikesNumber = UpdateLikesNumber;
            _ChangeIsUserLiked = ChangeIsUserLiked;
        }
        public override void Execute(object parameter)
        {
            using (var db = new InstagramDbContext())
            {
                bool addOrRemove;
                int likesNumber = 0;
                var likes = db.UsersLiked.Where(u => (u.UserThatLikedId == _userId && u.LikedThingId == _likedThingId && (int)u.LikedThing == (int)_LikedThing)).ToList();
                if (likes.Count() == 0)
                {
                    addOrRemove = true;
                    db.UsersLiked.Add(new UserLiked()
                    {
                        LikedThing = _LikedThing,
                        LikedThingId= _likedThingId,
                        UserThatLikedId = _userId
                    });
                }
                else
                {
                    addOrRemove = false;
                    db.UsersLiked.Remove(likes[0]);
                }
                switch (_LikedThing)
                {
                    case LikedThingsEnum.Post:
                        likesNumber =
                            addOrRemove 
                            ? db.Posts.First(p => p.Id == _likedThingId).Likes++ + 1
                            : db.Posts.First(p => p.Id == _likedThingId).Likes-- - 1;
                        break;
                    case LikedThingsEnum.Comment:
                        likesNumber = 
                            addOrRemove
                            ? db.Comments.First(c => c.Id == _likedThingId).Likes++ + 1
                            : db.Comments.First(c => c.Id == _likedThingId).Likes-- - 1;
                        break;
                    case LikedThingsEnum.CommentResponse:
                        likesNumber = 
                            addOrRemove
                            ? db.CommentResponses.First(cr => cr.Id == _likedThingId).Likes++ + 1
                            : db.CommentResponses.First(cr => cr.Id == _likedThingId).Likes-- - 1;
                        break;
                    default:
                        break;
                }
                db.SaveChanges();
                _UpdateLikesNumber.Invoke(likesNumber);
                _ChangeIsUserLiked.Invoke(likes.Count() == 0 ? 1 : 0);
            }
        }
    }
}
