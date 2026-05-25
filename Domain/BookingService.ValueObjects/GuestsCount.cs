using BookingService.ValueObjects.Base;
using BookingService.ValueObjects.Validators;

namespace BookingService.ValueObjects;

/// <summary>
/// Represents type of the reservation's guests count.
/// </summary>
/// <param name="count">The number of guests for the reservation.</param>
public class GuestsCount(int count) : ValueObject<int>(new GuestsCountValidator(), count);
