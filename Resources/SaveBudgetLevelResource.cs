using System;
using System.ComponentModel.DataAnnotations;

namespace Budget.API.Resources
{
    public class SaveBudgetLevelResource
    {
        [Required]
        [MaxLength(50)]
        public string Level0 { get; set; }
        [Required]
        [MaxLength(50)]
        public string Level1 { get; set; }
        [Required]
        [MaxLength(50)]
        public string Level2 { get; set; }
        [Required]
        [MaxLength(50)]
        public string Level3 { get; set; }
        [Required]
        [MaxLength(50)]
        public string Level4 { get; set; }
        [Required]
        [MaxLength(50)]
        public string Level5 { get; set; }
        [Required]
        [MaxLength(4)]
        public string Year { get; set; }

        [Required]
        [MaxLength(10)]
        public string Status { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int ManagerId { get; set; }
        [Required]
        public int ApproverId { get; set; }
    }
}