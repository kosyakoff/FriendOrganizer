﻿// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// Author: 
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace FriendOrganizer.Model
{
    public class NullLookupItem : LookupItem
    {
        public new int? Id
        {
            get
            {
                return null;
            }
        }
    }
}
