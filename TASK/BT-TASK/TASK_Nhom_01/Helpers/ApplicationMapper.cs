using AutoMapper;
using TASK_Nhom_01.Data;
using TASK_Nhom_01.Models;

namespace TASK_Nhom_01.Helper
{
    // ApplicationMapper kế thừa từ lớp Profile của AutoMapper, đây là nơi cấu hình các ánh xạ (mapping) giữa các đối tượng
    public class ApplicationMapper : Profile
    {
        // Constructor để cấu hình ánh xạ
        public ApplicationMapper()
        {
            // Cấu hình ánh xạ giữa đối tượng Product và ProductModel
            // ReverseMap() cho phép ánh xạ hai chiều (từ Product sang ProductModel và ngược lại)
            CreateMap<Product, ProductModel>().ReverseMap();
        }
    }
}
