namespace Async_Inn.Models.Interfaces
{
    public interface IRoom
    {
        Task<Room> CreateRoom(Room room);

        Task<List<Room>> GetAllRooms();

        Task<Room> GetRoomById(int id);

        Task<Room> UpdateRoom(int id, Room room);

        Task DeleteRoom(int id);

        Task AddAmenityToRoom(int roomId, int amenityId);
        Task RemoveAmenityFromRoom(int roomId, int amenityId);
    }
}
