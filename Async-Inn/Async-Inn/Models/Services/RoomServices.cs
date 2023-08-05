using Async_Inn.Data;
using Async_Inn.Models.DTOs;
using Async_Inn.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Async_Inn.Models.Services
{
    public class RoomServices : IRoom
    {
        private readonly AsyncInnDbContext _context;
        private readonly IAmenity _amenity;
        public RoomServices(AsyncInnDbContext context, IAmenity _am)
        {
            _context = context;
            _amenity = _am;
        }


        /// <summary>
        /// to create a new room
        /// </summary>
        /// <param name="roomDTO"></param>
        /// <returns>the created room</returns>
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


        /// <summary>
        /// to update a specific room by passing it's id and the updates
        /// </summary>
        /// <param name="id"></param>
        /// <param name="roomDTO"></param>
        /// <returns>the updated room</returns>
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


        /// <summary>
        /// to delete a specific room by it's id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>nothing</returns>
        public async Task DeleteRoom(int id)
        {
            var room = await _context.Rooms.FindAsync(id);
            _context.Rooms.Remove(room);
            await _context.SaveChangesAsync();
        }



        /// <summary>
        /// to get all rooms
        /// </summary>
        /// <returns>list of all rooms</returns>
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


        /// <summary>
        /// to get a specific room by it's id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>the roo it self</returns>
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



        /// <summary>
        /// to add a specific amenity from a spesific room
        /// </summary>
        /// <param name="roomId"></param>
        /// <param name="amenityId"></param>
        /// <returns>nothing</returns>
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


        /// <summary>
        /// to remove a specific amenity from a spesific room
        /// </summary>
        /// <param name="roomId"></param>
        /// <param name="amenityId"></param>
        /// <returns>nothing</returns>
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
