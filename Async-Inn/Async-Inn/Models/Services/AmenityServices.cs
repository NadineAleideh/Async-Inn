using Async_Inn.Data;
using Async_Inn.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Async_Inn.Models.Services
{
    public class AmenityServices : IAmenity
    {
        private readonly AsyncInnDbContext _amenity;

        public AmenityServices(AsyncInnDbContext amenity)
        {
            _amenity = amenity;
        }
        public async Task<Amenity> CreateAmenity(Amenity amenity)
        {
            _amenity.Amenities.Add(amenity);

            await _amenity.SaveChangesAsync();

            return amenity;
        }

        public async Task DeleteAmenity(int id)
        {
            Amenity amenity = await GetAmenity(id);

            _amenity.Entry<Amenity>(amenity).State = EntityState.Deleted;

            await _amenity.SaveChangesAsync();
        }

        public async Task<List<Amenity>> GetAmenities()
        {
            var amenities = await _amenity.Amenities.ToListAsync();

            return amenities;
        }

        public async Task<Amenity> GetAmenity(int id)
        {
            Amenity? amenity = await _amenity.Amenities.FindAsync(id);
            return amenity;
        }

        public async Task<Amenity> UpdateAmenity(int id, Amenity amenity)
        {
            amenity = await GetAmenity(id);

            _amenity.Entry<Amenity>(amenity).State = EntityState.Modified;

            await _amenity.SaveChangesAsync();

            return amenity;
        }
    }
}
