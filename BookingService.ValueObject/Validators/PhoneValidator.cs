using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BookingService.ValueObject.Base;

namespace BookingService.ValueObject.Validators;

/// Валидатор для номера телефона
public class PhoneValidator : IValidator<string>
{
    public void Validate(string phone)
    {
        if (string.IsNullOrWhiteSpace(phone))
            throw new ArgumentException("Номер телефона не может быть пустым");

        // Простая валидация: телефон должен содержать только цифры, пробелы, скобки и знак +
        var cleaned = phone.Replace(" ", "").Replace("-", "").Replace("(", "").Replace(")", "");

        if (!cleaned.All(c => char.IsDigit(c) || c == '+'))
            throw new ArgumentException("Номер телефона содержит недопустимые символы");

        if (cleaned.Length < 10)
            throw new ArgumentException("Номер телефона слишком короткий");
    }
}