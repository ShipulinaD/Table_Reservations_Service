using BookingService.ValueObjects.Base;
using BookingService.ValueObjects.Validators;

namespace BookingService.ValueObjects;

/// <summary>
/// Represents the reservation's time range.
/// </summary>
/// <param name="startTime">The start time of the reservation.</param>
/// <param name="endTime">The end time of the reservation.</param>
public class TimeRange(DateTime startTime, DateTime endTime)
    : ValueObject<(DateTime StartTime, DateTime EndTime)>(new TimeRangeValidator(), (startTime, endTime))
{
    public DateTime StartTime => Value.StartTime;
    public DateTime EndTime => Value.EndTime;

    /// <summary>
    /// Checks if this time range overlaps with another (half-open intervals).
    /// </summary>
    public bool Overlaps(TimeRange other)
    {
        if (other == null) return false;
        return StartTime < other.EndTime && other.StartTime < EndTime;
    }
}
