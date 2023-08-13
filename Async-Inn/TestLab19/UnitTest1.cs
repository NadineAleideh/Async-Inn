using Async_Inn.Controllers;
using Async_Inn.Models.DTOs;
using Async_Inn.Models.Interfaces;
using Async_Inn.Models.Services;
using Async_Inn.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Security.Claims;

namespace TestLab19
{
    public class UnitTest1 : Mock
    {

        // room and amenity
        [Fact]
        public async void AddAmenityToRoom()
        {
            var room = await CreateAndSaveRoom();

            var amenity = await CreateAndSaveAmenity();

            var roomAmenities = new RoomServices(_db);

            await roomAmenities.AddAmenityToRoom(room.Id, amenity.Id);

            var result = await roomAmenities.GetRoomById(room.Id);

            Assert.Contains(result.Amenities, x => x.ID == amenity.Id);
        }

        [Fact]
        public async void RemoveAmenityFromRoom()
        {
            var room = await CreateAndSaveRoom();

            var amenity = await CreateAndSaveAmenity();

            var roomAmenities = new RoomServices(_db);

            await roomAmenities.AddAmenityToRoom(room.Id, amenity.Id);

            await roomAmenities.RemoveAmenityFromRoom(room.Id, amenity.Id);

            var result = await roomAmenities.GetRoomById(room.Id);

            Assert.DoesNotContain(result.Amenities, x => x.ID == amenity.Id);
        }

        //[Fact]
        //public async void UpdateRoomByAddingANewDataAndNewAmenityAndRemoveTheOldOne()
        //{
        //    var room = await CreateAndSaveRoom();

        //    var amenity = await CreateAndSaveAmenity();

        //    var amenity2 = await CreateAndSaveAmenity2();




        //    var roomService = new RoomServices(_db);

        //    await roomService.AddAmenityToRoom(room.Id, amenity.Id);

        //    var result1 = await roomService.GetRoomById(room.Id);

        //    Assert.Contains(result1.Amenities, am => am.ID == amenity.Id);

        //    Assert.DoesNotContain(result1.Amenities, am => am.ID == amenity2.Id);

        //    var newRoom = new Room()
        //    {
        //        Name = "Flat Room",
        //        layout = (Layout)2
        //    };

        //    AddNewRoomDTO newRoomDto = new AddNewRoomDTO()
        //    {
        //        Name = newRoom.Name,
        //        Layout = (int)newRoom.layout,
        //        AmenityId = amenity2.Id

        //    };

        //    await roomService.UpdateRoom(room.Id, newRoomDto);

        //    var result2 = await roomService.GetRoomById(room.Id);

        //    Assert.Contains(result2.Amenities, am => am.ID == amenity2.Id);

        //    await roomService.RemoveAmenityFromRoom(room.Id, amenity.Id);

        //    result2 = await roomService.GetRoomById(room.Id);

        //    Assert.DoesNotContain(result2.Amenities, am => am.ID == amenity.Id);


        //}

        [Fact]
        public async Task HotelService_Should_Add_Hotel_Room()
        {
            // Arrange
            var room = await CreateAndSaveRoom();
            var hotel = await CreateAndSaveHotel();

            var hotelRoom = new HotelRoomDTO()
            {
                HotelID = hotel.Id,
                RoomNumber = 10,
                Price = 33,
                PetFriendly = true,
                RoomID = room.Id
            };

            var hotelRoomService = new HotelRoomServices(_db);

            // Act
            await hotelRoomService.AddHotelRoom(hotel.Id, hotelRoom);

            var hotelService = new HotelServices(_db);


            // Assert
            var result = await hotelService.GetHotelById(hotel.Id);
            Assert.Contains(result.Rooms, x => x.RoomID == room.Id && x.HotelID == hotel.Id);
        }

        [Fact]
        public async void UpdateANewHotelRoom()
        {

            var room = await CreateAndSaveRoom();
            var hotel = await CreateAndSaveHotel();

            var hotelRoom = new HotelRoomDTO()
            {
                HotelID = hotel.Id,
                RoomNumber = 10,
                Price = 33,
                PetFriendly = true,
                RoomID = room.Id
            };

            var hotelRoomService = new HotelRoomServices(_db);

            await hotelRoomService.AddHotelRoom(hotel.Id, hotelRoom);

            var hotelService = new HotelServices(_db);


            var result = await hotelService.GetHotelById(hotel.Id);

            Assert.Contains(result.Rooms, x => x.RoomID == room.Id);

            Assert.Contains(result.Rooms, x => x.HotelID == hotel.Id);

            var room2 = await CreateAndSaveRoom2();

            var newHotelRoom = new HotelRoomDTO()
            {
                HotelID = hotel.Id,
                RoomNumber = 10,
                Price = 33,
                PetFriendly = false,
                RoomID = room2.Id
            };

            await hotelRoomService.UpdateHotelRoom(hotel.Id, newHotelRoom.RoomNumber, newHotelRoom);

            var result2 = await hotelService.GetHotelById(hotel.Id);

            Assert.Contains(result2.Rooms, x => x.HotelID == hotel.Id);

            Assert.Contains(result2.Rooms, x => x.RoomID == room2.Id);

            Assert.DoesNotContain(result2.Rooms, x => x.RoomID == room.Id);
        }

