using System.Collections.Generic;
using System.Threading.Tasks;
using Budget.API.Domain.Models;
using Budget.API.Domain.Models.Queries;

namespace Budget.API.Domain.Repositories
{
    public interface IBudgetLevelRepository
    {
        Task<QueryResult<BudgetLevel>> ListAsync(BudgetLevelQuery query);
        Task AddAsync(BudgetLevel budgetLevel);
        Task<BudgetLevel> FindByIdAsync(int id);
        void Update(BudgetLevel budgetLevel);
        void Remove(BudgetLevel budgetLevel);
    }
}