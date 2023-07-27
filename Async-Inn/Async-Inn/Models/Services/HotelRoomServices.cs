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

        public async Task<HotelRoom> AddHotelRoom(HotelRoom hotelRoom)
        {
            _context.HotelRooms.Add(hotelRoom);
            await _context.SaveChangesAsync();
            return hotelRoom;
        }
        public async Task<HotelRoom> CreateHotelRoom(HotelRoom hotelRoom)
        {
            _context.Entry(hotelRoom).State = EntityState.Added;
            await _context.SaveChangesAsync();
            return hotelRoom;

        }

        public async Task DeleteHotelRoom(int hotelId, int roomNumber)
        {
            HotelRoom HotelRoom = await GetHotelRoom(hotelId, roomNumber);
            _context.Entry(HotelRoom).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        public async Task<HotelRoom> GetHotelRoom(int hotelId, int roomNumber)
        {
            return await _context.HotelRooms
                                .Where(h => h.HotelId == hotelId && h.RoomNumber == roomNumber)
                                .FirstOrDefaultAsync();
        }

        public async Task<List<HotelRoom>> GetHotelRooms(int hotelId)
        {
            return await _context.HotelRooms
                                 .Where(h => h.HotelId == hotelId)
                                 .ToListAsync();
        }

        public async Task<HotelRoom> UpdateHotelRoom(HotelRoom hotelRoom)
        {
            _context.Entry(hotelRoom).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return hotelRoom;
        }
        public async Task RemoveRoomFromHotel(int roomNumber, int hotelId)
        {
            var result = await _context.HotelRooms.FirstOrDefaultAsync(x => x.RoomNumber == roomNumber && x.HotelId == hotelId);

            _context.Entry(result).State = EntityState.Deleted;

            await _context.SaveChangesAsync();
        }



    }
}
