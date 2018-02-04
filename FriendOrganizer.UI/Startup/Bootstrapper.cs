// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// Author: 
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace FriendOrganizer.UI.Startup
{
    using Autofac;

    using Data;

    using DataAccess;

    using ViewModel;

    public class Bootstrapper
    {
        #region Methods

        public IContainer Bootstrap()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<MainWindow>().AsSelf();
            builder.RegisterType<MainViewModel>().AsSelf();
            builder.RegisterType<FriendDataService>().As<IFriendDataService>();
            builder.RegisterType<FriendOrganizerDbContext>().AsSelf();

            return builder.Build();
        }

        #endregion
    }
}
