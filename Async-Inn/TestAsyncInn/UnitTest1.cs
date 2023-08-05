using Async_Inn;
using Async_Inn.Models;
using Async_Inn.Models.DTOs;
using Async_Inn.Models.Interfaces;
using Async_Inn.Models.Services;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace TestAsyncInn
{
    public class UnitTest1 : Mock
    {


        [Fact]
        public async void Can_Add_And_Delete_Rooms()
        {
            //Arrange
            var room = await CreateAndSaveTestRoom();
            var amenity = await CreateAndSaveTestAmenity();

            var service = new RoomServices(_db, _am);

            //Act
            await service.AddAmenityToRoom(room.Id, amenity.Id);

            //Assert 
            var x = await service.GetRoomById(room.Id);
            Assert.Contains(x.Amenities, a => a.ID == amenity.Id);
        }

        //[Fact]
        //public async void Delete_Room_FromDataBase_Test()
        //{
        //    // Arrange 
        //    var room = await DeleteAndSaveRoomTest();

        //    var service = new RoomServices(_db, _am);

        //    // Act
        //    var deletedRoom = await service.GetRoomById(room.Id);

        //    // Assert
        //    Assert.Null(deletedRoom);
        //}

        //[Fact]
        //public async void Create_Room_Test()
        //{

        //    var room = new RoomDTO()
        //    {

        //        Name = "Test1",
        //        Layout = "1"


        //    };
        //    var service = new RoomServices(_db, _am);

        //    var add = await service.CreateRoom(room);

        //    Assert.NotNull(add);

        //}

        [Fact]
        public async void Create_Amenity_Test()
        {

            var amenity = new AmenityDTO()
            {

                Name = "Test1",



            };
            var service = new AmenityServices(_db);

            var add = await service.CreateAmenity(amenity);

            Assert.NotNull(add);

        }


        [Fact]
        public async void Delete_Amenity_FromDataBase_Test()
        {
            // Arrange 
            var amenity = await DeleteAndSaveAmenityTest();

            var service = new AmenityServices(_db);

            // Act
            var deletedAmenity = await service.GetAmenityById(amenity.Id);

            // Assert
            Assert.Null(deletedAmenity);
        }


        [Fact]
        public async void DataSeedingTest_HotelDbSet()
        {
            //Arrange
            var hotels = new List<Hotel>
            {
            new Hotel { Id = 1, Name = "Lux Room", StreetAddress = "TV-Street", City = "Amman", Country = "Jordan", Phone = "0796153883", State = "Middle-East" },
            new Hotel { Id = 2, Name = "Grand Hotel", StreetAddress = "Central Avenue", City = "New York", Country = "USA", Phone = "1234567890", State = "North America" },
            new Hotel { Id = 3, Name = "Seaside Resort", StreetAddress = "Beach Road", City = "Sydney", Country = "Australia", Phone = "9876543210", State = "Oceania" }

            };

            var service = new HotelServices(_db);

            //Act
            var Hots = await service.GetAllHotels();
            Assert.Equal(hotels.Count, Hots.Count);
        }



    }
}