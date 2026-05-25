using BookingService.ValueObjects.Base;
using BookingService.ValueObjects.Validators;

namespace BookingService.ValueObjects;

/// <summary>
/// Represents type of the table's seats count.
/// </summary>
/// <param name="seats">The number of seats at the table.</param>
public class Seats(int seats) : ValueObject<int>(new SeatsValidator(), seats);
