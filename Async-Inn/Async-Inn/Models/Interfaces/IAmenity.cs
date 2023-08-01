using Async_Inn.Models.DTOs;

namespace Async_Inn.Models.Interfaces
{
    public interface IAmenity
    {
        Task<AmenityDTO> CreateAmenity(AmenityDTO amenity);
        Task<AmenityDTO> UpdateAmenity(int id, AmenityDTO amenity);
        Task DeleteAmenity(int id);
        Task<List<AmenityDTO>> GetAllAmenities();
        Task<AmenityDTO> GetAmenityById(int id);
    }
}
