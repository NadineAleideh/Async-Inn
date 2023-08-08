
using Async_Inn.Models.DTOs;
using Async_Inn.Models.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Async_Inn.Models.Services
{
    public class IdentityUserServices : IUser
    {
        //inject user manager service it's help us to manage related actions (CRUD), also it will do all the validations for us
        private UserManager<ApplicationUser> userManager;

        public IdentityUserServices(UserManager<ApplicationUser> manager)
        {
            userManager = manager;
        }
        public async Task<UserDTO> Register(RegisterUserDTO RegisterUserDTO, ModelStateDictionary modelState)
        {
            var user = new ApplicationUser
            {
                UserName = RegisterUserDTO.Username,
                Email = RegisterUserDTO.Email,
                PhoneNumber = RegisterUserDTO.PhoneNumber
            };

            //now I will add the above new user info and create new user by the usermanager
            var result = await userManager.CreateAsync(user, RegisterUserDTO.Password);

            if (result.Succeeded)
            {
                return new UserDTO
                {
                    Id = user.Id,
                    Username = user.UserName
                };
            }

            foreach (var error in result.Errors)
            {
                var errorKey = error.Code.Contains("Password") ? nameof(RegisterUserDTO.Password) :
                    error.Code.Contains("Email") ? nameof(RegisterUserDTO.Email) :
                    error.Code.Contains("Username") ? nameof(RegisterUserDTO.Username) :
                    "";

                modelState.AddModelError(errorKey, error.Description);
            }

            return null;
        }

        public async Task<UserDTO> Authenticate(string username, string password)
        {
            var user = await userManager.FindByNameAsync(username);

            bool validPassword = await userManager.CheckPasswordAsync(user, password);

            if (validPassword)
            {
                return new UserDTO { Id = user.Id, Username = user.UserName };
            }

            return null;

        }
    }
}
