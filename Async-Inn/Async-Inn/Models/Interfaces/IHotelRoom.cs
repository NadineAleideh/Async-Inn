namespace Async_Inn.Models.Interfaces
{
    public interface IHotelRoom
    {
        Task<HotelRoom> AddHotelRoom(int hotelId, HotelRoom hotelRoom);
        Task<HotelRoom> UpdateHotelRoom(int hotelId, int roomNumber, HotelRoom hotelRoom);
        Task DeleteHotelRoom(int hotelId, int roomNumber);
        Task<List<HotelRoom>> GetAllHotelRooms(int hotelId);
        Task<HotelRoom> GetHotelRoom(int hotelId, int roomNumber);
    }
}
