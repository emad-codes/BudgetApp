namespace Budget.API.Domain.Models.Queries
{
    public class BudgetLevelQuery : Query
    {
        public int? UserId { get; set; }

        public BudgetLevelQuery(int? userId, int page, int itemsPerPage) : base(page, itemsPerPage)
        {
            UserId = userId;
        }
    }
}