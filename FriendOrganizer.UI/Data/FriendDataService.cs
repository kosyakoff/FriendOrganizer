// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// Author: 
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace FriendOrganizer.UI.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Threading.Tasks;

    using DataAccess;

    using Model;

    public class FriendDataService : IFriendDataService
    {
        #region Fields

        private readonly Func<FriendOrganizerDbContext> _contextCreator;

        #endregion

        #region Constructors

        public FriendDataService(Func<FriendOrganizerDbContext> contextCreator)
        {
            _contextCreator = contextCreator;
        }

        #endregion

        #region Methods

        public async Task<List<Friend>> GetaAllAsync()
        {
            List<Friend> friends = null;

            using (var ctx = _contextCreator())
            {
                try
                {
                    friends = await ctx.Friends.AsNoTracking().ToListAsync();
                }
                catch (Exception e)
                {
                }

                return friends;
            }
        }

        #endregion
    }
}
