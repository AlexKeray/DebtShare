namespace DebtShare.API.DTOs
{
    public class AuthenticationTokensDTO
    {
        public string AccessToken { get; set; } = null!;
        public string RefreshToken { get; set; } = null!;
    }
}
