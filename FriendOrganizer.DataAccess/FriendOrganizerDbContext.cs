// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// Author: 
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace FriendOrganizer.DataAccess
{
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;

    using Model;

    /// <summary>
    /// With this class we can connect to database and load and save friends
    /// </summary>
    public class FriendOrganizerDbContext : DbContext
    {
        #region Properties

        /// <summary>
        /// With this dbSet we can save and load friends using 
        /// FriendOrganizerDbContext
        /// </summary>
        public DbSet<Friend> Friends { get; set; }

        public DbSet<ProgrammingLanguage> ProgrammingLanguages { get; set; }

        public DbSet<FriendPhoneNumber> FriendPhoneNumbers { get; set; }

        public DbSet<Meeting> Meetings { get; set; }
        #endregion

        #region Constructors

        /// <summary>
        /// "FriendOrganizerDb" - name of connection string in app.config file 
        /// </summary>
        public FriendOrganizerDbContext()
            : base("FriendOrganizerDb")
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //Remove convetion that says to pluralize model in coressponfind datatable name 
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        #endregion
    }
}
