using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BookingService.ValueObject.Base;

namespace BookingService.ValueObject.Validators;

/// Валидатор для количества мест
public class SeatsValidator : IValidator<int>
{
    public void Validate(int seats) 
    {
        if (seats < 1)
            throw new ArgumentException("Количество мест должно быть не менее 1");

        if (seats > 20)
            throw new ArgumentException("Количество мест не должно превышать 20");
    }
}
