using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.Data;
using NZWalks.Models.Domain;

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

            return Ok(regions);
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

            return Ok(region);
        }
    }
}
