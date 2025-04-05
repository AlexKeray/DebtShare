using DebtShare.Data.Models;

namespace DebtShare.Data.Repositories
{
    public interface IRefreshTokenRepository
    {
        Task AddAsync(RefreshToken token);
        Task<RefreshToken?> GetByValueAsync(string token);
        Task RevokeAsync(string token);
    }
}
