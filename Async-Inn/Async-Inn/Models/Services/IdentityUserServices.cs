
using Async_Inn.Models.DTOs;
using Async_Inn.Models.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Security.Claims;

namespace Async_Inn.Models.Services
{
    public class IdentityUserServices : IUser
    {
        //inject user manager service it's help us to manage related actions (CRUD), also it will do all the validations for us
        private UserManager<ApplicationUser> userManager;

        private JwtTokenService tokenService;
        public IdentityUserServices(UserManager<ApplicationUser> manager, JwtTokenService tokenService)
        {
            userManager = manager;
            this.tokenService = tokenService;
        }
        public async Task<UserDTO> Register(RegisterUserDTO RegisterUserDTO, ModelStateDictionary modelState, ClaimsPrincipal User)
        {
            bool isDistrictManager = User.IsInRole("District Manager");
            bool isPropertyManager = User.IsInRole("Property Manager");

            if (isDistrictManager || (isPropertyManager && RegisterUserDTO.Roles.Contains("Agent")))
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
                    // Assign roles based on role-specific conditions
                    if (isDistrictManager)
                    {
                        await userManager.AddToRolesAsync(user, RegisterUserDTO.Roles);
                    }
                    else if (isPropertyManager && RegisterUserDTO.Roles.Contains("Agent"))
                    {
                        await userManager.AddToRolesAsync(user, new[] { "Agent" });
                    }


                    return new UserDTO
                    {
                        Id = user.Id,
                        Username = user.UserName,
                        Token = await tokenService.GetToken(user, System.TimeSpan.FromSeconds(60)),
                        Roles = await userManager.GetRolesAsync(user)
                    };
                }
                else
                {


                    foreach (var error in result.Errors)
                    {
                        var errorKey = error.Code.Contains("Password") ? nameof(RegisterUserDTO.Password) :
                            error.Code.Contains("Email") ? nameof(RegisterUserDTO.Email) :
                            error.Code.Contains("Username") ? nameof(RegisterUserDTO.Username) :
                            "";

                        modelState.AddModelError(errorKey, error.Description);
                    }
                    return null; // Return some appropriate response for a failed user creation
                }
            }
            else
            {
                modelState.AddModelError("", "You don't have permission to create this type of account.");
                return null;
            }


            //var user = new ApplicationUser
            //{
            //    UserName = RegisterUserDTO.Username,
            //    Email = RegisterUserDTO.Email,
            //    PhoneNumber = RegisterUserDTO.PhoneNumber
            //};

            ////now I will add the above new user info and create new user by the usermanager
            //var result = await userManager.CreateAsync(user, RegisterUserDTO.Password);


            //if (result.Succeeded)
            //{
            //    await userManager.AddToRolesAsync(user, RegisterUserDTO.Roles);
            //    return new UserDTO
            //    {
            //        Id = user.Id,
            //        Username = user.UserName,
            //        Token = await tokenService.GetToken(user, System.TimeSpan.FromSeconds(60)),
            //        Roles = await userManager.GetRolesAsync(user)
            //    };


            //}



            //foreach (var error in result.Errors)
            //{
            //    var errorKey = error.Code.Contains("Password") ? nameof(RegisterUserDTO.Password) :
            //        error.Code.Contains("Email") ? nameof(RegisterUserDTO.Email) :
            //        error.Code.Contains("Username") ? nameof(RegisterUserDTO.Username) :
            //        "";

            //    modelState.AddModelError(errorKey, error.Description);
            //}


            //return null;
        }

        public async Task<UserDTO> Authenticate(string username, string password)
        {
            var user = await userManager.FindByNameAsync(username);

            bool validPassword = await userManager.CheckPasswordAsync(user, password);

            if (validPassword)
            {
                return
                    new UserDTO
                    {
                        Id = user.Id,
                        Username = user.UserName,
                        Token = await tokenService.GetToken(user, System.TimeSpan.FromSeconds(60)),
                        Roles = await userManager.GetRolesAsync(user)
                    };
            }

            return null;

        }



        public async Task<UserDTO> GetUser(ClaimsPrincipal principal)
        {
            var user = await userManager.GetUserAsync(principal);

            return new UserDTO
            {
                Id = user.Id,
                Username = user.UserName,
                Token = await tokenService.GetToken(user, System.TimeSpan.FromSeconds(60)),
                Roles = await userManager.GetRolesAsync(user)
            };



        }

    }
}
