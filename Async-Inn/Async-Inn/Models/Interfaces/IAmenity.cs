namespace Async_Inn.Models.Interfaces
{
    public interface IAmenity
    {
        Task<Amenity> CreateAmenity(Amenity amenity);
        Task<Amenity> UpdateAmenity(int id, Amenity amenity);
        Task DeleteAmenity(int id);
        Task<List<Amenity>> GetAllAmenities();
        Task<Amenity> GetAmenityById(int id);
    }
}
