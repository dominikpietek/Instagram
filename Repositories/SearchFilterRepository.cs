using Instagram.DTOs;
using Instagram.StartupHelpers;
using Instagram.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instagram.Repositories
{
    class SearchFilterRepository
    {
        private readonly List<SearchUserDto> _searchUsersDtos;

        public SearchFilterRepository(List<SearchUserDto> searchUsersDtos)
        {
            _searchUsersDtos = searchUsersDtos;
        }

        public List<int> GetMatchingProfiles(string searchingText)
        {
            List<int> profileIds = new List<int>();
            foreach (SearchUserDto user in _searchUsersDtos.Where(u =>
            u.Nickname.Substring(0, searchingText.Length > u.Nickname.Length ? u.Nickname.Length : searchingText.Length).Equals(searchingText.ToLower())))
            {
                profileIds.Add(user.Id);
            }
            return profileIds;
        }
    }
}
