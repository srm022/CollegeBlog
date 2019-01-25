using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Entities;
using Blog.Helpers;
using Blog.Models.Article;
using Blog.Models.User;
using Microsoft.AspNetCore.Identity;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace Blog.Services.User
{
    public interface IUserService
    {
        Task<SignInResult> Authenticate(string email, string password);
        Task<IdentityResult> Register(Models.RegisterModel registerModel);
        List<Entities.User> GetAllUsers();
        Task Logout();
        Task<int> DeleteUser(int id);
        string GetUsernameById(int id);
    }

    public class UserService : IUserService
    {
        private readonly SignInManager<Entities.User> _signInManager;
        private readonly UserManager<Entities.User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly DataContext _db;


        public UserService(DataContext db,
            SignInManager<Entities.User> signInManager, 
            RoleManager<Role> roleManager,
            UserManager<Entities.User> userManager)
        {
            _db = db;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _userManager = userManager;
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
                    await AddUserToRoleAsync(entity, "author");
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
        
        public async Task<IdentityResult> AddUserToRoleAsync(Entities.User user, string roleName)
        {
            var result = new IdentityResult();
            
            var role = await _roleManager.FindByNameAsync(roleName);

            if (role != null)
                result = await _signInManager.UserManager.AddToRoleAsync(user, roleName);

            return result;
        }

        public async Task Logout() => await _signInManager.SignOutAsync();

        private Entities.User MapToEntity(Models.RegisterModel registerModelModel)
        {
            return new Entities.User
            {
                Email = registerModelModel.Email,
                DisplayName = registerModelModel.DisplayName
            };
        }

        public string GetUsernameById(int id)
        {
            var user = _db.Users.SingleOrDefault(u => u.UserId == id);

            if (user == null)
                return "Someone";

            return user.DisplayName;
        }

        public List<Entities.User> GetAllUsers()
        {

            var users = _db.Users.ToList();

            return users;
        }

        public async Task<int> DeleteUser(int id)
        {
            var user = _db.Users.SingleOrDefault(u => u.UserId == id);

            _db.Users.Remove(user ?? throw new InvalidOperationException());
            return await _db.SaveChangesAsync();
        }
    }
}