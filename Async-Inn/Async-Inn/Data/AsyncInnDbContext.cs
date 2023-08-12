using Async_Inn.Models;
using Async_Inn.Models.DTOs;
using Async_Inn.Models.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Async_Inn.Data
{
    public class AsyncInnDbContext : IdentityDbContext<ApplicationUser>// it's a bradige between our app and the DB , allowing us to interacte with the DB. entry point for interacting with the db to perform operations 
    {// I updated the DbContext to IdentityDbContext deal with DB in context of using user authentication and authrization. 
        public AsyncInnDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //this calling setsup many things related to the user roles and the other identity related data
            base.OnModelCreating(modelBuilder);

            //composite key associations
            modelBuilder.Entity<HotelRoom>().HasKey(x => new { x.HotelId, x.RoomNumber });
            modelBuilder.Entity<RoomAmenity>().HasKey(x => new { x.RoomId, x.AmenityId });

            modelBuilder.Entity<Hotel>().HasData(
                new Hotel() { Id = 1, Name = "AsyncInn1", StreetAddress = "Al-Hussein St", City = "Aqaba", Country = "Jordan", Phone = "0096232034020", State = "Aqaba" },
                new Hotel() { Id = 2, Name = "AsyncInn2", StreetAddress = "Queen Rania St", City = "Petra", Country = "Jordan", Phone = "0096232157201", State = "Petra" },
                new Hotel() { Id = 3, Name = "AsyncInn3", StreetAddress = "Madina Monawarah St", City = "Amman", Country = "Jordan", Phone = "0096265528822", State = "Amman" });
            modelBuilder.Entity<Room>().HasData(
               new Room() { Id = 1, Name = "Studio", layout = (Layout)0 },
               new Room() { Id = 2, Name = "Single Room", layout = (Layout)1 },
               new Room() { Id = 3, Name = "Double Room", layout = (Layout)2 });

            modelBuilder.Entity<Amenity>().HasData(
                new Amenity() { Id = 1, Name = "Coffee Maker" },
                new Amenity() { Id = 2, Name = "AC" },
                new Amenity() { Id = 3, Name = "Sea View" });


            modelBuilder.Entity<HotelRoom>().HasData(
                 new HotelRoom
                 {
                     HotelId = 1,
                     RoomNumber = 101,
                     RoomId = 1,
                     Price = 100.00,
                     PetFriendly = true
                 },
                 new HotelRoom
                 {
                     HotelId = 1,
                     RoomNumber = 102,
                     RoomId = 2,
                     Price = 120.00,
                     PetFriendly = false
                 });



            // Configure the relationship between Room and RoomAmenity entities
            modelBuilder.Entity<RoomAmenity>()
            .HasOne(ra => ra.Room)
            .WithMany(r => r.RoomAmenities)
            .HasForeignKey(ra => ra.RoomId)
            .OnDelete(DeleteBehavior.Cascade); // This line configures cascading delete


            // Configure the relationship between RoomAmenity and Amenity entities
            modelBuilder.Entity<RoomAmenity>()
                .HasOne(ra => ra.Amenities)
                .WithMany(a => a.RoomAmenities)
                .HasForeignKey(ra => ra.AmenityId)
                .OnDelete(DeleteBehavior.Cascade); // <-- This enables cascading delete

            // Configuring cascading delete for HotelRoom entity
            modelBuilder.Entity<HotelRoom>()
                .HasOne(hr => hr.Hotel)
                .WithMany(h => h.HotelRoom)
                .HasForeignKey(hr => hr.HotelId)
                .OnDelete(DeleteBehavior.Cascade);

            // configure cascading delete for Room
            modelBuilder.Entity<Room>()
                .HasMany(r => r.HotelRooms)
                .WithOne(hr => hr.Room)
                .HasForeignKey(hr => hr.RoomId)
                .OnDelete(DeleteBehavior.Cascade);




            SeedRole(modelBuilder, "District Manager", "Create", "Read", "Update", "Delete");
            SeedRole(modelBuilder, "Property Manager", "Create", "Read", "Update");
            SeedRole(modelBuilder, "Agent", "Create", "Read", "Update", "Delete");
            SeedRole(modelBuilder, "Anonymous users");
        }



        int nextId = 1;
        private void SeedRole(ModelBuilder modelBuilder, string roleName, params string[] permissions)
        {
            var role = new IdentityRole()
            {
                Id = roleName.ToLower(),
                Name = roleName,
                NormalizedName = roleName.ToUpper(),
                ConcurrencyStamp = Guid.Empty.ToString()
            };

            modelBuilder.Entity<IdentityRole>().HasData(role);

            var roleClaim = permissions.Select(permission =>
            new IdentityRoleClaim<string>
            {
                Id = nextId++,
                RoleId = role.Id,
                ClaimType = "permissions",
                ClaimValue = permission
            }).ToArray();

            modelBuilder.Entity<IdentityRoleClaim<string>>().HasData(roleClaim);
        }




        // correspond to db tables
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Amenity> Amenities { get; set; }

        public DbSet<HotelRoom> HotelRooms { get; set; }
        public DbSet<RoomAmenity> RoomAmenities { get; set; }
    }
}
