using System;
using System.Collections.Generic;

namespace CMS.Models
{
    public partial class TblFeedback
    {
        public int Id { get; set; }
        public int? Userid { get; set; }
        public string? Title { get; set; }
        public int? Rating { get; set; }
        public string? Text { get; set; }
        public int? Pid { get; set; }

        public virtual TblClientRegister IdNavigation { get; set; } = null!;
        public virtual TblProduct? RatingNavigation { get; set; }
    }
}
