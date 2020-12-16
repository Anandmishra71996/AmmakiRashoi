using System.ComponentModel.DataAnnotations;

namespace API.Dtos
{
    public class AddressDto
    {
        [Required]
         public string FirstName { get; set; }
         [Required]
        public string LastName { get; set; }
        [Required]
        public string Street { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        [MaxLength(6)]
        [MinLength(6)]
        public string PinCode { get; set; }
    }
}