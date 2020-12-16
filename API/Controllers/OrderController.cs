using System.Threading.Tasks;
using API.Dtos;
using API.Errors;
using API.Extensions;
using AutoMapper;
using Core.Entities.OrderAggregate;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    public class OrderController : BaseApiController
    {
        private readonly IOrderService _orderservice;
        private readonly IMapper _mapper;
        public OrderController(IOrderService orderservice, IMapper mapper)
        {
            this._mapper = mapper;
            this._orderservice = orderservice;
        }
        [HttpPost]

        public async Task<ActionResult> CreateOrder(OrderDto orderDto)
        {
            var email = HttpContext.User.RetrieveEmailFromClaimsPrincipal();
            var address = _mapper.Map<AddressDto , Address>(orderDto.shiptoAddress);
            var order = await _orderservice.CreateOrderAsync(email,orderDto.deliveryMethodId, orderDto.basketId,address);

            if(order == null) return BadRequest(new ApiResponse(400, "Problem Creating Order"));
            return Ok(order);
        }
    }
}