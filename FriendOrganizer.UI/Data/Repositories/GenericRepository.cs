// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// Author: 
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace FriendOrganizer.UI.Data.Repositories
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Threading.Tasks;

    public class GenericRepository<TEntity, TContext> : IGenericRepository<TEntity>
        where TContext : DbContext where TEntity : class
    {
        #region Fields

        protected readonly TContext Context;

        #endregion

        #region Constructors

        protected GenericRepository(TContext context)
        {
            Context = context;
        }

        #endregion

        #region Methods

        public void Add(TEntity model)
        {
            Context.Set<TEntity>().Add(model);
        }

        public virtual async Task<TEntity> GetaByIdAsync(int id)
        {
            return await Context.Set<TEntity>().FindAsync(id);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await Context.Set<TEntity>().ToListAsync();
        }

        public bool HasChanges()
        {
            return Context.ChangeTracker.HasChanges();
        }

        public void Remove(TEntity model)
        {
            Context.Set<TEntity>().Remove(model);
        }

        public async Task SaveAsync()
        {
            await Context.SaveChangesAsync();
        }

        #endregion
    }
}
