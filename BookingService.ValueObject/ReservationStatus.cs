using System;
using System.Collections.Generic;
using BookingService.ValueObject.Base;

namespace BookingService.ValueObject;


/// Value Object: Статус бронирования
public class ReservationStatus : Base.ValueObject  
{
    public string Value { get; private set; }

    // Предопределённые статусы 
    public static readonly ReservationStatus Reserved = new("reserved");
    public static readonly ReservationStatus Cancelled = new("cancelled");

    private ReservationStatus(string value)
    {
        Value = value;
    }

    public static ReservationStatus FromString(string status)
    {
        return status?.ToLower() switch
        {
            "reserved" => Reserved,
            "cancelled" => Cancelled,
            _ => throw new ArgumentException($"Недопустимый статус: {status}")
        };
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString() => Value;
}