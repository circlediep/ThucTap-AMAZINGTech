using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TASK_Nhom_01.Data;
using TASK_Nhom_01.Models;

namespace TASK_Nhom_01.Repositories
{
    // Khai báo lớp AccountRepository, triển khai từ giao diện IAccountRepository
    public class AccountRepository : IAccountRepository
    {
        // Constructor của lớp AccountRepository
        // Được gọi khi tạo một đối tượng AccountRepository và cho phép Dependency Injection
        public AccountRepository(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration configuration)
        {
            // Gán các đối tượng UserManager, SignInManager và IConfiguration được truyền vào cho các thuộc tính tương ứng
            this.UserManager = userManager;
            this.signInManager = signInManager;
            this.configuration = configuration;
        }

        // Thuộc tính configuration để đọc các cấu hình từ appsettings.json (ví dụ: JWT secret)
        private readonly IConfiguration configuration;

        // Thuộc tính UserManager quản lý người dùng của ứng dụng, xử lý các hoạt động như tạo, xóa và cập nhật người dùng
        private readonly UserManager<ApplicationUser> UserManager;

        // Thuộc tính signInManager xử lý việc đăng nhập và đăng xuất người dùng
        private readonly SignInManager<ApplicationUser> signInManager;

        // Triển khai phương thức SignInAsync từ giao diện IAccountRepository
        async Task<string> IAccountRepository.SignInAsync(SignInModel model)
        {
            // Gọi phương thức PasswordSignInAsync để đăng nhập bằng email và mật khẩu
            var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);

            // Nếu đăng nhập không thành công, trả về chuỗi rỗng
            if (!result.Succeeded)
            {
                return string.Empty;
            }

            // Tạo danh sách các claim, là các thông tin người dùng cần lưu trữ trong token JWT
            var authClaims = new List<Claim>
            {
                // Claim chứa email của người dùng
                new Claim(ClaimTypes.Email, model.Email),

                // Claim chứa một mã định danh duy nhất cho token (JTI)
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            // Tạo một SymmetricSecurityKey từ chuỗi bí mật trong cấu hình (JWT:Secret)
            var authenKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]));

            // Tạo một đối tượng JwtSecurityToken với các thông tin như nhà phát hành, người nhận, thời gian hết hạn, danh sách claim và khóa ký
            var token = new JwtSecurityToken(
                issuer: configuration["JWT:ValidIssuer"], // Nhà phát hành token (issuer)
                audience: configuration["JWT:ValidAudience"], // Người nhận token (audience)
                expires: DateTime.Now.AddMinutes(20), // Thời gian hết hạn của token là 20 phút từ thời điểm hiện tại
                claims: authClaims, // Danh sách claim chứa thông tin người dùng
                signingCredentials: new SigningCredentials(authenKey, SecurityAlgorithms.HmacSha512Signature) // Thuật toán ký HMAC-SHA512
            );

            // Sử dụng JwtSecurityTokenHandler để tạo chuỗi token từ đối tượng JwtSecurityToken và trả về
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        // Triển khai phương thức SignUpAsync từ giao diện IAccountRepository
        async Task<IdentityResult> IAccountRepository.SignUpAsync(SignUpModel model)
        {
            // Tạo một đối tượng ApplicationUser mới từ thông tin đăng ký
            var user = new ApplicationUser
            {
                FirstName = model.FirstName, // Lưu tên đầu tiên của người dùng
                LastName = model.LastName, // Lưu họ của người dùng
                Email = model.Email, // Lưu email của người dùng
                UserName = model.Email, // Đặt tên người dùng là email
            };

            // Gọi phương thức CreateAsync để tạo người dùng mới với mật khẩu đã được mã hóa
            return await UserManager.CreateAsync(user, model.Password);
        }
    }
}
