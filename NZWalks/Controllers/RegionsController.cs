using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.Data;
using NZWalks.Models.Domain;
using NZWalks.Models.DTO;
using NZWalks.Repositories;

namespace NZWalks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext dbContext;
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionsController(NZWalksDbContext dbContext, IRegionRepository regionRepository, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }

        public NZWalksDbContext DbContext { get; }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            /*var regions = new List<Region>
            {
                new Region {
                    Id = Guid.NewGuid(),
                    Name = "Auckland Region",
                    Code = "AKL",
                    RegionImageUrl="https://www.pexels.com/pl-pl/zdjecie/lot-krajobraz-natura-wzgorze-6243776/"
                },
                new Region {
                    Id = Guid.NewGuid(),
                    Name = "Welligton Region",
                    Code = "WLG",
                    RegionImageUrl="https://www.pexels.com/pl-pl/zdjecie/miasto-krajobraz-gory-indie-6619053/"
                }
            };*/

            /*var regions = await dbContext.Regions.ToListAsync();*/
            var regionsDomain = await regionRepository.GetAllAsync();

            /*var regionsDto = new List<RegionDTO>();
            foreach (var region in regionsDomain)
            {
                regionsDto.Add(new RegionDTO()
                {
                    Id = region.Id,
                    Code = region.Code,
                    Name = region.Name,
                    RegionImageUrl = region.RegionImageUrl,
                });
            }*/

            var regionsDto = mapper.Map<List<RegionDTO>>(regionsDomain);

            return Ok(regionsDto);
        }

        [HttpGet]
        [Route("{id:Guid}")]

        public async Task<IActionResult> GetById(Guid id) 
        {
            /*var region = dbContext.Regions.FirstOrDefault(r => r.Id == id);*/
            /*var region = await dbContext.Regions.FindAsync(id);*/
            var region = await regionRepository.GetByIdAsync(id);

            if (region == null)
            {
                return NotFound();
            }

            /*var regionDto = new RegionDTO
            {
                Id = region.Id,
                Code = region.Code,
                Name = region.Name,
                RegionImageUrl = region.RegionImageUrl,
            };*/
            var regionDto = mapper.Map<RegionDTO>(region);

            return Ok(regionDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddRegionReqDto addRegionReqDto)
        {
            // Map
            /*var regionDomainModel = new Region
            {
                Code = addRegionReqDto.Code,
                Name = addRegionReqDto.Name,
                RegionImageUrl = addRegionReqDto.RegionImageUrl,
            };*/
            var regionDomainModel = mapper.Map<Region>(addRegionReqDto);

            /*await dbContext.Regions.AddAsync(regionDomainModel);
            await dbContext.SaveChangesAsync();*/

            regionDomainModel = await regionRepository.CreateAsync(regionDomainModel);

            /*var regionDto = new RegionDTO
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl,
            };*/

            var regionDto = mapper.Map<RegionDTO>(regionDomainModel);

            return CreatedAtAction(nameof(GetById), new { id = regionDomainModel.Id}, regionDto);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionReqDto updateRegionReqDto )
        {
            /*var regionDomainModel = new Region
            {
                Code = updateRegionReqDto.Code,
                Name = updateRegionReqDto.Name,
                RegionImageUrl = updateRegionReqDto.RegionImageUrl,
            };*/
            var regionDomainModel = mapper.Map<Region>(updateRegionReqDto);

            /*var regionDomainModel = await dbContext.Regions.FirstOrDefaultAsync(r => r.Id == id);*/
            regionDomainModel = await regionRepository.UpdateAsync(id, regionDomainModel);
            if(regionDomainModel == null)
            {
                return NotFound();  
            }

            // Map
            /*regionDomainModel.Code = updateRegionReqDto.Code;
            regionDomainModel.Name = updateRegionReqDto.Name;
            regionDomainModel.RegionImageUrl = updateRegionReqDto.RegionImageUrl;

            await dbContext.SaveChangesAsync();*/

            /*var regionDto = new RegionDTO
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl,
            };*/

            var regionDto = mapper.Map<RegionDTO>(regionDomainModel);

            return Ok(regionDto);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id) 
        {
            /*var regionDomainModel = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);*/
            var regionDomainModel = await regionRepository.DeleteAsync(id);

            if ( regionDomainModel == null)
            {
                return NotFound();
            }

            /*dbContext.Regions.Remove(regionDomainModel);
            await dbContext.SaveChangesAsync();*/

            // return deletad
            /*var regionDto = new RegionDTO
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl,
            };*/

            var regionDto = mapper.Map<RegionDTO>(regionDomainModel);

            return Ok(regionDto);
        }
    }
}
