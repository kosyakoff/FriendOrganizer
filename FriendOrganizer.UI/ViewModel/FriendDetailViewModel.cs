// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// Author: 
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace FriendOrganizer.UI.ViewModel
{
    using System.Threading.Tasks;

    using Data;

    using Event;

    using Model;

    using Prism.Events;

    public class FriendDetailViewModel : ViewModelBase, IFriendDetailViewModel
    {
        #region Fields

        private readonly IFriendDataService _dataService;
        private IEventAggregator _eventAggregator;
        private Friend _friend;

        #endregion

        #region Constructors

        public FriendDetailViewModel(IFriendDataService friendDataService, IEventAggregator eventAggregator)
        {
            _dataService = friendDataService;
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<OpenFriendDetailViewEvent>().Subscribe(OnOpenFriendDetailView);
        }

        #endregion

        #region Methods

        public async Task LoadAsync(int friendId)
        {
            Friend = await _dataService.GetaByIdAsync(friendId);
        }

        private async void OnOpenFriendDetailView(int friendId)
        {
            await LoadAsync(friendId);
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
