using TASK_Nhom_01.Data;
using TASK_Nhom_01.Models;

namespace TASK_Nhom_01.Repositories
{
    // Khai báo interface IMayRepository
    public interface IMayRepository
    {
        // Phương thức để lấy tất cả các sản phẩm
        public Task<List<ProductModel>> GetAllMaysSync();

        // Phương thức để lấy sản phẩm theo ID
        public Task<ProductModel> GetMaysASync(int id);

        // Phương thức để thêm sản phẩm mới
        public Task<int> AddMaysASync(ProductModel model);

        // Phương thức để cập nhật sản phẩm theo ID
        public Task UpdateMaysASync(int id, ProductModel model);

        // Phương thức để xóa sản phẩm theo ID
        public Task DeleteMaysASync(int id);
    }
}
