namespace Async_Inn.Models.Interfaces
{
    public interface IHotelRoom
    {
        Task<HotelRoom> GetHotelRoom(int hotelId, int roomNumber);
        Task<List<HotelRoom>> GetHotelRooms(int hotelId);
        Task<HotelRoom> UpdateHotelRoom(HotelRoom hotelRoom);
        Task DeleteHotelRoom(int hotelId, int roomNumber);
        Task<HotelRoom> AddHotelRoom(HotelRoom hotelRoom);

        Task RemoveRoomFromHotel(int roomId, int hotelId);
    }
}
