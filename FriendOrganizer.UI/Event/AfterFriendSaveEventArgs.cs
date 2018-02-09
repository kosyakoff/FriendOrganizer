﻿namespace FriendOrganizer.UI.Event
{
    public class AfterFriendSaveEventArgs
    {
        public int Id { get; set; }
        public string DisplayMember { get; set; }
        public AfterFriendSaveEventArgs(int id, string displayMember)
        {
            Id = id;
            DisplayMember = displayMember;
        }
    }
}