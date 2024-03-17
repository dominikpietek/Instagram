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
        public static async Task<User> FromDbAndFileAsync(IUserRepository userRepository)
        {
            JSON<UserDataModel> userJSON = new JSON<UserDataModel>("UserData");
            UserDataModel userJSONModel = userJSON.Get<UserDataModel>();
            return await userRepository.GetUserWithPhotoAndRequestsAsync(userJSONModel.UserId);
        }
        public static async Task<UserDataModel> FromFileAsync()
        {
            JSON<UserDataModel> userJSON = new JSON<UserDataModel>("UserData");
            return userJSON.Get<UserDataModel>();
        }
        public static async Task<int> IdFromFile()
        {
            UserDataModel userModel = await GetUser.FromFileAsync();
            return userModel.UserId;
        }
    }
}
