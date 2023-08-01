using Async_Inn.Models.DTOs;

namespace Async_Inn.Models.Interfaces
{
    public interface IHotel
    {
        Task<HotelDTO> CreateHotel(HotelDTO hotelDTO);
        Task<HotelDTO> UpdateHotel(int id, HotelDTO hotelDTO);
        Task DeleteHotel(int Id);
        Task<HotelDTO> GetHotelById(int id);
        Task<HotelDTO> GetHotelByName(string name);
        Task<List<HotelDTO>> GetAllHotels();

    }
}
