using Async_Inn.Models.DTOs;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Async_Inn.Models.Interfaces

{
    public interface IUser
    {
        public Task<UserDTO> Register(RegisterUserDTO registerUserDTO, ModelStateDictionary modelState);
        public Task<UserDTO> Authenticate(string username, string password);

    }
}
