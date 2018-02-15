// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// Author: 
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace FriendOrganizer.UI.Data.Repositories
{
    using System.Threading.Tasks;

    using Model;

    public interface IFriendRepository
    {
        #region Methods

        void Add(Friend friend);

        Task<Friend> GetaByIdAsync(int friendId);
        bool HasChanges();

        void Remove(Friend friendModel);
        Task SaveAsync();

        #endregion
    }
}
