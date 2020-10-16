using System.Collections.Generic;
using System.Threading.Tasks;
using Budget.API.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Budget.API.Persistence.Repositories;
using Budget.API.Domain.Models;
using Budget.API.Persistence.Contexts;
using System.Linq;

namespace Budget.API.Persistence.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<User>> ListAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> FindByAliasAsync(string alias)
        {
           var user = await Task.Run<User>(() => _context.Users.FirstOrDefault(x => x.Alias == alias));
           return user;
        }
        

        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
        }

        public async Task<User> FindByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public void Update(User user)
        {
            _context.Users.Update(user);
        }

        public void Remove(User user)
        {
            _context.Users.Remove(user);
        }
    }
}