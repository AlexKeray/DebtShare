using DebtShare.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace DebtShare.Data.Repositories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly ApplicationDbContext _context;

        public RefreshTokenRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(RefreshToken token)
        {
            _context.RefreshTokens.Add(token);
            await _context.SaveChangesAsync();
        }

        public async Task<RefreshToken?> GetByValueAsync(string tokenValue)
        {
            return await _context.RefreshTokens.FirstOrDefaultAsync(i => i.Value == tokenValue);
        }

        public async Task RevokeAsync(string tokenValue)
        {
            var refreshToken = await GetByValueAsync(tokenValue);
            if (refreshToken != null)
            {
                refreshToken.IsRevoked = true;
                refreshToken.RevocationDate = DateTime.UtcNow;
                await _context.SaveChangesAsync();
            }
        }


    }
}
