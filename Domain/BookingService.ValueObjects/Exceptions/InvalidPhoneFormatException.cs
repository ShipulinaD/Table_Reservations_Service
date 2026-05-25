namespace BookingService.ValueObjects.Exceptions;

public class InvalidPhoneFormatException(string paramName, string value)
: FormatException($"The \"{paramName}\" value \"{value}\" is not a valid phone number format.")
{
    public string Value => value;
}
