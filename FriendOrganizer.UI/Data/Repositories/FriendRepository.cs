﻿// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// Author: 
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace FriendOrganizer.UI.Data.Repositories
{
    using System;
    using System.Data.Entity;
    using System.Threading.Tasks;

    using DataAccess;

    using Model;

    public class FriendRepository : GenericRepository<Friend,FriendOrganizerDbContext>, IFriendRepository
    {

        #region Constructors

        public FriendRepository(FriendOrganizerDbContext context) : base(context)
        {
        }

        #endregion

        #region Methods

        public override async Task<Friend> GetaByIdAsync(int friendId)
        {
                return await Context.Friends
                           .Include(f => f.PhoneNumbers)
                           .SingleAsync(f => f.Id == friendId);
        }


        public void RemovePhoneNumber(FriendPhoneNumber model)
        {
            Context.FriendPhoneNumbers.Remove(model);
        }

        #endregion
    }
}
