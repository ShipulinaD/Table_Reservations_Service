using BookingService.ValueObjects.Base;
using BookingService.ValueObjects.Exceptions;

namespace BookingService.ValueObjects.Validators;

/// <summary>
/// Определяет метод, реализующий проверку количества мест.
/// </summary>
public class SeatsValidator : IValidator<int>
{
    /// <summary>
    /// Максимальное допустимое значение количества мест
    /// </summary>
    public static int MAX_VALUE => 20;

    /// <summary>
    /// Минимальное допустимое значение количества мест
    /// </summary>
    public static int MIN_VALUE => 1;

    /// <summary>
    /// Проверяет количество мест, чтобы убедиться, что оно находится в допустимом диапазоне.
    /// </summary>
    /// <param name="value">Количество мест.</param>
    /// <exception cref="ArgumentOutOfRangeIntException"></exception>
    public void Validate(int value)
    {
        // Проверка: количество мест должно быть в диапазоне от MIN_VALUE до MAX_VALUE
        if (value < MIN_VALUE || value > MAX_VALUE)
            throw new ArgumentOutOfRangeIntException(nameof(value), value, MIN_VALUE, MAX_VALUE);
    }
}