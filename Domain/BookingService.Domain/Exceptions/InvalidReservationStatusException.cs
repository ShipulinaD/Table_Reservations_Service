using BookingService.ValueObjects;

namespace BookingService.Domain.Exceptions;

public class InvalidReservationStatusException(Reservation reservation, ReservationStatus targetStatus)
    : InvalidOperationException($"The reservation can't be transitioned from {reservation.Status} to {targetStatus} (reservation id = {reservation.Id}).")
{
    public Reservation Reservation => reservation;
    public ReservationStatus TargetStatus => targetStatus;
}
