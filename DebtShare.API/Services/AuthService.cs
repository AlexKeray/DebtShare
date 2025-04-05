using Microsoft.AspNetCore.Identity;
using DebtShare.Data.Models;
using DebtShare.API.InputModels;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DebtShare.Data.Repositories;
using DebtShare.API.DTOs;


namespace DebtShare.API.Services
{
    public class AuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly IRefreshTokenRepository _refreshTokenRepository;

        public AuthService(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration,
            IRefreshTokenRepository refreshTokenRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _refreshTokenRepository = refreshTokenRepository;
        }

        public async Task<IdentityResult> RegisterAsync(RegisterInputModel inputModel)
        {
            var user = new ApplicationUser
            {
                UserName = inputModel.Email,
                Email = inputModel.Email
            };

            return await _userManager.CreateAsync(user, inputModel.Password);
        }

        public async Task<AuthenticationTokensDTO?> LoginAsync(LoginInputModel inputModel)
        {
            var user = await _userManager.FindByEmailAsync(inputModel.Email);
            if (user == null)
                return null;

            var result = await _signInManager.CheckPasswordSignInAsync(user, inputModel.Password, false);
            if (!result.Succeeded)
                return null;

            // Тук вече вземаме двата токена от GenerateTokensAsync:
            var tokens = await GenerateTokensAsync(user);

            return new AuthenticationTokensDTO
            {
                AccessToken = tokens.accessToken,
                RefreshToken = tokens.refreshToken
            };
        }

        public async Task<AuthenticationTokensDTO?> RefreshTokenAsync(string oldRefreshTokenValue)
        {
            var refreshTokenObj = await _refreshTokenRepository.GetByValueAsync(oldRefreshTokenValue);

            if (refreshTokenObj == null)
            {
                return null;
            }

            if (refreshTokenObj.IsRevoked || refreshTokenObj.ExpirationDate < DateTime.UtcNow)
            {
                return null;
            }

            await _refreshTokenRepository.RevokeAsync(oldRefreshTokenValue);

            var user = await _userManager.FindByIdAsync(refreshTokenObj.UserId);
            if (user == null)
            {
                return null;
            }

            var tokens = await GenerateTokensAsync(user);

            return new AuthenticationTokensDTO
            {
                AccessToken = tokens.accessToken,
                RefreshToken = tokens.refreshToken
            };
        }

        public async Task<bool> LogoutAsync(string refreshTokenValue, string userId)
        {
            // Търсим рефреш токена в базата
            var refreshTokenObj = await _refreshTokenRepository.GetByValueAsync(refreshTokenValue);

            if (refreshTokenObj == null)
                return false; // несъществуващ токен

            // Проверяваме дали този токен принадлежи на текущия потребител
            if (refreshTokenObj.UserId != userId)
                return false; // опит за logout на чужд токен

            // Оттегляме/анулираме токена
            await _refreshTokenRepository.RevokeAsync(refreshTokenValue);

            return true;
        }

        private async Task<(string accessToken, string refreshToken)> GenerateTokensAsync(ApplicationUser user)
        {
            var accessTokenTask = GenerateAccessTokenAsync(user);
            var refreshTokenTask = GenerateAndStoreRefreshTokenAsync(user);

            await Task.WhenAll(accessTokenTask, refreshTokenTask);

            return (accessTokenTask.Result, refreshTokenTask.Result);
        }

        private async Task<string> GenerateAccessTokenAsync(ApplicationUser user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                // user.Email! means that the developer ensures the compilator that the Email property wont be null.
                // This is needed because Email of IdentityUser is string?
                // Ef Core will ensure that the Email property wont be null because it is overriden by the model ApplicationUser
                new Claim(JwtRegisteredClaimNames.Email, user.Email!),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var keyString = _configuration["Jwt:Key"];
            if (string.IsNullOrWhiteSpace(keyString))
            {
                throw new InvalidOperationException("JWT key is missing from configuration.");
            }    
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyString));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddMinutes(15);

            var token = new JwtSecurityToken(
                // issuer: is a read-only property of JwtSecurityToken class, which means it doesnt have set method ({ get; })
                // that's why it is the only syntax that allows setting the property using the constructor
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: expires,
                signingCredentials: creds
            );

            var accessToken = await Task.FromResult(new JwtSecurityTokenHandler().WriteToken(token));
            return accessToken;
        }

        private async Task<string> GenerateAndStoreRefreshTokenAsync(ApplicationUser user)
        {
            var refreshTokenValue = Convert.ToBase64String(Guid.NewGuid().ToByteArray());

            var refreshTokenObj = new RefreshToken
            {
                Value = refreshTokenValue,
                UserId = user.Id,
                CreationDate = DateTime.UtcNow,
                ExpirationDate = DateTime.UtcNow.AddDays(7),
                IsRevoked = false
            };

            await _refreshTokenRepository.AddAsync(refreshTokenObj);

            return refreshTokenValue;
        }

        

    }
}
