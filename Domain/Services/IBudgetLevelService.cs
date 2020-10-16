using System.Threading.Tasks;
using Budget.API.Domain.Models;
using Budget.API.Domain.Models.Queries;
using Budget.API.Domain.Services.Communication;

namespace Budget.API.Domain.Services
{
    public interface IBudgetLevelService
    {
        Task<QueryResult<BudgetLevel>> ListAsync(BudgetLevelQuery query);
        Task<BudgetLevelResponse> SaveAsync(BudgetLevel budgetLevel);
        Task<BudgetLevelResponse> UpdateAsync(int id, BudgetLevel budgetLevel);
        Task<BudgetLevelResponse> DeleteAsync(int id);
        
    }
}