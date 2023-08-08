using Async_Inn;
using Async_Inn.Models;
using Async_Inn.Models.DTOs;
using Async_Inn.Models.Interfaces;
using Async_Inn.Models.Services;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using System.Threading.Tasks;

namespace TestAsyncInn
{
    public class UnitTest1 : Mock
    {

        //Hotel tests 

        //Create
        [Fact]
        public async Task CreatHotelTest()
        {
            var hotel = await CreateAndSaveTestHotel();
            var service = new HotelServices(_db);

            var hotelDTO = new HotelDTO()
            {
                ID = hotel.Id,
                Name = "nametest",
                StreetAddress = "addresstest",
                City = "citytest",
                State = "statetest",
                Phone = "phonetest"

            };

            var actualHotel = await service.CreateHotel(hotelDTO);

            Assert.NotNull(actualHotel);
            Assert.Equal(actualHotel.Name, hotelDTO.Name);
        }


        //update

        [Fact]
        public async void UpdateHotel()
        {
            var hotel = await CreateAndSaveTestHotel();
            var service = new HotelServices(_db);

            var hotelDTO = new HotelDTO()
            {
                ID = hotel.Id,
                Name = "nametest",
                StreetAddress = "addresstest",
                City = "citytest",
                State = "statetest",
                Phone = "phonetest"

            };

            var UpdatedHotel = new HotelDTO()
            {
                Name = "Updated Name",
                City = "Updated City",
                StreetAddress = hotel.StreetAddress,
                State = hotel.State,
                Phone = hotel.Phone,
            };

            var actualHotel = await service.UpdateHotel(hotelDTO.ID, UpdatedHotel);

            Assert.Equal(UpdatedHotel.City, actualHotel.City);
            Assert.Equal(UpdatedHotel.StreetAddress, actualHotel.StreetAddress);
            Assert.Equal(UpdatedHotel.State, actualHotel.State);
            Assert.Equal(UpdatedHotel.Phone, actualHotel.Phone);

        }

        //Delete
        [Fact]
        public async Task DeleteHotelTest()
        {
            var hotel = await CreateAndSaveTestHotel();
            var service = new HotelServices(_db);

            var hotelDTO = new HotelDTO()
            {
                ID = hotel.Id,
                Name = "nametest",
                StreetAddress = "addresstest",
                City = "citytest",
                State = "statetest",
                Phone = "phonetest"

            };

            await service.CreateHotel(hotelDTO);

            var actualHotel = await service.GetHotelById(hotelDTO.ID);
            await service.DeleteHotel(hotelDTO.ID);
            var deletedHotel = await service.GetHotelById(hotelDTO.ID);
            Assert.Null(deletedHotel);

        }


        //Amenity tests

        //create 
        [Fact]
        public async Task CreatAmenityTest()
        {
            var amenity = await CreateAndSaveTestAmenity();
            var service = new AmenityServices(_db);

            var AmenityDTO = new AmenityDTO()
            {

                Name = "amenityTest1",

            };

            var actualAmenity = await service.CreateAmenity(AmenityDTO);



            Assert.NotNull(actualAmenity);
            Assert.Equal(actualAmenity.Name, amenity.Name);
        }


        //update

        [Fact]
        public async void UpdateAmenity()
        {
            var amenity = await CreateAndSaveTestAmenity();
            var service = new AmenityServices(_db);

            var AmenityDTO = new AmenityDTO()
            {

                Name = "amenityTest1",

            };

            var UpdatedAmenity = new AmenityDTO()
            {
                Name = "newamenityTest1",

            };

            var actualAmenity = await service.UpdateAmenity(AmenityDTO.ID, UpdatedAmenity);

            Assert.Equal(UpdatedAmenity.Name, actualAmenity.Name);

        }

        //delete

        [Fact]
        public async Task DeleteAmenityTest()
        {
            var amenity = await CreateAndSaveTestAmenity();
            var service = new AmenityServices(_db);

            var AmenityDTO = new AmenityDTO()
            {

                Name = "amenityTest1",

            };

            await service.CreateAmenity(AmenityDTO);
            var actualAmenity = await service.GetAmenityById(AmenityDTO.ID);

            await service.DeleteAmenity(AmenityDTO.ID);

            var deletedAmenity = await service.GetAmenityById(AmenityDTO.ID);

            Assert.Null(deletedAmenity);

        }

    }
}