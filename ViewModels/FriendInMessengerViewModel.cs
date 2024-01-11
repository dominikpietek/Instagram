using Instagram.Commands;
using Instagram.DTOs;
using Instagram.Models;
using Instagram.Services;
using System;
using System.Collections.Generic;
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
        public string TrashIconPath { get; set; } = @"C:\Programs\Instagram\Instagram\Resources\trashIcon.png";
        public BitmapImage ProfilePhotoSource { get; set; }
        public string Nickname { get; set; }
        public string LastMessage { get; set; }
        public ICommand OpenChatButton;
        public ICommand RemoveFriendButton;
        public FriendInMessengerViewModel(FriendDto friend)
        {
            OpenChatButton = new OpenChatCommand();
            RemoveFriendButton = new RemoveFriendCommand();
            ProfilePhotoSource = ConvertImage.FromByteArray(friend.ProfilePhoto.ImageBytes);
            Nickname = friend.Nickname;
            LastMessage = friend.LastMessage;
        }
    }
}
