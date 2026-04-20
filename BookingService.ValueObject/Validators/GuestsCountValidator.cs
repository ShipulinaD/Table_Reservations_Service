using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BookingService.ValueObject.Base;

namespace BookingService.ValueObject.Validators;


/// Валидатор для количества гостей
public class GuestsCountValidator : IValidator<int>
{
    public void Validate(int guestsCount)
    {
        if (guestsCount < 1)
            throw new ArgumentException("Количество гостей должно быть не менее 1");

        if (guestsCount > 50)
            throw new ArgumentException("Количество гостей не должно превышать 50");
    }
}