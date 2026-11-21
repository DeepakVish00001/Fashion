using System.ComponentModel.DataAnnotations;

namespace Core.Entities;

public class ProductCategory : BaseEntity
{
    [Required(ErrorMessage = "Category name is required")]
    [MinLength(3, ErrorMessage = "Category name must be at least 3 characters long")]
    [MaxLength(100, ErrorMessage = "Category name cannot exceed 100 characters")]
    public string Name { get; set; } = string.Empty;

    [MaxLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
    public string? Description { get; set; }

    public ICollection<ProductBrand> ProductBrands {get;set;}= new List<ProductBrand>();
}
