using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Domain.Exceptions;


/// Исключение: попытка подтвердить отменённую бронь
public class ConfirmCancelledReservationException : DomainException
{
    public Guid ReservationId { get; }

    public ConfirmCancelledReservationException(Guid reservationId)
        : base($"Нельзя подтвердить отменённую бронь (ID: {reservationId})")
    {
        ReservationId = reservationId;
    }
}
