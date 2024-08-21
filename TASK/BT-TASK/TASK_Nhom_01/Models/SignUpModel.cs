using System.ComponentModel.DataAnnotations;

namespace TASK_Nhom_01.Models
{
    public class SignUpModel
    {
        [Required]
        public string FirstName { get; set; } = null!;
        [Required]
        public string LastName { get; set; } = null!;
        [Required, EmailAddress]
        public string Email { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null;
        public string ConfirmEmail { get; set; } = null!;
    }
}
