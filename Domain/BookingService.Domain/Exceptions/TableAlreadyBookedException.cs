namespace BookingService.Domain.Exceptions;

public class TableAlreadyBookedException(Table table, Reservation reservation)
    : InvalidOperationException($"The table {table.Number} is already booked for the period from {reservation.TimeRange.StartTime} to {reservation.TimeRange.EndTime} (table id = {table.Id}).")
{
    public Table Table => table;
    public Reservation Reservation => reservation;
}
