using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RelicsAPI.Auth.Model;
using RelicsAPI.Data.DTOs.Orders;
using RelicsAPI.Data.Entities;
using RelicsAPI.Data.Repositories;

namespace RelicsAPI.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersRepository _ordersRepository;
        private readonly IAuthorizationService _authorizationService;
        private readonly IMapper _mapper;

        public OrdersController(IOrdersRepository ordersRepository, IMapper mapper, IAuthorizationService authorizationService)
        {
            _ordersRepository = ordersRepository;
            _authorizationService = authorizationService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("all")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IEnumerable<OrderDTO>> GetAll()
        {
            var orders = await _ordersRepository.GetAll();

            return orders.Select(o => _mapper.Map<OrderDTO>(o));
        }

        [HttpGet]
        [Authorize(Roles = UserRoles.Customer)]
        public async Task<IEnumerable<OrderDTO>> GetAllCurrentUser()
        {
            var userId = (User.Identity as ClaimsIdentity).Claims.FirstOrDefault(c => c.Type == CustomClaims.UserId).Value;

            var orders = await _ordersRepository.GetAllCurrentUser(userId);

            return orders.Select(o => _mapper.Map<OrderDTO>(o));
        }

        [HttpGet("{orderId}")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<ActionResult<OrderDTO>> GetById(int orderId)
        {
            var order = await _ordersRepository.GetById(orderId);

            if (order == null)
                return NotFound($"Order with id {orderId} does not exist");

            return Ok(_mapper.Map<OrderDTO>(order));
        }
        
        [HttpPost]
        [Authorize(Roles = UserRoles.Customer)]
        public async Task<ActionResult<OrderDTO>> Create(CreateOrderDTO orderDTO)
        {
            var order = _mapper.Map<Order>(orderDTO);
            var userId = (User.Identity as ClaimsIdentity).Claims.FirstOrDefault(c => c.Type == CustomClaims.UserId).Value;

            order.UserId = userId;

            await _ordersRepository.Create(order);

            return Created($"/api/orders/{order.Id}", _mapper.Map<OrderDTO>(order));
        }

        [HttpPut("{orderId}")]
        [Authorize(Roles = UserRoles.Customer)]
        public async Task<ActionResult<OrderDTO>> Update(int orderId, UpdateOrderDTO orderDTO)
        {
            var oldOrder = await _ordersRepository.GetById(orderId);

            if (oldOrder == null)
                return NotFound($"Order with id {orderId} was not found");

            var authorizationResult = await _authorizationService.AuthorizeAsync(User, oldOrder, PolicyNames.SameUser);

            if (!authorizationResult.Succeeded)
                return Forbid(); // grazina 403, bet galima naudoti 404, kad neisduoti, jog toks resursas jau yra, 401 - kai blogas/nelegalus tokenas

            _mapper.Map(orderDTO, oldOrder);

            await _ordersRepository.Update(oldOrder);

            return Ok(_mapper.Map<OrderDTO>(oldOrder));
        }

        [HttpDelete("{orderId}")]
        [Authorize(Roles = UserRoles.Customer)]
        public async Task<ActionResult> Delete(int orderId)
        {
            var order = await _ordersRepository.GetById(orderId);

            if (order == null)
                return NotFound($"Order with id {orderId} was not found");

            var authorizationResult = await _authorizationService.AuthorizeAsync(User, order, PolicyNames.SameUser);

            if (!authorizationResult.Succeeded)
                return Forbid(); // grazina 403, bet galima naudoti 404, kad neisduoti, jog toks resursas jau yra, 401 - kai blogas/nelegalus tokenas

            await _ordersRepository.Delete(order);

            return NoContent();
        }
    }
}
