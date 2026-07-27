namespace Aqarak_WebAPI.Interfaces
{
    public interface IPropertyRepository
    {
        Task<IEnumerable<PropertyDTO>> GetAll(int? GovernorateId, int? CategoryId, decimal? MinPrice, decimal? MaxPrice);


        Task<Property?> GetbyId(int id);


        Task<Property> Add(Property property);
        Task AddImages(int propertyId, List<string> imageUrls);
        Task RemoveImagesByPropertyId(int propertyId);
        Task<Property> GetbyIdWithImages(int id);

        Task Update(Property property);


        Task Delete(Property property);

    }
}
