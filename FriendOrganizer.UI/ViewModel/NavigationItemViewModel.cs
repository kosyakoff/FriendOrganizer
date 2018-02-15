// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// Author: 
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace FriendOrganizer.UI.ViewModel
{
    using System.Windows.Input;

    using Event;

    using Prism.Commands;
    using Prism.Events;

    public class NavigationItemViewModel : ViewModelBase
    {
        #region Fields

        private string _displayMember;
        private readonly IEventAggregator _eventAggregator;

        #endregion

        #region Properties

        public int Id { get; }

        public ICommand OpenFriendDetailCommand { get; }


        public string DisplayMember
        {
            get
            {
                return _displayMember;
            }
            set
            {
                _displayMember = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Constructors

        public NavigationItemViewModel(int id, string displayMember, IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;

            Id = id;
            DisplayMember = displayMember;
            OpenFriendDetailCommand = new DelegateCommand(OnOpenFriendDetailView);
        }

        #endregion

        #region Methods

        private void OnOpenFriendDetailView()
        {
            _eventAggregator.GetEvent<OpenFriendDetailViewEvent>().Publish(Id);
        }

        #endregion
    }
}
