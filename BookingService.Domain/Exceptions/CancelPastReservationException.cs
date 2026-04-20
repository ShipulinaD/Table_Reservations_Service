using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Domain.Exceptions;

 
/// Исключение: попытка отменить уже прошедшую бронь
public class CancelPastReservationException : DomainException
{
    public DateTime StartTime { get; }

    public CancelPastReservationException(DateTime startTime)
        : base($"Нельзя отменить уже прошедшую бронь (время начала: {startTime})")
    {
        StartTime = startTime;
    }
}
