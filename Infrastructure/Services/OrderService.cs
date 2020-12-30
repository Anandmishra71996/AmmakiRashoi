using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Entities.OrderAggregate;
using Core.Interfaces;
using Core.Specifications;

namespace Infrastructure.Services
{
    public class OrderService : IOrderService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IUnitOfWork _unitOfWork;
        public OrderService(IBasketRepository basketRepository, IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
            this._basketRepository = basketRepository;

        }

        public async Task<Order> CreateOrderAsync(string buyerEmail, int deliveryMethodid, string basketId, Address shippingAddress)
        {
            //Get the basket from the repo
            var basket = await _basketRepository.GetBasketAsync(basketId);
            //get Items from product Repo
            var items = new List<OrderItem>();
            foreach (var item in basket.Items)
            {
                var ProductItem = await _unitOfWork.Repository<Product>().GetByIdAsync(item.ProductId);
                var itemOrdered = new ProductItemOrdered(ProductItem.Id, ProductItem.Name,
                                                         ProductItem.PictureUrl);
                var orderItem = new OrderItem(itemOrdered, item.Quantity, ProductItem.Price);
                items.Add(orderItem);

            }
            //get deliveryMethod from Repo
            var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(deliveryMethodid);
            //Creating Subtotal
            var subTotal = items.Sum(item => item.Price * item.Quantity);
            //createOrder
            var order = new Order(buyerEmail, shippingAddress, deliveryMethod, items, subTotal);
            _unitOfWork.Repository<Order>().Add(order);
            //Save to DB
            var result = await _unitOfWork.Complete();
            if(result<1) return null;
            //Delete Basket
            await _basketRepository.DeleteBasketAsync(basketId);
            //return order
            return order;
        }

        public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethods()
        {
            return await _unitOfWork.Repository<DeliveryMethod>().ListAllAsync();
        }

        public async Task<Order> GetOrderByIdAsync(int id, string buyerEmail)
        {
           var spec = new OrderWithItemsandOrderingSpecification(id,buyerEmail);
           return await _unitOfWork.Repository<Order>().GetEntityWithSpec(spec);
        }

        public async Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
        {
            var spec = new OrderWithItemsandOrderingSpecification(buyerEmail);
            return  await _unitOfWork.Repository<Order>().ListAsync(spec);
        }
    }
}