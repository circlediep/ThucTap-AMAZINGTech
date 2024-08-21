using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TASK_Nhom_01.Data;

namespace TASK_Nhom_01.Models
{
    public class ProductModel
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string NhanHieu { get; set; }
        public string Description { get; set; }
        [Range(0, double.MaxValue)]
        public double Price { get; set; }
        [Range(0, 9999)]
        public double Quanity { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
