using System.ComponentModel.DataAnnotations;

namespace Core.Entities;

public class ProductBrand : BaseEntity
{
    [Required(ErrorMessage = "Brand name is required")]
    [MinLength(3, ErrorMessage = "Brand name must be at least 3 characters long")]
    [MaxLength(100, ErrorMessage = "Brand name cannot exceed 100 characters")]
    public string Name { get; set; } = string.Empty;

    [MaxLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
    public string? Description { get; set; }
    public bool IsActive { get; set; } = true;

    public ProductCategory? ProductCategory { get; set; }
    public int ProductCategoryId { get; set; }
}
