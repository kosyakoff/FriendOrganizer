// ---------------------------------------------------------------------------------------------------------------------------------------------------
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

    public class FriendRepository : IFriendRepository
    {
        #region Fields

        private readonly FriendOrganizerDbContext _context;

        #endregion

        #region Constructors

        public FriendRepository(FriendOrganizerDbContext context)
        {
            _context = context;
        }

        #endregion

        #region Methods

        public void Add(Friend friend)
        {
            _context.Friends.Add(friend);
        }

        public async Task<Friend> GetaByIdAsync(int friendId)
        {
                return await _context.Friends.SingleAsync(f => f.Id == friendId);
        }

        public bool HasChanges()
        {
            return _context.ChangeTracker.HasChanges();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Remove(Friend friendModel)
        {
            _context.Friends.Remove(friendModel);
        }

        #endregion
    }
}
