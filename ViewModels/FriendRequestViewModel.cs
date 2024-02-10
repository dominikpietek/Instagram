using Instagram.Commands;
using Instagram.Databases;
using Instagram.DTOs;
using Instagram.Models;
using Instagram.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;
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
        #region Resources
        private string _path;
        public BitmapImage ProfilePhoto { get; set; }
        public string DeclineIconSource { get; set; }
        public string AcceptIconSource { get; set; }
        #endregion
        #region Commands
        public ICommand CheckProfileButton { get; set; }
        public ICommand AcceptRequestButton { get; set; }
        public ICommand DeclineRequestButton { get; set; }
        #endregion
        public string Nickname { get; set; }
        private FriendDto _friendDto;
        private Action<InstagramDbContext, int> _LoadFriendRequest;
        private int _userId;
        public FriendRequestViewModel(FriendDto friendDto, int userId, Action<InstagramDbContext, int> LoadFriendRequest)
        {
            #region CommandsInstances
            CheckProfileButton = new ShowProfileCommand();
            AcceptRequestButton = new AcceptRequestCommand(AddFriendAsync);
            DeclineRequestButton = new DeclineRequestCommand(RemoveFriendAsync);
            #endregion
            #region PrivatePropertiesAssignment
            _friendDto = friendDto;
            _userId = userId;
            _LoadFriendRequest = LoadFriendRequest;
            _path = ConfigurationManager.AppSettings.Get("ResourcesPath");
            #endregion
            InitResources();
            BindDtoToData();
        }
        private void InitResources()
        {   
            DeclineIconSource = $"{_path}declineIcon.png";
            AcceptIconSource = $"{_path}acceptIcon.png";
        }
        private void BindDtoToData()
        {
            ProfilePhoto = ConvertImage.FromByteArray(_friendDto.ProfilePhoto.ImageBytes);
            Nickname = _friendDto.Nickname;
        }
        private async Task AddFriendAsync()
        {
            var GetFromDatabaseAsync = async Task () =>
            {

                using (var db = new InstagramDbContext("MainDb"))
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
                        await RemoveFriendAsync();
                    }
                }
            };
            await GetFromDatabaseAsync.Invoke();
        }
        private async Task RemoveFriendAsync()
        {
            var RemoveFromDatabase = async Task () =>
            {
                using (var db = new InstagramDbContext("MainDb"))
                {
                    if (!db.Users.First(u => u.Id == _userId).GotFriendRequests.Select(gfr => gfr.Id).Contains(_friendDto.Id))
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
            };
            await RemoveFromDatabase.Invoke();
        }
    }
}
