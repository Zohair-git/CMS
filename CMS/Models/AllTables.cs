namespace CMS.Models
{
	public class AllTables
	{
        public IEnumerable<TblProduct> product_list { get; set; }

        public IEnumerable<TblClientRegister> client_register { get; set; }	

		public IEnumerable<TblUpcomingEvent> upcoming_events {get; set; }
		public IEnumerable<TblImage> Images_list {get; set; }

        public TblProduct productss { get; set; }
        public TblImage imagess { get; set; }
		public TblClientRegister clientRegister { get; set; }
		public TblUpcomingEvent upcomingEvent { get; set; }
		public TblEventBooking bookingss { get; set; }
	}
}
