using Async_Inn.Models.DTOs;

namespace Async_Inn.Models.Interfaces
{
    public interface IRoom
    {
        /// <summary>
        /// to create a new room
        /// </summary>
        /// <param name="roomDTO"></param>
        /// <returns>the created room</returns>
        Task<RoomDTO> CreateRoom(RoomDTO roomDTO);

        /// <summary>
        /// to update a specific room by passing it's id and the updates
        /// </summary>
        /// <param name="id"></param>
        /// <param name="roomDTO"></param>
        /// <returns>the updated room</returns>
        Task<RoomDTO> UpdateRoom(int id, RoomDTO roomDTO);

        /// <summary>
        /// to delete a specific room by it's id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>nothing</returns>
        Task DeleteRoom(int id);


        /// <summary>
        /// to get all rooms
        /// </summary>
        /// <returns>list of all rooms</returns>
        Task<List<RoomDTO>> GetAllRooms();

        /// <summary>
        /// to get a specific room by it's id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>the roo it self</returns>
        Task<RoomDTO> GetRoomById(int id);

        /// <summary>
        /// to add a specific amenity from a spesific room
        /// </summary>
        /// <param name="roomId"></param>
        /// <param name="amenityId"></param>
        /// <returns>nothing</returns>
        Task AddAmenityToRoom(int roomId, int amenityId);


        /// <summary>
        /// to remove a specific amenity from a spesific room
        /// </summary>
        /// <param name="roomId"></param>
        /// <param name="amenityId"></param>
        /// <returns>nothing</returns>
        Task RemoveAmenityFromRoom(int roomId, int amenityId);
    }
}
