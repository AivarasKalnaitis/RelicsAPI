using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RelicsAPI.Data.DTOs.Categories;
using RelicsAPI.Data.Entities;
using RelicsAPI.Data.Repositories;

namespace RelicsAPI.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoriesController: ControllerBase
    {
        private readonly ICategoriesRepository _categoriesRepository;
        private readonly IMapper _mapper;

        public CategoriesController(ICategoriesRepository categoriesRepository, IMapper mapper)
        {
            _categoriesRepository = categoriesRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<CategoryDTO>> GetAll()
        {
            return (await _categoriesRepository.GetAll()).Select(o => _mapper.Map<CategoryDTO>(o));
        }

        [HttpGet("{categoryId}")]
        public async Task<ActionResult<CategoryDTO>> GetById(int categoryId)
        {
            var category = await _categoriesRepository.GetById(categoryId);

            if (category == null)
                return NotFound();

            return Ok(_mapper.Map<CategoryDTO>(category));
        }

        [HttpPost]
        public async Task<ActionResult<CategoryDTO>> Create(CreateCategoryDTO categoryDTO)
        {
            var category = _mapper.Map<Category>(categoryDTO);

            await _categoriesRepository.Create(category);

            return Created($"/api/categories/{category.Id}", _mapper.Map<CategoryDTO>(category));
        }

        [HttpPut("{categoryId}")]
        public async Task<ActionResult<CategoryDTO>> Update(int categoryId, UpdateCategoryDTO categoryDTO)
        {
            var category = await _categoriesRepository.GetById(categoryId);

            if (category == null)
                return NotFound();

            _mapper.Map(categoryDTO, category);

            await _categoriesRepository.Update(category);

            return Ok(_mapper.Map<CategoryDTO>(category));
        }

        [HttpDelete("{categoryId}")]
        public async Task<ActionResult> Delete(int categoryId)
        {
            var category = await _categoriesRepository.GetById(categoryId);

            if (category == null)
                return NotFound();

            await _categoriesRepository.Delete(category);

            return NoContent();
        }
    }
}
