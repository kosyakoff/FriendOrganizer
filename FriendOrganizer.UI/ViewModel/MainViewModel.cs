// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// Author: 
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace FriendOrganizer.UI.ViewModel
{
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Input;

    using Autofac.Features.Indexed;

    using Event;

    using Prism.Commands;
    using Prism.Events;

    using View.Services;

    public class MainViewModel : ViewModelBase
    {
        private readonly IEventAggregator _eventAggregator;
        private IDetailViewModel _selectedDetailViewModel;
        private readonly IMessageDialogService _messageDialogService;
        private readonly IIndex<string, IDetailViewModel> _detailViewModelCreator;
        private int _nextNewItemId = 0;

        #region Properties

        public ICommand CreateNewDetailCommand { get; }

        public ICommand OpenSingleDetailViewCommand { get; }

        public ObservableCollection<IDetailViewModel> DetailViewModels { get; }

        public IDetailViewModel SelectedDetailViewModel
        {
            get
            {
                return _selectedDetailViewModel;
            }
            set
            {
                _selectedDetailViewModel = value;
                OnPropertyChanged();
            }
        }

        public INavigationViewModel NavigationViewModel { get; }

        #endregion

        #region Constructors

        public MainViewModel(INavigationViewModel navigationViewModel, 
                             IIndex<string,IDetailViewModel> detailViewModelCreator,
                             IEventAggregator eventAggregator,
                             IMessageDialogService messageDialogService)
        {
            _messageDialogService = messageDialogService;
            _detailViewModelCreator = detailViewModelCreator;
            _eventAggregator = eventAggregator;

            DetailViewModels = new ObservableCollection<IDetailViewModel>();

            _eventAggregator.GetEvent<OpenDetailViewEvent>().Subscribe(OnOpenDetailView);
            _eventAggregator.GetEvent<AfterDetailDeletedEvent>().Subscribe(AfterDetailDeleted);
            _eventAggregator.GetEvent<AfterDetailClosedEvent>().Subscribe(AfterDetailClosed);

            CreateNewDetailCommand = new DelegateCommand<Type>(OnCreateNewDetailExecute);
            OpenSingleDetailViewCommand = new DelegateCommand<Type>(OnOpenSingleDetailViewExecute);

            NavigationViewModel = navigationViewModel;
        }

        #endregion

        #region Methods

        public async Task LoadAsync()
        {
            await NavigationViewModel.LoadAsync();
        }

        private async void OnOpenDetailView(OpenDetailViewEventArgs args)
        {
            IDetailViewModel detailViewModel = DetailViewModels.
                SingleOrDefault(vm => vm.Id == args.Id 
                                      && vm.GetType().Name == args.ViewModelName);

            if (detailViewModel == null)
            {
                detailViewModel = _detailViewModelCreator[args.ViewModelName];
                try
                {
                    await detailViewModel.LoadAsync(args.Id);
                }
                catch
                {
                    await _messageDialogService.ShowInfoDialogAsync("Could not load entiry");
                    await NavigationViewModel.LoadAsync();
                    return;
                }
                
                DetailViewModels.Add(detailViewModel);
            }

            SelectedDetailViewModel = detailViewModel;
        }

        private void OnCreateNewDetailExecute(Type viewModelType)
        {
            OnOpenDetailView(new OpenDetailViewEventArgs {Id = _nextNewItemId--, ViewModelName = viewModelType.Name});
        }

        private void OnOpenSingleDetailViewExecute(Type viewModelType)
        {
            OnOpenDetailView(new OpenDetailViewEventArgs { Id = -1, ViewModelName = viewModelType.Name });
        }

        private void AfterDetailClosed(AfterDetailClosedEventArgs args)
        {
            RemoveDetailViewModel(args.Id, args.ViewModelName);
        }

        private void AfterDetailDeleted(AfterDetailDeletedEventArgs args)
        {
            RemoveDetailViewModel(args.Id,args.ViewModelName);
        }

        private void RemoveDetailViewModel(int id, string viewModelName)
        {
            IDetailViewModel detailViewModel = DetailViewModels.
                SingleOrDefault(vm => vm.Id == id
                                      && vm.GetType().Name == viewModelName);

            if (detailViewModel != null)
            {
                DetailViewModels.Remove(detailViewModel);
            }
        }

        #endregion
    }
}
