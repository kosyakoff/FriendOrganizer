// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// Author: 
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace FriendOrganizer.UI.Data.Lookups
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;

    using DataAccess;

    using Model;

    public class LookupDataService : IFriendLookupDataService
    {
        #region Fields

        private readonly Func<FriendOrganizerDbContext> _contextCreator;

        #endregion

        #region Constructors

        public LookupDataService(Func<FriendOrganizerDbContext> contextCreater)
        {
            _contextCreator = contextCreater;
        }

        #endregion

        #region Methods

        public async Task<IEnumerable<LookupItem>> GetFriendLookupAsync()
        {
            using (FriendOrganizerDbContext ctx = _contextCreator())
            {
                return await ctx.Friends.AsNoTracking().Select(
                           friend => new LookupItem
                           {
                               Id = friend.Id,
                               DisplayMember = friend.FirstName + " " + friend.LastName
                           }).ToListAsync();
            }
        }

        #endregion
    }
}
