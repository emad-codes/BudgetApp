using System.Collections.Generic;
using System.Threading.Tasks;
using Budget.API.Domain.Models;

namespace Budget.API.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> ListAsync();
        Task AddAsync(User user);
        Task<User> FindByIdAsync(int id);
        Task<User> FindByAliasAsync(string alias);
        void Update(User user);
        void Remove(User user);
    }
}