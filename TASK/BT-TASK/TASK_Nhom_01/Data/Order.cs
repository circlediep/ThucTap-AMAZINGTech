using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TASK_Nhom_01.Data
{
    [Table("Orders")]
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime NgayLap { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime NgayGiao { get; set; }

        // Khóa ngoại cho Sản phẩm
        [Required]
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; } // Thuộc tính điều hướng

        [Range(0, double.MaxValue, ErrorMessage = "Total must be greater than or equal to 0.")]
        public decimal TongTien { get; set; }

        [Required]
        [Range(1, 9999, ErrorMessage = "Quantity must be between 1 and 9999.")]
        public int Quantity { get; set; }

        // Total price calculated from Product price and Quantity
        [NotMapped] // This is not stored in the database
        public decimal Total => Product != null ? Product.Price * Quantity : 0;

        
    }
}