﻿// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// Author: 
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace FriendOrganizer.UI.ViewModel
{
    using System.Threading.Tasks;

    public interface IFriendDetailViewModel
    {
        #region Methods

        Task LoadAsync(int? friendId);

        #endregion

        bool HasChanges { get; }
    }
}
