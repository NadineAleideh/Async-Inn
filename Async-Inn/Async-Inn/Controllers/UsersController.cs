using Async_Inn.Models.DTOs;
using Async_Inn.Models;
using Async_Inn.Models.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Async_Inn.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUser userService;

        public UsersController(IUser service)
        {
            userService = service;
        }


        [HttpPost("Register")]
        public async Task<ActionResult<UserDTO>> Register(RegisterUserDTO data)
        {
            var user = await userService.Register(data, this.ModelState, User);

            if (ModelState.IsValid)
            {
                if (user != null)
                    return user;

                else
                    return NotFound();
            }

            return BadRequest(new ValidationProblemDetails(ModelState));
        }


        [HttpPost("Login")]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO loginDTO)
        {
            var user = await userService.Authenticate(loginDTO.Username, loginDTO.Password);

            if (user == null)
            {
                return Unauthorized();
            }
            return user;
        }



        // [Authorize(Roles = "Admin")]
        //[Authorize(Policy = "create")]
        [HttpGet("Profile")]
        public async Task<ActionResult<UserDTO>> Profile()
        {
            return Ok(await userService.GetUser(this.User));
        }

    }
}
