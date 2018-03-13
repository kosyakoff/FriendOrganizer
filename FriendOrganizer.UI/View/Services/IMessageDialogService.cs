// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// Author: 
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace FriendOrganizer.UI.View.Services
{
    using System.Threading.Tasks;

    public interface IMessageDialogService
    {
        #region Methods

        Task<MessageDialogResult> ShowOkCancelDialogAsync(string text, string title);

        Task ShowInfoDialogAsync(string text);

        #endregion
    }
}
