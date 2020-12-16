using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Entities.OrderAggregate;
using Core.Interfaces;

namespace Infrastructure.Services
{
    public class OrderService : IOrderService
    {
        private readonly IGenericRepository<Order> _orderRepo;
        private readonly IGenericRepository<DeliveryMethod> _dmRepo;
        private readonly IGenericRepository<Product> _productRepo;
        private readonly IBasketRepository _basketRepository;
        public OrderService(IGenericRepository<Order> orderRepo, IGenericRepository<DeliveryMethod> dmRepo,
        IGenericRepository<Product> productRepo, IBasketRepository basketRepository)
        {
            this._basketRepository = basketRepository;
            this._productRepo = productRepo;
            this._dmRepo = dmRepo;
            this._orderRepo = orderRepo;
        }

        public async Task<Order> CreateOrderAsync(string buyerEmail, int deliveryMethodid, string basketId, Address shippingAddress)
        {
            //Get the basket from the repo
            var basket = await _basketRepository.GetBasketAsync(basketId);
            //get Items from product Repo
            var items = new List<OrderItem>();
            foreach (var item in basket.Items)
            {
                var ProductItem = await _productRepo.GetByIdAsync(item.ProductId);
                var itemOrdered = new ProductItemOrdered(ProductItem.Id, ProductItem.Name,
                                                         ProductItem.PictureUrl);
                var orderItem= new OrderItem(itemOrdered,item.Quantity, ProductItem.Price);
                items.Add(orderItem);

            }
            //get deliveryMethod from Repo
            var deliveryMethod= await _dmRepo.GetByIdAsync(deliveryMethodid);
            //Creating Subtotal
            var subTotal = items.Sum(item => item.Price* item.Quantity);
            //createOrder
            var order = new Order(buyerEmail,shippingAddress,deliveryMethod,items,subTotal);
            //Save to DB


            //return order
            return order;
        }

        public Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethods()
        {
            throw new System.NotImplementedException();
        }

        public Task<Order> GetOrderByIdAsync(int id, string buyerEmail)
        {
            throw new System.NotImplementedException();
        }

        public Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
        {
            throw new System.NotImplementedException();
        }
    }
}