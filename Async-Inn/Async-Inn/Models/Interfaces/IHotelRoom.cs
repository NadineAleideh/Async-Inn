using Async_Inn.Models.DTOs;

namespace Async_Inn.Models.Interfaces
{
    public interface IHotelRoom
    {
        Task<HotelRoomDTO> AddHotelRoom(int hotelId, HotelRoomDTO hotelRoomDTO);
        Task<HotelRoomDTO> UpdateHotelRoom(int hotelId, int roomNumber, HotelRoomDTO hotelRoomDTO);
        Task DeleteHotelRoom(int hotelId, int roomNumber);
        Task<List<HotelRoomDTO>> GetAllHotelRooms(int hotelId);
        Task<HotelRoomDTO> GetHotelRoom(int hotelId, int roomNumber);
    }
}
