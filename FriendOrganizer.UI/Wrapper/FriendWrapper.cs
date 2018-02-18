// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// Author: 
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace FriendOrganizer.UI.Wrapper
{
    using System;
    using System.Collections.Generic;

    using Model;

    public class FriendWrapper : ModelWrapper<Friend>
    {
        #region Constructors

        public FriendWrapper(Friend model)
            : base(model)
        {
        }

        #endregion

        #region Properties

        public int Id
        {
            get
            {
                return Model.Id;
            }
        }

        public string FirstName
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

        public string LastName
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

        public string Email
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

        public int? FavouriteLanguageId
        {
            get
            {
                return GetValue<int?>();
            }
            set
            {
                SetValue(value);
            }
        }

        #endregion

        #region Methods

        protected override IEnumerable<string> ValidateProperty(string propertyName)
        {
            switch (propertyName)
            {
                case nameof(FirstName):
                    if (string.Equals(FirstName, "Robot", StringComparison.OrdinalIgnoreCase))
                    {
                        yield return "Robot is not valid friend";
                    }

                    break;
            }
        }

        #endregion
    }
}
