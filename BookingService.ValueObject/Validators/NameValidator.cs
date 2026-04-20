using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BookingService.ValueObject.Base;

namespace BookingService.ValueObject.Validators;

/// Валидатор для имени
public class NameValidator : IValidator<string>
{
    public void Validate(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Имя не может быть пустым");

        if (name.Length < 2)
            throw new ArgumentException("Имя должно содержать минимум 2 символа");

        if (name.Length > 100)
            throw new ArgumentException("Имя не должно превышать 100 символов");
    }
}
