// Service/AuthService.cs
using BCrypt.Net;
using CrmService.Interfaces;
using CrmService.Models;
using CrmService.PostgreDb;
using Microsoft.EntityFrameworkCore;

namespace CrmService.Service;

public class AuthService : IAuthService
{
    private readonly AppDbContext _db;
    private readonly SessionState _session;

    public AuthService(AppDbContext db, SessionState session)
    {
        _db = db; _session = session;
    }

    public async Task<(bool ok, string err)> RegisterAsync(string email, string? phone, string pass)
    {
        if (await _db.Users.AnyAsync(u => u.Email == email))
            return (false, "Пользователь уже существует");

        var user = new UserAccount
        {
            Id = Guid.NewGuid(),
            Email = email,
            Phone = phone,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(pass)
        };

        _db.Users.Add(user);
        await _db.SaveChangesAsync();

        _session.SignIn(user.Id, user.Email);
        return (true, "");
    }

    public async Task<(bool ok, string err)> LoginAsync(string login, string pass)
    {
        var user = login.Contains('@')
                 ? await _db.Users.FirstOrDefaultAsync(u => u.Email == login)
                 : await _db.Users.FirstOrDefaultAsync(u => u.Phone == login);

        if (user is null) return (false, "Пользователь не найден");
        if (!BCrypt.Net.BCrypt.Verify(pass, user.PasswordHash))
            return (false, "Неверный пароль");

        _session.SignIn(user.Id, user.Email);
        return (true, "");
    }
}
