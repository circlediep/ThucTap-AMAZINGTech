using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TASK_Nhom_01.Data;
using TASK_Nhom_01.Models;

namespace TASK_Nhom_01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {

        private readonly XuongMayContext _context;

        public OrderController(XuongMayContext context)
        {
            _context = context;
        }

        // GET: api/Order
        // Phương thức này trả về danh sách các đơn hàng theo phân trang
        [HttpGet]
        public IActionResult GetOrders([FromQuery] int pageSize = 10, [FromQuery] int pageNumber = 1)
        {
            // Lấy danh sách các đơn hàng theo số trang và số lượng trên mỗi trang
            var orders = _context.Orders!
                .Skip((pageNumber - 1) * pageSize) // Bỏ qua các sản phẩm của các trang trước
                .Take(pageSize); // Lấy số lượng sản phẩm theo kích thước trang (pageSize)

            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderModel>> GetOrder(int id)
        {
            var order = await _context.Orders
                .FirstOrDefaultAsync(o => o.OrderId == id);

            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        // POST: api/Order
        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(OrderModel orderModel)
        {
            if (orderModel == null)
            {
                return BadRequest("Order cannot be null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Kiểm tra nếu sản phẩm có tồn tại
            var product = await _context.Mays.FindAsync(orderModel.ProductId);
            if (product == null)
            {
                return NotFound("Product not found.");
            }

            // Kiểm tra nếu số lượng sản phẩm trong kho có đủ
            if (product.Quanity < orderModel.Quantity) 
            {
                return BadRequest("Insufficient stock.");
            }

            // Ánh xạ từ DTO (OrderModel) sang Entity (Order)
            var order = new Order
            {
                NgayLap = orderModel.NgayLap,
                NgayGiao = orderModel.NgayGiao,
                ProductId = orderModel.ProductId,
                Quantity = orderModel.Quantity,
                 TongTien = product.Price * orderModel.Quantity
            };
           

            _context.Orders.Add(order);

            // Cập nhật lại số lượng sản phẩm trong kho
            product.Quanity -= orderModel.Quantity;
            _context.Entry(product).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetOrder), new { id = order.OrderId }, order);
        }


        // DELETE: api/Order/5
        // Phương thức này xóa một đơn hàng dựa trên ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            // Tìm đơn hàng theo ID và bao gồm thông tin sản phẩm liên quan
            var order = await _context.Orders
                .Include(o => o.Product)
                .FirstOrDefaultAsync(o => o.OrderId == id);

            if (order == null)
            {
                return NotFound();
            }

            // Lấy sản phẩm liên quan
            var product = order.Product;

            if (product != null)
            {
                // Trả lại số lượng sản phẩm về kho
                product.Quanity += order.Quantity;
                _context.Entry(product).State = EntityState.Modified;
            }

            // Xóa đơn hàng khỏi cơ sở dữ liệu
            _context.Orders.Remove(order);

            // Lưu các thay đổi vào cơ sở dữ liệu
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.OrderId == id);
        }
    }

}
