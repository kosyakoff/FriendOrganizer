// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// Author: 
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace FriendOrganizer.UI.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Input;

    using Data;
    using Data.Lookups;
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
        private readonly IProgrammingLanguageLookupDataService _programmingLanguageLookupDataService;
        private FriendPhoneNumberWrapper _selectedPhoneNumber;

        #endregion

        #region Properties

        public ICommand SaveCommand { get; }

        public ICommand DeleteCommand { get; }

        public ICommand AddPhoneNumberCommand { get; }

        public ICommand RemovePhoneNumberCommand { get; }

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

        public FriendPhoneNumberWrapper SelectedPhoneNumber
        {
            get { return _selectedPhoneNumber; }
            set
            {
                _selectedPhoneNumber = value;
                OnPropertyChanged();
                ((DelegateCommand)RemovePhoneNumberCommand).RaiseCanExecuteChanged();
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

        public ObservableCollection<LookupItem> ProgrammingLanguages { get; }

        public ObservableCollection<FriendPhoneNumberWrapper> PhoneNumbers { get; }

        #endregion

        #region Constructors

        public FriendDetailViewModel(IFriendRepository friendRepository, 
                                     IEventAggregator eventAggregator, 
                                     IMessageDialogService dialogService,
                                     IProgrammingLanguageLookupDataService programmingLanguageLookupDataService)
        {
            _friendRepository = friendRepository;
            _eventAggregator = eventAggregator;
            _programmingLanguageLookupDataService = programmingLanguageLookupDataService;

            _dialogService = dialogService;

            SaveCommand = new DelegateCommand(OnSaveExecute, OnSaveCanExecute);
            DeleteCommand = new DelegateCommand(OnDeleteExecute);

            AddPhoneNumberCommand = new DelegateCommand(OnAddPhoneNumberExecute);
            RemovePhoneNumberCommand = new DelegateCommand(OnRemovePhoneNumberExecute, OnRemovePhoneNumberCanExecute);

            ProgrammingLanguages = new ObservableCollection<LookupItem>();
            PhoneNumbers = new ObservableCollection<FriendPhoneNumberWrapper>();
        }

        #endregion

        #region Methods

        public async Task LoadAsync(int? friendId)
        {
            var friend = friendId.HasValue ? await _friendRepository.GetaByIdAsync(friendId.Value) :
                CreateNewFriend();

            InitalizeFriend(friend);

            InitializeFriendPhoneNumbers(friend.PhoneNumbers);

            await LoadProgrammingLanguageLookupsAsync();
        }

        private async Task LoadProgrammingLanguageLookupsAsync()
        {
            ProgrammingLanguages.Clear();

            ProgrammingLanguages.Add(new NullLookupItem() {DisplayMember = " - "});

            var lookup = await _programmingLanguageLookupDataService.GetProgrammingLanguageLookupAsync();

            foreach (LookupItem item in lookup)
            {
                ProgrammingLanguages.Add(item);
            }
        }

        private void InitializeFriendPhoneNumbers(ICollection<FriendPhoneNumber> friendPhoneNumbers)
        {
            foreach (FriendPhoneNumberWrapper wrapper in PhoneNumbers)
            {
                wrapper.PropertyChanged -= FriendPhoneNumberWrapperPropertyChangedHandler;
            }

            PhoneNumbers.Clear();

            foreach (FriendPhoneNumber friendPhoneNumber in friendPhoneNumbers)
            {
                var wrapper = new FriendPhoneNumberWrapper(friendPhoneNumber);
                PhoneNumbers.Add(wrapper);
                wrapper.PropertyChanged += FriendPhoneNumberWrapperPropertyChangedHandler;
            }
        }

        private void FriendPhoneNumberWrapperPropertyChangedHandler(object sender, PropertyChangedEventArgs e)
        {
            if (!HasChanges)
            {
                HasChanges = _friendRepository.HasChanges();
            }

            if (e.PropertyName == nameof(FriendPhoneNumberWrapper.HasErrors))
            {
                ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
            }
        }

        private void InitalizeFriend(Friend friend)
        {
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
            return Friend != null 
                   && !Friend.HasErrors 
                   && PhoneNumbers.All(pn => !pn.HasErrors)
                   && HasChanges;
        }

        private async void OnSaveExecute()
        {
           await _friendRepository.SaveAsync();
            HasChanges = _friendRepository.HasChanges();

            _eventAggregator.GetEvent<AfterFriendSaveEvent>().Publish(
                new AfterFriendSaveEventArgs(Friend.Id,
                    $"{Friend.FirstName} {Friend.LastName}"));
        }

        private bool OnRemovePhoneNumberCanExecute()
        {
            return SelectedPhoneNumber != null;
        }

        private void OnRemovePhoneNumberExecute()
        {
            SelectedPhoneNumber.PropertyChanged -= FriendPhoneNumberWrapperPropertyChangedHandler;

            _friendRepository.RemovePhoneNumber(SelectedPhoneNumber.Model);
            PhoneNumbers.Remove(SelectedPhoneNumber);
            SelectedPhoneNumber = null;
            HasChanges = _friendRepository.HasChanges();

            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
        }

        private void OnAddPhoneNumberExecute()
        {
            var newNumber = new FriendPhoneNumberWrapper(new FriendPhoneNumber());

            newNumber.PropertyChanged += FriendPhoneNumberWrapperPropertyChangedHandler;

            PhoneNumbers.Add(newNumber);
            Friend.Model.PhoneNumbers.Add(newNumber.Model);

            newNumber.Number = string.Empty; //To trigger validation
        }

        #endregion

    }
}
