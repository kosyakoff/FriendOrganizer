// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// Author: 
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace FriendOrganizer.DataAccess.Migrations
{
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;

    using Model;

    internal sealed class Configuration : DbMigrationsConfiguration<FriendOrganizerDbContext>
    {
        #region Constructors

        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Seed method to add initial data in creating Friend table
        /// </summary>
        /// <param name="context">Previously created FriendOrganizerDbContext</param>
        protected override void Seed(FriendOrganizerDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            //First is identify expresion, we first check if we have friend in table with same
            //first name, if so - update record, if not - create new record
            context.Friends.AddOrUpdate(
                friend => friend.FirstName,
                new Model.Friend
                {
                    FirstName = "Thomas",
                    LastName = "Huber"
                },
                new Model.Friend
                {
                    FirstName = "Urs",
                    LastName = "Meier"
                },
                new Model.Friend
                {
                    FirstName = "Erkan",
                    LastName = "Ergin"
                },
                new Model.Friend
                {
                    FirstName = "Sara",
                    LastName = "Huber"
                });

            context.ProgrammingLanguages.AddOrUpdate(
                pl => pl.Name,
                new ProgrammingLanguage
                {
                    Name = "C#"
                },
                new ProgrammingLanguage
                {
                    Name = "TypeScript"
                },
                new ProgrammingLanguage
                {
                    Name = "F#"
                },
                new ProgrammingLanguage
                {
                    Name = "Swift"
                },
                new ProgrammingLanguage
                {
                    Name = "Java"
                });

            context.SaveChanges();

            context.FriendPhoneNumbers.AddOrUpdate(
                pn => pn.Number,
                new FriendPhoneNumber
                {
                    Number = "+49 12345678",
                    FriendId = context.Friends.First().Id
                });

            context.Meetings.AddOrUpdate(
                m => m.Title,
                new Meeting
                {
                    Title = "Watching Soccer",
                    DateFrom = new System.DateTime(2018,5,26),
                    DateTo = new System.DateTime(2018,5,26),
                    Friends = new List<Friend>
                    {
                        context.Friends.Single(f => f.FirstName == "Thomas" &&
                                                    f.LastName == "Huber"),
                        context.Friends.Single(f => f.FirstName == "Urs" &&
                                                    f.LastName == "Meier")
                    }
                });
        }

        #endregion
    }
}
