using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.ValueObject.Base;


/// Интерфейс валидатора для Value Object'ов
public interface IValidator<in T>
{
    void Validate(T value);
}
