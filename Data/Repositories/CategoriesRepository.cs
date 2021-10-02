using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RelicsAPI.Data.Entities;

namespace RelicsAPI.Data.Repositories
{
    public interface ICategoriesRepository
    {
        Task<List<Category>> GetAll();
        Task<Category> GetById(int categoryId);
        Task Create(Category category);
        Task Update(Category category);
        Task Delete(Category category);
    }

    public class CategoriesRepository : ICategoriesRepository
    {
        private readonly RelicsContext _relicsContext;

        public CategoriesRepository(RelicsContext relicsContext)
        {
            _relicsContext = relicsContext;
        }

        public async Task<List<Category>> GetAll()
        {
            return await _relicsContext.Categories.ToListAsync();
        }

        public async Task<Category> GetById(int categoryId)
        {
            return await _relicsContext.Categories.FirstOrDefaultAsync(o => o.Id == categoryId);
        }

        public async Task Create(Category category)
        {
            _relicsContext.Categories.Add(category);

            await _relicsContext.SaveChangesAsync();
        }

        public async Task Update(Category category)
        {
            _relicsContext.Categories.Update(category);

            await _relicsContext.SaveChangesAsync();
        }

        public async Task Delete(Category category)
        {
            _relicsContext.Categories.Remove(category);

            await _relicsContext.SaveChangesAsync();
        }
    }
}
