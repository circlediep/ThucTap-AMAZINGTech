using TASK_Nhom_01.Data;
using System.ComponentModel.DataAnnotations;

namespace TASK_Nhom_01.Models
{
    public class CategoryModel
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
