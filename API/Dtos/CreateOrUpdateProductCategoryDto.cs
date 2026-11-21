using System.ComponentModel.DataAnnotations;

namespace API.Dtos;

public class CreateOrUpdateProductCategoryDto
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Category name is required")]
    [MinLength(3, ErrorMessage = "Category name must be at least 3 characters long")]
    [MaxLength(100, ErrorMessage = "Category name cannot exceed 100 characters")]
    public string Name { get; set; } = string.Empty;

    [MaxLength(500)]
    public string? Description { get; set; }
}
