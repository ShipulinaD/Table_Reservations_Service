using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Domain.Exceptions;


/// Исключение: стол уже забронирован на указанное время
public class TableAlreadyBookedException : DomainException
{
    public Guid TableId { get; }
    public DateTime StartTime { get; }
    public DateTime EndTime { get; }

    public TableAlreadyBookedException(Guid tableId, DateTime startTime, DateTime endTime)
        : base($"Стол с ID {tableId} уже забронирован на период с {startTime} по {endTime}")
    {
        TableId = tableId;
        StartTime = startTime;
        EndTime = endTime;
    }
}