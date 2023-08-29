using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.Data;
using NZWalks.Models.Domain;
using NZWalks.Models.DTO;

namespace NZWalks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext dbContext;
        public RegionsController(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public NZWalksDbContext DbContext { get; }

        [HttpGet]
        public IActionResult GetAll()
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

            var regions = dbContext.Regions.ToList();

            var regionsDto = new List<RegionDTO>();
            foreach (var region in regions)
            {
                regionsDto.Add(new RegionDTO()
                {
                    Id = region.Id,
                    Code = region.Code,
                    Name = region.Name,
                    RegionImageUrl = region.RegionImageUrl,
                });
            }

            return Ok(regionsDto);
        }

        [HttpGet]
        [Route("{id:Guid}")]

        public IActionResult GetById(Guid id) 
        {
            /*var region = dbContext.Regions.FirstOrDefault(r => r.Id == id);*/
            var region = dbContext.Regions.Find(id);

            if (region == null)
            {
                return NotFound();
            }

            var regionDto = new RegionDTO
            {
                Id = region.Id,
                Code = region.Code,
                Name = region.Name,
                RegionImageUrl = region.RegionImageUrl,
            };

            return Ok(regionDto);
        }

        [HttpPost]
        public IActionResult Create([FromBody] AddRegionReqDto addRegionReqDto)
        {
            // Map
            var regionDomainModel = new Region
            {
                Code = addRegionReqDto.Code,
                Name = addRegionReqDto.Name,
                RegionImageUrl = addRegionReqDto.RegionImageUrl,
            };

            dbContext.Regions.Add(regionDomainModel);
            dbContext.SaveChanges();

            var regionDto = new RegionDTO
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl,
            };

            return CreatedAtAction(nameof(GetById), new { id = regionDomainModel.Id}, regionDto);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public IActionResult Update([FromRoute] Guid id, [FromBody] UpdateRegionReqDto updateRegionReqDto )
        {
            var regionDomainModel = dbContext.Regions.FirstOrDefault(r => r.Id == id);

            if(regionDomainModel == null)
            {
                return NotFound();  
            }

            // Map
            regionDomainModel.Code = updateRegionReqDto.Code;
            regionDomainModel.Name = updateRegionReqDto.Name;
            regionDomainModel.RegionImageUrl = updateRegionReqDto.RegionImageUrl;

            dbContext.SaveChanges();

            var regionDto = new RegionDTO
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl,
            };

            return Ok(regionDto);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public IActionResult Delete([FromRoute] Guid id) 
        {
            var regionDomainModel = dbContext.Regions.FirstOrDefault(x => x.Id == id);

            if( regionDomainModel == null)
            {
                return NotFound();
            }

            dbContext.Regions.Remove(regionDomainModel);
            dbContext.SaveChanges();

            // return deletad
            var regionDto = new RegionDTO
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl,
            };

            return Ok(regionDto);
        }
    }
}
