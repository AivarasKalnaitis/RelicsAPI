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
        [Route("api/categories/{categoryId}/relics")]
        public async Task<IEnumerable<RelicDTO>> GetAll(int categoryId)
        {
            var relics = await _relicsRepository.GetAll(categoryId);

            return relics.Select(o => _mapper.Map<RelicDTO>(o));
        }

        [HttpGet]
        [Route("api/categories/{categoryId}/relics/{relicId}")]
        public async Task<ActionResult<RelicDTO>> GetById(int categoryId, int relicId)
        {
            var relic = await _relicsRepository.GetById(categoryId, relicId);

            if (relic == null)
                return NotFound();

            return Ok(_mapper.Map<RelicDTO>(relic));
        }

        //[HttpGet]
        //[Route("api/relics/all")]
        //public async Task<IEnumerable<RelicDTO>> GetAllFromAllCategories(string sortOrder)
        //{
        //    var categories = await _categoriesRepository.GetAll();

        //    var allRelics = new List<Relic>();

        //    foreach (var category in categories)
        //    {
        //        var relics = await _relicsRepository.GetAll(category.Id);
        //        allRelics.AddRange(relics);
        //    }

        //    switch (sortOrder)
        //    {
        //        case "asc(name)":
        //            allRelics = allRelics.OrderBy(r => r.Name).ToList();
        //            break;
        //        case "desc(name)":
        //            allRelics = allRelics.OrderByDescending(r => r.Name).ToList();
        //            break;
        //        case "asc(price)":
        //            allRelics = allRelics.OrderBy(r => r.Price).ToList();
        //            break;
        //        case "desc(price)":
        //            allRelics = allRelics.OrderByDescending(r => r.Price).ToList();
        //            break;
        //    }

        //    return allRelics.Select(o => _mapper.Map<RelicDTO>(o));
        //}

        [HttpPost]
        [Route("api/categories/{categoryId}/relics")]
        public async Task<ActionResult<RelicDTO>> Create(int categoryId, CreateRelicDTO relicDTO)
        {
            var category = await _categoriesRepository.GetById(categoryId);

            if (category == null)
                return NotFound($"Couldn't find category with id {categoryId}");

            var relic = _mapper.Map<Relic>(relicDTO);

            if (string.IsNullOrEmpty(relic.Creator))
                relic.Creator = "Unknown";

            if (string.IsNullOrEmpty(relic.YearMade))
                relic.YearMade = "Unknown";

            relic.CategoryId = categoryId;

            await _relicsRepository.Create(relic);

            return Created($"/api/categories/{categoryId}/relics/{relic.Id}", _mapper.Map<RelicDTO>(relic));
        }

        [HttpPut]
        [Route("api/categories/{categoryId}/relics/{relicId}")]
        public async Task<ActionResult<RelicDTO>> Update(int categoryId, int relicId, UpdateRelicDTO relicDTO)
        {
            var category = await _categoriesRepository.GetById(categoryId);

            if (category == null)
                return NotFound($"Category with id {categoryId} was not found");

            var oldRelic = await _relicsRepository.GetById(categoryId, relicId);

            if (oldRelic == null)
                return NotFound($"Relic with id {relicId} was not found");

            _mapper.Map(relicDTO, oldRelic);

            if (string.IsNullOrEmpty(oldRelic.Creator))
                oldRelic.Creator = "Unknown";

            if (string.IsNullOrEmpty(oldRelic.YearMade))
                oldRelic.YearMade = "Unknown";

            await _relicsRepository.Update(oldRelic);

            return Ok(_mapper.Map<RelicDTO>(oldRelic));
        }

        [HttpDelete]
        [Route("api/categories/{categoryId}/relics/{relicId}")]
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
