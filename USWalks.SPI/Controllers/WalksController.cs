using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using USWalks.SPI.CustomActionFilters;
using USWalks.SPI.Models.Domain;
using USWalks.SPI.Models.DTO;
using USWalks.SPI.Repositories;

namespace USWalks.SPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IWalkRepository walkRepository;
        private readonly ILogger<WalksController> logger;

        public WalksController(IMapper mapper, IWalkRepository walkRepository, ILogger<WalksController> logger)
        {
            this.mapper = mapper;
            this.walkRepository = walkRepository;
            this.logger = logger;
        }
        //Create Walk
        //Post: /api/walks
        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] AddWalkRequestDTO addWalkRequestDTO)
        {
            //map DTO to domain model
            var walkDomainModel = mapper.Map<Walk>(addWalkRequestDTO);
            await walkRepository.CreateAsync(walkDomainModel);

            return Ok(mapper.Map<WalkDTO>(walkDomainModel));

        }

        //Get Walks
        //Get : /api/walks
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                //logger.LogWarning("This is a warning");
                //logger.LogError("This is a log error");
                throw new Exception("test exception");

                var walkDomainModel = await walkRepository.GetAllAsync();
                return Ok(mapper.Map<List<WalkDTO>>(walkDomainModel));
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        //Get Walk by Id
        //GET: /api/Walks/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var walkDomainModel = await walkRepository.GetByIdAsync(id);
            if (walkDomainModel == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<WalkDTO>(walkDomainModel));
        }
        //Update Walk by Id
        //PUT : api/Walks/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> Update(Guid id, UpdateWalkRequestDTO updatewalkRequestDTO)
        {
            //Map DTO to Domain model
            var walkDomainModel = mapper.Map<Walk>(updatewalkRequestDTO);

            walkDomainModel = await walkRepository.UpdateAsync(id, walkDomainModel);
            if (walkDomainModel == null)
            { return NotFound(); }

            //Map Domain model to DTO
            return Ok(mapper.Map<WalkDTO>(walkDomainModel));
        }

        //Delete a walk by Id
        //DELETE: api/walks/{id}
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var deletedWalkDomainModel = await walkRepository.DeleteAsync(id);
            if (deletedWalkDomainModel == null)
            { return NotFound(); }

            return Ok(mapper.Map<WalkDTO>(deletedWalkDomainModel));
        }

    }
}
