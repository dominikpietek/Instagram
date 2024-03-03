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
        private readonly Func<Task> _ShowPosts;

        public DeletePostCommand(int postId, IPostRepository postRepository, Func<Task> ShowPosts)
        {
            _postId = postId;
            _postRepository = postRepository;
            _ShowPosts = ShowPosts;
        }
        public override async void Execute(object parameter)
        {
            await _postRepository.RemovePostByIdAsync(_postId);
            await _ShowPosts.Invoke();
        }
    }
}
