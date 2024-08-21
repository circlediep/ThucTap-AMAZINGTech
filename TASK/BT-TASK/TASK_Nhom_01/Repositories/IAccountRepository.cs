using Microsoft.AspNetCore.Identity;
using TASK_Nhom_01.Models;

namespace TASK_Nhom_01.Repositories
{
    // Khai báo interface IAccountRepository
    public interface IAccountRepository
    {
        // Phương thức SignUpAsync cho việc đăng ký người dùng mới
        public Task<IdentityResult> SignUpAsync(SignUpModel model);

        // Phương thức SignInAsync cho việc đăng nhập người dùng
        public Task<string> SignInAsync(SignInModel model);
    }
}
