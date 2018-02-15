// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// Author: 
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace FriendOrganizer.UI.ViewModel
{
    using System;
    using System.Threading.Tasks;
    using System.Windows.Input;

    using Event;

    using Prism.Commands;
    using Prism.Events;

    using View.Services;

    public class MainViewModel : ViewModelBase
    {
        private IEventAggregator _eventAggregator;
        private readonly Func<IFriendDetailViewModel> _friendDetailViewModelCreator;
        private IFriendDetailViewModel _friendDetailViewModel;
        private readonly IMessageDialogService _messageDialogService;

        #region Properties

        public ICommand CreateNewFriendCommand { get; }

        public IFriendDetailViewModel FriendDetailViewModel
        {
            get
            {
                return _friendDetailViewModel;
            }
            private set
            {
                _friendDetailViewModel = value;
                OnPropertyChanged();
            }
        }

        public INavigationViewModel NavigationViewModel { get; }

        #endregion

        #region Constructors

        public MainViewModel(INavigationViewModel navigationViewModel, Func<IFriendDetailViewModel> friendDetailViewModelCreator, IEventAggregator eventAggregator, IMessageDialogService messageDialogService)
        {

            _messageDialogService = messageDialogService;
            _friendDetailViewModelCreator = friendDetailViewModelCreator;
            _eventAggregator = eventAggregator;

            _eventAggregator.GetEvent<OpenFriendDetailViewEvent>().Subscribe(OnOpenFriendDetailView);
            _eventAggregator.GetEvent<AfterFriendDeletedEvent>().Subscribe(AfterFriendDelete);

            CreateNewFriendCommand = new DelegateCommand(OnCreateNewFriendExecute);

            NavigationViewModel = navigationViewModel;
        }

        #endregion

        #region Methods

        public async Task LoadAsync()
        {
            await NavigationViewModel.LoadAsync();
        }

        private async void OnOpenFriendDetailView(int? friendId)
        {
            if (FriendDetailViewModel != null && FriendDetailViewModel.HasChanges)
            {
               
               var result = _messageDialogService.ShowOkCancelDialog("You have changes. Navigate away?", "Question");
                if (result == MessageDialogResult.Cancel)
                {
                    return;
                }
            }

            FriendDetailViewModel = _friendDetailViewModelCreator();

            await FriendDetailViewModel.LoadAsync(friendId);
        }

        private void OnCreateNewFriendExecute()
        {
            OnOpenFriendDetailView(null);
        }

        private void AfterFriendDelete(int obj)
        {
            FriendDetailViewModel = null;
        }

        #endregion
    }
}
