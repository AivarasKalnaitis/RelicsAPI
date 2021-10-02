using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RelicsAPI.Data.DTOs.Orders;
using RelicsAPI.Data.Entities;

namespace RelicsAPI.Data.Repositories
{
    public interface IOrdersRepository
    {
        Task<List<Order>> GetAll();
        Task<Order> GetById(int orderId);
        Task Create(Order order);
        Task Update(Order order);
        Task Delete(Order order);
    }

    public class OrdersRepository : IOrdersRepository
    {
        private readonly RelicsContext _relicsContext;
        private readonly IMapper _mapper;

        public OrdersRepository(RelicsContext relicsContext, IMapper mapper)
        {
            _relicsContext = relicsContext;
            _mapper = mapper;
        }

        public async Task<List<Order>> GetAll()
        {
            return await _relicsContext.Orders.ToListAsync();
        }

        public async Task<Order> GetById(int orderId)
        {
            return await _relicsContext.Orders.FirstOrDefaultAsync(o => o.Id == orderId);
        }

        public async Task Create(Order order)
        {
            _relicsContext.Orders.Add(order);

            await _relicsContext.SaveChangesAsync();
        }

        public async Task Update(Order order)
        {
            _relicsContext.Orders.Update(order);

            await _relicsContext.SaveChangesAsync();
        }

        public async Task Delete(Order order)
        {
            _relicsContext.Orders.Remove(order);

            await _relicsContext.SaveChangesAsync();
        }
    }
}
