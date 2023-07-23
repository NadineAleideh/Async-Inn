using Async_Inn.Data;
using Async_Inn.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Async_Inn.Models.Services
{
    public class HotelServices : IHotel
    {
        private readonly AsyncInnDbContext _hotel;

        public HotelServices(AsyncInnDbContext hotel)
        {
            _hotel = hotel;
        }
        public async Task<Hotel> CreateHotel(Hotel hotel)
        {
            _hotel.Hotels.Add(hotel);

            await _hotel.SaveChangesAsync();

            return hotel;
        }

        public async Task DeleteHotel(int id)
        {
            Hotel hotel = await GetHotel(id);

            _hotel.Entry<Hotel>(hotel).State = EntityState.Deleted;

            await _hotel.SaveChangesAsync();
        }

        public async Task<Hotel> GetHotel(int id)
        {
            Hotel hotel = await _hotel.Hotels.FindAsync(id);

            return hotel;
        }

        public async Task<List<Hotel>> GetHotels()
        {
            var hotels = await _hotel.Hotels.ToListAsync();

            return hotels;
        }

        public async Task<Hotel> UpdateHotel(int id, Hotel hotel)
        {
            var oldhotel = await GetHotel(id);

            if (oldhotel != null)
            {
                oldhotel.Name = hotel.Name;
                oldhotel.StreetAddress = hotel.StreetAddress;
                oldhotel.City = hotel.City;
                oldhotel.State = hotel.State;
                oldhotel.Country = hotel.Country;
                oldhotel.Phone = hotel.Phone;

                await _hotel.SaveChangesAsync();
            }


            return oldhotel;
        }
    }
}
