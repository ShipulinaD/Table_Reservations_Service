namespace BookingService.Domain.Exceptions;

public class CancelPastReservationException(Reservation reservation)
    : InvalidOperationException($"The reservation can't be cancelled because its start time {reservation.TimeRange.StartTime} is in the past (reservation id = {reservation.Id}).")
{
    public Reservation Reservation => reservation;
}
