

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Blog.Entities;
using Blog.Infrastructure.Database;
using Blog.Models.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Extensions.Internal;

namespace Blog.Infrastructure
{
    public class UserStore : IUserStore<UserEntity>, IUserPasswordStore<UserEntity>, IUserRoleStore<UserEntity>
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
 
        public Task<string> GetUserIdAsync(UserEntity userEntity, CancellationToken cancellationToken)
        {
            return Task.FromResult(userEntity.UserId.ToString());
        }
 
        public Task<string> GetUserNameAsync(UserEntity userEntity, CancellationToken cancellationToken)
        {
            return Task.FromResult(userEntity.DisplayName);
        }
 
        public Task SetUserNameAsync(UserEntity userEntity, string userName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException(nameof(SetUserNameAsync));
        }
 
        public Task<string> GetNormalizedUserNameAsync(UserEntity userEntity, CancellationToken cancellationToken)
        {
            throw new NotImplementedException(nameof(GetNormalizedUserNameAsync));
        }
 
        public Task SetNormalizedUserNameAsync(UserEntity userEntity, string normalizedName, CancellationToken cancellationToken)
        {
            return Task.FromResult((object) null);
        }
 
        public async Task<IdentityResult> CreateAsync(UserEntity userEntity, CancellationToken cancellationToken)
        {
            _db.Add(userEntity);
 
            await _db.SaveChangesAsync(cancellationToken);
 
            return await Task.FromResult(IdentityResult.Success);
        }
 
        public async Task<IdentityResult> UpdateAsync(UserEntity userEntity, CancellationToken cancellationToken)
        {
            return await Task.FromResult(IdentityResult.Success);
        }
 
        public async Task<IdentityResult> DeleteAsync(UserEntity userEntity, CancellationToken cancellationToken)
        {
            _db.Remove(userEntity);
             
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
 
        public Task SetPasswordHashAsync(UserEntity userEntity, string password, CancellationToken cancellationToken)
        {
            userEntity.PasswordHash = password;
 
            return Task.FromResult((object) null);
        }
 
        public Task<string> GetPasswordHashAsync(UserEntity userEntity, CancellationToken cancellationToken)
        {
            return Task.FromResult(userEntity.PasswordHash);
        }
 
        public Task<bool> HasPasswordAsync(UserEntity userEntity, CancellationToken cancellationToken)
        {
            return Task.FromResult(!string.IsNullOrWhiteSpace(userEntity.PasswordHash));
        }

        public async Task AddToRoleAsync(UserEntity userEntity, string roleName, CancellationToken cancellationToken)
        {
            var userRole = new UserRole
            {
                UserId = userEntity.UserId,
                RoleId = _db.Roles.Where(r => r.Name == roleName).Select(s => s.Id).Single()
            };

            _db.UserRoles.Add(userRole);

            await _db.SaveChangesAsync(cancellationToken);
        }

        public async Task RemoveFromRoleAsync(UserEntity userEntity, string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<IList<string>> GetRolesAsync(UserEntity userEntity, CancellationToken cancellationToken)
        {
            var userRole = _db.UserRoles.SingleOrDefault(x => x.UserId == userEntity.UserId);

            var list = _db.Roles.Where(w => w.Id == userRole.RoleId).Select(s => s.Name).ToList();

            return list;
        }

        public async Task<bool> IsInRoleAsync(UserEntity userEntity, string roleName, CancellationToken cancellationToken)
        {
            return await _db.UserRoles
                .AnyAsync(ur => 
                        ur.UserId == userEntity.UserId &&
                        ur.Role.Name == roleName
                    , cancellationToken);
        }

        public async Task<IList<UserEntity>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}