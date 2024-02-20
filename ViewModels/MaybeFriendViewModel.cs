using Instagram.Commands;
using Instagram.Databases;
using Instagram.DTOs;
using Instagram.Models;
using Instagram.Services;
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
    public class MaybeFriendViewModel : ViewModelBase
    {
        #region Resources
        private string _path;
        public BitmapImage ProfilePhoto { get; set; }
        public string AddUserIconPath { get; set; }
        #endregion
        #region Commands
        public ICommand CheckProfileButton { get; set; }
        public ICommand AddUserButton { get; set; }
        #endregion
        public string Nickname { get; set; }
        public int userId;
        public FriendDto _friendDto;
        private Action<InstagramDbContext, int> _LoadMaybeFriends;
        #region OnPropertyChangeProperties
        private bool _IsInvitationSent = false;
        public bool IsInvitationSent
        {
            get { return _IsInvitationSent; }
            set
            {
                _IsInvitationSent = value;
                OnPropertyChanged(nameof(IsInvitationSent));
            }
        }
        #endregion
        public MaybeFriendViewModel(FriendDto friendDto, int userId, Action<InstagramDbContext, int> LoadMaybeFriends)
        {
            #region CommandInstances
            CheckProfileButton = new ShowProfileCommand();
            AddUserButton = new SendInvitationCommand(ChangeInvitationStatus);
            #endregion
            _path = ConfigurationManager.AppSettings.Get("ResourcesPath");
            InitResources();
            _friendDto = friendDto;
            BindDtoToData();
            this.userId = userId;
            _LoadMaybeFriends = LoadMaybeFriends;
        }
        private void InitResources()
        {
            AddUserIconPath = $"{_path}plusIcon.png";
        }
        private void BindDtoToData()
        {
            ProfilePhoto = ConvertImage.FromByteArray(_friendDto.ProfilePhoto.ImageBytes);
            Nickname = _friendDto.Nickname;
        }
        private void ChangeInvitationStatus()
        {
            IsInvitationSent ^= true;
            SentInvitationAsync();
            // remove isInvitationSent
            // and change image sent or unsent invitation
        }
        private async Task SentInvitationAsync()
        {
            var ChangeDatabaseAsync = async Task () =>
            {

                //using (var db = new InstagramDbContext("MainDb"))
                //{
                //    if (!db.UserIdSentModels.Any(uism => uism.StoredUserId == _friendDto.Id))
                //    {
                //        db.Users.First(u => u.Id == userId).SentFriendRequests.Add(new UserIdSentModel()
                //        {
                //            StoredUserId = _friendDto.Id
                //        });
                //        db.Users.First(u => u.Id == _friendDto.Id).GotFriendRequests.Add(new UserIdGotModel()
                //        {
                //            StoredUserId = userId
                //        });
                //        db.SaveChanges();
                //        _LoadMaybeFriends.Invoke(db, userId);
                //    }
                //}
            };
            await ChangeDatabaseAsync.Invoke();
        }
    }
}
