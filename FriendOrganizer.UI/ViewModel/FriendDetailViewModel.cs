// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// Author: 
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace FriendOrganizer.UI.ViewModel
{
    using System.Threading.Tasks;

    using Data;

    using Model;

    public class FriendDetailViewModel : ViewModelBase, IFriendDetailViewModel
    {
        #region Fields

        private readonly IFriendDataService _dataService;
        private Friend _friend;

        #endregion

        #region Constructors

        public FriendDetailViewModel(IFriendDataService friendDataService)
        {
            _dataService = friendDataService;
        }

        #endregion

        #region Methods

        public async Task LoadAsync(int friendId)
        {
            Friend = await _dataService.GetaByIdAsync(friendId);
        }

        #endregion

        public Friend Friend
        {
            get
            {
                return _friend;
            }
            set
            {
                _friend = value;
                OnPropertyChanged();
            }
        }
    }
}
