// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// Author: 
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace FriendOrganizer.UI.Wrapper
{
    using Model;

    public class ProgrammingLanguageWrapper : ModelWrapper<ProgrammingLanguage>
    {
        #region Properties

        public int Id
        {
            get
            {
                return Model.Id;
            }
        }

        public string Name
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

        public ProgrammingLanguageWrapper(ProgrammingLanguage model)
            : base(model)
        {
        }

        #endregion
    }
}
