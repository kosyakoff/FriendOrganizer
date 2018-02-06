// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// Author: 
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace FriendOrganizer.UI.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;

    using Data;

    using Model;

    public class NavigationViewModel : INavigationViewModel
    {
        #region Fields

        private readonly IFriendLookupDataService _friendLookupDataService;

        #endregion

        #region Properties

        public ObservableCollection<LookupItem> Friends { get; }

        #endregion

        #region Constructors

        public NavigationViewModel(IFriendLookupDataService friendLookupDataService)
        {
            _friendLookupDataService = friendLookupDataService;
            Friends = new ObservableCollection<LookupItem>();
        }

        #endregion

        #region Methods

        public async Task LoadAsync()
        {
            IEnumerable<LookupItem> lookup = null;
            try
            {
                lookup = await _friendLookupDataService.GetFriendLookupAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            Friends.Clear();

            foreach (LookupItem item in lookup)
            {
                Friends.Add(item);
            }
        }

        #endregion
    }
}
