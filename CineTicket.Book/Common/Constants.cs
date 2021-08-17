namespace CineTicket.Core.Common
{
    public class Constants
    {
        public const byte MINIMUM_NUMBER = 0;
        public const byte MAXIMUM_ROW_NUMBER = 99;
        public const byte MAXIMUM_SEAT_NUMBER = 49;
        public const byte MAXIMUM_SEATS = 5;

        public const string SUCCESS_MESSAGE = "Successfully booked.";
        public const string SINGLE_GAP_MESSAGE = "Bookings not possible with single seat gap.";
        public const string ALREADY_BOOKED_MESSAGE = "Selected seats were already booked.";

        public static string EXCEPTION_STACK_TRACE = "Exception Stack trace : ";

        public const string FIRST_SEAT_ROW_LIMIT_MESSAGE = "Invalid First Seat Row Number. Permitted Range 1-100.";
        public const string LAST_SEAT_ROW_LIMIT_MESSAGE = "Invalid Last Seat Row Number. Permitted Range 1-100.";
        public const string FIRST_SEAT_LIMIT_MESSAGE = "Invalid First Seat Number. Permitted Range 1-50.";
        public const string LAST_SEAT_LIMIT_MESSAGE = "Invalid Last Seat Number. Permitted Range 1-50.";
        public const string INVALID_SEAT_NUMBER_MESSAGE = "Invalid first/last seat numbers. Last seat number should be greateer than first seat number.";
        public const string MAX_SEAT_MESSAGE = "Total number of seats exceeds 5.";
        public const string DIFF_ROW_BOOKING_MESSAGE = "Seats are not in the same row.";
    }
}
