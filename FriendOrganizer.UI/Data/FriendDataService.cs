﻿// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// Author: 
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace FriendOrganizer.UI.Data
{
    using System;
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

        public async Task<Friend> GetaByIdAsync(int friendId)
        {
            using (var ctx = _contextCreator())
            {
                return await ctx.Friends.AsNoTracking().SingleAsync(f => f.Id == friendId);
            }
        }

        #endregion
    }
}
