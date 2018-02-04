// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// Author: 
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace FriendOrganizer.UI.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Model;

    public interface IFriendDataService
    {
        #region Methods

        Task<List<Friend>> GetaAllAsync();

        #endregion
    }
}
