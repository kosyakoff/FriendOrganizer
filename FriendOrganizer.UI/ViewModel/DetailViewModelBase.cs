// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// Author: 
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace FriendOrganizer.UI.ViewModel
{
    using System;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Input;

    using Event;

    using Model;

    using Prism.Commands;
    using Prism.Events;

    using View.Services;

    public abstract class DetailViewModelBase : ViewModelBase, IDetailViewModel
    {
        #region Fields

        protected readonly IEventAggregator EventAggregator;
        protected readonly IMessageDialogService MessageDialogService;
        private bool _hasChanges;
        private int _id;

        private string _title;

        #endregion

        #region Properties

        public ICommand DeleteCommand { get; private set; }

        public ICommand SaveCommand { get; private set; }

        public ICommand CloseDetailViewCommand { get; private set; }

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

        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                OnPropertyChanged();
            }
        }

        public int Id
        {
            get
            {
                return _id;
            }
            protected set
            {
                _id = value;
            }
        }

        #endregion

        #region Constructors

        protected DetailViewModelBase(IEventAggregator eventAggregator,
                                      IMessageDialogService messageDialogService)
        {
            EventAggregator = eventAggregator;
            MessageDialogService = messageDialogService;

            SaveCommand = new DelegateCommand(OnSaveExecute, OnSaveCanExecute);
            DeleteCommand = new DelegateCommand(OnDeleteExecute);
            CloseDetailViewCommand = new DelegateCommand(OnCloseDetailViewExecute);
        }

        #endregion

        #region Methods

        public abstract Task LoadAsync(int id);
        protected abstract void OnDeleteExecute();
        protected abstract bool OnSaveCanExecute();

        protected abstract void OnSaveExecute();

        protected virtual void OnCloseDetailViewExecute()
        {
            if (HasChanges)
            {
                MessageDialogResult result = MessageDialogService.ShowOkCancelDialog("You've made changes. Close this item?", "Question");

                if (result == MessageDialogResult.Cancel)
                {
                    return;
                }
            }

            EventAggregator.GetEvent<AfterDetailClosedEvent>().Publish(
                new AfterDetailClosedEventArgs
                {
                    Id = this.Id,
                    ViewModelName = this.GetType().Name
                });
        }

        protected virtual void RaiseCollectionSavedEvent()
        {
            EventAggregator.GetEvent<AfterCollectionSavedEvent>().Publish(
                new AfterCollectionSavedEventArgs
                {
                    ViewModelName = this.GetType().Name
                });
        }

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

        protected async Task SaveWithOptimisticConcurrencyAsync(Func<Task> saveFunc, Action afterSaveAction)
        {
            try
            {
                await saveFunc();
            }
            catch (DbUpdateConcurrencyException e)
            {
                var databaseValues = e.Entries.Single().GetDatabaseValues();
                if (databaseValues == null)
                {
                    MessageDialogService.ShowInfoDialog("The entity has been deleted by another user");
                    RaiseDetailDeletedEvent(Id);
                    return;
                }

                var result = MessageDialogService.ShowOkCancelDialog(
                    "The entity has been changed. Click save to save anyway, click Cancel to reload entity form database",
                    "Question");

                if (result == MessageDialogResult.Ok)
                {
                    //Update original values with database values
                    var entry = e.Entries.Single();
                    entry.OriginalValues.SetValues(entry.GetDatabaseValues());
                    await saveFunc();
                }
                else
                {
                    //Reload entiry from database
                    await e.Entries.Single().ReloadAsync();
                    await LoadAsync(Id);
                }
            }

            afterSaveAction();
        }

        #endregion

    }
}
