using AutoMapper;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
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
        [Route("{id:guid}")]
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
            //validate the request
            if (!ValidateAddRegionAsync(addRegionRequest))
            {
                return BadRequest(ModelState);
            } 
            
           
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
            //validate incoming request
            if (!ValidationUpdateRegionAsync(updateRegion))
            {
                return BadRequest(ModelState);
            }
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

        #region Private methods
        private bool ValidateAddRegionAsync(Models.DTO.AddRegionRequest addRegionRequest)
        {
            if (addRegionRequest == null)
            {
                ModelState.AddModelError(nameof(addRegionRequest), $" Add Region field is required.");
                return false;

            }
            if (string.IsNullOrWhiteSpace(addRegionRequest.Code))
            {
                ModelState.AddModelError(nameof(addRegionRequest.Code), $"{nameof(addRegionRequest.Code)} cannot be null or whitespace.");

            }
            if (string.IsNullOrWhiteSpace(addRegionRequest.Name))
            {
                ModelState.AddModelError(nameof(addRegionRequest.Name), $"{nameof(addRegionRequest.Name)} cannot be null or whitespace.");
            }
            if (addRegionRequest.Area <= 0)
            {
                ModelState.AddModelError(nameof(addRegionRequest.Area), $"{nameof(addRegionRequest.Area)} cannot be less than equal to zero");
            }
            if (addRegionRequest.Lat <= 0)
            {
                ModelState.AddModelError(nameof(addRegionRequest.Lat), $"{nameof(addRegionRequest.Lat)} cannot be less than equal to zero");
            }
            if (addRegionRequest.Long <= 0)
            {
                ModelState.AddModelError(nameof(addRegionRequest.Long), $"{nameof(addRegionRequest.Long)} cannot be less than equal to zero");
            }
            if (addRegionRequest.Population <= 0)
            {
                ModelState.AddModelError(nameof(addRegionRequest.Population), $"{nameof(addRegionRequest.Population)} cannot be less than equal to zero");
            }
            if(ModelState.ErrorCount > 0)
            {
                return false;
            }
            return true;
        }
        private bool ValidationUpdateRegionAsync(Models.DTO.UpdateRegion updateRegion)
        {
            if (updateRegion == null)
            {
                ModelState.AddModelError(nameof(updateRegion), $" Add Region field is required.");
                return false;

            }
            if (string.IsNullOrWhiteSpace(updateRegion.Code))
            {
                ModelState.AddModelError(nameof(updateRegion.Code), $"{nameof(updateRegion.Code)} cannot be null or whitespace.");

            }
            if (string.IsNullOrWhiteSpace(updateRegion.Name))
            {
                ModelState.AddModelError(nameof(updateRegion.Name), $"{nameof(updateRegion.Name)} cannot be null or whitespace.");
            }
            if (updateRegion.Area <= 0)
            {
                ModelState.AddModelError(nameof(updateRegion.Area), $"{nameof(updateRegion.Area)} cannot be less than equal to zero");
            }
            if (updateRegion.Lat <= 0)
            {
                ModelState.AddModelError(nameof(updateRegion.Lat), $"{nameof(updateRegion.Lat)} cannot be less than equal to zero");
            }
            if (updateRegion.Long <= 0)
            {
                ModelState.AddModelError(nameof(updateRegion.Long), $"{nameof(updateRegion.Long)} cannot be less than equal to zero");
            }
            if (updateRegion.Population <= 0)
            {
                ModelState.AddModelError(nameof(updateRegion.Population), $"{nameof(updateRegion.Population)} cannot be less than equal to zero");
            }
            if (ModelState.ErrorCount > 0)
            {
                return false;
            }
            return true;
        }


        #endregion

    }
    //to validate add API creating method


}
