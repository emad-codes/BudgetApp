using System;

namespace Budget.API.Domain.Models
{
    public class BudgetLevel
    {
        public int Id { get; set; }
        public string Level0 {get;set;}
        public string Level1 {get;set;}
        public string Level2 {get;set;}
        public string Level3 {get;set;}
        public string Level4 {get;set;}
        public string Level5 {get;set;}
        public string Year {get;set;}
        public string Status {get;set;}
        public DateTime CreatedAt { get; set; } 
        public DateTime ModifiedAt { get; set; } 
        
        public int ManagerId { get; set; }
        public int ApproverId { get; set; }

        public int UserId { get; set; }
        public User User  {get;set;}
   
    }
    
}