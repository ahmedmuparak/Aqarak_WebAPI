namespace Aqarak_WebAPI.Interfaces
{
    public interface IFavoriteRepository
    {
        Task AddFavorite(string userId, int propertyId);
        Task RemoveFavorite(string userId, int propertyId);
        Task<List<Property>> GetUserFavorites(string userId);

    }
}
