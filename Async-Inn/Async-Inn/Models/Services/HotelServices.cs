using Async_Inn.Data;
using Async_Inn.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Async_Inn.Models.Services
{
    public class HotelServices : IHotel

    {    // this will brings the DB to the service

        private readonly AsyncInnDbContext _context;
        public HotelServices(AsyncInnDbContext context)
        {
            _context = context;
        }

        public async Task<Hotel> CreateHotel(Hotel hotel)
        {
            _context.Hotels.Add(hotel);

            await _context.SaveChangesAsync();

            return hotel;
        }
        public async Task<Hotel> UpdateHotel(int id, Hotel hotel)
        {
            var hoteltoupdte = await GetHotelById(id);

            if (hoteltoupdte != null)
            {
                hoteltoupdte.Name = hotel.Name;
                hoteltoupdte.StreetAddress = hotel.StreetAddress;
                hoteltoupdte.City = hotel.City;
                hoteltoupdte.State = hotel.State;
                hoteltoupdte.Country = hotel.Country;
                hoteltoupdte.Phone = hotel.Phone;

                await _context.SaveChangesAsync();
            }
            return hoteltoupdte;
        }

        public async Task DeleteHotel(int id)
        {
            var hotel = await GetHotelById(id);
            if (hotel != null)
            {
                _context.Hotels.Remove(hotel);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Hotel>> GetAllHotels()
        {
            var hotels = await _context.Hotels.Include(h => h.HotelRoom).ToListAsync();
            return hotels;
        }

        public async Task<Hotel> GetHotelById(int id)
        {
            var hotel = await _context.Hotels.Where(h => h.Id == id).Include(h => h.HotelRoom).FirstOrDefaultAsync();
            return hotel;
        }

        public async Task<Hotel> GetHotelByName(string name)
        {
            var hotels = await _context.Hotels.Include(h => h.HotelRoom).ToListAsync();
            var foundhotelbyname = hotels.FirstOrDefault(h => h.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            return foundhotelbyname;


        }




    }

}

