﻿// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// Author: 
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace FriendOrganizer.UI.Data.Repositories
{
    using Model;

    public interface IFriendRepository : IGenericRepository<Friend>
    {
        #region Methods

        void RemovePhoneNumber(FriendPhoneNumber model);

        #endregion
    }
}
