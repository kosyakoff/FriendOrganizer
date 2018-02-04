// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// Author: 
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace FriendOrganizer.DataAccess.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class InitialDatabase : DbMigration
    {
        #region Methods

        public override void Down()
        {
            DropTable("dbo.Friend");
        }

        public override void Up()
        {
            CreateTable(
                "dbo.Friend",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Email = c.String(maxLength: 50),
                    FirstName = c.String(nullable: false, maxLength: 50),
                    LastName = c.String(maxLength: 50),
                }).PrimaryKey(t => t.Id);
        }

        #endregion
    }
}
