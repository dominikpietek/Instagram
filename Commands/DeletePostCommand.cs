using Instagram.Databases;
using Instagram.Interfaces;
using Instagram.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Instagram.Commands
{
    public class DeletePostCommand : CommandBase
    {
        private readonly int _postId;
        private readonly IPostRepository _postRepository;
        public DeletePostCommand(int postId, IPostRepository postRepository)
        {
            _postId = postId;
            _postRepository = postRepository;
        }
        public override async void Execute(object parameter)
        {
            await _postRepository.RemovePostByIdAsync(_postId);
            // refresh posts
        }
    }
}
