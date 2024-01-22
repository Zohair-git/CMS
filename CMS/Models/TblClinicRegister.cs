﻿using System;
using System.Collections.Generic;

namespace CMS.Models
{
    public partial class TblClinicRegister
    {
        public TblClinicRegister()
        {
            TblEventBookings = new HashSet<TblEventBooking>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Image { get; set; }
        public string? Details { get; set; }

        public virtual TblFeedback TblFeedback { get; set; } = null!;
        public virtual ICollection<TblEventBooking> TblEventBookings { get; set; }
    }
}
