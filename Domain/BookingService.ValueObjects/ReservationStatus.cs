namespace BookingService.ValueObjects;

/// <summary>
/// Represents the reservation status.
/// Stored in the database as a string (see ERD: reservation_status).
/// </summary>
public enum ReservationStatus
{
    Pending = 0,
    Reserved = 1,
    Cancelled = 2
}
