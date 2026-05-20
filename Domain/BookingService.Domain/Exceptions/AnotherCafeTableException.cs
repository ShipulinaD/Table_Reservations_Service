namespace BookingService.Domain.Exceptions;

public class AnotherCafeTableException(Cafe cafe, Table table)
    : InvalidOperationException($"The table {table.Number} doesn't belong to the cafe {cafe.Name} (table id = {table.Id}, cafe id = {cafe.Id}).")
{
    public Cafe Cafe => cafe;
    public Table Table => table;
}
