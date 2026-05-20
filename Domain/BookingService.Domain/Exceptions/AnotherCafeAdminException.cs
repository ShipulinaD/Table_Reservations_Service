namespace BookingService.Domain.Exceptions;

public class AnotherCafeAdminException(Cafe cafe, Admin admin)
    : InvalidOperationException($"The admin {admin.Name} doesn't belong to the cafe {cafe.Name} (admin id = {admin.Id}, cafe id = {cafe.Id}).")
{
    public Cafe Cafe => cafe;
    public Admin Admin => admin;
}
