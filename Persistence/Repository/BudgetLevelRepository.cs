using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Budget.API.Domain.Models;
using Budget.API.Domain.Models.Queries;
using Budget.API.Domain.Repositories;
using Budget.API.Persistence.Contexts;

namespace Budget.API.Persistence.Repositories
{
	public class BudgetLevelRepository : BaseRepository, IBudgetLevelRepository
	{
		public BudgetLevelRepository(AppDbContext context) : base(context) { }

		public async Task<QueryResult<BudgetLevel>> ListAsync(BudgetLevelQuery query)
		{
			IQueryable<BudgetLevel> queryable = _context.BudgetLevels
													.Include(p => p.User)
													.AsNoTracking();

			// AsNoTracking tells EF Core it doesn't need to track changes on listed entities. Disabling entity
			// tracking makes the code a little faster
			if (query.UserId.HasValue && query.UserId > 0)
			{
				queryable = queryable.Where(p => p.UserId == query.UserId);
			}

			// Here I count all items present in the database for the given query, to return as part of the pagination data.
			int totalItems = await queryable.CountAsync();

			// Here I apply a simple calculation to skip a given number of items, according to the current page and amount of items per page,
			// and them I return only the amount of desired items. The methods "Skip" and "Take" do the trick here.
			List<BudgetLevel> BudgetLevels = await queryable.Skip((query.Page - 1) * query.ItemsPerPage)
													.Take(query.ItemsPerPage)
													.ToListAsync();

			// Finally I return a query result, containing all items and the amount of items in the database (necessary for client-side calculations ).
			return new QueryResult<BudgetLevel>
			{
				Items = BudgetLevels,
				TotalItems = totalItems,
			};
		}

		public async Task<BudgetLevel> FindByIdAsync(int id)
		{
			return await _context.BudgetLevels
								 .Include(p => p.User)
								 .FirstOrDefaultAsync(p => p.Id == id); // Since Include changes the method's return type, we can't use FindAsync
		}

		public async Task AddAsync(BudgetLevel BudgetLevel)
		{
			await _context.BudgetLevels.AddAsync(BudgetLevel);
		}

		public void Update(BudgetLevel BudgetLevel)
		{
			_context.BudgetLevels.Update(BudgetLevel);
		}

		public void Remove(BudgetLevel BudgetLevel)
		{
			_context.BudgetLevels.Remove(BudgetLevel);
		}
	}
}