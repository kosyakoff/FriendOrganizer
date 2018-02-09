// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// Author: 
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace FriendOrganizer.UI.ViewModel
{
    using System;
    using System.Threading.Tasks;
    using System.Windows.Input;

    using Data;

    using Event;

    using Model;

    using Prism.Commands;
    using Prism.Events;

    public class FriendDetailViewModel : ViewModelBase, IFriendDetailViewModel
    {
        #region Fields

        private readonly IFriendDataService _dataService;
        private readonly IEventAggregator _eventAggregator;
        private Friend _friend;

        #endregion

        #region Properties

        public ICommand SaveCommand { get; }

        #endregion

        #region Constructors

        public FriendDetailViewModel(IFriendDataService friendDataService, IEventAggregator eventAggregator)
        {
            _dataService = friendDataService;
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<OpenFriendDetailViewEvent>().Subscribe(OnOpenFriendDetailView);
            SaveCommand = new DelegateCommand(OnSaveExecute, OnSaveCanExecute);
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

        private bool OnSaveCanExecute()
        {
            return true;
        }

        private async void OnSaveExecute()
        {
           await _dataService.SaveAsync(Friend);
            _eventAggregator.GetEvent<AfterFriendSaveEvent>().Publish(
                new AfterFriendSaveEventArgs(Friend.Id,
                    $"{Friend.FirstName} {Friend.LastName}"));
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
