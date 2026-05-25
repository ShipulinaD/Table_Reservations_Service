using BookingService.Domain.Repositories.Abstractions.Base;

namespace BookingService.Domain.Repositories.Abstractions;

public interface IAdminRepository : IRepository<Admin, Guid>
{
    // Так как номер телефона уникален
    Task<Admin?> GetAdminByPhoneAsync(string phone, CancellationToken cancellationToken);
}