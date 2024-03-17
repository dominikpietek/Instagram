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

        public ObservableCollection<SearchedUserView> GetMatchingProfiles(string searchingText, IAbstractFactory<SearchedUserView> searchedUserFactory, Action<int> ShowCheckProfile)
        {
            ObservableCollection<SearchedUserView> searchedUsersSection = new ObservableCollection<SearchedUserView>();
            foreach (SearchUserDto user in _searchUsersDtos.Where(u =>
            u.Nickname.Substring(0, searchingText.Length > u.Nickname.Length ? u.Nickname.Length : searchingText.Length).Equals(searchingText.ToLower())))
            {
                var userSearched = searchedUserFactory.Create();
                userSearched.SetDataContext(user.Id, ShowCheckProfile);
                searchedUsersSection.Add(userSearched);
            }
            return searchedUsersSection;
        }
    }
}
