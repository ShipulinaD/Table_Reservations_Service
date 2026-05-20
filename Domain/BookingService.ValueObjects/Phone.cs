using BookingService.ValueObjects.Base;
using BookingService.ValueObjects.Validators;

namespace BookingService.ValueObjects;

/// <summary>
/// Represents type of the entity's phone number.
/// </summary>
/// <param name="phone">The phone number of the entity.</param>
public class Phone(string phone) : ValueObject<string>(new PhoneValidator(), phone);
