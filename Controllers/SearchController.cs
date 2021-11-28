using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RelicsAPI.Data.DTOs.Relics;
using RelicsAPI.Data.Entities;
using RelicsAPI.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RelicsAPI.Controllers
{
    [ApiController]
    [Route("api/search")]
    public class SearchController : ControllerBase
    {
        private readonly IRelicsRepository _relicsRepository;
        private readonly ICategoriesRepository _categoriesRepository;
        private readonly IMapper _mapper;

        public SearchController(IRelicsRepository relicsRepository, IMapper mapper, ICategoriesRepository categoriesRepository)
        {
            _relicsRepository = relicsRepository;
            _categoriesRepository = categoriesRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RelicDTO>> Search(string query)
        {
            var categories = await _categoriesRepository.GetAll();

            var allRelics = new List<Relic>();
            
            foreach(var cateogry in categories)
            {
                var relics = await _relicsRepository.GetAll(cateogry.Id);
                allRelics.AddRange(relics);
            }

            var filteredRelics = allRelics.Where(r => r.Name.ToLower().Contains(query) || r.Materials.Contains(query.ToLower()));

            return filteredRelics.Select(r => _mapper.Map<RelicDTO>(r));
        }
    }
}
