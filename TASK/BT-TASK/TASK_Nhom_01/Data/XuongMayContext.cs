using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TASK_Nhom_01.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace TASK_Nhom_01.Data
{
    public class XuongMayContext : IdentityDbContext<ApplicationUser> //Kế thừa từ DBContext
    {
        // Constructor cho lớp XuongMayContext, truyền DbContextOptions vào lớp cơ sở DbContext.
        // Điều này cho phép cấu hình như chuỗi kết nối cơ sở dữ liệu được truyền vào.
        public XuongMayContext(DbContextOptions<XuongMayContext> opt) : base(opt)
        {

        }

        #region DBSet
        // DbSet đại diện cho bảng 'May' trong cơ sở dữ liệu. 
        // 'Mays' là tập hợp các đối tượng 'Product' sẽ được truy xuất hoặc thao tác từ bảng này.
        public DbSet<Product>? Mays { get; set; }
        public DbSet<Category>? Categories { get; set; }

        public DbSet<Order>? Orders { get; set; }
        public IEnumerable<object> Products { get; internal set; }
        #endregion

    }
}
