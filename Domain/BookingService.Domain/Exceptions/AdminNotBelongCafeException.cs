namespace BookingService.Domain.Exceptions;

public class AdminNotBelongCafeException(Cafe cafe, Admin admin)
    : InvalidOperationException($"The admin {admin.Name} is not in the cafe's admin sequence (cafe {cafe.Name}, admin id = {admin.Id}).")
{
    public Cafe Cafe => cafe;
    public Admin Admin => admin;
}
