using BookingService.Domain.Repositories.Abstractions.Base;

namespace BookingService.Domain.Repositories.Abstractions;

public interface IUserRepository : IRepository<User, Guid>
{
    // Так как номер телефона уникален
    Task<User?> GetUserByPhoneAsync(string phone, CancellationToken cancellationToken);
}
