namespace BookingService.Domain.Exceptions;

public class GuestsExceedSeatsException(Table table, int guestsCount)
    : InvalidOperationException($"The guests count {guestsCount} can't exceed the table's seats count {table.Seats.Value} (table id = {table.Id}).")
{
    public Table Table => table;
    public int GuestsCount => guestsCount;
}
