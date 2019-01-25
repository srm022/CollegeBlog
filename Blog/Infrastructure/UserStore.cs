

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Blog.Entities;
using Blog.Helpers;
using Blog.Models.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Extensions.Internal;

namespace Blog.Infrastructure
{
    public class UserStore : IUserStore<User>, IUserPasswordStore<User>, IUserRoleStore<User>
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
 
        public Task<string> GetUserIdAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.UserId.ToString());
        }
 
        public Task<string> GetUserNameAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.DisplayName);
        }
 
        public Task SetUserNameAsync(User user, string userName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException(nameof(SetUserNameAsync));
        }
 
        public Task<string> GetNormalizedUserNameAsync(User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException(nameof(GetNormalizedUserNameAsync));
        }
 
        public Task SetNormalizedUserNameAsync(User user, string normalizedName, CancellationToken cancellationToken)
        {
            return Task.FromResult((object) null);
        }
 
        public async Task<IdentityResult> CreateAsync(User user, CancellationToken cancellationToken)
        {
            _db.Add(user);
 
            await _db.SaveChangesAsync(cancellationToken);
 
            return await Task.FromResult(IdentityResult.Success);
        }
 
        public async Task<IdentityResult> UpdateAsync(User user, CancellationToken cancellationToken)
        {
            return await Task.FromResult(IdentityResult.Success);
        }
 
        public async Task<IdentityResult> DeleteAsync(User user, CancellationToken cancellationToken)
        {
            _db.Remove(user);
             
            int i = await _db.SaveChangesAsync(cancellationToken);
 
            return await Task.FromResult(i == 1 ? IdentityResult.Success : IdentityResult.Failed());
        }

        public async Task<User> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            if (int.TryParse(userId, out int id))
            {
                return await _db.Users.FindAsync(id);
            }
            else
            {
                return await Task.FromResult((User) null);
            }
        }
 
        public async Task<User> FindByNameAsync(string email, CancellationToken cancellationToken)
        {
            return await _db.Users
                           .AsAsyncEnumerable()
                           .SingleOrDefault(p => p.Email.Equals(email, StringComparison.OrdinalIgnoreCase), cancellationToken);
        }
 
        public Task SetPasswordHashAsync(User user, string password, CancellationToken cancellationToken)
        {
            user.PasswordHash = password;
 
            return Task.FromResult((object) null);
        }
 
        public Task<string> GetPasswordHashAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PasswordHash);
        }
 
        public Task<bool> HasPasswordAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(!string.IsNullOrWhiteSpace(user.PasswordHash));
        }

        public async Task AddToRoleAsync(User user, string roleName, CancellationToken cancellationToken)
        {
            var userRole = new UserRole
            {
                UserId = user.UserId,
                RoleId = _db.Roles.Where(r => r.Name == roleName).Select(s => s.Id).Single()
            };

            _db.UserRoles.Add(userRole);

            await _db.SaveChangesAsync(cancellationToken);
        }

        public async Task RemoveFromRoleAsync(User user, string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<IList<string>> GetRolesAsync(User user, CancellationToken cancellationToken)
        {
            var userRole = _db.UserRoles.SingleOrDefault(x => x.UserId == user.UserId);

            var list = _db.Roles.Where(w => w.Id == userRole.RoleId).Select(s => s.Name).ToList();

            return list;
        }

        public async Task<bool> IsInRoleAsync(User user, string roleName, CancellationToken cancellationToken)
        {
            return await _db.UserRoles
                .AnyAsync(ur => 
                        ur.UserId == user.UserId &&
                        ur.Role.Name == roleName
                    , cancellationToken);
        }

        public async Task<IList<User>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}