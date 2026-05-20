namespace BookingService.Domain.Exceptions;

public class AnotherUserCancelReservationException(Reservation reservation, User user)
    : InvalidOperationException($"The user {user.Name} can't cancel the reservation owned by the user {reservation.User.Name} (reservation id = {reservation.Id}).")
{
    public Reservation Reservation => reservation;
    public User User => user;
}
