using BookingService.Domain.Repositories.Abstractions.Base;

namespace BookingService.Domain.Repositories.Abstractions;

public interface IReservationRepository : IRepository<Reservation, Guid>
{
    // "Мои брони" (Пользователь)
    Task<IEnumerable<Reservation>> GetReservationsByUserIdAsync(Guid userId, CancellationToken cancellationToken);

    // Все брони столика — для проверки пересечений
    Task<IEnumerable<Reservation>> GetReservationsByTableIdAsync(Guid tableId, CancellationToken cancellationToken);

    // "Просмотреть брони" (Администратор)
    Task<IEnumerable<Reservation>> GetReservationsByCafeIdAsync(Guid cafeId, CancellationToken cancellationToken);

    // Брони столика, пересекающиеся с интервалом
    Task<IEnumerable<Reservation>> GetOverlappingReservationsAsync(
        Guid tableId,
        DateTime startTime,
        DateTime endTime,
        CancellationToken cancellationToken);
}
