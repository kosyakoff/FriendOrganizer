// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// Author: 
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace FriendOrganizer.Model
{
    using System.ComponentModel.DataAnnotations;

    public class FriendPhoneNumber
    {
        public int Id { get; set; }

        [Phone]
        [Required]
        public string Number { get; set; }
        public int FriendId { get; set; }
        public Friend Friend { get; set; }
    }
}
