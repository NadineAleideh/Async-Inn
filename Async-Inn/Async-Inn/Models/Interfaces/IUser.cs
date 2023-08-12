using Async_Inn.Models.DTOs;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Security.Claims;

namespace Async_Inn.Models.Interfaces

{
    public interface IUser
    {
        public Task<UserDTO> Register(RegisterUserDTO registerUserDTO, ModelStateDictionary modelState, ClaimsPrincipal User);
        public Task<UserDTO> Authenticate(string username, string password);

        public Task<UserDTO> GetUser(ClaimsPrincipal principal);


    }
}
