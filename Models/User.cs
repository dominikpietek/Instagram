using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instagram.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Nickname { get; set; }
        public string EmailAdress { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public DateTime Birthdate { get; set; }
        public ProfileImage ProfilePhoto { get; set; }
        public List<Friend> Friends { get; set; } = new List<Friend>();
        public List<Post> Posts { get; set; } = new List<Post>();
        public List<Story> Stories { get; set; } = new List<Story>();
        public List<SentFriendRequestModel> SentFriendRequests { get; set; } = new List<SentFriendRequestModel>();
        public List<GotFriendRequestModel> GotFriendRequests { get; set; } = new List<GotFriendRequestModel>();
    }
}
