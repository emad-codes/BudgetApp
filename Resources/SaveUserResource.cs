using System.ComponentModel.DataAnnotations;

namespace Budget.API.Resources
{
    public class SaveUserResource
    {
        [Required]
        [MaxLength(30)]
        public string Alias { get; set; }
    }
}