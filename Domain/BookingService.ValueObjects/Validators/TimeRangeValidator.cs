using BookingService.ValueObjects.Base;
using BookingService.ValueObjects.Exceptions;

namespace BookingService.ValueObjects.Validators;

/// <summary>
/// Определяет метод, реализующий проверку временного диапазона (StartTime, EndTime).
/// </summary>
public class TimeRangeValidator : IValidator<(DateTime StartTime, DateTime EndTime)>
{
    /// <summary>
    /// Максимальная длительность бронирования в часах
    /// </summary>
    public static int MAX_DURATION_HOURS => 3;

    /// <summary>
    /// Минимальная длительность бронирования в минутах
    /// </summary>
    public static int MIN_DURATION_MINUTES => 30;

    /// <summary>
    /// Проверяет временной диапазон, чтобы убедиться, что он является допустимым интервалом бронирования.
    /// </summary>
    /// <param name="value">Кортеж, содержащий время начала и окончания.</param>
    /// <exception cref="InvalidTimeRangeException"></exception>
    public void Validate((DateTime StartTime, DateTime EndTime) value)
    {
        // Проверка: время начала должно быть меньше времени окончания
        if (value.StartTime >= value.EndTime)
            throw new InvalidTimeRangeException(value.StartTime, value.EndTime,
                "Start time must be less than end time.");

        // Проверка: нельзя создавать бронирование в прошлом
        if (value.StartTime < DateTime.UtcNow)
            throw new InvalidTimeRangeException(value.StartTime, value.EndTime,
                "Cannot create reservation in the past.");

        // Вычисление длительности бронирования
        var duration = value.EndTime - value.StartTime;

        // Проверка: минимальная длительность бронирования
        if (duration.TotalMinutes < MIN_DURATION_MINUTES)
            throw new InvalidTimeRangeException(value.StartTime, value.EndTime,
                $"Reservation must be at least {MIN_DURATION_MINUTES} minutes long.");

        // Проверка: максимальная длительность бронирования
        if (duration.TotalHours > MAX_DURATION_HOURS)
            throw new InvalidTimeRangeException(value.StartTime, value.EndTime,
                $"Reservation must not be longer than {MAX_DURATION_HOURS} hours.");
    }
}