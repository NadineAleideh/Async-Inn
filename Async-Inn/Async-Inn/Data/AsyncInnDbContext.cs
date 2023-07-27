﻿using Async_Inn.Models;
using Microsoft.EntityFrameworkCore;

namespace Async_Inn.Data
{
    public class AsyncInnDbContext : DbContext // it's a bradige between our app and the DB , allowing us to interacte with the DB. entry point for interacting with the db to perform operations 
    {
        public AsyncInnDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //composite key associations
            modelBuilder.Entity<HotelRoom>().HasKey(x => new { x.HotelId, x.RoomNumber });
            modelBuilder.Entity<RoomAmenity>().HasKey(x => new { x.RoomId, x.AmenityId });

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

        public DbSet<Hotel> Hotels { get; set; } // correspond to db tables
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Amenity> Amenities { get; set; }

        public DbSet<HotelRoom> HotelRooms { get; set; }
        public DbSet<RoomAmenity> RoomAmenities { get; set; }
    }
}
