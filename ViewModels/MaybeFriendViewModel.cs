using Instagram.Commands;
using Instagram.Databases;
using Instagram.DTOs;
using Instagram.Models;
using Instagram.Services;
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
    public class MaybeFriendViewModel : ViewModelBase
    {
        public ICommand CheckProfileButton { get; set; }
        public ICommand AddUserButton { get; set; }
        public BitmapImage ProfilePhoto { get; set; }
        public string Nickname { get; set; }
        public string AddUserIconPath { get; set; } = @"C:\Programs\Instagram\Instagram\Resources\plusIcon.png";
        public FriendDto _friendDto;
        private Action<InstagramDbContext, int> _LoadMaybeFriends;
        private bool _IsInvitationSent = false;
        public int userId;
        public bool IsInvitationSent
        {
            get { return _IsInvitationSent; }
            set
            {
                _IsInvitationSent = value;
                OnPropertyChanged(nameof(IsInvitationSent));
            }
        }
        public MaybeFriendViewModel(FriendDto friendDto, int userId, Action<InstagramDbContext, int> LoadMaybeFriends)
        {
            _friendDto = friendDto;
            CheckProfileButton = new ShowProfileCommand();
            AddUserButton = new SendInvitationCommand(ChangeInvitationStatus);
            BindDtoToData();
            this.userId = userId;
            _LoadMaybeFriends = LoadMaybeFriends;
        }
        private void BindDtoToData()
        {
            ProfilePhoto = ConvertImage.FromByteArray(_friendDto.ProfilePhoto.ImageBytes);
            Nickname = _friendDto.Nickname;
        }
        private void ChangeInvitationStatus()
        {
            IsInvitationSent ^= true;
            SentInvitation();
            // remove isInvitationSent
            // and change image sent or unsent invitation
        }
        private void SentInvitation()
        {
            using (var db = new InstagramDbContext())
            {
                if (!db.UserIdSentModels.Any(uism => uism.StoredUserId == _friendDto.Id))
                {
                    db.Users.First(u => u.Id == userId).SentFriendRequests.Add(new UserIdSentModel()
                    {
                        StoredUserId = _friendDto.Id
                    });
                    db.Users.First(u => u.Id == _friendDto.Id).GotFriendRequests.Add(new UserIdGotModel()
                    {
                        StoredUserId = userId
                    });
                    db.SaveChanges();
                    _LoadMaybeFriends.Invoke(db, userId);
                }
            }
        }
    }
}
