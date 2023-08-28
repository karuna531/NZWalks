using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NXWalks.Api.Repositories;
using NXWalks.Api.Repositories.WalkDifficultyRepository;
using NXWalks.Api.Repositories.WalksRepository;

namespace NXWalks.Api.Controllers
{
    [ApiController]
    [Route("Walk")]
    public class WalkController : Controller
    {
        private readonly IWalksRepository _walksRepository;
        private readonly IMapper _mapper;
        private readonly IRegionRepository regionRepository;
        private readonly IWalkDifficultRepository walkDifficultRepository;

        public WalkController(IWalksRepository walksRepository, IMapper mapper, IRegionRepository regionRepository, IWalkDifficultRepository walkDifficultRepository)
        {
            _walksRepository = walksRepository;
            _mapper = mapper;
            this.regionRepository = regionRepository;
            this.walkDifficultRepository = walkDifficultRepository;
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
            //Validate incoming request
            if(!(ValidateCreateWalks(walk)))
            {
                return BadRequest(ModelState);
            }
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
            if (!(ValidateUpdateWalkAsync(updateWalk)))
            {
                return BadRequest(ModelState);
            }
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
        #region private methods
        private bool ValidateCreateWalks( Models.DTO.AddWalk walk)
        {
            if(walk == null)
            {
                ModelState.AddModelError(nameof(walk), $"{nameof(walk)} cannot be empty");  
            }
            if(string.IsNullOrWhiteSpace(walk.Name))
            {
                ModelState.AddModelError(nameof(walk.Name), $"{nameof(walk.Name)} is required.");
            }
            if (walk.Length< 0)
            {
                ModelState.AddModelError(nameof(walk.Length), $"{nameof(walk.Length)} cannot be less than  zero");
            }
           var region = regionRepository.GetById(walk.RegionID);
            if (region == null)
            {
                ModelState.AddModelError(nameof(walk.RegionID), $"{nameof(walk.RegionID)} is invalid");
            }
            var walkdifficulty = walkDifficultRepository.GetWalkDifficultyById(walk.WalkDifficultyId);
            if(walkdifficulty == null)
            {
                ModelState.AddModelError(nameof(walk.WalkDifficultyId), $"{nameof(walk.WalkDifficultyId)} is invalid");
            }
            if(ModelState.ErrorCount > 0)
            {
                return false;
            }
            return true;

        }
        private bool ValidateUpdateWalkAsync(Models.DTO.UpdateWalk updateWalk)
        {
            if (updateWalk == null)
            {
                ModelState.AddModelError(nameof(updateWalk), $"{nameof(updateWalk)} cannot be empty");
            }
            if (string.IsNullOrWhiteSpace(updateWalk.Name))
            {
                ModelState.AddModelError(nameof(updateWalk.Name), $"{nameof(updateWalk.Name)} is required.");
            }
            if (updateWalk.Length < 0)
            {
                ModelState.AddModelError(nameof(updateWalk.Length), $"{nameof(updateWalk.Length)} cannot be less than  zero");
            }
            var region = regionRepository.GetById(updateWalk.RegionID);
            if (region == null)
            {
                ModelState.AddModelError(nameof(updateWalk.RegionID), $"{nameof(updateWalk.RegionID)} is invalid");
            }
            var walkdifficulty = walkDifficultRepository.GetWalkDifficultyById(updateWalk.WalkDifficultyId);
            if (walkdifficulty == null)
            {
                ModelState.AddModelError(nameof(updateWalk.WalkDifficultyId), $"{nameof(updateWalk.WalkDifficultyId)} is invalid");
            }
            if (ModelState.ErrorCount > 0)
            {
                return false;
            }
            return true;

        }



        #endregion

    }
}
