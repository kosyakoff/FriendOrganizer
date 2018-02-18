// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// Author: 
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace FriendOrganizer.Model
{
    using System.ComponentModel.DataAnnotations;

    public class ProgrammingLanguage
    {
        #region Properties

        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        #endregion
    }
}
