namespace Async_Inn.Models.Interfaces
{
    public interface IHotel
    {
        Task<Hotel> CreateHotel(Hotel hotel);
        Task<Hotel> GetHotel(int Id);
        Task<List<Hotel>> GetHotels();
        Task<Hotel> UpdateHotel(int Id, Hotel hotel);
        Task DeleteHotel(int Id);
    }
}
