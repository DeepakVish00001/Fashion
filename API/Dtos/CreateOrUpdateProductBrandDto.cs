using System.ComponentModel.DataAnnotations;

namespace API.Dtos;

public class CreateOrUpdateProductBrandDto
{
    [Required(ErrorMessage = "Brand name is required")]
    [MinLength(3, ErrorMessage = "Brand name must be at least 3 characters long")]
    [MaxLength(100, ErrorMessage = "Brand name cannot exceed 100 characters")]
    public string Name { get; set; } = string.Empty;

    [MaxLength(500)]
    public string? Description { get; set; }

    public bool IsActive { get; set; } = true;


    [Required(ErrorMessage = "Category is required")]
    [Range(1, int.MaxValue, ErrorMessage = "Please select a valid category")]
    public int ProductCategoryId { get; set; }
}
