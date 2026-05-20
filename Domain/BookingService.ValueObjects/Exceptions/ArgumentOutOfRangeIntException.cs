namespace BookingService.ValueObjects.Exceptions;

public class ArgumentOutOfRangeIntException(string paramName, int value, int min, int max)
: ArgumentOutOfRangeException(paramName, $"The \"{paramName}\" value {value} must be in range [{min}; {max}].")
{
    public int Value => value;
    public int Min => min;
    public int Max => max;
}
