using BookingService.Domain.Repositories.Abstractions.Base;

namespace BookingService.Domain.Repositories.Abstractions;

public interface ITableRepository : IRepository<Table, Guid>
{
    // Получить все столики конкретного кафе
    Task<IEnumerable<Table>> GetTablesByCafeIdAsync(Guid cafeId, CancellationToken cancellationToken);
}
