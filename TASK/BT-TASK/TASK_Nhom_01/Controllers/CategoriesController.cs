using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TASK_Nhom_01.Data;
using TASK_Nhom_01.Data;

namespace TASK_Nhom_01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly XuongMayContext _context;

        public CategoriesController(XuongMayContext context)
        {
            _context = context;
        }

        // GET: api/Categories
        // Phương thức này trả về danh sách các sản phẩm theo phân trang
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories([FromQuery] int pageSize = 10, [FromQuery] int pageNumber = 1)
        {
            // Lấy danh sách các category theo số trang và số lượng trên mỗi trang
            var categories = _context.Categories!
                .Skip((pageNumber - 1) * pageSize) // Bỏ qua các sản phẩm của các trang trước
                .Take(pageSize); // Lấy số lượng sản phẩm theo kích thước trang (pageSize)

            return Ok(categories);
        }


        // GET: api/Categories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
            var category = await _context.Categories!.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return category;
        }

        // PUT: api/Categories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(int id, Category category)
        {
            if (id != category.Id)
            {
                return BadRequest();
            }

            _context.Entry(category).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Categories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Category>> PostCategory(Category category)
        {
            _context.Categories!.Add(category);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCategory", new { id = category.Id }, category);
        }

        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _context.Categories!.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            _context.Categories!.Remove(category);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CategoryExists(int id)
        {
            return _context.Categories!.Any(e => e.Id == id);
        }

        // GET api/categories/number/products
        [HttpGet("{categoryId}/products")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductsByCategoryAsync(int categoryId)
        {
            var products = await _context.Set<Product>()
                .Where(p => p.CategoryId == categoryId)
                .ToListAsync();
            return Ok(products);
        }
    }
}
