using BookingService.ValueObjects.Base;
using BookingService.ValueObjects.Validators;

namespace BookingService.ValueObjects;

/// <summary>
/// Represents type of the entity's name.
/// </summary>
/// <param name="name">The name of the entity.</param>
public class Name(string name) : ValueObject<string>(new NameValidator(), name);
