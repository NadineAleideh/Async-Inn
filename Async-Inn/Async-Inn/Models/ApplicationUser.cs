using Microsoft.AspNetCore.Identity;

namespace Async_Inn.Models
{ //IdentityUser contains the built in proerties realated to the user such as name , email ..
    //and I created the ApplicationUser to add extra properites such as fullname, unversity....

    public class ApplicationUser : IdentityUser
    {
    }
}
