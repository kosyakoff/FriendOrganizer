// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// Author: 
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace FriendOrganizer.UI.Data.Repositories
{
    using System.Threading.Tasks;

    using Model;

    public interface IGenericRepository<T>
    {
        #region Methods

        void Add(T model);
        Task<T> GetaByIdAsync(int id);
        bool HasChanges();
        void Remove(T model);
        Task SaveAsync();

        #endregion
    }
}
