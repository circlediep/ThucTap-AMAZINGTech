using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TASK_Nhom_01.Data;
using TASK_Nhom_01.Models;

namespace TASK_Nhom_01.Repositories
{
    // Khai báo lớp CategoryRepository, triển khai từ giao diện ICategoryRepository
    public class CategoryRepository : ICategoryRepository
    {
        // Thuộc tính _context để làm việc với cơ sở dữ liệu
        private readonly XuongMayContext _context;

        // Thuộc tính _mapper dùng để ánh xạ giữa các model và entity
        private readonly IMapper _mapper;

        // Constructor của CategoryRepository, nhận vào một đối tượng XuongMayContext và IMapper
        public CategoryRepository(XuongMayContext context, IMapper mapper)
        {
            _context = context; // Gán đối tượng XuongMayContext cho thuộc tính _context
            _mapper = mapper; // Gán đối tượng IMapper cho thuộc tính _mapper
        }

        // Phương thức lấy tất cả các danh mục (categories) và ánh xạ sang CategoryModel
        public async Task<IEnumerable<CategoryModel>> GetAllCategoriesAsync()
        {
            // Lấy danh sách tất cả các danh mục và bao gồm cả các sản phẩm (products) liên quan
            var categories = await _context.Categories!
                .Include(c => c.Products)
                .ToListAsync();

            // Ánh xạ danh sách các danh mục sang CategoryModel và trả về
            return _mapper.Map<IEnumerable<CategoryModel>>(categories);
        }

        // Phương thức lấy một danh mục dựa trên ID và ánh xạ sang CategoryModel
        public async Task<CategoryModel> GetCategoryAsync(int id)
        {
            // Tìm danh mục theo ID và bao gồm cả các sản phẩm liên quan
            var category = await _context.Categories!
                .Include(c => c.Products)
                .FirstOrDefaultAsync(c => c.Id == id);

            // Ánh xạ đối tượng danh mục sang CategoryModel và trả về
            return _mapper.Map<CategoryModel>(category);
        }

        // Phương thức tạo mới một danh mục từ CategoryModel
        public async Task<bool> CreateCategoryAsync(CategoryModel category)
        {
            // Ánh xạ CategoryModel sang đối tượng Category (entity)
            var categoryEntity = _mapper.Map<Category>(category);

            // Thêm danh mục mới vào DbSet Categories
            _context.Categories!.Add(categoryEntity);

            // Lưu thay đổi vào cơ sở dữ liệu và trả về kết quả
            return await _context.SaveChangesAsync() > 0;
        }

        // Phương thức cập nhật một danh mục từ CategoryModel
        public async Task<bool> UpdateCategoryAsync(CategoryModel category)
        {
            // Đánh dấu thực thể danh mục là đã bị sửa đổi
            _context.Entry(category).State = EntityState.Modified;

            // Lưu thay đổi vào cơ sở dữ liệu và trả về kết quả
            return await _context.SaveChangesAsync() > 0;
        }

        // Phương thức xóa một danh mục theo ID
        public async Task<bool> DeleteCategoryAsync(int id)
        {
            // Tìm danh mục theo ID
            var categoryEntity = await _context.Categories!
                .FirstOrDefaultAsync(c => c.Id == id);

            // Nếu tìm thấy danh mục, xóa danh mục đó khỏi DbSet Categories
            if (categoryEntity != null)
            {
                _context.Categories!.Remove(categoryEntity);

                // Lưu thay đổi vào cơ sở dữ liệu và trả về kết quả
                return await _context.SaveChangesAsync() > 0;
            }

            // Trả về false nếu không tìm thấy danh mục
            return false;
        }

        // Phương thức kiểm tra xem một danh mục có tồn tại theo ID hay không
        public async Task<bool> CategoryExists(int id)
        {
            // Kiểm tra sự tồn tại của danh mục theo ID
            return await _context.Categories!.AnyAsync(e => e.Id == id);
        }
    }
}
