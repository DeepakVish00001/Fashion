
using API.Dtos;
using Core.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class BrandController(StoreContext context) : BaseApiController
    {

        [HttpGet]
        public async Task<ActionResult<List<BrandsToReturnDto>>> GetBrands()
        {
            var brands = await context.ProductBrands
        .Select(x => new BrandsToReturnDto
        {
            Id = x.Id,
            Name = x.Name,
            Description = x.Description,
            IsActive = x.IsActive,
            ProductCategory = x.ProductCategory!.Name
        })
        .ToListAsync();
            if (brands == null || brands.Count == 0)
            {
                return NotFound(new
                {
                    success = false,
                    message = "Sorry no records found."
                });
            }
            return Ok(brands);
        }

        [HttpPost]
        public async Task<ActionResult<ProductBrand>> Createbrand(CreateOrUpdateProductBrandDto brand)
        {
            if (string.IsNullOrWhiteSpace(brand.Name) || brand.Name.Length < 3 || brand.Name.Length > 100)
            {
                return BadRequest("Brand name must be between 3 and 100 characters long.");
            }

            var brandName = brand.Name.Trim();
            var existingbrand = await context.ProductBrands.FirstOrDefaultAsync(c => c.Name.ToLower() == brandName.ToLower());
            if (existingbrand != null) return Conflict(new
            {
                success = false,
                message = "A brand with the same name already exists."
            });

            var productbrand = new ProductBrand
            {
                Name = brand.Name,
                Description = brand.Description,
                IsActive = brand.IsActive,
                ProductCategoryId = brand.ProductCategoryId
            };
            context.ProductBrands.Add(productbrand);
            var result = await context.SaveChangesAsync();
            if (result <= 0)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = "An error occurred while creating the brand."
                });
            }
            return Ok(new
            {
                success = true,
                message = "brand created successfully.",
                data = productbrand
            });
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult> Getbrand(int id)
        {
            var brand = await context.ProductBrands.FindAsync(id);
            if (brand == null) return NotFound(new
            {
                success = false,
                message = "brand not found."
            });
            return Ok(brand);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Updatebrand(int id, CreateOrUpdateProductBrandDto brand)
        {

            var existingbrand = await context.ProductBrands.FindAsync(id);
            if (existingbrand == null) return NotFound(new
            {
                success = false,
                message = "brand not found."
            });
            // 2. Validate brand name
            if (string.IsNullOrWhiteSpace(brand.Name))
            {
                return BadRequest(new
                {
                    success = false,
                    message = "brand name is required."
                });
            }
            var brandName = brand.Name.Trim();

            if (brandName.Length < 3 || brandName.Length > 100)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "brand name must be between 3 and 100 characters long."
                });
            }
            // 3. Check duplicate brand (excluding current brand)
            var duplicatebrand = await context.ProductBrands.AnyAsync(
                    x => x.Id != id &&
                    x.Name.ToLower() == brandName.ToLower());

            if (duplicatebrand)
            {
                return Conflict(new
                {
                    success = false,
                    message = "A brand with the same name already exists."
                });
            }



            // 4. Update existing entity
            existingbrand.Name = brandName;
            existingbrand.Description = brand.Description?.Trim();
            existingbrand.IsActive = brand.IsActive;
            existingbrand.ProductCategoryId = brand.ProductCategoryId;
            await context.SaveChangesAsync();



            // 6. Return response
            return Ok(new
            {
                success = true,
                message = "brand updated successfully.",
                data = existingbrand
            });
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Deletebrand(int id)
        {
            var brand = await context.ProductBrands.FindAsync(id);
            if (brand == null) return NotFound(new
            {
                success = false,
                message = "brand not found."
            });
            context.ProductBrands.Remove(brand);
            var result = await context.SaveChangesAsync();
            if (result <= 0)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = "An error occurred while deleting the brand."
                });
            }
            return Ok(new
            {
                success = true,
                message = "brand deleted successfully."
            });
        }
    }
}
