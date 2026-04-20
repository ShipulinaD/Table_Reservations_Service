using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.ValueObject.Exceptions;


/// Исключение для ошибок валидации Value Object'ов
public class ValueObjectValidationException : Exception
{
    public ValueObjectValidationException(string message) : base(message) { }

    public ValueObjectValidationException(string message, Exception innerException)
        : base(message, innerException) { }
}