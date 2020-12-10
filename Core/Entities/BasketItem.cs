namespace Core.Entities
{
    public class BasketItem
    {
        public int ProductId { get; set; }  
        public string ProductName { get; set; } 
        public decimal ProductPrice { get; set; }   
        public int Quantity { get; set; }   
        public string State { get; set; }   
        public string Type { get; set; }
        public string PictureUrl{get;set;}

    }
}