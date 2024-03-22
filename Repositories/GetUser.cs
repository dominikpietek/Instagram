using Instagram.Databases;
using Instagram.Interfaces;
using Instagram.JSONModels;
using Instagram.Models;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instagram.Repositories
{
    public static class GetUser
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        public static async Task<User> FromDbAndFileAsync(IUserRepository userRepository)
        {
            try
            {
                JSON<UserDataModel> userJSON = new JSON<UserDataModel>("UserData");
                UserDataModel userJSONModel = userJSON.Get<UserDataModel>();
                return await userRepository.GetUserWithPhotoAndRequestsAsync(userJSONModel.UserId);
            }
            catch (Exception e)
            {
                _logger.Error($"{e.Message};GetUser");
                throw;
            }
        }

        public static async Task<UserDataModel> FromFileAsync()
        {
            try
            {
                JSON<UserDataModel> userJSON = new JSON<UserDataModel>("UserData");
                return userJSON.Get<UserDataModel>();
            }
            catch (Exception e)
            {
                _logger.Error($"{e.Message};GetUser");
                throw;
            }
        }

        public static async Task<int> IdFromFile()
        {
            try
            {
                UserDataModel userModel = await GetUser.FromFileAsync();
                return userModel.UserId;
            }
            catch (Exception e)
            {
                _logger.Error($"{e.Message};GetUser");
                throw;
            }
        }
    }
}
