namespace CrmService.Interfaces
{
    public interface IAuthService
    {
        Task<(bool ok, string err)> RegisterAsync(string email, string? phone, string pass);
        Task<(bool ok, string err)> LoginAsync(string login, string pass);
    }
}
