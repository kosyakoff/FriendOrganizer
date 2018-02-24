using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriendOrganizer.UI.View.Services
{
    using System.Windows;

    public class MessageDialogService : IMessageDialogService
    {
        public MessageDialogResult ShowOkCancelDialog(string text, string title)
        {
            MessageBoxResult result = MessageBox.Show(text, title, MessageBoxButton.OKCancel);

            return result == MessageBoxResult.OK ? MessageDialogResult.Ok : MessageDialogResult.Cancel;
        }

        public void ShowInfoDialog(string text)
        {
            MessageBox.Show(text, "Info");
        }
    }
}
