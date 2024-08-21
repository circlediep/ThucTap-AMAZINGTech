using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TASK_Nhom_01.Data;

namespace TASK_Nhom_01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly XuongMayContext _context;

        public ProductsController(XuongMayContext context)
        {
            _context = context;
        }

        // GET: api/Mays
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetMays([FromQuery] int pageSize = 10, [FromQuery] int pageNumber = 1)
        {
            // Lấy danh sách các sản phẩm (Product) từ cơ sở dữ liệu theo số trang và số lượng trên mỗi trang
            var products = _context.Products!
                .Skip((pageNumber - 1) * pageSize) // Bỏ qua các sản phẩm của các trang trước
                .Take(pageSize); // Lấy số lượng sản phẩm theo kích thước trang (pageSize)

            // Trả về danh sách sản phẩm dưới dạng JSON
            return Ok(products);
        }


        // GET: api/Mays/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetMay(int id)
        {
            var may = await _context.Mays!.FindAsync(id);

            if (may == null)
            {
                return NotFound();
            }

            return may;
        }

        // PUT: api/Mays/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMay(int id, Product may)
        {
            if (id != may.Id)
            {
                return BadRequest();
            }

            _context.Entry(may).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MayExists(id))
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

        // POST: api/Mays
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Product>> PostMay(Product may)
        {
            _context.Mays!.Add(may);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMay", new { id = may.Id }, may);
        }

        // DELETE: api/Mays/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMay(int id)
        {
            var may = await _context.Mays!.FindAsync(id);
            if (may == null)
            {
                return NotFound();
            }

            _context.Mays.Remove(may);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MayExists(int id)
        {
            return _context.Mays.Any(e => e.Id == id);
        }

       
    }
}
