using System;
using System.Collections.Generic;
using System.Linq;
using BookingService.ValueObject;
using BookingService.ValueObject.Validators;

namespace BookingService.Domain.Entities;


/// Сущность "Стол" с бизнес-логикой
public class Table
{
    private static readonly SeatsValidator _seatsValidator = new();

    public Guid Id { get; private set; }
    public Guid CafeId { get; private set; }
    public int Number { get; private set; }
    public int Seats { get; private set; }

    // Навигационные свойства
    public virtual Cafe Cafe { get; private set; }
    public virtual ICollection<Reservation> Reservations { get; private set; }

    private Table() { }

    public Table(Guid id, Guid cafeId, int number, int seats)
    {
        _seatsValidator.Validate(seats);

        Id = id;
        CafeId = cafeId;
        Number = number;
        Seats = seats;
        Reservations = new List<Reservation>();
    }

    
    /// Проверка, свободен ли стол в указанный временной интервал
    public bool IsFreeAt(DateTime startTime, DateTime endTime, IEnumerable<Reservation> allReservations)
    {
        return !allReservations.Any(r =>
            r.TableId == Id &&
            r.Status != ReservationStatus.Cancelled && // Отменённые брони не считаются
            r.OverlapsWith(startTime, endTime));
    }
}