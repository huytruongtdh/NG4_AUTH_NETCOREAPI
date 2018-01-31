using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NgAuth.Data;

namespace NgAuth.Controllers
{
    //[Produces("application/json")]
    //[Route("api/Users")]

    [Route("api/[Controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UsersController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // GET: api/Users
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var models = _userRepository.GetAll();
                return Ok(models);
            }
            catch (Exception ex)
            {
                return BadRequest("Failed to get users");
            }
        }

        // GET: api/Users/5
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(string id, bool findByName = false)
        {
            try
            {
                ApplicationUser user;
                if (findByName)
                    user = _userRepository.GetByUserName(id);
                else
                    user = _userRepository.GetById(id);

                if (user == null)
                    return BadRequest($"The user '{id}' is not found");

                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to get user #{id}");
            }
        }
        
        // POST: api/Users
        [HttpPost]
        public IActionResult Post([FromBody]ApplicationUser user)
        {
            try
            {
                ApplicationUser existedUser = _userRepository.GetByUserName(user.UserName);
                if (existedUser != null)
                    return BadRequest($"The name '{user.UserName}' is existed");

                if (ModelState.IsValid)
                {
                    _userRepository.Add(user);
                    return Created("/api/users/", user);
                }

                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                return BadRequest("Failed to save new user");
            }
        }
        
        // PUT: api/Users/5
        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody]ApplicationUser model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = _userRepository.GetById(id);
                    if(user != null)
                    {
                        user.MapFrom(model);
                        _userRepository.Update(user, newPassword: model.Password);
                        return Created($"/api/users/{model.Id}", model);
                    }

                    return BadRequest($"The user '{id}' is not found");
                }

                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to update user '{id}'");
            }
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
