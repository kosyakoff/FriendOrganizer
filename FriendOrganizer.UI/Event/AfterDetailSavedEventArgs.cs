// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// Author: 
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace FriendOrganizer.UI.Event
{
    public class AfterDetailSavedEventArgs
    {
        #region Properties

        public string DisplayMember { get; set; }
        public int Id { get; set; }
        public string ViewModelName { get; set; }

        #endregion
    }
}
