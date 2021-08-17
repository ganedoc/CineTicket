namespace CineTicket.Core.Entities
{
    public class BookingRequest
    {
        public long BookingId { get; set; }
        public byte FirstSeatRowNumber { get; set; }
        public byte FirstSeatNumber { get; set; }
        public byte LastSeatRowNumber { get; set; }
        public byte LastSeatNumber { get; set; }
    }
}
