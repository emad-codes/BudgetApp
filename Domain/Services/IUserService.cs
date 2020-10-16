using System.Collections.Generic;
using System.Threading.Tasks;
using Budget.API.Domain.Models;
using Budget.API.Domain.Services.Communication;

namespace Budget.API.Domain.Services
{
    public interface IUserService
    {
         Task<IEnumerable<User>> ListAsync();
         Task<UserResponse> ListAliasAsync(string alias);
         Task<UserResponse> SaveAsync (User user);
         Task<UserResponse> UpdateAsync(int id, User user);
         Task<UserResponse> DeleteAsync(int id);
         Task<UserResponse> GetAsync(int id);
    }
}