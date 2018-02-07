// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// Author: 
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace FriendOrganizer.UI.ViewModel
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;

    using Data;

    using Event;

    using Model;

    using Prism.Events;

    public class NavigationViewModel : ViewModelBase, INavigationViewModel
    {
        #region Fields

        private IEventAggregator _eventAggregator;

        private readonly IFriendLookupDataService _friendLookupDataService;

        private LookupItem _selectedFriend;

        #endregion

        #region Properties

        public ObservableCollection<LookupItem> Friends { get; }

        #endregion

        #region Constructors

        public NavigationViewModel(IFriendLookupDataService friendLookupDataService, IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _friendLookupDataService = friendLookupDataService;
            Friends = new ObservableCollection<LookupItem>();
        }

        #endregion

        #region Methods

        public async Task LoadAsync()
        {
            IEnumerable<LookupItem> lookup = null;
            lookup = await _friendLookupDataService.GetFriendLookupAsync();

            Friends.Clear();

            foreach (LookupItem item in lookup)
            {
                Friends.Add(item);
            }
        }

        #endregion

        public LookupItem SelectedFriend
        {
            get
            {
                return _selectedFriend;
            }
            set
            {
                _selectedFriend = value;
                OnPropertyChanged();
                if (_selectedFriend != null)
                {
                    _eventAggregator.GetEvent<OpenFriendDetailViewEvent>().Publish(_selectedFriend.Id);
                }
            }
        }
    }
}
