using Async_Inn.Models.DTOs;

namespace Async_Inn.Models.Interfaces
{
    public interface IRoom
    {
        Task<RoomDTO> CreateRoom(RoomDTO roomDTO);

        Task<RoomDTO> UpdateRoom(int id, RoomDTO roomDTO);

        Task DeleteRoom(int id);
        Task<List<RoomDTO>> GetAllRooms();

        Task<RoomDTO> GetRoomById(int id);


        Task AddAmenityToRoom(int roomId, int amenityId);
        Task RemoveAmenityFromRoom(int roomId, int amenityId);
    }
}
