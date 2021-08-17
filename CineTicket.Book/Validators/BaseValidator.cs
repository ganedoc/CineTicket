using CineTicket.Core.Entities;
using FluentValidation;
using static CineTicket.Core.Common.Constants;

namespace CineTicket.Core.Validators
{
    public class BaseValidator : AbstractValidator<BookingRequest>
    {
        public BaseValidator()
        {
            RuleFor(request => request.FirstSeatRowNumber).LessThanOrEqualTo(MAXIMUM_ROW_NUMBER).WithMessage(FIRST_SEAT_ROW_LIMIT_MESSAGE).GreaterThanOrEqualTo(MINIMUM_NUMBER).WithMessage(FIRST_SEAT_ROW_LIMIT_MESSAGE).DependentRules(() =>
            RuleFor(request => request.LastSeatRowNumber).LessThanOrEqualTo(MAXIMUM_ROW_NUMBER).WithMessage(LAST_SEAT_ROW_LIMIT_MESSAGE).GreaterThanOrEqualTo(MINIMUM_NUMBER).WithMessage(LAST_SEAT_ROW_LIMIT_MESSAGE)).DependentRules(() =>
            RuleFor(request => request.FirstSeatNumber).LessThanOrEqualTo(MAXIMUM_SEAT_NUMBER).WithMessage(FIRST_SEAT_LIMIT_MESSAGE).GreaterThanOrEqualTo(MINIMUM_NUMBER).WithMessage(FIRST_SEAT_LIMIT_MESSAGE)).DependentRules(() =>
            RuleFor(request => request.LastSeatNumber).LessThanOrEqualTo(MAXIMUM_SEAT_NUMBER).WithMessage(LAST_SEAT_LIMIT_MESSAGE).GreaterThanOrEqualTo(MINIMUM_NUMBER).WithMessage(LAST_SEAT_LIMIT_MESSAGE)).DependentRules(() =>
            RuleFor(request => request.FirstSeatNumber).Must((args, firstSeatNumber) => args.LastSeatNumber - firstSeatNumber >= 0).WithMessage(INVALID_SEAT_NUMBER_MESSAGE)).DependentRules(() =>
            RuleFor(request => request.FirstSeatNumber).Must((args, firstSeatNumber) => args.LastSeatNumber - firstSeatNumber < 5).WithMessage(MAX_SEAT_MESSAGE)).DependentRules(() =>
            RuleFor(request => request.FirstSeatRowNumber).Must((args, firstSeatRowNumber) => firstSeatRowNumber.Equals(args.LastSeatRowNumber)).WithMessage(DIFF_ROW_BOOKING_MESSAGE));
        }
    }
}
