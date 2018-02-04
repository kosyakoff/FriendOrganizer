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

    using Model;

    public class MainViewModel : ViewModelBase
    {
        #region Fields

        private readonly IFriendDataService _friendDataService;

        private Friend _selectedFriend;

        #endregion

        #region Properties

        public ObservableCollection<Friend> Friends { get; private set; }

        #endregion

        #region Constructors

        public MainViewModel(IFriendDataService friendDataService)
        {
            _friendDataService = friendDataService;
            Friends = new ObservableCollection<Friend>();
        }

        #endregion

        #region Methods

        public async Task LoadAsync()
        {
            List<Friend> friends = await _friendDataService.GetaAllAsync();
            Friends.Clear();

            foreach (var friend in friends)
            {
                Friends.Add(friend);
            }
        }

        #endregion

        public Friend SelectedFriend
        {
            get
            {
                return _selectedFriend;
            }
            set
            {
                _selectedFriend = value;
                OnPropertyChanged();
            }
        }
    }
}
