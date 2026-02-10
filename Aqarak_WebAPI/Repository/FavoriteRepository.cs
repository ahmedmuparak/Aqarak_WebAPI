using Aqarak_WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Aqarak_WebAPI.Repository
{
    public class FavoriteRepository : IFavoriteRepository
    {
        private readonly APIContext context;

        public FavoriteRepository(APIContext context)
        {
            this.context = context;
        }

        public async Task AddFavorite(string userId, int propertyId)
        {
            var exists = await context.FavoriteLists
                .AnyAsync(f => f.UserId == userId && f.PropertyId == propertyId);

            if (exists) return;

            context.FavoriteLists.Add(new FavoriteList
            {
                UserId = userId,
                PropertyId = propertyId
            });

            await context.SaveChangesAsync();
        }

        public async Task RemoveFavorite(string userId, int propertyId)
        {
            var fav = await context.FavoriteLists
                .FirstOrDefaultAsync(f => f.UserId == userId && f.PropertyId == propertyId);

            if (fav == null) return;

            context.FavoriteLists.Remove(fav);
            await context.SaveChangesAsync();
        }

        public async Task<List<Property>> GetUserFavorites(string userId)
        {
            return await context.FavoriteLists
                .Include(f => f.Property)
                 .ThenInclude(p => p.Images)
                .Where(f => f.UserId == userId)
                .Select(f => f.Property)
                .ToListAsync();

        }

    }
}
