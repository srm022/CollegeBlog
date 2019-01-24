using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Blog.Services.User
{
    public interface IUserService
    {
        Task<SignInResult> Authenticate(string email, string password);
        Entities.UserEntity GetById(int id);
        Task<IdentityResult> Register(Models.RegisterModel registerModel);
        Task Logout();
    }
}