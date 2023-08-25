using AutoMapper;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using NXWalks.Api.Models.Domain;
using NXWalks.Api.Models.DTO;
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
        [HttpGet]
        [Route("{id:Guid}")]
        [ActionName("getRegionById")]
        public async Task<IActionResult> getRegionById(Guid id)
        {
            var region = await _regionRepository.GetById(id);
            if(region == null)
            {
                return NotFound();
            }
            var regionDTO = _mapper.Map<Models.DTO.Region>(region);
            return Ok(regionDTO);

        }
        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult>AddRegionAsync(Models.DTO.AddRegionRequest addRegionRequest)
        {
            //Request to domain model
            var region = new Models.Domain.Region()
            {
                Code = addRegionRequest.Code,
                Area = addRegionRequest.Area,
                Lat = addRegionRequest.Lat,
                Long = addRegionRequest.Long,
                Name = addRegionRequest.Name,
                Population = addRegionRequest.Population,
            };
            //pass details to repository
            region = await _regionRepository.AddRegion(region);
            //convert back to dto
            var RegionDTO = new Models.DTO.Region
            {
                Id = region.Id,
                Code = region.Code,
                Area = region.Area,
                Lat = region.Lat,
                Long = region.Long,
                Name = region.Name,
                Population = region.Population,

            };
            return CreatedAtAction(nameof(getRegionById), new {id = RegionDTO.Id}, RegionDTO);
        }
        [HttpDelete]
        [Route("Delete/{id:guid}")]
        public async Task<IActionResult> DeleteRegionData(Guid id)
        {
            //Get region from the database
           var region = await _regionRepository.DeleteRegion(id);
            //If null NotFound
            if (region == null)
            {
                return NotFound();
            }

            //Convert resopnse back to DTO 

                var RegionDTO = new Models.DTO.Region
                {
                    Id = region.Id,
                    Code = region.Code,
                    Area = region.Area,
                    Lat = region.Lat,
                    Long = region.Long,
                    Name = region.Name,
                    Population = region.Population,

                };
            //Response bank to client
            return Ok(RegionDTO);

        }
        [HttpPut]
        [Route("Update/{id:guid}")]
        public async Task<IActionResult> UpdateRegionAsync([FromRoute] Guid id, [FromBody] Models.DTO.UpdateRegion updateRegion)
        {
            try
            {
                // Convert DTO to Domain model
                var region = new Models.Domain.Region
                {
                    Code = updateRegion.Code,
                    Area = updateRegion.Area,
                    Lat = updateRegion.Lat,
                    Long = updateRegion.Long,
                    Name = updateRegion.Name,
                    Population = updateRegion.Population,
                };

                // Update region using repository
                region = await _regionRepository.UpdateRegion(id, region);

                // If null, not found
                if (region == null)
                {
                    return NotFound();
                }

                // Convert domain back to DTO
                var regionDTO = new Models.DTO.Region
                {
                    Code = region.Code,
                    Area = region.Area,
                    Lat = region.Lat,
                    Long = region.Long,
                    Name = region.Name,
                    Population = region.Population,
                };

                // Return Ok Response
                return Ok(regionDTO);
            }
            catch (Exception ex)
            {
                // Log the exception for debugging
                // Log.LogError(ex, "An error occurred during region update.");
                return StatusCode(500); // Internal Server Error
            }
        }

    }
}
