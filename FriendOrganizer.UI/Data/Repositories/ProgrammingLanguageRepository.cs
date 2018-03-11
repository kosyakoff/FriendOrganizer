// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// Author: 
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace FriendOrganizer.UI.Data.Repositories
{
    using System.Data.Entity;
    using System.Threading.Tasks;

    using DataAccess;

    using Model;

    public class ProgrammingLanguageRepository : GenericRepository<ProgrammingLanguage, FriendOrganizerDbContext>, IProgrammingLanguageRepository
    {
        #region Constructors

        public ProgrammingLanguageRepository(FriendOrganizerDbContext context)
            : base(context)
        {
        }

        #endregion

        #region Methods

        public async Task<bool> IsReferencedByFriendAsync(int programmingLanguageId)
        {
            return await Context.Friends.AsNoTracking().AnyAsync(f => f.FavouriteLanguageId == programmingLanguageId);
        }

        #endregion
    }
}
