using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Blog.Entities;
using Blog.Helpers;
using Blog.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Extensions.Internal;

namespace Blog.Infrastructure
{
    public class UserStore : IUserStore<UserEntity>, IUserPasswordStore<UserEntity>
    {
        private readonly DataContext _db;
 
        public UserStore(DataContext db)
        {
            _db = db;
        }
 
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
 
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db?.Dispose();
            }
        }
 
        public Task<string> GetUserIdAsync(UserEntity user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Id.ToString());
        }
 
        public Task<string> GetUserNameAsync(UserEntity user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Email);
        }
 
        public Task SetUserNameAsync(UserEntity user, string userName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException(nameof(SetUserNameAsync));
        }
 
        public Task<string> GetNormalizedUserNameAsync(UserEntity user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException(nameof(GetNormalizedUserNameAsync));
        }
 
        public Task SetNormalizedUserNameAsync(UserEntity user, string normalizedName, CancellationToken cancellationToken)
        {
            return Task.FromResult((object) null);
        }
 
        public async Task<IdentityResult> CreateAsync(UserEntity user, CancellationToken cancellationToken)
        {
            _db.Add(user);
 
            await _db.SaveChangesAsync(cancellationToken);
 
            return await Task.FromResult(IdentityResult.Success);
        }
 
        public Task<IdentityResult> UpdateAsync(UserEntity user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException(nameof(UpdateAsync));
        }
 
        public async Task<IdentityResult> DeleteAsync(UserEntity user, CancellationToken cancellationToken)
        {
            _db.Remove(user);
             
            int i = await _db.SaveChangesAsync(cancellationToken);
 
            return await Task.FromResult(i == 1 ? IdentityResult.Success : IdentityResult.Failed());
        }
 
        public async Task<UserEntity> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            if (int.TryParse(userId, out int id))
            {
                return await _db.Users.FindAsync(id);
            }
            else
            {
                return await Task.FromResult((UserEntity) null);
            }
        }
 
        public async Task<UserEntity> FindByNameAsync(string email, CancellationToken cancellationToken)
        {
            return await _db.Users
                           .AsAsyncEnumerable()
                           .SingleOrDefault(p => p.Email.Equals(email, StringComparison.OrdinalIgnoreCase), cancellationToken);
        }
 
        public Task SetPasswordHashAsync(UserEntity user, string password, CancellationToken cancellationToken)
        {
            user.PasswordHash = password;
 
            return Task.FromResult((object) null);
        }
 
        public Task<string> GetPasswordHashAsync(UserEntity user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PasswordHash);
        }
 
        public Task<bool> HasPasswordAsync(UserEntity user, CancellationToken cancellationToken)
        {
            return Task.FromResult(!string.IsNullOrWhiteSpace(user.PasswordHash));
        }
    }
}