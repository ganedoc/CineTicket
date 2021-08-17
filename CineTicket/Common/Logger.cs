using CineTicket.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using static CineTicket.Common.Constants;

namespace CineTicket.Common
{
    public static class Logger
    {
        public static void ConsoleLogSummary(string filePath,List<BookingRequest> bookingRequests, BookingResponse bookingResponse)
        {
            Console.WriteLine(FILE_PATH + COLON + filePath + Environment.NewLine);
            Console.WriteLine(TOTAL_NUM_REQUESTS + bookingRequests.Count());
            Console.WriteLine(BOOKING_PROCESSED + bookingResponse.ProcessedCount);
            Console.WriteLine(SUCCESS_BOOKINGS + (bookingResponse.ProcessedCount - bookingResponse.FailedBookings.Count()));
            Console.WriteLine(FAILED_BOOKINGS + bookingResponse.FailedBookings.Count());
            Console.WriteLine(Environment.NewLine + FAILED_HEADER);
            bookingResponse.FailedBookings.GroupBy(x => x.ReasonPhrase).ToList().ForEach(x =>
            {
                Console.WriteLine(Environment.NewLine + x.Key + Environment.NewLine + BOOKING_ID + string.Join(COMMA, x.Select(z => z.BookingId.ToString()).ToList()));
            });
            Console.ReadKey();
        }
    }
}
