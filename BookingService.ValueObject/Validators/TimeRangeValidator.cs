using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BookingService.ValueObject.Base;

namespace BookingService.ValueObject.Validators;

/// Валидатор для временного интервала (StartTime, EndTime)
public class TimeRangeValidator : IValidator<(DateTime StartTime, DateTime EndTime)> 
{
    public void Validate((DateTime StartTime, DateTime EndTime) range)
    {
        // start_time должен быть меньше end_time
        if (range.StartTime >= range.EndTime)
            throw new ArgumentException("Время начала должно быть меньше времени окончания");

        // Нельзя создавать бронирование в прошлом
        if (range.StartTime < DateTime.UtcNow)
            throw new ArgumentException("Нельзя создать бронирование в прошлом");

        // Дополнительно: бронирование не может быть длиннее 4 часов (опционально)
        var duration = range.EndTime - range.StartTime;
        if (duration.TotalHours > 4)
            throw new ArgumentException("Бронирование не может быть длиннее 4 часов");

        if (duration.TotalMinutes < 30)
            throw new ArgumentException("Бронирование должно быть не короче 30 минут");
    }
}
