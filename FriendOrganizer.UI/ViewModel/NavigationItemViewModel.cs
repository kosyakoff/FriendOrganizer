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
        private readonly string _detailViewModelName;

        #endregion

        #region Properties

        public int Id { get; }

        public ICommand OpenDetailCommand { get; }


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

        public NavigationItemViewModel(int id, 
                                       string displayMember, 
                                       IEventAggregator eventAggregator,
                                       string detailViewModelName)
        {
            _eventAggregator = eventAggregator;
            _detailViewModelName = detailViewModelName;

            Id = id;
            DisplayMember = displayMember;
            OpenDetailCommand = new DelegateCommand(OnOpenDetailViewExecute);
        }

        #endregion

        #region Methods

        private void OnOpenDetailViewExecute()
        {
            _eventAggregator.GetEvent<OpenDetailViewEvent>().Publish(new OpenDetailViewEventArgs {Id = Id, ViewModelName = _detailViewModelName });
        }

        #endregion
    }
}
