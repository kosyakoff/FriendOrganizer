// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// Author: 
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace FriendOrganizer.UI.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Input;

    using Data.Repositories;

    using Model;

    using Prism.Commands;
    using Prism.Events;

    using View.Services;

    using Wrapper;

    public class ProgrammingLanguageDetailViewModel : DetailViewModelBase, IProgrammingLanguageDetailViewModel
    {
        #region Fields

        private readonly IProgrammingLanguageRepository _programmingLanguageRepository;
        private ProgrammingLanguageWrapper _selectedProgrammingLanguage;

        #endregion

        #region Properties

        public ObservableCollection<ProgrammingLanguageWrapper> ProgrammingLanguages { get; }
        public ICommand AddCommand { get; }
        public ICommand RemoveCommand { get; }

        public ProgrammingLanguageWrapper SelectedProgrammingLanguage
        {
            get
            {
                return _selectedProgrammingLanguage;
            }
            set
            {
                _selectedProgrammingLanguage = value;
                OnPropertyChanged();
                ((DelegateCommand)RemoveCommand).RaiseCanExecuteChanged();
            }
        }

        #endregion

        #region Constructors

        public ProgrammingLanguageDetailViewModel(
            IEventAggregator eventAggregator,
            IMessageDialogService messageDialogService,
            IProgrammingLanguageRepository programmingLanguageRepository)
            : base(eventAggregator, messageDialogService)
        {
            _programmingLanguageRepository = programmingLanguageRepository;
            Title = "Programming Languages";
            ProgrammingLanguages = new ObservableCollection<ProgrammingLanguageWrapper>();
            AddCommand = new DelegateCommand(OnAddExecute);
            RemoveCommand = new DelegateCommand(OnRemoveExecute, OnRemoveCanExecute);
        }

        #endregion

        #region Methods

        public override async Task LoadAsync(int id)
        {
            Id = id;

            foreach (ProgrammingLanguageWrapper wrapper in ProgrammingLanguages)
            {
                wrapper.PropertyChanged -= HandleOnWrapperPropertyChanged;
            }

            ProgrammingLanguages.Clear();

            IEnumerable<ProgrammingLanguage> languages = 
                await _programmingLanguageRepository.GetAllAsync();

            foreach (ProgrammingLanguage language in languages)
            {
                var wrapper = new ProgrammingLanguageWrapper(language);
                wrapper.PropertyChanged += HandleOnWrapperPropertyChanged;
                ProgrammingLanguages.Add(wrapper);
            }
        }

        protected override void OnDeleteExecute()
        {
            throw new NotImplementedException();
        }

        protected override bool OnSaveCanExecute()
        {
            return HasChanges && ProgrammingLanguages.All(p => !p.HasErrors);
        }

        protected override async void OnSaveExecute()
        {
            try
            {
                await _programmingLanguageRepository.SaveAsync();
                HasChanges = _programmingLanguageRepository.HasChanges();
                RaiseCollectionSavedEvent();
            }
            catch (Exception e)
            {
                while (e.InnerException != null)
                {
                    e = e.InnerException;
                }

                await MessageDialogService.
                    ShowInfoDialogAsync("Error while saving entities," + "the data will be realoaded. Details: " + e.Message);

                await LoadAsync(Id);
            }
        }

        private void HandleOnWrapperPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (!HasChanges)
            {
                HasChanges = _programmingLanguageRepository.HasChanges();
            }

            if (e.PropertyName == nameof(ProgrammingLanguageWrapper.HasErrors))
            {
                ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
            }
        }

        private async void OnRemoveExecute()
        {
            bool isReferenced = 
                await _programmingLanguageRepository.IsReferencedByFriendAsync(SelectedProgrammingLanguage.Id);

            if (isReferenced)
            {
                await MessageDialogService.ShowInfoDialogAsync($"The language {SelectedProgrammingLanguage.Name} can't be removed, as it is referenced by at least one friend");
                return;
            }

            SelectedProgrammingLanguage.PropertyChanged -= HandleOnWrapperPropertyChanged;
            _programmingLanguageRepository.Remove(SelectedProgrammingLanguage.Model);
            ProgrammingLanguages.Remove(SelectedProgrammingLanguage);
            SelectedProgrammingLanguage = null;
            HasChanges = _programmingLanguageRepository.HasChanges();
            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
        }

        private void OnAddExecute()
        {
            var wrapper = new ProgrammingLanguageWrapper(new ProgrammingLanguage());
            wrapper.PropertyChanged += HandleOnWrapperPropertyChanged;
            _programmingLanguageRepository.Add(wrapper.Model);
            ProgrammingLanguages.Add(wrapper);

            //Trigger validation
            wrapper.Name = "";
        }

        private bool OnRemoveCanExecute()
        {
            return SelectedProgrammingLanguage != null;
        }

        #endregion
    }
}
