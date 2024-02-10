using Instagram.Commands;
using Instagram.DTOs;
using Instagram.Models;
using Instagram.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Instagram.ViewModels
{
    public class FriendInMessengerViewModel : ViewModelBase
    {
        #region Resources
        private string _path;
        public string TrashIconPath { get; set; }
        public BitmapImage ProfilePhotoSource { get; set; }
        #endregion
        #region Properties
        public string Nickname { get; set; }
        public string LastMessage { get; set; }
        private FriendDto _friend;
        #endregion
        #region Commands
        public ICommand OpenChatButton;
        public ICommand RemoveFriendButton;
        #endregion
        public FriendInMessengerViewModel(FriendDto friend)
        {
            #region CommandsInstances
            OpenChatButton = new OpenChatCommand();
            RemoveFriendButton = new RemoveFriendCommand();
            #endregion
            _friend = friend;
            _path = ConfigurationManager.AppSettings.Get("ResourcesPath");
            InitResources();
            Nickname = friend.Nickname;
            LastMessage = friend.LastMessage;
        }
        private void InitResources()
        {
            TrashIconPath = $"{_path}trashIcon.png";
            ProfilePhotoSource = ConvertImage.FromByteArray(_friend.ProfilePhoto.ImageBytes);
        }
    }
}
