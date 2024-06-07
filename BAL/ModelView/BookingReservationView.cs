using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.ModelView
{
    public class BookingReservationView
    {
        public string RoomNumber { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal? ActualPrice { get; set; }
        public int CustomerId { get; set; }
        public string CustomerFullName { get; set; }
    }
}
