using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RelicsAPI.Data.Entities;

namespace RelicsAPI.Data.Repositories
{
    public interface IRelicsRepository
    {
        Task<List<Relic>> GetAll(int categoryId);
        Task<Relic> GetById(int categoryId, int relicId);
        Task Create(Relic relic);
        Task Update(Relic relic);
        Task Delete(Relic relic);
    }

    public class RelicsRepository : IRelicsRepository
    {
        private readonly RelicsContext _relicsContext;

        public RelicsRepository(RelicsContext relicsContext)
        {
            _relicsContext = relicsContext;
        }
        public async Task<List<Relic>> GetAll(int categoryId)
        {
            return await _relicsContext.Relics.Where(o => o.CategoryId == categoryId).ToListAsync();
        }

        public async Task<Relic> GetById(int categoryId, int relicId)
        {
            return await _relicsContext.Relics.FirstOrDefaultAsync(o => o.CategoryId == categoryId && o.Id == relicId);
        }

        public async Task Create(Relic relic)
        {
            _relicsContext.Relics.Add(relic);

            await _relicsContext.SaveChangesAsync();
        }

        public async Task Update(Relic relic)
        {
            _relicsContext.Relics.Update(relic);

            await _relicsContext.SaveChangesAsync();
        }

        public async Task Delete(Relic relic)
        {
            _relicsContext.Relics.Remove(relic);

            await _relicsContext.SaveChangesAsync();
        }
    }
}
