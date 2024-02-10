using Instagram.Databases;
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
        private Post _post;
        private Func<Task> _ShowPosts;
        public DeletePostCommand(Post post, Func<Task> ShowPosts)
        {
            _post = post;
            _ShowPosts = ShowPosts;
        }
        public override void Execute(object parameter)
        {
            using(var db = new InstagramDbContext("MainDb"))
            {
                db.Posts.Remove(_post);
                db.SaveChanges();
                _ShowPosts.Invoke();
            }
        }
    }
}
