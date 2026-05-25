using BookingService.Domain.Repositories.Abstractions.Base;

namespace BookingService.Domain.Repositories.Abstractions;

public interface ICafeRepository : IRepository<Cafe, Guid>
{
    // Поиск кафе по части названия (use-case "Выбрать кафе")
    Task<IEnumerable<Cafe>> SearchByNameAsync(string name, CancellationToken cancellationToken);
}
