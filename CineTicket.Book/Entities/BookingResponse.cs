using System.Collections.Generic;

namespace CineTicket.Core.Entities
{
    public class BookingResponse
    {
        public List<Booking> FailedBookings { get; set; }
        public int ProcessedCount { get; set; }
    }
}
