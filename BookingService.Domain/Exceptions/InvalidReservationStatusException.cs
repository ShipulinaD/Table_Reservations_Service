using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Domain.Exceptions;


/// Исключение: недопустимая операция со статусом бронирования
public class InvalidReservationStatusException : DomainException
{
    public InvalidReservationStatusException(string message) : base(message) { }
}
