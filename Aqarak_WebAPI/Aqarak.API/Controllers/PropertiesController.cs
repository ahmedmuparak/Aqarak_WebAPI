using Aqarak_WebAPI.DTOs;
using Aqarak_WebAPI.Models;
using Aqarak_WebAPI.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Security.Claims;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Aqarak_WebAPI.Aqarak.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertiesController : ControllerBase
    {
        private readonly IPropertyRepository repo;

        public PropertiesController(IPropertyRepository repo)
        {
            this.repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int? GovernorateId, int? CategoryId, decimal? MinPrice, decimal? MaxPrice)
        {
            var Properties = await repo.GetAll(GovernorateId, CategoryId, MinPrice, MaxPrice);
            return Ok(Properties);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var Property = await repo.GetbyId(id);
            if (Property == null)
                return BadRequest();
            return Ok(Property);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromForm] CreatePropertyDTO dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return Unauthorized();

            var property = new Models.Property
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                Insurance = dto.Insurance,
                CategoryId = dto.CategoryId,
                GovernorateId = dto.GovernorateId,
                CreatedAt = DateTime.UtcNow,
                UserId = userId
            };

            var createdProperty = await repo.Add(property);

            var uploadsFolder = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot",
                "uploads"
            );

            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            var urls = new List<string>();

            if (dto.Images != null)
            {
                foreach (var file in dto.Images)
                {
                    var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                    var path = Path.Combine(uploadsFolder, fileName);

                    using var stream = new FileStream(path, FileMode.Create);
                    await file.CopyToAsync(stream);

                    urls.Add("/uploads/" + fileName);
                }

                await repo.AddImages(createdProperty.Id, urls);
            }

            return Ok(new
            {
                propertyId = createdProperty.Id,
                images = urls
            });
        }




        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Update(int id, [FromForm] UpdatePropertyDTO dto)
        {
            var property = await repo.GetbyIdWithImages(id);
            if (property == null)
                return NotFound();

            property.Name = dto.Name;
            property.Description = dto.Description;
            property.Price = dto.Price;
            property.Insurance = dto.Insurance;
            property.CategoryId = dto.CategoryId;
            property.GovernorateId = dto.GovernorateId;

            await repo.Update(property);

            if (dto.Images != null && dto.Images.Count > 0)
            {
                await repo.RemoveImagesByPropertyId(property.Id);

                foreach (var img in property.Images)
                {
                    var physicalPath = Path.Combine(
                        Directory.GetCurrentDirectory(),
                        "wwwroot",
                        img.ImageUrl.TrimStart('/')
                    );

                    if (System.IO.File.Exists(physicalPath))
                        System.IO.File.Delete(physicalPath);
                }

                var urls = new List<string>();

                foreach (var file in dto.Images)
                {
                    var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                    var path = Path.Combine("wwwroot/uploads", fileName);

                    using var stream = new FileStream(path, FileMode.Create);
                    await file.CopyToAsync(stream);

                    urls.Add("/uploads/" + fileName);
                }

                await repo.AddImages(property.Id, urls);
            }

            return Ok("Updated Successfully");
        }



        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var Property =await repo.GetbyId(id);

            if (Property == null)
                return NotFound();

            if (Property.UserId != UserId)
                return Forbid();

            await repo.Delete(Property);
            return Ok();
        }
    }
}
