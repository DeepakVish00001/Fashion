using System.ComponentModel.DataAnnotations;

namespace Core.Entities;

public class ProductCategory : BaseEntity
{
    [Required(ErrorMessage = "Category name is required")]
    [MinLength(3, ErrorMessage = "Category name must be at least 3 characters long")]
    [MaxLength(100, ErrorMessage = "Category name cannot exceed 50 characters")]
    public string Name { get; set; } = string.Empty;

    [MaxLength(500)]
    public string? Description { get; set; }
}
