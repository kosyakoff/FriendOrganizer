// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// Author: 
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace FriendOrganizer.UI.Data
{
    using System.Threading.Tasks;

    using Model;

    public interface IFriendDataService
    {
        #region Methods

        Task<Friend> GetaByIdAsync(int friendId);
        Task SaveAsync(Friend friend);

        #endregion
    }
}
