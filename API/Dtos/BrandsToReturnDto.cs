using System.ComponentModel.DataAnnotations;

namespace API.Dtos;

public class BrandsToReturnDto
{
 public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public bool IsActive { get; set; }

    public string ProductCategory { get; set; } = string.Empty;
}
