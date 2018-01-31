using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using NgAuth.Data;
using NgAuth.ViewModels;

namespace NgAuth.Controllers
{
    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _config;

        public AccountController(
            SignInManager<ApplicationUser> signInManager, 
            ILogger<AccountController> logger,
            UserManager<ApplicationUser> userManager,
            IConfiguration config)
        {
            _signInManager = signInManager;
            _logger = logger;
            _userManager = userManager;
            _config = config;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            return RedirectToPage("/Index");
        }

        [HttpPost]
        public async Task<IActionResult> CreateToken([FromBody] LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.UserName);

                if(user != null)
                {
                    var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
                    if (result.Succeeded)
                    {
                        // create a token
                        var claims = new[]
                        {
                            new Claim(JwtRegisteredClaimNames.Sub, model.UserName),
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                            new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName)
                        };

                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
                        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                        var token = new JwtSecurityToken(
                                issuer: _config["Tokens:Issuer"],
                                audience: _config["Tokens:Audience"],
                                claims: claims,
                                expires: DateTime.UtcNow.AddMinutes(20),
                                signingCredentials: creds
                            );

                        var results = new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(token),
                            expiration = token.ValidTo,
                            userName = user.UserName,
                            email = user.Email,
                        };

                        return Created("", results);
                    }
                    else
                    {
                        ModelState.AddModelError("", "Username or password is incorrect");
                    }
                }
                else
                {
                    ModelState.AddModelError("UserName", "UserName is not existed.");
                }
            }

            return BadRequest(ModelState);
        }

        [HttpPost]
        public async Task<IActionResult> CreateToken2([FromBody] LoginViewModel model)
        {
            // Neu API goi thi co dang nhap duoc ben cookie hay ko???

            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.UserName);

                if (user != null)
                {
                    // Ensure that cookie logged too
                    var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
                    if (result.Succeeded)
                    {
                        var results = GenerateTokenInfo(user, model);

                        return Created("", results);
                    }
                    else
                    {
                        ModelState.AddModelError("Password", "Wrong password.");
                    }
                }
                else
                {
                    ModelState.AddModelError("Email", "Email is not existed.");
                }

            }

            return BadRequest(ModelState);
        }

        private object GenerateTokenInfo(ApplicationUser user, LoginViewModel model)
        {
            // luon dam bao cookie & jwt login dong thoi
            // create the token
            var claims = new[]
            {
                            new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                            new Claim(JwtRegisteredClaimNames.UniqueName, user.Email)
                        };


            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _config["Tokens:Issuer"],
                _config["Tokens:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);


            var results = new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                tokenExpiration = token.ValidTo,
                userName = user.UserName,
                email = user.Email,
            };

            return results;
        }
    }
}
