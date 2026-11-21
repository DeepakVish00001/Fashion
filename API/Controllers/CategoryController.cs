
using API.Dtos;
using Core.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class CategoryController(StoreContext context) : BaseApiController
    {

        [HttpGet]
        public async Task<ActionResult<List<ProductCategory>>> GetCategories()
        {
            var categories = await context.ProductCategories.ToListAsync();
            if (categories == null || categories.Count == 0)
            {
                return NotFound(new
                {
                    success = false,
                    message = "Sorry no records found."
                });
            }
            return Ok(categories);
        }

        [HttpPost]
        public async Task<ActionResult<ProductCategory>> CreateCategory(CreateOrUpdateProductCategoryDto category)
        {
            if (string.IsNullOrWhiteSpace(category.Name) || category.Name.Length < 3 || category.Name.Length > 100)
            {
                return BadRequest("Category name must be between 3 and 100 characters long.");
            }

            var categoryName = category.Name.Trim();
            var existingCategory = await context.ProductCategories.FirstOrDefaultAsync(c => c.Name.ToLower() == categoryName.ToLower());
            if (existingCategory != null) return Conflict(new
            {
                success = false,
                message = "A category with the same name already exists."
            });

            var productCategory = new ProductCategory
            {
                Name = category.Name,
                Description = category.Description
            };
            context.ProductCategories.Add(productCategory);
            var result = await context.SaveChangesAsync();
            if (result <= 0)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = "An error occurred while creating the category."
                });
            }
            return Ok(new
            {
                success = true,
                message = "Category created successfully.",
                data = productCategory
            });
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetCategory(int id)
        {
            var category = await context.ProductCategories.FindAsync(id);
            if (category == null) return NotFound(new
            {
                success = false,
                message = "Category not found."
            });
            return Ok(category);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateCategory(int id, CreateOrUpdateProductCategoryDto category)
        {

            var existingCategory = await context.ProductCategories.FindAsync(id);
            if (existingCategory == null) return NotFound(new
            {
                success = false,
                message = "Category not found."
            });
            // 2. Validate category name
            if (string.IsNullOrWhiteSpace(category.Name))
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Category name is required."
                });
            }
            var categoryName = category.Name.Trim();

            if (categoryName.Length < 3 || categoryName.Length > 100)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Category name must be between 3 and 100 characters long."
                });
            }
            // 3. Check duplicate category (excluding current category)
            var duplicateCategory = await context.ProductCategories.AnyAsync(
                    x => x.Id != id &&
                    x.Name.ToLower() == categoryName.ToLower());

            if (duplicateCategory)
            {
                return Conflict(new
                {
                    success = false,
                    message = "A category with the same name already exists."
                });
            }



            // 4. Update existing entity
            existingCategory.Name = categoryName;
            existingCategory.Description = category.Description?.Trim();
            await context.SaveChangesAsync();

            

            // 6. Return response
            return Ok(new
            {
                success = true,
                message = "Category updated successfully.",
                data = existingCategory
            });
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteCategory(int id)
        {
            var category = await context.ProductCategories.FindAsync(id);
            if (category == null) return NotFound(new
            {
                success = false,
                message = "Category not found."
            });
            context.ProductCategories.Remove(category);
            var result = await context.SaveChangesAsync();
            if (result <= 0)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = "An error occurred while deleting the category."
                });
            }
            return Ok(new
            {
                success = true,
                message = "Category deleted successfully."
            });
        }
    }
}
