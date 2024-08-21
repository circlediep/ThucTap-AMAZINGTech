using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TASK_Nhom_01.Data;
using TASK_Nhom_01.Models;
using NuGet.Protocol.Core.Types;

namespace TASK_Nhom_01.Repositories
{
    // Lớp MayRepository thực hiện các thao tác liên quan đến sản phẩm
    public class MayRepository : IMayRepository
    {
        // Đối tượng DbContext dùng để tương tác với cơ sở dữ liệu
        private readonly XuongMayContext _context;

        // Đối tượng AutoMapper dùng để ánh xạ giữa các model
        private readonly IMapper _mapper;

        // Constructor nhận vào DbContext và AutoMapper
        public MayRepository(XuongMayContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // Phương thức để thêm một sản phẩm mới vào cơ sở dữ liệu
        public async Task<int> AddMaysASync(ProductModel model)
        {
            // Ánh xạ ProductModel thành đối tượng Product
            var newMay = _mapper.Map<Product>(model);

            // Thêm sản phẩm vào DbSet
            _context.Mays!.Add(newMay);

            // Lưu các thay đổi vào cơ sở dữ liệu
            await _context.SaveChangesAsync();

            // Trả về ID của sản phẩm mới
            return newMay.Id;
        }

        // Phương thức để xóa sản phẩm theo ID
        public async Task DeleteMaysASync(int id)
        {
            // Tìm sản phẩm cần xóa theo ID
            var deleteMay = _context.Mays!.SingleOrDefault(m => m.Id == id);

            // Nếu sản phẩm tồn tại, xóa nó khỏi DbSet
            if (deleteMay != null)
            {
                _context.Mays!.Remove(deleteMay);
                await _context.SaveChangesAsync();
            }
        }

        // Phương thức để lấy danh sách tất cả sản phẩm
        public async Task<List<ProductModel>> GetAllMaysSync()
        {
            // Lấy tất cả sản phẩm từ cơ sở dữ liệu
            var mays = await _context.Mays!.ToListAsync();

            // Ánh xạ danh sách Product thành danh sách ProductModel
            return _mapper.Map<List<ProductModel>>(mays);
        }

        // Phương thức để lấy thông tin một sản phẩm theo ID
        public async Task<ProductModel> GetMaysASync(int id)
        {
            // Tìm sản phẩm theo ID
            var may = await _context.Mays!.FindAsync(new object[] { id });

            // Ánh xạ đối tượng Product thành ProductModel
            return _mapper.Map<ProductModel>(may);
        }

        // Phương thức để cập nhật thông tin sản phẩm theo ID
        public async Task UpdateMaysASync(int id, ProductModel model)
        {
            // Kiểm tra xem ID từ model có trùng với ID từ tham số không
            if (id == model.Id)
            {
                // Ánh xạ ProductModel thành đối tượng Product
                var updateMay = _mapper.Map<Product>(model);

                // Cập nhật sản phẩm trong DbSet
                _context.Mays!.Update(updateMay);
                await _context.SaveChangesAsync();
            }
        }
    }
}
