// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// Author: 
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace FriendOrganizer.UI.ViewModel
{
    using System.Threading.Tasks;

    public class MainViewModel : ViewModelBase
    {
        #region Properties

        public IFriendDetailViewModel FriendDetailViewModel { get; }

        public INavigationViewModel NavigationViewModel { get; }

        #endregion

        #region Constructors

        public MainViewModel(INavigationViewModel navigationViewModel, IFriendDetailViewModel friendDetailViewModel)
        {
            NavigationViewModel = navigationViewModel;
            FriendDetailViewModel = friendDetailViewModel;
        }

        #endregion

        #region Methods

        public async Task LoadAsync()
        {
            await NavigationViewModel.LoadAsync();
        }

        #endregion
    }
}
