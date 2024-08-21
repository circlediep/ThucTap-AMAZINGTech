using Microsoft.AspNetCore.Identity;
namespace TASK_Nhom_01.Data

{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;
    }
}
