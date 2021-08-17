using CineTicket.Core.Entities;
using CineTicket.Core.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using static CineTicket.Core.Common.Constants;

namespace CineTicket.Core.Serrvices
{
    public class Booker : IBooker
    {
        public BookingResponse BulkProcess(List<BookingRequest> bookingRequests)
        {
            var successBookings = new List<Booking>();
            var failedBookings = new List<Booking>();
            var NoOfBookingProcessed = 0;
            var orderedBookingRequests = bookingRequests.OrderBy(x => x.BookingId).ToArray();//First come first serve
            try
            {
                IEnumerable<BookingRequest> GetSuccessBookingRequests(byte firstSeatRowNumber)
                     => bookingRequests.Join(successBookings, br => br.BookingId, sb => sb.BookingId, (requests, success) => new BookingRequest
                     {
                         BookingId = success.BookingId,
                         FirstSeatRowNumber = requests.FirstSeatRowNumber,
                         FirstSeatNumber = requests.FirstSeatNumber,
                         LastSeatRowNumber = requests.LastSeatRowNumber,
                         LastSeatNumber = requests.LastSeatNumber
                     }).Where(x => x.FirstSeatRowNumber.Equals(firstSeatRowNumber));

                bool IsSeatsAvailable(BookingRequest bookingRequest)
                    => !GetSuccessBookingRequests(bookingRequest.FirstSeatRowNumber).Any(x => GetEnumRange(x).Intersect(GetEnumRange(bookingRequest)).Any());

                bool IsSingleSeatGap(BookingRequest bookingRequest)
                {
                    var successBookingRequests = GetSuccessBookingRequests(bookingRequest.FirstSeatRowNumber);
                    var oneSideBookingRequests = successBookingRequests.Where(y => y.LastSeatNumber < bookingRequest.FirstSeatNumber);
                    var otherSideBookingRequests = successBookingRequests.Where(y => y.FirstSeatNumber > bookingRequest.LastSeatNumber);
                    return
                       (oneSideBookingRequests.Any(x => (bookingRequest.FirstSeatNumber - x.LastSeatNumber).Equals(2)) 
                        && !oneSideBookingRequests.Any(x => (bookingRequest.FirstSeatNumber - x.LastSeatNumber).Equals(1))) || // One side of single gap validation
                        (otherSideBookingRequests.Any(x => (x.FirstSeatNumber - bookingRequest.LastSeatNumber).Equals(2)) 
                        && !otherSideBookingRequests.Any(x => (x.FirstSeatNumber - bookingRequest.LastSeatNumber).Equals(1)));// Other side of single gap validation
                }

                var basicValidator = new BaseValidator();
                Array.ForEach(orderedBookingRequests, request =>
                {
                    var results = basicValidator.Validate(request);
                    if (results.IsValid)
                    {
                        if (IsSeatsAvailable(request))
                        {
                            if (!IsSingleSeatGap(request))
                                successBookings.Add(new Booking() { BookingId = request.BookingId, ReasonPhrase = SUCCESS_MESSAGE });
                            else//Seats are booked with signle seat gap
                                failedBookings.Add(new Booking() { BookingId = request.BookingId, ReasonPhrase = SINGLE_GAP_MESSAGE });
                        }
                        else//Seats are already booked with FCFS
                            failedBookings.Add(new Booking() { BookingId = request.BookingId, ReasonPhrase = ALREADY_BOOKED_MESSAGE });
                    }
                    else //Failed at Base validator
                        failedBookings.Add(new Booking() { BookingId = request.BookingId, ReasonPhrase = results?.Errors?.FirstOrDefault()?.ErrorMessage });
                    NoOfBookingProcessed += 1;
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(EXCEPTION_STACK_TRACE + ex.ToString());
            }
            return new BookingResponse { FailedBookings = failedBookings, ProcessedCount = NoOfBookingProcessed };
        }

        private IEnumerable<int> GetEnumRange(BookingRequest bookingRequest)
            => Enumerable.Range(bookingRequest.FirstSeatNumber, bookingRequest.LastSeatNumber - bookingRequest.FirstSeatNumber + 1);
    }
}
