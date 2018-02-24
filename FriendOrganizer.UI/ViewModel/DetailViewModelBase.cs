// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// Author: 
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace FriendOrganizer.UI.ViewModel
{
    using System.Threading.Tasks;
    using System.Windows.Input;

    using Event;

    using Prism.Commands;
    using Prism.Events;

    public abstract class DetailViewModelBase : ViewModelBase, IDetailViewModel
    {
        #region Fields

        protected readonly IEventAggregator EventAggregator;
        private bool _hasChanges;

        #endregion

        #region Properties

        public ICommand DeleteCommand { get; private set; }

        public ICommand SaveCommand { get; private set; }

        public bool HasChanges
        {
            get
            {
                return _hasChanges;
            }
            protected set
            {
                if (_hasChanges == value)
                    return;

                _hasChanges = value;
                OnPropertyChanged();
                ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
            }
        }

        #endregion

        #region Constructors

        protected DetailViewModelBase(IEventAggregator eventAggregator)
        {
            EventAggregator = eventAggregator;

            SaveCommand = new DelegateCommand(OnSaveExecute, OnSaveCanExecute);
            DeleteCommand = new DelegateCommand(OnDeleteExecute);
        }

        #endregion

        #region Methods

        public abstract Task LoadAsync(int? meetingId);
        protected abstract void OnDeleteExecute();
        protected abstract bool OnSaveCanExecute();

        protected abstract void OnSaveExecute();

        protected virtual void RaiseDetailDeletedEvent(int modelId)
        {
            EventAggregator.GetEvent<AfterDetailDeletedEvent>().Publish(
                new AfterDetailDeletedEventArgs
                {
                    Id = modelId,
                    ViewModelName = GetType().Name
                });
        }

        protected virtual void RaiseDetailSavedEvent(int modelId, string displayMember)
        {
            EventAggregator.GetEvent<AfterDetailSavedEvent>().Publish(
                new AfterDetailSavedEventArgs
                {
                    Id = modelId,
                    ViewModelName = GetType().Name,
                    DisplayMember = displayMember
                });
        }

        #endregion

    }
}
