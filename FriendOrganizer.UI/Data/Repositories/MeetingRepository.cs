﻿// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// Author: 
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace FriendOrganizer.UI.Data.Repositories
{
    using System.Threading.Tasks;
    using DataAccess;

    using Model;
    using System.Data.Entity;

    public class MeetingRepository : GenericRepository<Meeting, FriendOrganizerDbContext>, IMeetingRepository
    {
        #region Constructors

        public MeetingRepository(FriendOrganizerDbContext context)
            : base(context)
        {
        }

        public override async Task<Meeting> GetaByIdAsync(int id)
        {
           return await Context.Meetings.Include(m => m.Friends).SingleAsync(m => m.Id == id);
        }

        #endregion
    }
}