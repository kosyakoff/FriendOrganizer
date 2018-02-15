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
    using Data.Repositories;

    using Event;

    using Model;

    using Prism.Commands;
    using Prism.Events;

    using View.Services;

    using Wrapper;

    public class FriendDetailViewModel : ViewModelBase, IFriendDetailViewModel
    {
        #region Fields

        private readonly IFriendRepository _friendRepository;
        private readonly IEventAggregator _eventAggregator;
        private FriendWrapper _friend;
        private bool _hasChanges;
        private readonly IMessageDialogService _dialogService;

        #endregion

        #region Properties

        public ICommand SaveCommand { get; }

        public ICommand DeleteCommand { get; }

        public FriendWrapper Friend
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

        public bool HasChanges
        {
            get { return _hasChanges; }
            private set
            {
                if (_hasChanges == value)
                    return;

                _hasChanges = value;
                OnPropertyChanged();
                ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
            }
        }


        #endregion

        #region Constructors

        public FriendDetailViewModel(IFriendRepository friendRepository, IEventAggregator eventAggregator, IMessageDialogService dialogService)
        {
            _friendRepository = friendRepository;
            _eventAggregator = eventAggregator;

            _dialogService = dialogService;

            SaveCommand = new DelegateCommand(OnSaveExecute, OnSaveCanExecute);
            DeleteCommand = new DelegateCommand(OnDeleteExecute);
        }

        #endregion

        #region Methods

        public async Task LoadAsync(int? friendId)
        {
            var friend = friendId.HasValue ?  await _friendRepository.GetaByIdAsync(friendId.Value) : 
                CreateNewFriend();
            Friend = new FriendWrapper(friend);
            Friend.PropertyChanged += (s, e) =>
            {
                if (!HasChanges)
                {
                    HasChanges = _friendRepository.HasChanges();
                }

                if (e.PropertyName == nameof(Friend.HasErrors))
                {
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            };

            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();

            if (friend.Id == 0)
            {
                //Used to trigger validation
                Friend.FirstName = string.Empty;
            }
        }

        private Friend CreateNewFriend()
        {
            var friend = new Friend();

            _friendRepository.Add(friend);
            return friend;
        }

        private async void OnDeleteExecute()
        {
            var result = _dialogService.
                ShowOkCancelDialog($"Do you realy want to delete {Friend.FirstName} {Friend.LastName}?","Question");

            if (result == MessageDialogResult.Cancel)
            {
                return;
            }

            _friendRepository.Remove(Friend.Model);
            await _friendRepository.SaveAsync();

            _eventAggregator.GetEvent<AfterFriendDeletedEvent>().Publish(Friend.Id);
        }

        private bool OnSaveCanExecute()
        {
            return Friend != null && !Friend.HasErrors && HasChanges;
        }

        private async void OnSaveExecute()
        {
           await _friendRepository.SaveAsync();
            HasChanges = _friendRepository.HasChanges();

            _eventAggregator.GetEvent<AfterFriendSaveEvent>().Publish(
                new AfterFriendSaveEventArgs(Friend.Id,
                    $"{Friend.FirstName} {Friend.LastName}"));
        }

        #endregion

    }
}
