namespace BookingService.Domain.Exceptions;

public class TableNotBelongCafeException(Cafe cafe, Table table)
    : InvalidOperationException($"The table {table.Number} is not in the cafe's table sequence (cafe {cafe.Name}, table id = {table.Id}).")
{
    public Cafe Cafe => cafe;
    public Table Table => table;
}
