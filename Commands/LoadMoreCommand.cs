﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instagram.Commands
{
    public class LoadMoreCommand : CommandBase
    {
        private readonly Action _LoadMorePosts;

        public LoadMoreCommand(Action LoadMorePosts)
        {
            _LoadMorePosts = LoadMorePosts;
        }
        public override void Execute(object parameter)
        {
            _LoadMorePosts.Invoke();
        }
    }
}
