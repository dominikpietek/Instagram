using Instagram.Commands;
using Instagram.Databases;
using Instagram.DTOs;
using Instagram.Models;
using Instagram.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Instagram.ViewModels
{
    public class FriendRequestViewModel : ViewModelBase
    {
        public ICommand CheckProfileButton { get; set; }
        public BitmapImage ProfilePhoto { get; set; }
        public string Nickname { get; set; }
        public ICommand AcceptRequestButton { get; set; }
        public string AcceptIconSource { get; set; } = @"C:\Programs\Instagram\Instagram\Resources\acceptIcon.png";
        public ICommand DeclineRequestButton { get; set; }
        public string DeclineIconSource { get; set; } = @"C:\Programs\Instagram\Instagram\Resources\declineIcon.png";
        private FriendDto _friendDto;
        private Action<InstagramDbContext, int> _LoadFriendRequest;
        private int _userId;
        public FriendRequestViewModel(FriendDto friendDto, int userId, Action<InstagramDbContext, int> LoadFriendRequest)
        {
            CheckProfileButton = new ShowProfileCommand();
            AcceptRequestButton = new AcceptRequestCommand(AddFriend);
            DeclineRequestButton = new DeclineRequestCommand(RemoveFriend);
            _friendDto = friendDto;
            _userId = userId;
            _LoadFriendRequest = LoadFriendRequest;
            BindDtoToData();
        }
        private void BindDtoToData()
        {
            ProfilePhoto = ConvertImage.FromByteArray(_friendDto.ProfilePhoto.ImageBytes);
            Nickname = _friendDto.Nickname;
        }
        private void AddFriend()
        {
            using (var db = new InstagramDbContext())
            {
                if (!db.Users.First(u => u.Id == _userId).Friends.Select(f => f.FriendId).Contains(_friendDto.Id))
                {
                    db.Users.First(u => u.Id == _userId).Friends.Add(new Friend() 
                    { 
                        UserId = _userId,
                        FriendId = _friendDto.Id
                    });
                    db.Users.First(u => u.Id == _friendDto.Id).Friends.Add(new Friend()
                    {
                        UserId = _friendDto.Id,
                        FriendId = _userId
                    });
                    db.SaveChanges();
                    RemoveFriend();
                }
            }
        }
        private void RemoveFriend()
        {
            using (var db = new InstagramDbContext())
            {
                if(!db.Users.First(u => u.Id == _userId).GotFriendRequests.Select(gfr => gfr.Id).Contains(_friendDto.Id))
                {
                    UserIdGotModel userIdGotModel = db.UserIdGotModels.First(uigm => uigm.UserId == _userId && uigm.StoredUserId == _friendDto.Id);
                    db.UserIdGotModels.Remove(userIdGotModel);
                    UserIdSentModel userIdSentModel = db.UserIdSentModels.First(uigm => uigm.UserId == _friendDto.Id && uigm.StoredUserId == _userId);
                    db.UserIdSentModels.Remove(userIdSentModel);
                    db.SaveChanges();
                    _LoadFriendRequest.Invoke(db, _userId);
                    // rename those two functions
                }
            }
        }
    }
}
