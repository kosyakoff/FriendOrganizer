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

    using Data.Lookups;

    using Event;

    using Model;

    using Prism.Events;

    public class NavigationViewModel : ViewModelBase, INavigationViewModel
    {
        #region Fields

        private readonly IEventAggregator _eventAggregator;

        private readonly IFriendLookupDataService _friendLookupDataService;

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
            _eventAggregator.GetEvent<AfterFriendDeletedEvent>().Subscribe(AfterFriendDeleted);
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
                Friends.Add(new NavigationItemViewModel(item.Id, item.DisplayMember, _eventAggregator));
            }
        }

        private void AfterFriendDeleted(int friendId)
        {
            var friend = Friends.FirstOrDefault(x => x.Id == friendId);

            if (friend != null)
            {
                Friends.Remove(friend);
            }
        }

        private void AfterFriendSaved(AfterFriendSaveEventArgs args)
        {
            var lookupItem = Friends.SingleOrDefault(x => x.Id == args.Id);

            if (lookupItem == null)
            {
                Friends.Add(new NavigationItemViewModel(args.Id, args.DisplayMember, _eventAggregator));
            }
            else
            {
                lookupItem.DisplayMember = args.DisplayMember;
            }
        }

        #endregion
    }
}
