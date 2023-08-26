using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NXWalks.Api.Models.Domain;
using NXWalks.Api.Models.DTO;
using NXWalks.Api.Repositories.WalkDifficultyRepository;

namespace NXWalks.Api.Controllers
{
    [ApiController]
    [Route("WalkDIfficulty")]
    public class WalkDIfficultyController : Controller
    {
        private readonly IWalkDifficultRepository _walkDifficultRepository;
        private readonly IMapper _mapper;
        public WalkDIfficultyController(IWalkDifficultRepository walkDifficultyRepository, IMapper mapper)
        {
            _mapper = mapper;
            _walkDifficultRepository = walkDifficultyRepository;
        }
        [HttpGet]
        [Route("get")]
        public async  Task<IActionResult> GetAllData() 
        {
            var walkDifficulty = await _walkDifficultRepository.GellAllWalkDifficulty();
            return Ok(walkDifficulty);
             
        }
        [HttpGet]
        [Route("get/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var walkDifficulty = await _walkDifficultRepository.GetWalkDifficultyById(id);
            return Ok(walkDifficulty);

        }
        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateWalkDifficulty(AddWalkDifficulty walkDifficulty)
        {
            //change dto to domain
            //var walkDifficultyDomain = _mapper.Map<Models.Domain.WalkDifficulty>(walkDifficulty);
            var walkDifficultyDomain = new Models.Domain.WalkDifficulty
            {
                code = walkDifficulty.code
               
            };
            //create data
            await _walkDifficultRepository.CreateWalkDifficulty(walkDifficultyDomain);
            //change domain to dto
            var walkDifficultyDTO = new Models.DTO.WalkDifficulty
            {

                Id = walkDifficultyDomain.Id,
                code = walkDifficultyDomain.code

            };            //return response
            return Ok(walkDifficultyDTO);
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteWalkDIfficulty(Guid id)
        {
           var walkDifficulty = await  _walkDifficultRepository.DeleteWalkDIfficulty(id);
            if(walkDifficulty == null)
            {
                return NotFound();
            }
            var WalkDIfficultyDTO = _mapper.Map<Models.DTO.WalkDifficulty>(walkDifficulty);
            return Ok(WalkDIfficultyDTO);


        }
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateWalkDifficulty([FromRoute] Guid id, [FromBody] UpdateWalkDIfficulty updateWalkDifficulty)
        {
            //change DTO to Domain
            var walkDomain = new Models.Domain.WalkDifficulty
            {
                code = updateWalkDifficulty.code
            };
            
            
            //Perform operation
           walkDomain =  await _walkDifficultRepository.UpdateWalkDifficulty(id, walkDomain);
            if (walkDomain == null)
            {
                return NotFound();
            }

            //change domain to DTO
            var WalkDIfficultyDTO = new Models.DTO.WalkDifficulty
            {
                Id= walkDomain.Id,
                code = walkDomain.code
            };
            //return Response
            return Ok(WalkDIfficultyDTO);
        }


    }
}
