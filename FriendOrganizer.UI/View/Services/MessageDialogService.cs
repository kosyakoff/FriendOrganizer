// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// Author: 
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace FriendOrganizer.UI.View.Services
{
    using System.Threading.Tasks;
    using System.Windows;

    using MahApps.Metro.Controls;
    using MahApps.Metro.Controls.Dialogs;

    public class MessageDialogService : IMessageDialogService
    {
        private MetroWindow MetroWindow
        {
            get
            {
                return (MetroWindow)Application.Current.MainWindow;
            }
        }

        #region Methods

        public async Task ShowInfoDialogAsync(string text)
        {
            await MetroWindow.ShowMessageAsync("Info", text);
        }

        public async Task<MessageDialogResult> ShowOkCancelDialogAsync(string text, string title)
        {
            var result = await MetroWindow.ShowMessageAsync(title, text, MessageDialogStyle.AffirmativeAndNegative);

            return result ==  MahApps.Metro.Controls.Dialogs.MessageDialogResult.Affirmative ? MessageDialogResult.Ok : MessageDialogResult.Cancel;
        }

        #endregion
    }
}
