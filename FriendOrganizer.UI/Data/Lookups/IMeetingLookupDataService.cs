// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// Author: 
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace FriendOrganizer.UI.Data.Lookups
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Model;

    public interface IMeetingLookupDataService
    {
        #region Methods

        Task<IEnumerable<LookupItem>> GetMeetingLookupAsync();

        #endregion
    }
}
