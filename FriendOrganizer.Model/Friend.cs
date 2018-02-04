// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// Author: 
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace FriendOrganizer.Model
{
    using System.ComponentModel.DataAnnotations;

    public class Friend
    {
        #region Properties

        [StringLength(50)]
        public string Email { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        public int Id { get; set; }

        [StringLength(50)]
        public string LastName { get; set; }

        #endregion
    }
}
