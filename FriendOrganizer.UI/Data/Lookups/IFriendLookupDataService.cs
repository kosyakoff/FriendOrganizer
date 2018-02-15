// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// Author: 
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace FriendOrganizer.UI.Data.Lookups
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Model;

    public interface IFriendLookupDataService
    {
        #region Methods

        Task<IEnumerable<LookupItem>> GetFriendLookupAsync();

        #endregion
    }
}
