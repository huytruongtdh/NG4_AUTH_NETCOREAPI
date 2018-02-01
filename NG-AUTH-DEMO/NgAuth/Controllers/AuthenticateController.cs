using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NgAuth.Data;
using NgAuth.ViewModels;

namespace NgAuth.Controllers
{
    [Produces("application/json")]
    [Route("api/Authenticate")]
    public class AuthenticateController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _config;

        public AuthenticateController(
            UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager,
            IConfiguration config)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._config = config;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.UserName);

                if (user != null)
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
    }
}