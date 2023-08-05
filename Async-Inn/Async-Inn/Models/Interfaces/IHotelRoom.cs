using Async_Inn.Models.DTOs;

namespace Async_Inn.Models.Interfaces
{
    public interface IHotelRoom
    {

        /// <summary>
        /// to add a room with it's own specifications to a specific hotel
        /// </summary>
        /// <param name="hotelId"></param>
        /// <param name="hotelRoomDTO"></param>
        /// <returns>the room added itself</returns>
        Task<HotelRoomDTO> AddHotelRoom(int hotelId, HotelRoomDTO hotelRoomDTO);


        /// <summary>
        /// to update a room with it's own specifications in a specific hotel
        /// </summary>
        /// <param name="hotelId"></param>
        /// <param name="roomNumber"></param>
        /// <param name="hotelRoomDTO"></param>
        /// <returns>the room updated itself</returns>
        Task<HotelRoomDTO> UpdateHotelRoom(int hotelId, int roomNumber, HotelRoomDTO hotelRoomDTO);

        /// <summary>
        /// to delete a room with it's own specifications from a specific hotel
        /// </summary>
        /// <param name="hotelId"></param>
        /// <param name="roomNumber"></param>
        /// <returns>nothing</returns>
        Task DeleteHotelRoom(int hotelId, int roomNumber);

        /// <summary>
        /// to get all rooms with it's own specifications that belong to specific hotel
        /// </summary>
        /// <param name="hotelId"></param>
        /// <returns>list of rooms for a specific hotel</returns>
        Task<List<HotelRoomDTO>> GetAllHotelRooms(int hotelId);


        /// <summary>
        /// to get a room with it's own specifications that belongs to specific hotel
        /// </summary>
        /// <param name="hotelId"></param>
        /// <param name="roomNumber"></param>
        /// <returns>room for a specific hotel</returns>
        Task<HotelRoomDTO> GetHotelRoom(int hotelId, int roomNumber);
    }
}
