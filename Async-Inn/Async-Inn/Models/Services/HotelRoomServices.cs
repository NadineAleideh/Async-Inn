using Async_Inn.Data;
using Async_Inn.Models.DTOs;
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


        /// <summary>
        /// to add a room with it's own specifications to a specific hotel
        /// </summary>
        /// <param name="hotelId"></param>
        /// <param name="hotelRoomDTO"></param>
        /// <returns>the room added itself</returns>
        public async Task<HotelRoomDTO> AddHotelRoom(int hotelId, HotelRoomDTO hotelRoomDTO)
        {
            var room = await _context.Rooms.FindAsync(hotelRoomDTO.RoomID);
            var hotel = await _context.Hotels.FindAsync(hotelRoomDTO.HotelID);

            var hotelRoom = new HotelRoom
            {
                HotelId = hotelId,
                RoomId = hotelRoomDTO.RoomID,
                RoomNumber = hotelRoomDTO.RoomNumber,
                Price = hotelRoomDTO.Price,
                PetFriendly = hotelRoomDTO.PetFriendly,
                Room = room,
                Hotel = hotel
            };

            _context.HotelRooms.Add(hotelRoom);
            await _context.SaveChangesAsync();

            return hotelRoomDTO;
        }



        /// <summary>
        /// to update a room with it's own specifications in a specific hotel
        /// </summary>
        /// <param name="hotelId"></param>
        /// <param name="roomNumber"></param>
        /// <param name="hotelRoomDTO"></param>
        /// <returns>the room updated itself</returns>
        public async Task<HotelRoomDTO> UpdateHotelRoom(int hotelId, int roomNumber, HotelRoomDTO hotelRoomDTO)
        {
            var hotelRoom = await _context.HotelRooms.FindAsync(hotelId, roomNumber);
            if (hotelRoom != null)
            {
                hotelRoom.HotelId = hotelRoomDTO.HotelID;
                hotelRoom.RoomId = hotelRoomDTO.RoomID;
                hotelRoom.RoomNumber = hotelRoomDTO.RoomNumber;
                hotelRoom.Price = hotelRoomDTO.Price;
                hotelRoom.PetFriendly = hotelRoomDTO.PetFriendly;

                await _context.SaveChangesAsync();
            }

            return hotelRoomDTO;
        }


        /// <summary>
        /// to delete a room with it's own specifications from a specific hotel
        /// </summary>
        /// <param name="hotelId"></param>
        /// <param name="roomNumber"></param>
        /// <returns>nothing</returns>
        public async Task DeleteHotelRoom(int hotelId, int roomNumber)
        {
            var roomtodelete = await _context.HotelRooms
                .Where(r => r.HotelId == hotelId && r.RoomNumber == roomNumber)
                .FirstOrDefaultAsync();
            if (roomtodelete != null)
            {
                _context.Entry<HotelRoom>(roomtodelete).State = EntityState.Deleted;

                await _context.SaveChangesAsync();
            }
        }


        /// <summary>
        /// to get all rooms with it's own specifications that belong to specific hotel
        /// </summary>
        /// <param name="hotelId"></param>
        /// <returns>list of rooms for a specific hotel</returns>
        public async Task<List<HotelRoomDTO>> GetAllHotelRooms(int hotelId)
        {
            var hotelRooms = await _context.HotelRooms
                .Include(hotel => hotel.Hotel)
                .Include(room => room.Room)
                .ThenInclude(amenities => amenities.RoomAmenities)
                .ThenInclude(roomAmenities => roomAmenities.Amenities)
                .Where(x => x.HotelId == hotelId)
                .ToListAsync();

            var result = hotelRooms.Select(hr => new HotelRoomDTO
            {
                HotelID = hr.HotelId,
                RoomNumber = hr.RoomNumber,
                Price = hr.Price,
                PetFriendly = hr.PetFriendly,
                RoomID = hr.RoomId,
                Room = new RoomDTO
                {
                    ID = hr.Room.Id,
                    Name = hr.Room.Name,
                    Layout = hr.Room.layout.ToString(), // Convert the enum to string
                    Amenities = hr.Room.RoomAmenities.Select(ra => new AmenityDTO
                    {
                        ID = ra.Amenities.Id,
                        Name = ra.Amenities.Name,
                    }).ToList()
                }
            }).ToList();

            return result;
        }


        /// <summary>
        /// to get a room with it's own specifications that belongs to specific hotel
        /// </summary>
        /// <param name="hotelId"></param>
        /// <param name="roomNumber"></param>
        /// <returns>room for a specific hotel</returns>
        public async Task<HotelRoomDTO> GetHotelRoom(int hotelId, int roomNumber)
        {
            var hotelroom = await _context.HotelRooms
                .Include(hotel => hotel.Hotel)
                .Include(room => room.Room)
                .ThenInclude(roomAmenities => roomAmenities.RoomAmenities)
                .ThenInclude(amenity => amenity.Amenities)
                .Where(hotel => hotel.HotelId == hotelId && hotel.RoomNumber == roomNumber)
                .FirstOrDefaultAsync();

            if (hotelroom == null)
            {
                return null;
            }

            var result = new HotelRoomDTO
            {
                HotelID = hotelroom.HotelId,
                RoomNumber = hotelroom.RoomNumber,
                Price = hotelroom.Price,
                PetFriendly = hotelroom.PetFriendly,
                RoomID = hotelroom.RoomId,
                Room = new RoomDTO
                {
                    ID = hotelroom.Room.Id,
                    Name = hotelroom.Room.Name,
                    Layout = hotelroom.Room.layout.ToString(), // Convert the enum to string
                    Amenities = hotelroom.Room.RoomAmenities.Select(ra => new AmenityDTO
                    {
                        ID = ra.Amenities.Id,
                        Name = ra.Amenities.Name,
                    }).ToList()
                }
            };

            return result;
        }
    }



}
