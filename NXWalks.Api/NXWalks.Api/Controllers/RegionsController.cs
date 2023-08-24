using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using NXWalks.Api.Models.Domain;
using NXWalks.Api.Repositories;

namespace NXWalks.Api.Controllers
{
    [ApiController]
    [Route("Regions")]
    //  [Route("[controller]")]
    public class RegionsController : Controller
    {
        private readonly IRegionRepository _regionRepository;
        private readonly IMapper _mapper;
        public RegionsController(IRegionRepository regionRepository, IMapper mapper)
        {
            _regionRepository = regionRepository;
            _mapper = mapper;


        }
        [HttpGet]
      public async Task<IActionResult> GetAllRegions()
        { 
        
               var regions = await _regionRepository.GetAllAsync();
            //return DTO regions
            //var regionsDTO = new List<Models.DTO.Region>();

            // regions.ToList().ForEach(region =>
            // {
            // var regionDTO = new Models.DTO.Region()
            // {
            // Id = region.Id,
            // Code = region.Code,
            // Name = region.Name,
            // Area = region.Area,
            // Lat = region.Lat,
            // Long = region.Long,
            // Population = region.Population,

            // };
            // regionsDTO.Add(regionDTO);

            // });
            var regionsDTO = _mapper.Map<List<Models.DTO.Region>>(regions);
                 return Ok(regionsDTO);

        }
    }
}
