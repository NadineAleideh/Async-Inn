using Async_Inn.Data;
using Async_Inn.Models.DTOs;
using Async_Inn.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Async_Inn.Models.Services
{
    public class HotelServices : IHotel
    {
        private readonly AsyncInnDbContext _context;

        public HotelServices(AsyncInnDbContext context)
        {
            _context = context;
        }

        public async Task<HotelDTO> CreateHotel(HotelDTO hotelDTO)
        {
            var hotel = new Hotel
            {
                Name = hotelDTO.Name,
                StreetAddress = hotelDTO.StreetAddress,
                City = hotelDTO.City,
                State = hotelDTO.State,
                Phone = hotelDTO.Phone
            };

            _context.Hotels.Add(hotel);
            await _context.SaveChangesAsync();

            hotelDTO.ID = hotel.Id;
            return hotelDTO;
        }

        public async Task<HotelDTO> UpdateHotel(int id, HotelDTO hotelDTO)
        {
            var hotel = await GetHotelById(id);

            if (hotel != null)
            {
                hotel.Name = hotelDTO.Name;
                hotel.StreetAddress = hotelDTO.StreetAddress;
                hotel.City = hotelDTO.City;
                hotel.State = hotelDTO.State;
                hotel.Phone = hotelDTO.Phone;

                await _context.SaveChangesAsync();
            }

            return hotelDTO;
        }

        public async Task DeleteHotel(int id)
        {
            var hotel = await _context.Hotels.FindAsync(id);
            if (hotel != null)
            {
                _context.Hotels.Remove(hotel);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<HotelDTO>> GetAllHotels()
        {
            var hotels = await _context.Hotels
        .Include(h => h.HotelRoom)
            .ThenInclude(hr => hr.Room)
                .ThenInclude(r => r.RoomAmenities)
                    .ThenInclude(ra => ra.Amenities).ToListAsync();
            var hotelDTOs = hotels.Select(hotel => new HotelDTO
            {
                ID = hotel.Id,
                Name = hotel.Name,
                StreetAddress = hotel.StreetAddress,
                City = hotel.City,
                State = hotel.State,
                Phone = hotel.Phone,
                Rooms = hotel.HotelRoom.Select(hr => new HotelRoomDTO
                {
                    HotelID = hr.HotelId,
                    RoomNumber = hr.RoomNumber,
                    Price = hr.Price,
                    PetFriendly = hr.PetFriendly,
                    RoomID = hr.Room.Id,
                    Room = new RoomDTO
                    {
                        ID = hr.Room.Id,
                        Name = hr.Room.Name,
                        Layout = hr.Room.layout.ToString(), // Convert the enum to string
                        Amenities = hr.Room.RoomAmenities.Select(ra => new AmenityDTO
                        {
                            ID = ra.Amenities.Id,
                            Name = ra.Amenities.Name
                        }).ToList()
                    }
                }).ToList()
            }).ToList();

            return hotelDTOs;
        }

        public async Task<HotelDTO> GetHotelById(int id)
        {
            var hotel = await _context.Hotels
        .Include(h => h.HotelRoom)
            .ThenInclude(hr => hr.Room)
                .ThenInclude(r => r.RoomAmenities)
                    .ThenInclude(ra => ra.Amenities)
        .FirstOrDefaultAsync(h => h.Id == id);

            if (hotel == null)
            {
                return null; // or throw an exception if needed
            }

            var hotelDTO = new HotelDTO
            {
                ID = hotel.Id,
                Name = hotel.Name,
                StreetAddress = hotel.StreetAddress,
                City = hotel.City,
                State = hotel.State,
                Phone = hotel.Phone,
                Rooms = hotel.HotelRoom.Select(hr => new HotelRoomDTO
                {
                    HotelID = hr.HotelId,
                    RoomNumber = hr.RoomNumber,
                    Price = hr.Price,
                    PetFriendly = hr.PetFriendly,
                    RoomID = hr.Room.Id,
                    Room = new RoomDTO
                    {
                        ID = hr.Room.Id,
                        Name = hr.Room.Name,
                        Layout = hr.Room.layout.ToString(), // Convert the enum to string
                        Amenities = hr.Room.RoomAmenities.Select(ra => new AmenityDTO
                        {
                            ID = ra.Amenities.Id,
                            Name = ra.Amenities.Name
                        }).ToList()
                    }
                }).ToList()
            };

            return hotelDTO;
        }

        public async Task<HotelDTO> GetHotelByName(string name)
        {
            var hotels = await _context.Hotels
        .Include(h => h.HotelRoom)
            .ThenInclude(hr => hr.Room)
                .ThenInclude(r => r.RoomAmenities)
                    .ThenInclude(ra => ra.Amenities).ToListAsync();
            var foundhotelbyname = hotels.FirstOrDefault(h => h.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            if (foundhotelbyname == null)
            {
                return null;
            }

            var hotelDTO = new HotelDTO
            {
                ID = foundhotelbyname.Id,
                Name = foundhotelbyname.Name,
                StreetAddress = foundhotelbyname.StreetAddress,
                City = foundhotelbyname.City,
                State = foundhotelbyname.State,
                Phone = foundhotelbyname.Phone,
                Rooms = foundhotelbyname.HotelRoom.Select(hr => new HotelRoomDTO
                {
                    HotelID = hr.HotelId,
                    RoomNumber = hr.RoomNumber,
                    Price = hr.Price,
                    PetFriendly = hr.PetFriendly,
                    RoomID = hr.Room.Id,
                    Room = new RoomDTO
                    {
                        ID = hr.Room.Id,
                        Name = hr.Room.Name,
                        Layout = hr.Room.layout.ToString(), // Convert the enum to string
                        Amenities = hr.Room.RoomAmenities.Select(ra => new AmenityDTO
                        {
                            ID = ra.Amenities.Id,
                            Name = ra.Amenities.Name
                        }).ToList()
                    }
                }).ToList()
            };

            return hotelDTO;
        }
    }



}

