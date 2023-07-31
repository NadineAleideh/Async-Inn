using Async_Inn.Data;
using Async_Inn.Models.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace Async_Inn.Models.Services
{
    public class HotelRoomServices : IHotelRoom
    {
        private readonly AsyncInnDbContext _context;

        public HotelRoomServices(AsyncInnDbContext context)
        {
            _context = context;
        }




        public async Task<HotelRoom> AddHotelRoom(int hotelId, HotelRoom hotelRoom)
        {
            var room = await _context.Rooms.FindAsync(hotelRoom.RoomId);
            var hotel = await _context.Hotels.FindAsync(hotelRoom.HotelId);

            hotelRoom.HotelId = hotelId;

            hotelRoom.Room = room;
            hotelRoom.Hotel = hotel;

            _context.HotelRooms.Add(hotelRoom);

            await _context.SaveChangesAsync();

            return hotelRoom;
        }

        public async Task<HotelRoom> UpdateHotelRoom(int hotelId, int roomNumber, HotelRoom hotelRoom)
        {
            var hotel = await _context.HotelRooms.FindAsync(hotelId, roomNumber);
            if (hotel != null)
            {
                hotel.HotelId = hotelRoom.HotelId;
                hotel.RoomId = hotelRoom.RoomId;
                hotel.RoomNumber = hotelRoom.RoomNumber;
                hotel.Price = hotelRoom.Price;
                hotel.PetFriendly = hotelRoom.PetFriendly;

                await _context.SaveChangesAsync();
            }

            return hotelRoom;
        }


        public async Task DeleteHotelRoom(int hotelId, int roomNumber)
        {

            var roomtodelete = await _context.HotelRooms
                .Where(r => r.HotelId == hotelId && r.RoomNumber == roomNumber)
                .FirstOrDefaultAsync();
            if (roomtodelete != null)
            {
                _context.Entry<HotelRoom>(roomtodelete).State = EntityState.Deleted;

                await _context.SaveChangesAsync();
            }
        }



        public async Task<List<HotelRoom>> GetAllHotelRooms(int hotelId)
        {
            var hotelRooms = await _context.HotelRooms
              .Include(hotel => hotel.Hotel)
              .Include(room => room.Room)
              .ThenInclude(amenities => amenities.RoomAmenities)
              .ThenInclude(roomAmenities => roomAmenities.Amenities)
              .Where(x => x.HotelId == hotelId)
              .ToListAsync();

            var result = hotelRooms.Select(hr => new HotelRoom
            {
                HotelId = hr.HotelId,
                RoomId = hr.RoomId,
                RoomNumber = hr.RoomNumber,
                Price = hr.Price,
                PetFriendly = hr.PetFriendly,
                Room = new Room
                {
                    Id = hr.Room.Id,
                    Name = hr.Room.Name,
                    layout = hr.Room.layout,
                    RoomAmenities = hr.Room.RoomAmenities.Select(ra => new RoomAmenity
                    {
                        RoomId = ra.RoomId,
                        AmenityId = ra.AmenityId,
                        Amenities = new Amenity
                        {
                            Id = ra.Amenities.Id,
                            Name = ra.Amenities.Name,
                        }
                    }).ToList()
                },
                Hotel = new Hotel
                {
                    Id = hr.Hotel.Id,
                    Name = hr.Hotel.Name,
                    StreetAddress = hr.Hotel.StreetAddress,
                    City = hr.Hotel.City,
                    State = hr.Hotel.State,
                    Country = hr.Hotel.Country,
                    Phone = hr.Hotel.Phone
                }

            }).ToList();

            return result;
        }




        public async Task<HotelRoom> GetHotelRoom(int hotelId, int roomNumber)
        {
            var hotelroom = await _context.HotelRooms
               .Include(hotel => hotel.Hotel)
               .Include(room => room.Room)
               .ThenInclude(roomAmenities => roomAmenities.RoomAmenities)
               .ThenInclude(amenity => amenity.Amenities)
               .Where(hotel => hotel.HotelId == hotelId && hotel.RoomNumber == roomNumber)
               .FirstOrDefaultAsync();

            return hotelroom;
        }

    }
}
