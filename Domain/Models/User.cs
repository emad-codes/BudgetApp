using System.Collections.Generic;

namespace Budget.API.Domain.Models
{
    public class User
    {
        public int Id {get;set;}
        public string Alias { get; set; }

        public IList<BudgetLevel> BudgetLevels { get; set; } = new List<BudgetLevel>();
        
    }
    
}