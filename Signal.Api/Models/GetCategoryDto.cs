

using Entity;

namespace Models;

public class GetCategoryDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<GetCategoryDto> Children { get; set; }
    
}
