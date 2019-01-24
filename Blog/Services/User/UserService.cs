using System;
using System.Linq;
using System.Threading.Tasks;
using Blog.Entities;
using Blog.Helpers;
using Microsoft.AspNetCore.Identity;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace Blog.Services.User
{
    public interface IUserService
    {
        Task<SignInResult> Authenticate(string email, string password);
        string GetUsernameById(int id);
        Task<IdentityResult> Register(Models.RegisterModel registerModel);
        Task Logout();
    }

    public class UserService : IUserService
    {
        private readonly SignInManager<UserEntity> _signInManager;
        private readonly DataContext _db;


        public UserService(SignInManager<UserEntity> signInManager, 
            DataContext db)
        {
            _signInManager = signInManager;
            _db = db;
        }

        public Task<SignInResult> Authenticate(string email, string password)
        {
            return _signInManager.PasswordSignInAsync(email,
                password, false, lockoutOnFailure: true);
        }

        public async Task<IdentityResult> Register(Models.RegisterModel registerModel)
        {
            try
            {
                if (_signInManager.UserManager.FindByNameAsync(registerModel.Email).Result == null)
                {
                    var entity = MapToEntity(registerModel);
                    var result = await _signInManager.UserManager.CreateAsync(entity, registerModel.Password);

                    return result;
                }

                return null;
            }
            catch (FormatException e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task Logout() => await _signInManager.SignOutAsync();

        private UserEntity MapToEntity(Models.RegisterModel registerModelModel)
        {
            return new UserEntity
            {
                Email = registerModelModel.Email,
                DisplayName = registerModelModel.DisplayName
            };
        }

        public string GetUsernameById(int id)
        {
            return _db.Users.Where(u => u.UserId == id).SingleOrDefault().DisplayName;
        }
    }
}