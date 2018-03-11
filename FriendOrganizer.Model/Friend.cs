// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// Author: 
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace FriendOrganizer.Model
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel.DataAnnotations;

    public class Friend
    {
        public Friend()
        {
            PhoneNumbers = new Collection<FriendPhoneNumber>();
            Meetings = new Collection<Meeting>();
        }

        #region Properties

        [StringLength(50)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Имя")]
        [StringLength(50)]
        public string FirstName { get; set; }

        public int Id { get; set; }

        [StringLength(50)]
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }

        public int? FavouriteLanguageId { get; set; }

        public ProgrammingLanguage FavouriteLanguage { get; set; }

        public ICollection<FriendPhoneNumber> PhoneNumbers { get; set; }

        public ICollection<Meeting> Meetings { get; set; }

        #endregion
    }
}
