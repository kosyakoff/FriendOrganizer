// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// Author: 
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace FriendOrganizer.UI.Startup
{
    using Autofac;

    using Data;
    using Data.Lookups;
    using Data.Repositories;

    using DataAccess;

    using Prism.Events;

    using View.Services;

    using ViewModel;

    public class Bootstrapper
    {
        #region Methods

        public IContainer Bootstrap()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<EventAggregator>().As<IEventAggregator>().SingleInstance();
            builder.RegisterType<MainWindow>().AsSelf();
            builder.RegisterType<MainViewModel>().AsSelf();
            builder.RegisterType<NavigationViewModel>().As<INavigationViewModel>();
            builder.RegisterType<FriendDetailViewModel>().As<IFriendDetailViewModel>();
            builder.RegisterType<LookupDataService>().AsImplementedInterfaces();
            builder.RegisterType<FriendRepository>().As<IFriendRepository>();
            builder.RegisterType<FriendOrganizerDbContext>().AsSelf();
            builder.RegisterType<MessageDialogService>().As<IMessageDialogService>();

            return builder.Build();
        }

        #endregion
    }
}
