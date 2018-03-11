// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// Author: 
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace FriendOrganizer.UI.ViewModel
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Input;

    using Data.Lookups;
    using Data.Repositories;

    using Event;

    using Model;

    using Prism.Commands;
    using Prism.Events;

    using View.Services;

    using Wrapper;

    public class FriendDetailViewModel : DetailViewModelBase, IFriendDetailViewModel
    {
        #region Fields

        private FriendWrapper _friend;

        private readonly IFriendRepository _friendRepository;
        private readonly IProgrammingLanguageLookupDataService _programmingLanguageLookupDataService;
        private FriendPhoneNumberWrapper _selectedPhoneNumber;

        #endregion

        #region Properties

        public ICommand AddPhoneNumberCommand { get; }

        public ObservableCollection<FriendPhoneNumberWrapper> PhoneNumbers { get; }

        public ObservableCollection<LookupItem> ProgrammingLanguages { get; }

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
            get
            {
                return _selectedPhoneNumber;
            }
            set
            {
                _selectedPhoneNumber = value;
                OnPropertyChanged();
                ((DelegateCommand)RemovePhoneNumberCommand).RaiseCanExecuteChanged();
            }
        }

        #endregion

        #region Constructors

        public FriendDetailViewModel(
            IFriendRepository friendRepository,
            IEventAggregator eventAggregator,
            IMessageDialogService dialogService,
            IProgrammingLanguageLookupDataService programmingLanguageLookupDataService)
            : base(eventAggregator, dialogService)
        {
            _friendRepository = friendRepository;
            _programmingLanguageLookupDataService = programmingLanguageLookupDataService;

            eventAggregator.GetEvent<AfterCollectionSavedEvent>().Subscribe(HandleOnAfterCollectionSaved);

            AddPhoneNumberCommand = new DelegateCommand(OnAddPhoneNumberExecute);
            RemovePhoneNumberCommand = new DelegateCommand(OnRemovePhoneNumberExecute, OnRemovePhoneNumberCanExecute);

            ProgrammingLanguages = new ObservableCollection<LookupItem>();
            PhoneNumbers = new ObservableCollection<FriendPhoneNumberWrapper>();
        }

        #endregion

        #region Methods

        public override async Task LoadAsync(int friendId)
        {
            var friend = friendId > 0 
                             ? await _friendRepository.GetaByIdAsync(friendId) 
                             : CreateNewFriend();
            Id = friendId;

            InitalizeFriend(friend);

            InitializeFriendPhoneNumbers(friend.PhoneNumbers);

            await LoadProgrammingLanguageLookupsAsync();
        }

        protected override async void OnDeleteExecute()
        {
            if (await _friendRepository.HasMeetingsAsync(firendId: Friend.Id))
            {
                MessageDialogService.ShowInfoDialog($"{Friend.FirstName} {Friend.LastName} can't be deleted, as this friend is part of at least one meeting");
                return;
            }

            var result = MessageDialogService.ShowOkCancelDialog($"Do you realy want to delete {Friend.FirstName} {Friend.LastName}?", "Question");

            if (result == MessageDialogResult.Cancel)
            {
                return;
            }

            _friendRepository.Remove(Friend.Model);
            await _friendRepository.SaveAsync();

            RaiseDetailDeletedEvent(modelId: Friend.Id);
        }

        protected override bool OnSaveCanExecute()
        {
            return Friend != null && !Friend.HasErrors && PhoneNumbers.All(pn => !pn.HasErrors) && HasChanges;
        }

        protected override async void OnSaveExecute()
        {
            await _friendRepository.SaveAsync();
            HasChanges = _friendRepository.HasChanges();

            Id = Friend.Id;

            RaiseDetailSavedEvent(modelId: Friend.Id, displayMember: $"{Friend.FirstName} {Friend.LastName}");
        }

        private Friend CreateNewFriend()
        {
            var friend = new Friend();

            _friendRepository.Add(friend);
            return friend;
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

        private async void HandleOnAfterCollectionSaved(AfterCollectionSavedEventArgs args)
        {
            if (args.ViewModelName == nameof(ProgrammingLanguageDetailViewModel))
            {
                await LoadProgrammingLanguageLookupsAsync();
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

                if (e.PropertyName == nameof(Friend.FirstName) || e.PropertyName == nameof(Friend.LastName))
                {
                    SetTitle();
                }
            };

            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();

            if (friend.Id == 0)
            {
                //Used to trigger validation
                Friend.FirstName = string.Empty;
            }

            SetTitle();
        }

        private void SetTitle()
        {
            Title = $"{Friend.FirstName} {Friend.LastName}";
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

        private async Task LoadProgrammingLanguageLookupsAsync()
        {
            ProgrammingLanguages.Clear();

            ProgrammingLanguages.Add(
                new NullLookupItem()
                {
                    DisplayMember = " - "
                });

            var lookup = await _programmingLanguageLookupDataService.GetProgrammingLanguageLookupAsync();

            foreach (LookupItem item in lookup)
            {
                ProgrammingLanguages.Add(item);
            }
        }

        private void OnAddPhoneNumberExecute()
        {
            var newNumber = new FriendPhoneNumberWrapper(new FriendPhoneNumber());

            newNumber.PropertyChanged += FriendPhoneNumberWrapperPropertyChangedHandler;

            PhoneNumbers.Add(newNumber);
            Friend.Model.PhoneNumbers.Add(newNumber.Model);

            newNumber.Number = string.Empty; //To trigger validation
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

        #endregion
    }
}
