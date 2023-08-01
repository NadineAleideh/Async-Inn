using Async_Inn.Data;
using Async_Inn.Models.DTOs;
using Async_Inn.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Async_Inn.Models.Services
{
    public class RoomServices : IRoom
    {
        private readonly AsyncInnDbContext _context;

        public RoomServices(AsyncInnDbContext context)
        {
            _context = context;
        }

        public async Task<RoomDTO> CreateRoom(RoomDTO roomDTO)
        {
            var room = new Room
            {
                Name = roomDTO.Name,
                layout = Enum.Parse<Layout>(roomDTO.Layout)
            };

            _context.Rooms.Add(room);
            await _context.SaveChangesAsync();

            return MapToDTO(room);
        }

        public async Task<RoomDTO> UpdateRoom(int id, RoomDTO roomDTO)
        {
            var roomToUpdate = await _context.Rooms.FindAsync(id);

            if (roomToUpdate != null)
            {
                roomToUpdate.Name = roomDTO.Name;
                roomToUpdate.layout = Enum.Parse<Layout>(roomDTO.Layout);

                await _context.SaveChangesAsync();
            }

            return MapToDTO(roomToUpdate);
        }

        public async Task DeleteRoom(int id)
        {
            var room = await _context.Rooms.FindAsync(id);
            _context.Rooms.Remove(room);
            await _context.SaveChangesAsync();
        }

        public async Task<List<RoomDTO>> GetAllRooms()
        {
            var rooms = await _context.Rooms
                .Include(r => r.RoomAmenities)
                .ThenInclude(ra => ra.Amenities)
                .Include(hotelRooms => hotelRooms.HotelRooms)
                .ThenInclude(hotel => hotel.Hotel)
                .ToListAsync();

            return rooms.Select(MapToDTO).ToList();
        }

        public async Task<RoomDTO> GetRoomById(int id)
        {
            var room = await _context.Rooms
                .Include(amenities => amenities.RoomAmenities)
                .ThenInclude(amenity => amenity.Amenities)
                .Include(hotelRooms => hotelRooms.HotelRooms)
                .ThenInclude(hotel => hotel.Hotel)
                .FirstOrDefaultAsync(rId => rId.Id == id);

            return MapToDTO(room);
        }

        public async Task AddAmenityToRoom(int roomId, int amenityId)
        {
            RoomAmenity roomAmenity = new RoomAmenity()
            {
                RoomId = roomId,
                AmenityId = amenityId
            };

            _context.Entry(roomAmenity).State = EntityState.Added;

            await _context.SaveChangesAsync();
        }

        public async Task RemoveAmenityFromRoom(int roomId, int amenityId)
        {
            var result = await _context.RoomAmenities.FirstOrDefaultAsync(r => r.AmenityId == amenityId && r.RoomId == roomId);

            _context.Entry(result).State = EntityState.Deleted;

            await _context.SaveChangesAsync();
        }

        private RoomDTO MapToDTO(Room room)
        {
            return new RoomDTO
            {
                ID = room.Id,
                Name = room.Name,
                Layout = room.layout.ToString(),
                Amenities = room.RoomAmenities.Select(ra => new AmenityDTO
                {
                    ID = ra.Amenities.Id,
                    Name = ra.Amenities.Name
                }).ToList()
            };
        }
    }

}
