namespace API.Dtos
{
    public class OrderDto
    {
        public string basketId { get; set; }
        public int  deliveryMethodId { get; set; }
        public AddressDto shiptoAddress { get; set; }

    }
}