// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// Author: 
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace FriendOrganizer.UI.ViewModel
{
    using System;
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
        private IMeetingLookupDataService _meetingLookupDataService;

        #endregion

        #region Properties

        public ObservableCollection<NavigationItemViewModel> Friends { get; }
        public ObservableCollection<NavigationItemViewModel> Meetings { get; }

        #endregion

        #region Constructors

        public NavigationViewModel(IFriendLookupDataService friendLookupDataService, IMeetingLookupDataService meetingLookupDataService, IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _friendLookupDataService = friendLookupDataService;
            _meetingLookupDataService = meetingLookupDataService;
            Friends = new ObservableCollection<NavigationItemViewModel>();
            Meetings = new ObservableCollection<NavigationItemViewModel>();
            _eventAggregator.GetEvent<AfterDetailSavedEvent>().Subscribe(AfterDetailSaved);
            _eventAggregator.GetEvent<AfterDetailDeletedEvent>().Subscribe(AfterDetailDeleted);
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
                Friends.Add(new NavigationItemViewModel(item.Id, item.DisplayMember, _eventAggregator, nameof(FriendDetailViewModel)));
            }

            lookup = await _meetingLookupDataService.GetMeetingLookupAsync();

            Meetings.Clear();

            foreach (LookupItem item in lookup)
            {
                Meetings.Add(new NavigationItemViewModel(item.Id, item.DisplayMember, _eventAggregator, nameof(MeetingDetailViewModel)));
            }
        }

        private void AfterDetailDeleted(AfterDetailDeletedEventArgs args)
        {
            switch (args.ViewModelName)
            {
                case nameof(FriendDetailViewModel):
                    AfterDetailDeleted(Friends, args);
                    break;
                case nameof(MeetingDetailViewModel):
                    AfterDetailDeleted(Meetings, args);
                    break;
            }
        }

        private void AfterDetailDeleted(ObservableCollection<NavigationItemViewModel> items, 
                                        AfterDetailDeletedEventArgs args)
        {
            var item = items.FirstOrDefault(x => x.Id == args.Id);

            if (item != null)
            {
                items.Remove(item);
            }
        }
    

        private void AfterDetailSaved(AfterDetailSavedEventArgs args)
        {
            switch (args.ViewModelName)
            {
                case nameof(FriendDetailViewModel):
                    AfterDetailSaved(Friends, args);
                    break;
                case nameof(MeetingDetailViewModel):
                    AfterDetailSaved(Meetings, args);
                    break;
            }
        }

        private void AfterDetailSaved(ObservableCollection<NavigationItemViewModel> items, AfterDetailSavedEventArgs args)
        {
            var lookupItem = items.SingleOrDefault(x => x.Id == args.Id);

            if (lookupItem == null)
            {
                items.Add(new NavigationItemViewModel(args.Id, args.DisplayMember, _eventAggregator, args.ViewModelName));
            }
            else
            {
                lookupItem.DisplayMember = args.DisplayMember;
            }
        }

        #endregion
    }
}
