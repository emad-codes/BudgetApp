using Budget.API.Domain.Models;

namespace Budget.API.Domain.Services.Communication
{
    public class BudgetLevelResponse : BaseResponse<BudgetLevel>
    {
        public BudgetLevelResponse(BudgetLevel budgetLevel) : base(budgetLevel) { }

        public BudgetLevelResponse(string message) : base(message) { }
    }
}