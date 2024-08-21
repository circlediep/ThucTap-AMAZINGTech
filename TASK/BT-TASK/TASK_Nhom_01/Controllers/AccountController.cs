using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TASK_Nhom_01.Models;
using TASK_Nhom_01.Repositories;

namespace TASK_Nhom_01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository accountRepo;

        public AccountController(IAccountRepository repo)
        {
            accountRepo = repo;
        }
        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp(SignUpModel signUpModel)
        {
            var result = await accountRepo.SignUpAsync(signUpModel);
            if (result.Succeeded)
            {
                return Ok(result.Succeeded);
            }
            return Unauthorized();

        }
        [HttpPost("SigIn")]
        public async Task<IActionResult> SigIn(SignInModel signInModel)
        {
            var result = await accountRepo.SignInAsync(signInModel);
            if(string.IsNullOrEmpty(result)){
                return Unauthorized();
            }
            return Ok(result);

        }
    }
}
