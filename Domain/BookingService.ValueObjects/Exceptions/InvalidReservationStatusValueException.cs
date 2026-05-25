namespace BookingService.ValueObjects.Exceptions;

public class InvalidReservationStatusValueException(string status)
: FormatException($"The reservation status value \"{status}\" is not valid.")
{
    public string Status => status;
}
