// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// Author: 
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace FriendOrganizer.UI.Wrapper
{
    using System;

    using Model;

    public class MeetingWrapper : ModelWrapper<Meeting>
    {

        #region Properties

        public int Id
        {
            get
            {
                return Model.Id;
            }
        }

        public string Title
        {
            get
            {
                return GetValue<string>();
            }
            set
            {
                SetValue(value);
            }
        }

        public DateTime DateFrom
        {
            get
            {
                return GetValue<DateTime>();
            }
            set
            {
                SetValue(value);
                if (DateTo < DateFrom)
                {
                    DateTo = DateFrom;
                }
            }
        }

        public DateTime DateTo
        {
            get
            {
                return GetValue<DateTime>();
            }
            set
            {
                SetValue(value);
                if (DateTo < DateFrom)
                {
                    DateFrom = DateTo;
                }
            }
        }

        #endregion

        #region Constructors

        public MeetingWrapper(Meeting model)
            : base(model)
        {
        }

        #endregion

    }
}