        [Fact]
        public async void RemoveHotelRooms()
        {
            var room = await CreateAndSaveRoom();
            var hotel = await CreateAndSaveHotel();

            var hotelRoom = new HotelRoomDTO()
            {
                HotelID = hotel.Id,
                RoomNumber = 10,
                Price = 33,
                PetFriendly = true,
                RoomID = room.Id
            };

            var hotelRoomService = new HotelRoomServices(_db);

            await hotelRoomService.AddHotelRoom(hotel.Id, hotelRoom);

            var hotelService = new HotelServices(_db);


            var result = await hotelService.GetHotelById(hotel.Id);

            Assert.Contains(result.Rooms, x => x.RoomID == room.Id);

            Assert.Contains(result.Rooms, x => x.HotelID == hotel.Id);

            await hotelRoomService.DeleteHotelRoom(hotel.Id, hotelRoom.RoomNumber);
            result = await hotelService.GetHotelById(hotel.Id);
            Assert.DoesNotContain(result.Rooms, x => x.RoomID == room.Id);

            Assert.DoesNotContain(result.Rooms, x => x.HotelID == hotel.Id);
        }

        [Fact]
        public async void AddHotelAndRemoveIt()
        {
            var hotel = await CreateAndSaveHotel();
            var hotel2 = await CreateAndSaveHotel2();

            var hotelService = new HotelServices(_db);

            var result = await hotelService.GetAllHotels();

            Assert.Contains(result, x => x.ID == hotel.Id);
            Assert.Contains(result, x => x.ID == hotel2.Id);

            await hotelService.DeleteHotel(hotel.Id);

            result = await hotelService.GetAllHotels();
            Assert.DoesNotContain(result, x => x.ID == hotel.Id);
            Assert.Contains(result, x => x.ID == hotel2.Id);

        }

        [Fact]
        public async Task Register_User_As_District_Manager()
        {
            // Arrange
            var userMock = new Mock<IUser>();
            var userManagerMock = new Mock<UserManager<ApplicationUser>>(MockBehavior.Strict, null, null, null, null, null, null, null, null);
            var jwtTokenServiceMock = new Mock<JwtTokenService>(null, null);

            var roles = new List<Claim> { new Claim(ClaimTypes.Role, "District Manager") };
            var userPrincipal = new ClaimsPrincipal(new ClaimsIdentity(roles));

            var controller = new UsersController(userMock.Object);
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = userPrincipal
                }
            };

            var registerDto = new RegisterUserDTO
            {
                Username = "TestUser",
                Email = "test@example.com",
                PhoneNumber = "123456789",
                Password = "P@ssw0rd",
                Roles = new List<string> { "Agent" } // Adjust the roles as needed
            };

            var expectedResult = new UserDTO
            {
                Id = "UserId",
                Username = registerDto.Username,
                Token = "MockedToken",
                Roles = new List<string> { "Agent" } // Adjust the roles as needed
            };

            userMock.Setup(u => u.Register(It.IsAny<RegisterUserDTO>(), It.IsAny<Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary>(), It.IsAny<ClaimsPrincipal>()))
                            .ReturnsAsync(expectedResult);

            // Act
            var result = await controller.Register(registerDto);

            // Assert
            var actionResult = Assert.IsType<ActionResult<UserDTO>>(result);
            var userDto = Assert.IsType<UserDTO>(actionResult.Value);

            Assert.Equal(expectedResult.Username, userDto.Username);
            Assert.Equal(expectedResult.Roles, userDto.Roles);

        }
        [Fact]
        public async Task SignIn_User_Successfully()
        {
            // Arrange
            var expectedResult = new UserDTO
            {
                Id = "UserId",
                Username = "TestUser",
                Token = "MockedToken",
                Roles = new List<string> { "Agent" }
            };

            var userMock = SetupUserMock(expectedResult);
            var controller = new UsersController(userMock);

            var loginDto = new LoginDTO
            {
                Username = "TestUser",
                Password = "P@ssw0rd"
            };

            // Act
            var result = await controller.Login(loginDto);

            // Assert
            var actionResult = Assert.IsType<ActionResult<UserDTO>>(result);
            var userDto = Assert.IsType<UserDTO>(actionResult.Value);

            Assert.Equal(expectedResult.Username, userDto.Username);
            Assert.Equal(expectedResult.Roles, userDto.Roles);
        }


    }
}