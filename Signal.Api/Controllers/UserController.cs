using Context;
using Entity;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Signal.Api.Entity;
using Signal.Api.Models;

namespace Signal.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[EnableCors("any")]
public class UserController : ControllerBase
{
    private readonly AppDbContext _context;
    public UserController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<List<User>> GetList()
    {
        List<User> users = await _context.Users.ToListAsync();

        return users;
    }
    [HttpPost]
    public async Task<IActionResult> Create(UserDto dto)
    {
        if (dto.GrupId != null
            && !await _context.Users
                .AnyAsync(c => c.Id == dto.GrupId))
        {
            return NotFound();
        }
        var user = new User()
        {
            Name = dto.Name,
            Id = new Guid(),
            PhoneNumber = dto.PhoneNumber,
            Email = dto.Email,
            GrupId = dto.GrupId
        };
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return Ok();
    }

}
