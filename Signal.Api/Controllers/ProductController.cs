using Context;
using Entity;
using Models;
using Pro.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Pro.Controllers;

[Route("/api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly AppDbContext _context;
    public ProductController(AppDbContext context)
    {
        _context = context;
    }
    [HttpGet]
    public async Task<List<Product>> GetList()
    {
        return await _context.Products.ToListAsync();
    }
    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var produsct = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
        if (produsct == null)
        {
            return NotFound();
        }
        
        return Ok(produsct);
    }
    [HttpPost]
    public async Task<IActionResult> Create([FromForm] ProductDto productDto)
    {
        if (!await _context.Categories
                .AnyAsync(c => c.Id == productDto.CategoryId))
        {
            return NotFound();
        }

        var product = new Product()
        {
            Name = productDto.Name,
            Price = productDto.Price,
            CategoryId = productDto.CategoryId,
            Photo_url = await FileHelper.SaveProductFile(productDto.Photo_url)
        };

        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        return Ok(product);
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var produsct = await _context.Products.FirstOrDefaultAsync(s => s.Id == id);
        if(produsct == null)
        {
            return NotFound();
        }
        _context.Products.Remove(produsct);
        await _context.SaveChangesAsync();
        return Ok(produsct);
    }
    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromForm] ProductDto productDto)
    {
        var product = await _context.Products
            .FirstOrDefaultAsync(c => c.Id == id);

        if (product == null)
        {
            return NotFound();
        }

        product.Name = productDto.Name;
        product.Price = productDto.Price;
        await _context.SaveChangesAsync();

        return Ok(product);
    }
}
