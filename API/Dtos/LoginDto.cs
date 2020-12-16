using System.ComponentModel.DataAnnotations;

namespace API.Dtos
{
    public class LoginDto
    {
        
        [Required]
        
        public string email { get; set; }
        [Required]
        public string password { get; set; }
    }
}