// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// Author: 
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace FriendOrganizer.UI.Wrapper
{
    using Model;

    public class FriendPhoneNumberWrapper : ModelWrapper<FriendPhoneNumber>
    {
        #region Properties

        public string Number
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

        #endregion

        #region Constructors

        public FriendPhoneNumberWrapper(FriendPhoneNumber model)
            : base(model)
        {
        }

        #endregion
    }
}
