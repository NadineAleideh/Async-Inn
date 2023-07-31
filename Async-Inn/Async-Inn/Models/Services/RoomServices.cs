using Async_Inn.Data;
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
        public async Task<Room> CreateRoom(Room room)
        {
            _context.Rooms.Add(room);

            await _context.SaveChangesAsync();

            return room;
        }

        public async Task<Room> UpdateRoom(int id, Room room)
        {
            var roomtoupdate = await _context.Rooms.FindAsync(id);

            if (roomtoupdate != null)
            {
                roomtoupdate.Name = room.Name;
                roomtoupdate.layout = room.layout;

                await _context.SaveChangesAsync();
            }

            return roomtoupdate;
        }

        public async Task DeleteRoom(int id)
        {
            Room room = await GetRoomById(id);

            _context.Entry<Room>(room).State = EntityState.Deleted;

            await _context.SaveChangesAsync();
        }


        public async Task<List<Room>> GetAllRooms()
        {
            var rooms = await _context.Rooms
                .Include(r => r.RoomAmenities)
                    .ThenInclude(ra => ra.Amenities)
                .Include(hotelRooms => hotelRooms.HotelRooms)
                .ThenInclude(hotel => hotel.Hotel)
                .ToListAsync();


            var result = rooms.Select(r => new Room
            {
                Id = r.Id,
                Name = r.Name,
                layout = r.layout,
                RoomAmenities = r.RoomAmenities.Select(ra => new RoomAmenity
                {
                    RoomId = ra.RoomId,
                    AmenityId = ra.AmenityId,
                    Amenities = new Amenity
                    {
                        Id = ra.Amenities.Id,
                        Name = ra.Amenities.Name,
                    },
                    Room = null

                }).ToList(),
                HotelRooms = r.HotelRooms.Select(ra => new HotelRoom
                {
                    HotelId = ra.HotelId,
                    RoomId = ra.RoomId,
                    RoomNumber = ra.RoomNumber,
                    Price = ra.Price,
                    PetFriendly = ra.PetFriendly,
                    Hotel = new Hotel
                    {
                        Id = ra.Hotel.Id,
                        Name = ra.Hotel.Name,
                        StreetAddress = ra.Hotel.StreetAddress,
                        City = ra.Hotel.City,
                        State = ra.Hotel.State,
                        Country = ra.Hotel.Country,
                        Phone = ra.Hotel.Phone
                    }

                }).ToList()

            }).ToList();

            return result;

        }

        public async Task<Room> GetRoomById(int id)
        {
            var room = await _context.Rooms
                .Include(amenities => amenities.RoomAmenities)
                .ThenInclude(amenity => amenity.Amenities)
                .Include(hotelRooms => hotelRooms.HotelRooms)
                .ThenInclude(hotel => hotel.Hotel)
                .FirstOrDefaultAsync(rId => rId.Id == id);

            return room;

        }


        //logic to add and remove amenities from rooms

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
    }
}
