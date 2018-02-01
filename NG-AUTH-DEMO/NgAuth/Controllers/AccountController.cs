using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NgAuth.Data;

namespace NgAuth.Controllers
{
    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger _logger;
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountController(
            SignInManager<ApplicationUser> signInManager, 
            ILogger<AccountController> logger,
            UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _logger = logger;
            _userManager = userManager;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            return RedirectToPage("/Index");
        }

    }
}
