﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Instagram.Commands
{
    public class RemoveFriendCommand : CommandBase
    {
        public override void Execute(object parameter)
        {
            MessageBox.Show("ohhh remove him from my life...");
        }
    }
}