using TASK_Nhom_01.Data;
using TASK_Nhom_01.Models;

namespace TASK_Nhom_01.Repositories
{
    // Khai báo interface ICategoryRepository
    public interface ICategoryRepository
    {
        // Phương thức để lấy tất cả các danh mục
        Task<IEnumerable<CategoryModel>> GetAllCategoriesAsync();

        // Phương thức để lấy danh mục theo ID
        Task<CategoryModel> GetCategoryAsync(int id);

        // Phương thức để tạo danh mục mới
        Task<bool> CreateCategoryAsync(CategoryModel category);

        // Phương thức để cập nhật danh mục
        Task<bool> UpdateCategoryAsync(CategoryModel category);

        // Phương thức để xóa danh mục theo ID
        Task<bool> DeleteCategoryAsync(int id);

        // Phương thức để kiểm tra sự tồn tại của danh mục theo ID
        Task<bool> CategoryExists(int id);
    }
}
