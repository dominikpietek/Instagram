using Instagram.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instagram.DTOs
{
    public class FriendDto
    {
        public int Id { get; set; }
        public string Nickname { get; set; }
        public ProfileImage ProfilePhoto { get; set; }
        public string? LastMessage { get; set; }
    }
}
