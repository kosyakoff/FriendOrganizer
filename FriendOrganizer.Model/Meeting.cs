﻿// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// Author: 
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace FriendOrganizer.Model
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel.DataAnnotations;

    public class Meeting
    {
        #region Properties

        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }

        public ICollection<Friend> Friends { get; set; }

        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        #endregion

        #region Constructors

        public Meeting()
        {
            Friends = new Collection<Friend>();
        }

        #endregion
    }
}
