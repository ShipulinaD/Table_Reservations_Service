using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Domain.Exceptions;


/// Исключение: некорректное время бронирования
public class InvalidReservationTimeException : DomainException 
{
    public InvalidReservationTimeException(string message) : base(message) { }

    public InvalidReservationTimeException(string message, Exception innerException)
        : base(message, innerException) { }
}
