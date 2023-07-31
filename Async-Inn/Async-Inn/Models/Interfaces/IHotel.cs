namespace Async_Inn.Models.Interfaces
{
    public interface IHotel
    {
        Task<Hotel> GetHotelById(int Id);
        Task<Hotel> GetHotelByName(string name);
        Task<List<Hotel>> GetAllHotels();
        Task<Hotel> CreateHotel(Hotel hotel);
        Task<Hotel> UpdateHotel(int Id, Hotel hotel);
        Task DeleteHotel(int Id);
    }
}
