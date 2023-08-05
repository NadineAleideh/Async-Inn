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

        /// <summary>
        /// to create a new amenity 
        /// </summary>
        /// <param name="amenityDTO"></param>
        /// <returns>created amenity</returns>
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

        /// <summary>
        /// to update a spesific amenity by passing it's id and the updates 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="amenityDTO"></param>
        /// <returns>the updated amenity</returns>

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

        /// <summary>
        /// to delete a spesific amenity by it's id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>nothing</returns>
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

        /// <summary>
        /// To get all amenities 
        /// </summary>
        /// <returns>List of all amenities</returns>
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

        /// <summary>
        /// To get amenity by id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>the amenity which has the same id passed</returns>
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
