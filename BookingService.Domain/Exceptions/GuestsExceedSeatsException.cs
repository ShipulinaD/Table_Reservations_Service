using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Domain.Exceptions;


/// Исключение: количество гостей превышает количество мест за столом
public class GuestsExceedSeatsException : DomainException
{
    public int GuestsCount { get; }
    public int Seats { get; }

    public GuestsExceedSeatsException(int guestsCount, int seats)
        : base($"Количество гостей ({guestsCount}) не может превышать количество мест за столом ({seats})")
    {
        GuestsCount = guestsCount;
        Seats = seats;
    }
}
