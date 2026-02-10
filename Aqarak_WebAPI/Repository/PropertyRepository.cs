namespace Aqarak_WebAPI.Repository
{
    public class PropertyRepository : IPropertyRepository
    {
        private readonly APIContext context;

        public PropertyRepository(APIContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<PropertyDTO>> GetAll(
            int? GovernorateId,
            int? CategoryId,
            decimal? MinPrice,
            decimal? MaxPrice
        )
        {
            var query = context.Properties
                .Include(p => p.User)
                .Include(p => p.Category)
                .Include(p => p.Images)
                .Include(p => p.Governorate)
                .AsQueryable();

            if (GovernorateId.HasValue)
                query = query.Where(p => p.GovernorateId == GovernorateId.Value);

            if (CategoryId.HasValue)
                query = query.Where(p => p.CategoryId == CategoryId.Value);

            if (MinPrice.HasValue)
                query = query.Where(p => p.Price >= MinPrice.Value);

            if (MaxPrice.HasValue)
                query = query.Where(p => p.Price <= MaxPrice.Value);

            return await query.Select(p => new PropertyDTO
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                Insurance = p.Insurance,
                CreatedAt = p.CreatedAt,

                OwnerName = p.User.FullName,
                OwnerPhone = p.User.Phone,

                CategoryId = p.CategoryId,
                GovernorateId = p.GovernorateId,

                CategoryName = p.Category.Name,
                GovernorateName = p.Governorate.Name,

                Images = p.Images.Select(i => i.ImageUrl).ToList()
            }).ToListAsync();

        }


        public async Task<Property?> GetbyId(int id)
        {
            return await context.Properties
                .Include(p=>p.Images)
                .Include(p => p.Category)
                .Include(p => p.Governorate)
                .FirstOrDefaultAsync(p=>p.Id == id);
        }


        public async Task<Property> Add(Property property)
        {
            context.Properties.Add(property);
            await context.SaveChangesAsync();
            return property;
        }

        public async Task AddImages(int propertyId, List<string> imageUrls)
        {
            var images = imageUrls.Select(url => new PropertyImage
            {
                PropertyId = propertyId,
                ImageUrl = url
            }).ToList();

            context.PropertyImages.AddRange(images);
            await context.SaveChangesAsync();
        }

        public async Task<Property> GetbyIdWithImages(int id)
        {
            return await context.Properties
                .Include(p => p.Images)
                .FirstOrDefaultAsync(p => p.Id == id);
        }


        public async Task RemoveImagesByPropertyId(int propertyId)
        {
            var images = context.PropertyImages
                .Where(i => i.PropertyId == propertyId);

            context.PropertyImages.RemoveRange(images);
            await context.SaveChangesAsync();
        }


        public async Task Update(Property property)
        {
            context.Properties.Update(property);
            await context.SaveChangesAsync();
        }

        public async Task Delete (Property property)
        {
            context.Properties.Remove(property);
            await context.SaveChangesAsync();
        }
    }
}
