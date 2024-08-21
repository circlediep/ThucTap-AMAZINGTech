using System;
using System.ComponentModel.DataAnnotations;

namespace TASK_Nhom_01.Models
{
    public class OrderModel
    {
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime NgayLap { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime NgayGiao { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Tổng tiền phải lớn hơn hoặc bằng 0.")]
        public decimal TongTien { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Giá phải lớn hơn 0.")]
        public decimal Price { get; set; }

        [Required]
        [Range(1, 9999, ErrorMessage = "Số lượng phải từ 1 đến 9999.")]
        public int Quantity { get; set; }

        // Computed property for total
        public decimal Total => Price * Quantity;
    }
}