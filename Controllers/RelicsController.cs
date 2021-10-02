using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RelicsAPI.Data.DTOs.Relics;
using RelicsAPI.Data.Entities;
using RelicsAPI.Data.Repositories;

namespace RelicsAPI.Controllers
{
    [ApiController]
    [Route("api/categories/{categoryId}/relics")]
    public class RelicsController : ControllerBase
    {
        private readonly ICategoriesRepository _categoriesRepository;
        private readonly IRelicsRepository _relicsRepository;
        private readonly IMapper _mapper;

        public RelicsController(ICategoriesRepository categoriesRepository, IRelicsRepository relicsRepository, IMapper mapper)
        {
            _categoriesRepository = categoriesRepository;
            _relicsRepository = relicsRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<RelicDTO>> GetAll(int categoryId)
        {
            var relics = await _relicsRepository.GetAll(categoryId);

            return relics.Select(o => _mapper.Map<RelicDTO>(o));
        }

        [HttpGet("{relicId}")]
        public async Task<ActionResult<RelicDTO>> GetById(int categoryId, int relicId)
        {
            var relic = await _relicsRepository.GetById(categoryId, relicId);

            if (relic == null)
                return NotFound();

            return Ok(_mapper.Map<RelicDTO>(relic));
        }

        [HttpPost]
        public async Task<ActionResult<RelicDTO>> Create(int categoryId, CreateRelicDTO relicDTO)
        {
            var category = await _categoriesRepository.GetById(categoryId);

            if (category == null)
                return NotFound($"Couldn't find category with id {categoryId}");

            var relic = _mapper.Map<Relic>(relicDTO);

            relic.CategoryId = categoryId;

            await _relicsRepository.Create(relic);

            return Created($"/api/categories/{categoryId}/relics/{relic.Id}", _mapper.Map<RelicDTO>(relic));
        }

        [HttpPut("{relicId}")]
        public async Task<ActionResult<RelicDTO>> Update(int categoryId, int relicId, UpdateRelicDTO relicDTO)
        {
            var category = await _categoriesRepository.GetById(categoryId);

            if (category == null)
                return NotFound($"Category with id {categoryId} was not found");

            var oldRelic = await _relicsRepository.GetById(categoryId, relicId);

            if (oldRelic == null)
                return NotFound($"Relic with id {relicId} was not found");

            _mapper.Map(relicDTO, oldRelic);

            await _relicsRepository.Update(oldRelic);

            return Ok(_mapper.Map<RelicDTO>(oldRelic));
        }

        [HttpDelete("{relicId}")]
        public async Task<ActionResult> Delete(int categoryId, int relicId)
        {
            var relic = await _relicsRepository.GetById(categoryId, relicId);

            if (relic == null)
                return NotFound($"Relic with id {relicId} was not found");

            await _relicsRepository.Delete(relic);

            return NoContent();
        }
    }
}
