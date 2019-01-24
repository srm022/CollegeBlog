using System;
using System.Net.Mail;
using System.Threading.Tasks;
using Blog.Entities;
using Blog.Services.Exceptions;
using Microsoft.AspNetCore.Identity;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace Blog.Services.User
{
    public class UserService : IUserService
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly SignInManager<UserEntity> _signInManager;

        public UserService(SignInManager<UserEntity> signInManager,
            UserManager<UserEntity> userManager)
        {
            _signInManager = signInManager;
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

                    return result;
                }

                return null;
            }
            catch (FormatException e)
            {
                Console.WriteLine(e);
                throw;
            }
            catch (PasswordsNotIdenticalException e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task Logout() => await _signInManager.SignOutAsync();

        private UserEntity MapToEntity(Models.RegisterModel registerModelModel)
        {
            //CreatePasswordHash(registerModel.Password, out var passwordHash, out var passwordSalt);

            return new UserEntity
            {
                Email = registerModelModel.Email,
                DisplayName = registerModelModel.DisplayName
            };
        }

        private void AssertThatPasswordsMatch(string password, string confirmPassword)
        {
            if (password != confirmPassword)
                throw new PasswordsNotIdenticalException("Password do not match.");
        }

        private void AssertThatEmailIsInCorrectForm(string email)
        {
            var mailAddress = new MailAddress(email);
        }


        //public void Create(Model model)
        //{
        //    //if (string.IsNullOrWhiteSpace(model.Password))
        //    //    throw new LoginException("Password is required");

        //    if (CheckEmailAvailability(model.RegisterData.Email))
        //        throw new LoginException("Email \"" + model.RegisterData.Email + "\" is already taken");

        //    CreatePasswordHash(model.RegisterData.Password, out var passwordHash, out var passwordSalt);

        //    var userEntity = CreateUserEntity(model, passwordHash, passwordSalt);

        //    _userRepository.CreateUser(userEntity);
        //}

        //private Entities.Model CreateUserEntity(Model model, byte[] passwordHash, byte[] passwordSalt)
        //{
        //    return new Entities.Model
        //    {
        //        DisplayName = model.RegisterData.DisplayName,
        //        Email = model.RegisterData.Email,
        //        Password = passwordHash,
        //        PasswordSalt = passwordSalt
        //    };
        //}

        public UserEntity GetById(int id)
        {
            return null;
        }
        
        //private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        //{
        //    if (password == null) throw new ArgumentNullException("password");
        //    if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

        //    using (var hmac = new HMACSHA512())
        //    {
        //        passwordSalt = hmac.Key;
        //        passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        //    }
        //}

        //private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        //{
        //    if (password == null) throw new ArgumentNullException("password");
        //    if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
        //    if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
        //    if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

        //    using (var hmac = new HMACSHA512(storedSalt))
        //    {
        //        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        //        for (int i = 0; i < computedHash.Length; i++)
        //        {
        //            if (computedHash[i] != storedHash[i]) return false;
        //        }
        //    }

        //    return true;
        //}

        private void AssertThatLoginValuesAreProvided(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                throw new LoginException("Provided login values are empty.");
        }

        private bool CheckEmailAvailability(string email)
        {
            return false;
        }

        private UserEntity GetUserByEmail(string email)
        {
            return null;
        }
    }
}