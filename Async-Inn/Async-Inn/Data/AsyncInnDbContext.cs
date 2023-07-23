using Async_Inn.Models;
using Microsoft.EntityFrameworkCore;

namespace Async_Inn.Data
{
    public class AsyncInnDbContext : DbContext
    {
        public AsyncInnDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Hotel>().HasData(
                new Hotel() { Id = 1, Name = "AsyncInn1", StreetAddress = "Al-Hussein St", City = "Aqaba", Country = "Jordan", Phone = "0096232034020", State = "Aqaba" },
                new Hotel() { Id = 2, Name = "AsyncInn2", StreetAddress = "Queen Rania St", City = "Petra", Country = "Jordan", Phone = "0096232157201", State = "Petra" },
                new Hotel() { Id = 3, Name = "AsyncInn3", StreetAddress = "Madina Monawarah St", City = "Amman", Country = "Jordan", Phone = "0096265528822", State = "Amman" });
            modelBuilder.Entity<Room>().HasData(
                new Room() { Id = 1, Name = "Studio", Layout = 0 },
                new Room() { Id = 2, Name = "Single Room", Layout = 1 },
                new Room() { Id = 3, Name = "Double Room", Layout = 2 });

            modelBuilder.Entity<Amenity>().HasData(
                new Amenity() { Id = 1, Name = "Coffee Maker" },
                new Amenity() { Id = 2, Name = "AC" },
                new Amenity() { Id = 3, Name = "Sea View" });
        }

        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Amenity> Amenities { get; set; }
    }
}
