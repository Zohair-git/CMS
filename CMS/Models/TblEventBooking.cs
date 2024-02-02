using System;
using System.Collections.Generic;

namespace CMS.Models
{
    public partial class TblEventBooking
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? EventId { get; set; }
        public string? Img { get; set; }
        public int? Status { get; set; }
    }
}
