using Context;
using Entity;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Signal.Api.Entity;
using Signal.Api.Models;
using Signal.Api.Services;

namespace Signal.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[EnableCors("any")]
public class UserController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly UserServices _userServices;
    public UserController(AppDbContext context, UserServices userServices)
    {
        _context = context;
        _userServices = userServices;
    }

    [HttpGet]
    public async Task<List<User2>> GetList()
    {
        List<User2> users = await _context.Users2.ToListAsync();

        return users;
    }
    [HttpPost]
    public async Task<IActionResult> Create([FromForm]UserDto dto)
    {
        _userServices.Register(dto, HttpContext);

        return Ok();
    }

}
