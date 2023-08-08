using Async_Inn.Data;
using Async_Inn.Models;
using Async_Inn.Models.DTOs;
using Async_Inn.Models.Interfaces;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAsyncInn
{
    public abstract class Mock : IDisposable
    {
        private readonly SqliteConnection _connection;

        protected readonly AsyncInnDbContext _db;


        public Mock()
        {
            _connection = new SqliteConnection("Filename=:memory:");
            _connection.Open();

            _db = new AsyncInnDbContext(
                new DbContextOptionsBuilder<AsyncInnDbContext>()
                .UseSqlite(_connection).Options);

            _db.Database.EnsureCreated();

        }


        protected async Task<Hotel> CreateAndSaveTestHotel()
        {
            var hotel = new Hotel
            {
                Name = "testhotel",
                StreetAddress = "testhotel",
                City = "testhotel",
                State = "testhotel",
                Country = "testcountry",
                Phone = "testhotel"
            };

            _db.Hotels.Add(hotel);
            await _db.SaveChangesAsync();

            Assert.NotEqual(0, hotel.Id);

            return hotel;

        }


        protected async Task<Amenity> CreateAndSaveTestAmenity()
        {
            var amenity = new Amenity() { Name = "amenityTest1" };
            _db.Amenities.Add(amenity);
            await _db.SaveChangesAsync();

            Assert.NotEqual(0, amenity.Id);

            return amenity;

        }



        public void Dispose()
        {

            _db?.Dispose();

            _connection?.Dispose();
        }

    }
}
