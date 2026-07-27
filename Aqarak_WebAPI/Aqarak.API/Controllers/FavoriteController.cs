using Aqarak_WebAPI.Interfaces;

namespace Aqarak_WebAPI.Aqarak.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoriteController : ControllerBase
    {
        private readonly IFavoriteRepository repo;

        public FavoriteController(IFavoriteRepository repo)
        {
            this.repo = repo;
        }


        [HttpPost]
        public async Task<IActionResult> Add(FavoriteDTO dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await repo.AddFavorite(userId, dto.PropertyId);
            return Ok();
        }

        [HttpDelete("{propertyId}")]
        public async Task<IActionResult> Remove(int propertyId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await repo.RemoveFavorite(userId, propertyId);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> MyFavorites()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var properties = await repo.GetUserFavorites(userId);

            var result = properties.Select(p => new FavoritePropertyDTO
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Images = p.Images.Select(i => i.ImageUrl).ToList()
            });

            return Ok(result);
        }
    }
}
