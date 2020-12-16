using System.ComponentModel.DataAnnotations;

namespace API.Dtos
{
    public class BasketItemDto
    {
        [Required]
       public int ProductId { get; set; }  
       [Required]
        public string ProductName { get; set; } 
        [Required]
        [Range(0.1,double.MaxValue,ErrorMessage="Price must be greater than 0 ")]
        public decimal ProductPrice { get; set; }   
        [Required]
         [Range(1,double.MaxValue,ErrorMessage="Quantity must be at least 1 ")]
        public int Quantity { get; set; }   
        [Required]
        public string State { get; set; }   
        [Required]
        public string Type { get; set; }
        [Required]
        public string PictureUrl{get;set;}

    }
}