// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// Author: 
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace FriendOrganizer.UI.View.Services
{
    public interface IMessageDialogService
    {
        #region Methods

        MessageDialogResult ShowOkCancelDialog(string text, string title);

        #endregion
    }
}
