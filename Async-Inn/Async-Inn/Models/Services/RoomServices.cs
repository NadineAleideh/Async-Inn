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

            var roomAmenities = await _room.RoomAmenities
              .Where(r => r.RoomId == id)
              .Include(a => a.Amenities)
              .ThenInclude(a => a.RoomAmenities)
              .ToListAsync();

            room.RoomAmenities = roomAmenities;

            return room;

        }

        public async Task<List<Room>> GetRooms()
        {
            var rooms = await _room.Rooms.
               Include(a => a.RoomAmenities)
              .ThenInclude(hr => hr.Amenities)
              .ToListAsync();

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


        //logic to add and remove amenities from rooms

        public async Task AddAmenityToRoom(int roomId, int amenityId)
        {
            RoomAmenity roomAmenity = new RoomAmenity()
            {
                RoomId = roomId,
                AmenityId = amenityId
            };

            _room.Entry(roomAmenity).State = EntityState.Added;

            await _room.SaveChangesAsync();
        }

        public async Task RemoveAmenityFromRoom(int roomId, int amenityId)
        {
            var result = await _room.RoomAmenities.FirstOrDefaultAsync(r => r.AmenityId == amenityId && r.RoomId == roomId);

            _room.Entry(result).State = EntityState.Deleted;

            await _room.SaveChangesAsync();
        }
    }
}
