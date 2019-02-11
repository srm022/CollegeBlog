using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Infrastructure.Database;
using Blog.Models.User;
using Microsoft.AspNetCore.Identity;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace Blog.Services
{
    public interface IUserService
    {
        Task<SignInResult> Authenticate(string email, string password);
        Task<IdentityResult> Register(RegisterModel registerModel);
        List<Entities.UserEntity> GetAllUsers();
        Task Logout();
        Task<int> DeleteUser(int id);
        string GetUsernameById(int id);
    }

    public class UserService : IUserService
    {
        private readonly SignInManager<Entities.UserEntity> _signInManager;
        private readonly UserManager<Entities.UserEntity> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly DataContext _db;


        public UserService(DataContext db,
            SignInManager<Entities.UserEntity> signInManager, 
            RoleManager<Role> roleManager,
            UserManager<Entities.UserEntity> userManager)
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

        public async Task<IdentityResult> Register(RegisterModel registerModel)
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
        
        public async Task<IdentityResult> AddUserToRoleAsync(Entities.UserEntity userEntity, string roleName)
        {
            var result = new IdentityResult();
            
            var role = await _roleManager.FindByNameAsync(roleName);

            if (role != null)
                result = await _signInManager.UserManager.AddToRoleAsync(userEntity, roleName);

            return result;
        }

        public async Task Logout() => await _signInManager.SignOutAsync();

        private Entities.UserEntity MapToEntity(RegisterModel registerModelModel)
        {
            return new Entities.UserEntity
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

        public List<Entities.UserEntity> GetAllUsers()
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