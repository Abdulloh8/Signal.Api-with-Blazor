using Context;
using Microsoft.EntityFrameworkCore;
using Signal.Api.Entity;
using Signal.Api.Models;

namespace Signal.Api.Services;

public class UserServices
{
    private readonly AppDbContext _context;
    private const string UserIdCookieKey = "user_id";

    public UserServices(AppDbContext context)
    {
        _context = context;
    }
    public void Register(UserDto dto, HttpContext httpContext)
    {

        var user = new User2()
        {
            Name = dto.Name,
            Id = Guid.NewGuid().ToString(),
            PhoneNumber = dto.PhoneNumber,
            Email = dto.Email,
            GrupId = dto.GrupId
        };
        _context.Users2.Add(user);
        _context.SaveChangesAsync();
        httpContext.Response.Cookies.Append(UserIdCookieKey, user.Id);
    }

}
