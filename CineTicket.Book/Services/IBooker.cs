using CineTicket.Core.Entities;
using System.Collections.Generic;

namespace CineTicket.Core.Serrvices
{
    public interface IBooker
    {
        public BookingResponse BulkProcess(List<BookingRequest> bookingRequests);
    }
}
