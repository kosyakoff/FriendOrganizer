// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// Author: 
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace FriendOrganizer.UI.ViewModel
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;

    using Data;

    using Event;

    using Model;

    using Prism.Events;

    public class NavigationViewModel : ViewModelBase, INavigationViewModel
    {
        #region Fields

        private readonly IEventAggregator _eventAggregator;

        private readonly IFriendLookupDataService _friendLookupDataService;

        private NavigationItemViewModel _selectedFriend;

        #endregion

        #region Properties

        public ObservableCollection<NavigationItemViewModel> Friends { get; }

        #endregion

        #region Constructors

        public NavigationViewModel(IFriendLookupDataService friendLookupDataService, IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _friendLookupDataService = friendLookupDataService;
            Friends = new ObservableCollection<NavigationItemViewModel>();
            _eventAggregator.GetEvent<AfterFriendSaveEvent>().Subscribe(AfterFriendSaved);
        }

        private void AfterFriendSaved(AfterFriendSaveEventArgs obj)
        {
            var lookupItem = Friends.Single(x => x.Id == obj.Id);
            lookupItem.DisplayMember = obj.DisplayMember;
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
                Friends.Add(new NavigationItemViewModel(item.Id,item.DisplayMember));
            }
        }

        #endregion

        public NavigationItemViewModel SelectedFriend
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
