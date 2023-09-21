using Context;
using Entity;
using Models;

using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Pro.Controllers;

[Route("/api/[controller]")]
[ApiController]
[EnableCors("any")]
public class categoryController : ControllerBase
{
    private readonly AppDbContext _context;
    public categoryController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<List<GetCategoryDto>> GetList()
    {
        var categorys = await _context.Categories
            .Where(s => s.ParentId == null)
            .ToListAsync();

        return await MapTo(categorys);

    }
    private async Task<List<GetCategoryDto>> MapTo(List<Category> categories)
    {
        var categorydto = new List<GetCategoryDto>();
        foreach (var item in categories)
        {
            categorydto.Add(await MapToDto(item));
        }
        return categorydto;
    }
    private async Task<GetCategoryDto> MapToDto(Category category)
    {
        await _context.Entry(category).Collection(h => h.Children).LoadAsync();
        return new GetCategoryDto()
        {
            Id = category.Id,
            Name = category.Name,
            Children = await MapTo(category.Children)
        };
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var category = await _context.Categories
            .FirstOrDefaultAsync(c => c.Id == id);

        if (category == null)
        {
            return NotFound();
        }

        return Ok(category);
    }
    [HttpPost]
    public async Task<IActionResult> Create(CategoryDto dto)
    {
        if (dto.ParentId != null
            && !await _context.Categories
                .AnyAsync(c => c.Id == dto.ParentId))
        {
            return NotFound();
        }
        var category = new Category()
        {
            Name = dto.Name,
            ParentId = dto.ParentId
        };
        _context.Categories.Add(category);
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpDelete("guid")]
    public async Task<IActionResult> Delete( Guid guid)
    {
        var category = await _context.Categories
            .FirstOrDefaultAsync (c => c.Id == guid);
        if (category == null)
        {
            return NotFound();
        }
        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();
        return Ok();
    }
    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> Update(Guid id ,[FromForm] CategoryDto categoryDto)
    {
        var category = await _context.Categories.FirstOrDefaultAsync (c => c.Id == id);
        if (category == null)
        {
            return NotFound();
        }
        category.Name = categoryDto.Name;

        await _context.SaveChangesAsync();
        return Ok(category);
    }
}
