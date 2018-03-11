// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// Author: 
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace FriendOrganizer.UI.ViewModel
{
    using System.Threading.Tasks;

    public interface IDetailViewModel
    {
        #region Methods

        Task LoadAsync(int id);

        #endregion

        bool HasChanges { get; }
        int Id { get; }
    }
}
