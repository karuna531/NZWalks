using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NXWalks.Api.Repositories.WalksRepository;

namespace NXWalks.Api.Controllers
{
    [ApiController]
    [Route("Walk")]
    public class WalkController : Controller
    {
        private readonly IWalksRepository _walksRepository;
        private readonly IMapper _mapper;
        public WalkController(IWalksRepository walksRepository, IMapper mapper)
        {
            _walksRepository = walksRepository;
            _mapper = mapper;
            
        }
        [HttpGet]
        public async Task<IActionResult> GetAllWalk()
        {
            var walksData = await _walksRepository.GetAllWalks();
            var walkDTO = _mapper.Map<List<Models.DTO.Walk>>(walksData);

            return Ok(walkDTO);
        }
        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetWalksById")]

        public async Task<IActionResult> GetWalksById(Guid id)
            {
            var walks = await _walksRepository.GetWalkById(id);
            if(walks == null)
            {
                return NotFound();
            }
            var walksDTO = _mapper.Map<Models.DTO.Walk>(walks);
            return Ok(walksDTO);

        }
        [HttpPost]
        [Route("Create")]

        public async Task<IActionResult> CreateWalks([FromBody] Models.DTO.AddWalk walk)
        {
            // var WalkDomain = _mapper.Map<Models.Domain.Walk>(walk);
            var WalkDomain = new Models.Domain.Walk
            {
                Length = walk.Length,
                Name = walk.Name,
                RegionID = walk.RegionID,
                WalkDifficultyId = walk.WalkDifficultyId
            };
            // Pass domain to repository to persist this
            var CreatedData = await _walksRepository.CreateWalk(WalkDomain);
            // Map domain to DTO
            var WalkDTO = new Models.DTO.Walk
            {
                Length = WalkDomain.Length,
                Name = WalkDomain.Name,
                RegionID = WalkDomain.RegionID,
                WalkDifficultyId = WalkDomain.WalkDifficultyId
            };
            //var walkDTO = _mapper.Map<Models.DTO.Walk>(CreatedData);
            // Send DTO back to client
          return Ok(WalkDTO);

        }
        [HttpPut]
        [Route("Update/{id:guid}")]
        public async Task<IActionResult> UpdateWalkAsync([FromRoute] Guid id, [FromBody] Models.DTO.UpdateWalk updateWalk)
        {
            // Convert DTO to Domain Object
            var walkDomain = new Models.Domain.Walk
            {
                Id = id,
                Length = updateWalk.Length,
                Name = updateWalk.Name,
                RegionID = updateWalk.RegionID,
                WalkDifficultyId = updateWalk.WalkDifficultyId
            };

            // Pass details to repository - get domain in response or null
            walkDomain = await _walksRepository.UpdateWalk(id, walkDomain);

            // Handle null (Not found)
            if (walkDomain == null)
            {
                return NotFound();
            }

            // Convert back Domain to DTO
            var walkDTO = new Models.DTO.Walk
            {
                Id = walkDomain.Id,
                Length = walkDomain.Length,
                Name = walkDomain.Name,
                RegionID = walkDomain.RegionID,
                WalkDifficultyId = walkDomain.WalkDifficultyId
            };

            // Return Response
            return Ok(walkDTO);
        }
        [HttpDelete]
        [Route("Delete/{id:guid}")]
        public async Task<IActionResult> DeleteWalk(Guid id)
        {
            // Call repository to delete walk
            var walkDomain = await _walksRepository.DeleteWalk(id);

            // If walk was not found, return NotFound
            if (walkDomain == null)
            {
                return NotFound();
            }

            // Map deleted walk domain to DTO
            var walkDTO = _mapper.Map<Models.DTO.Walk>(walkDomain);

            // Return the deleted walk as DTO
            return Ok(walkDTO);
        }

    }
}
