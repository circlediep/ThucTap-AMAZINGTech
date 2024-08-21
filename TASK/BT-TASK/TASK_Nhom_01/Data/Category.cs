using TASK_Nhom_01.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TASK_Nhom_01.Data
{
    [Table("Category")]
    public class Category
    {
        [Key]   
        public int Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
