
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
            return Ok(categories);
        }

        [HttpPost]
        public async Task<ActionResult<ProductCategory>> CreateCategory(ProductCategory category)
        {
            context.ProductCategories.Add(category);
            await context.SaveChangesAsync();
            return Ok(category);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetCategory(int id)
        {
            var category = await context.ProductCategories.FindAsync(id);
            if (category == null) return NotFound();
            return Ok(category);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateCategory(int id, ProductCategory category)
        {
            if (id != category.Id) return BadRequest();
            context.Entry(category).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return Ok(category);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteCategory(int id)
        {
            var category = await context.ProductCategories.FindAsync(id);
            if (category == null) return NotFound();
            context.ProductCategories.Remove(category);
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}
