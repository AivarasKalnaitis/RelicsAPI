using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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
        //private readonly IRelicsRepository _relicsRepository;
        private readonly IMapper _mapper;

        public OrdersController(IOrdersRepository ordersRepository, IMapper mapper)
        {
            _ordersRepository = ordersRepository;
            //_relicsRepository = relicsRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<OrderDTO>> GetAll()
        {
            var orders = await _ordersRepository.GetAll();

            return orders.Select(o => _mapper.Map<OrderDTO>(o));
        }

        [HttpGet("{orderId}")]
        public async Task<ActionResult<OrderDTO>> GetById(int orderId)
        {
            var order = await _ordersRepository.GetById(orderId);

            if (order == null)
                return NotFound($"Order with id {orderId} does not exist");

            return Ok(_mapper.Map<OrderDTO>(order));
        }

        [HttpPost]
        public async Task<ActionResult<OrderDTO>> Create(CreateOrderDTO orderDTO)
        {
            var order = _mapper.Map<Order>(orderDTO);
            //var randomRelic = (await _relicsRepository.GetAll(1)).First();

            //order.Relics.Add(randomRelic);

            await _ordersRepository.Create(order);

            return Created($"/api/orders/{order.Id}", _mapper.Map<OrderDTO>(order));
        }

        [HttpPut("{orderId}")]
        public async Task<ActionResult<OrderDTO>> Update(int orderId, UpdateOrderDTO orderDTO)
        {
            var oldOrder = await _ordersRepository.GetById(orderId);

            if (oldOrder == null)
                return NotFound($"Order with id {orderId} was not found");

            _mapper.Map(orderDTO, oldOrder);

            await _ordersRepository.Update(oldOrder);

            return Ok(_mapper.Map<OrderDTO>(oldOrder));
        }

        [HttpDelete("{orderId}")]
        public async Task<ActionResult> Delete(int orderId)
        {
            var order = await _ordersRepository.GetById(orderId);

            if (order == null)
                return NotFound($"Order with id {orderId} was not found");

            await _ordersRepository.Delete(order);

            return NoContent();
        }
    }
}
