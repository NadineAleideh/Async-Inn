using Async_Inn.Data;
using Async_Inn.Models.DTOs;
using Async_Inn.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Async_Inn.Models.Services
{
    public class AmenityServices : IAmenity
    {
        private readonly AsyncInnDbContext _context;


        public AmenityServices(AsyncInnDbContext context)
        {
            _context = context;
        }
        public async Task<AmenityDTO> CreateAmenity(AmenityDTO amenityDTO)
        {
            var amenityEntity = new Amenity
            {
                Name = amenityDTO.Name
            };

            await _context.Amenities.AddAsync(amenityEntity);
            await _context.SaveChangesAsync();

            amenityDTO.ID = amenityEntity.Id;
            return amenityDTO;
        }


        public async Task<AmenityDTO> UpdateAmenity(int id, AmenityDTO amenityDTO)
        {
            var amenityToUpdate = await _context.Amenities.FindAsync(id);

            if (amenityToUpdate != null)
            {
                amenityToUpdate.Name = amenityDTO.Name;

                await _context.SaveChangesAsync();
            }
            return amenityDTO;
        }


        public async Task DeleteAmenity(int id)
        {
            var amenity = await _context.Amenities.FindAsync(id);
            if (amenity != null)
            {
                // Remove all associated RoomAmenities
                foreach (var roomAmenity in amenity.RoomAmenities.ToList())
                {
                    _context.RoomAmenities.Remove(roomAmenity);
                }

                // Remove the Amenity itself
                _context.Amenities.Remove(amenity);

                await _context.SaveChangesAsync();
            }
        }
        public async Task<List<AmenityDTO>> GetAllAmenities()
        {
            var amenities = await _context.Amenities
                .Include(a => a.RoomAmenities)
                .ToListAsync();

            var amenityDTOs = amenities.Select(amenity => new AmenityDTO
            {
                ID = amenity.Id,
                Name = amenity.Name
            }).ToList();

            return amenityDTOs;
        }

        public async Task<AmenityDTO> GetAmenityById(int id)
        {
            var amenity = await _context.Amenities.FindAsync(id);

            if (amenity == null)
            {
                return null;
            }

            var amenityDTO = new AmenityDTO
            {
                ID = amenity.Id,
                Name = amenity.Name
            };
            return amenityDTO;
        }
    }
}
