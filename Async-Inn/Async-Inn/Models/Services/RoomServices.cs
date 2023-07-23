using Async_Inn.Data;
using Async_Inn.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Async_Inn.Models.Services
{
    public class RoomServices : IRoom
    {
        private readonly AsyncInnDbContext _room;

        public RoomServices(AsyncInnDbContext room)
        {
            _room = room;
        }
        public async Task<Room> CreateRoom(Room room)
        {
            _room.Rooms.Add(room);

            await _room.SaveChangesAsync();

            return room;
        }

        public async Task DeleteRoom(int id)
        {
            Room room = await GetRoom(id);

            _room.Entry<Room>(room).State = EntityState.Deleted;

            await _room.SaveChangesAsync();
        }

        public async Task<Room> GetRoom(int id)
        {
            Room? room = await _room.Rooms.FindAsync(id);

            return room;
        }

        public async Task<List<Room>> GetRooms()
        {
            var rooms = await _room.Rooms.ToListAsync();

            return rooms;
        }

        public async Task<Room> UpdateRoom(int id, Room room)
        {
            var oldroom = await GetRoom(id);

            if (oldroom != null)
            {
                oldroom.Name = room.Name;
                oldroom.Layout = room.Layout;

                await _room.SaveChangesAsync();
            }


            return oldroom;
        }
    }
}
