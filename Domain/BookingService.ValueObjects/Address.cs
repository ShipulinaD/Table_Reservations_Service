using BookingService.ValueObjects.Base;
using BookingService.ValueObjects.Validators;

namespace BookingService.ValueObjects;

/// <summary>
/// Represents type of the entity's address.
/// </summary>
/// <param name="address">The address of the entity.</param>
public class Address(string address) : ValueObject<string>(new AddressValidator(), address);
