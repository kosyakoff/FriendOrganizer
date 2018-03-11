// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// Author: 
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace FriendOrganizer.UI.Data.Repositories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using DataAccess;

    using Model;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Linq;

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

        public async Task<List<Friend>> GetAllFriendsAsync()
        {
            return await Context.Set<Friend>().ToListAsync();
        }

        public async Task ReloadFriendAsync(int friendId)
        {
            DbEntityEntry<Friend> dbEntityEntry = 
                Context.ChangeTracker.Entries<Friend>().SingleOrDefault(db => db.Entity.Id == friendId);

            await dbEntityEntry?.ReloadAsync();
        }

        #endregion
    }
}
