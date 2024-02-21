using Instagram.Databases;
using Instagram.Interfaces;
using Instagram.JSONModels;
using Instagram.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instagram.Repositories
{
    public static class GetUser
    {
        public static async Task<User> FromDbAndFile(IUserRepository userRepository)
        {
            JSON<UserDataModel> userJSON = new JSON<UserDataModel>("UserData");
            UserDataModel userJSONModel = await userJSON.GetAsync<UserDataModel>();
            User user = await userRepository.GetUserWithPhotoAndRequestsAsync(userJSONModel.UserId);
            return user;
        }
    }
}
