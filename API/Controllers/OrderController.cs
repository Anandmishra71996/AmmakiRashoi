using System.Collections.Generic;
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
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<OrderToReturnDto>>> GetOrderForUser()
        {
            var email = HttpContext.User.RetrieveEmailFromClaimsPrincipal();
            var orders = await _orderservice.GetOrdersForUserAsync(email);
            return Ok(_mapper.Map<IReadOnlyList<Order>,IReadOnlyList<OrderToReturnDto>>(orders));
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderToReturnDto>> GetOrderByIdForUser(int id)
        {
            var email =HttpContext.User.RetrieveEmailFromClaimsPrincipal();
            var order = await _orderservice.GetOrderByIdAsync(id,email);
            if(order==null) return NotFound(new ApiResponse(404));
            return _mapper.Map<Order ,OrderToReturnDto>(order);
        }
        [HttpGet("deliveryMethods")]
        public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetDeliveryMethods()
        {
            return Ok(await _orderservice.GetDeliveryMethods());
        }
    }
}