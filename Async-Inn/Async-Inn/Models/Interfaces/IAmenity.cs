using Async_Inn.Models.DTOs;

namespace Async_Inn.Models.Interfaces
{
    public interface IAmenity
    {
        /// <summary>
        /// to create a new amenity 
        /// </summary>
        /// <param name="amenityDTO"></param>
        /// <returns>created amenity</returns>
        Task<AmenityDTO> CreateAmenity(AmenityDTO amenity);

        /// <summary>
        /// to update a spesific amenity by passing it's id and the updates 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="amenity"></param>
        /// <returns>the updated amenity</returns>
        Task<AmenityDTO> UpdateAmenity(int id, AmenityDTO amenity);

        /// <summary>
        /// to delete a spesific amenity by it's id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>nothing</returns>
        Task DeleteAmenity(int id);

        /// <summary>
        /// To get all amenities 
        /// </summary>
        /// <returns>List of all amenities</returns>
        Task<List<AmenityDTO>> GetAllAmenities();

        /// <summary>
        /// To get amenity by id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>the amenity which has the same id passed</returns>
        Task<AmenityDTO> GetAmenityById(int id);
    }
}
