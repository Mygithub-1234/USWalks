using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Threading.Tasks;
using USWalks.SPI.CustomActionFilters;
using USWalks.SPI.Data;
using USWalks.SPI.Models.Domain;
using USWalks.SPI.Models.DTO;
using USWalks.SPI.Repositories;

namespace USWalks.SPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly USWalksDbContext dbContext;
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;
        private readonly ILogger<RegionsController> logger;

        public RegionsController(USWalksDbContext _dbContext, IRegionRepository _regionRepository, IMapper mapper, ILogger<RegionsController> logger)
        {
            this.dbContext = _dbContext;
            this.regionRepository = _regionRepository;
            this.mapper = mapper;
            this.logger = logger;
        }
        //Get All Regions
        // Get: Https://localhost:portnumber/api/regions?filterOn=Name&filterQuery=Track&SortBy=Name&isAscending=true&pageNumber=1&pageSize=10
        [HttpGet]
        //[Authorize(Roles ="Reader")]
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] string? filterQuery,
            [FromQuery] string? sortBy, [FromQuery] bool? isAscending = null, [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 100)
        {
            logger.LogInformation("GetallREgions action method was invoked");
            
            //get data from domain model
            var regionsDomain = await regionRepository.GetAllAsync(pageNumber, pageSize, isAscending, filterOn,filterQuery,sortBy);
            //Map domain model to DTO
            //var regionsDTO = new List<RegionDTO>();
            //foreach (var region in regionsDomain)
            //{
            //    {
            //        regionsDTO.Add(new RegionDTO()
            //        {
            //            Id = region.Id,
            //            Name = region.Name,
            //            Code = region.Code,
            //            RegionImageUrl = region.RegionImageUrl
            //        });
            //    }
            //}
            var regionsDTO = mapper.Map<List<RegionDTO>>(regionsDomain);

            logger.LogInformation($"Finished getallRegions request with data:{JsonSerializer.Serialize(regionsDTO)}");
            return Ok(regionsDTO);
        }
        //Get Region By ID
        //GET :https://localhost:portnumberr/api/regions/{id}
        [HttpGet]
        [Route("{id:Guid}")]
       // [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            //var region = dbContext.Regions.Find(id);
            var regionDomain = await regionRepository.GetByIdAsync(id);

            if (regionDomain == null)
            {
                return NotFound();
            }
            //var regionsDTO = new RegionDTO()
            //{
            //    Id = regionDomain.Id,
            //    Name = regionDomain.Name,
            //    Code = regionDomain.Code,
            //    RegionImageUrl = regionDomain.RegionImageUrl

            //};
            var regionsDTO = (mapper.Map<RegionDTO>(regionDomain));
            return Ok(regionsDTO);

        }
        //Post to create new Region
        //POST: https://localhost:portnumber/api/regions
        [HttpPost]
        [ValidateModel]
       // [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDTO requestDTO)
        {
            // map DTO to domain model
            //var regionDomainModel = new Region()
            //{
            //    Code = requestDTO.Code,
            //    RegionImageUrl = requestDTO.RegionImageUrl,
            //    Name = requestDTO.Name
            //};
            var regionDomainModel = mapper.Map<Region>(requestDTO);
            // Add domain model to DBCContext
            regionDomainModel = await regionRepository.CreateAsync(regionDomainModel);

            //Map domain model back to DTO
            //var regionDTO = new RegionDTO()
            //{
            //    Id = regionDomainModel.Id,
            //    Name = regionDomainModel.Name,
            //    Code = regionDomainModel.Code,
            //    RegionImageUrl = regionDomainModel.RegionImageUrl
            //};
            var regionDTO = mapper.Map<RegionDTO>(regionDomainModel);

            return CreatedAtAction(nameof(GetById), new { Id = regionDTO.Id }, regionDTO);

        }

        //Update a region
        //PUT : https://localhost:portnumber/api/regions/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        //[Authorize(Roles = "Writer")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDTO updateRegionRequestDTO)
        {
            //var regionDomainModel = new Region
            //{
            //    Code = updateRegionRequestDTO.Code,
            //    Name = updateRegionRequestDTO.Name,
            //    RegionImageUrl = updateRegionRequestDTO.RegionImageUrl
            //};
            var regionDomainModel = mapper.Map<Region>(updateRegionRequestDTO);

            //Check if reqion exists
            regionDomainModel = await regionRepository.UpdateAsync(id, regionDomainModel);
            if (regionDomainModel == null)
            {
                return NotFound();
            }
            //Map DTO to domain model


            //regionDomainModel.Code = updateRegionRequestDTO.Code;
            //regionDomainModel.Name = updateRegionRequestDTO.Name;
            //regionDomainModel.Code = updateRegionRequestDTO.Code;
            //regionDomainModel.RegionImageUrl = updateRegionRequestDTO.RegionImageUrl;

            //await dbContext.SaveChangesAsync();
            //Convert Domain model to DTO
            //var regionDTO = new RegionDTO()
            //{
            //    Id = regionDomainModel.Id,
            //    Name = regionDomainModel.Name,
            //    Code = regionDomainModel.Code,
            //    RegionImageUrl = regionDomainModel.RegionImageUrl
            //};
            var regionDTO = mapper.Map<RegionDTO>(updateRegionRequestDTO);
            return Ok(regionDTO);

        }

        //To delete a region
        //GET: https://localhost:portnumber/api/regions/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var regionDomainModel = await regionRepository.DeleteAsync(id);

            if (regionDomainModel == null)
            {
                return NotFound();
            }

            //Convert Domain model to DTO
            //var regionDTO = new RegionDTO()
            //{
            //    Id = regionDomainModel.Id,
            //    Name = regionDomainModel.Name,
            //    Code = regionDomainModel.Code,
            //    RegionImageUrl = regionDomainModel.RegionImageUrl
            //};
            var regionDTO = mapper.Map<RegionDTO>(regionDomainModel);
            return Ok(regionDTO);

        }
    }

}


