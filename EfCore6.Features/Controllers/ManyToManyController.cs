using EFCore6.Core.Entities;
using EFCore6.Core.Models;
using EFCore6.Core.Servics.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EfCore6.Features.Controllers
{
    [Route("api/[controller]")]
    public class ManyToManyController : ControllerBase
    {

        public ManyToManyController(IManyToManyService manyToManyService)
        {
            ManyToManyService = manyToManyService;
        }

        public IManyToManyService ManyToManyService { get; }

        [HttpPost("SaveArtistsCovers")]
        public async Task<IActionResult> SaveArtistsCovers([FromBody] ManyToManyModel manyToManyModel)
        {
            try
            {
                await ManyToManyService.SaveNewArtistCover(manyToManyModel.Artist, manyToManyModel.Cover);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
