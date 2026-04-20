using BookingService.Domain.Exceptions;
using BookingService.ValueObject;
using BookingService.ValueObject.Validators;

namespace BookingService.Domain.Entities;


/// Сущность "Бронирование" с бизнес-логикой
public class Reservation
{
    private static readonly TimeRangeValidator _timeRangeValidator = new();
    private static readonly GuestsCountValidator _guestsCountValidator = new();

    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public Guid TableId { get; private set; }
    public DateTime StartTime { get; private set; }
    public DateTime EndTime { get; private set; }
    public int GuestsCount { get; private set; }
    public ReservationStatus Status { get; private set; }
    public Guid? ConfirmedBy { get; private set; }

    // Навигационные свойства
    public virtual User User { get; private set; }
    public virtual Table Table { get; private set; }
    public virtual Admin ConfirmedByAdmin { get; private set; }

    private Reservation() { }

    public Reservation(
        Guid id,
        Guid userId,
        Guid tableId,
        DateTime startTime,
        DateTime endTime,
        int guestsCount,
        Table table)
    {
        // Валидация временного интервала
        try
        {
            _timeRangeValidator.Validate((startTime, endTime));
        }
        catch (ArgumentException ex)
        {
            throw new InvalidReservationTimeException(ex.Message, ex);
        }

        // Валидация количества гостей
        try
        {
            _guestsCountValidator.Validate(guestsCount);
        }
        catch (ArgumentException ex)
        {
            throw new InvalidReservationTimeException($"Некорректное количество гостей: {ex.Message}", ex);
        }

        // Проверка: guests_count не должен превышать seats у стола
        if (guestsCount > table.Seats)
            throw new GuestsExceedSeatsException(guestsCount, table.Seats);

        Id = id;
        UserId = userId;
        TableId = table.Id;
        StartTime = startTime;
        EndTime = endTime;
        GuestsCount = guestsCount;
        Status = ReservationStatus.Reserved;
        ConfirmedBy = null;
    }


    /// Подтверждение брони администратором
    public void Confirm(Guid adminId)
    {
        // Нельзя подтвердить отменённую бронь
        if (Status == ReservationStatus.Cancelled)
            throw new ConfirmCancelledReservationException(Id);

        // Нельзя подтвердить уже подтверждённую бронь
        if (ConfirmedBy.HasValue)
            throw new InvalidReservationStatusException("Бронь уже подтверждена");

        ConfirmedBy = adminId;
    }

   
    /// Отмена брони
    public void Cancel()
    {
        // Нельзя отменить уже прошедшую бронь
        if (StartTime < DateTime.UtcNow)
            throw new CancelPastReservationException(StartTime);

        // Нельзя отменить уже отменённую бронь
        if (Status == ReservationStatus.Cancelled)
            throw new InvalidReservationStatusException("Бронь уже отменена");

        Status = ReservationStatus.Cancelled;
    }


    /// Проверка пересечения временных интервалов
    public bool OverlapsWith(DateTime startTime, DateTime endTime)
    {
        return StartTime < endTime && startTime < EndTime;
    }
}