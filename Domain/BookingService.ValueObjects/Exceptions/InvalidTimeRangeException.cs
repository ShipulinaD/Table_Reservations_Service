namespace BookingService.ValueObjects.Exceptions;

public class InvalidTimeRangeException(DateTime startTime, DateTime endTime, string reason)
: ArgumentException($"Invalid time range from {startTime} to {endTime}: {reason}")
{
    public DateTime StartTime => startTime;
    public DateTime EndTime => endTime;
    public string Reason => reason;
}
