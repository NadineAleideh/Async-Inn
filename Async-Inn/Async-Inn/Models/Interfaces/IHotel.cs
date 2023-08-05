using Async_Inn.Models.DTOs;

namespace Async_Inn.Models.Interfaces
{
    public interface IHotel
    {

        /// <summary>
        /// to create a new hotel
        /// </summary>
        /// <param name="hotelDTO"></param>
        /// <returns>the created hotel</returns>
        Task<HotelDTO> CreateHotel(HotelDTO hotelDTO);

        /// <summary>
        /// to update a specific hotel by passing it's id and the updates
        /// </summary>
        /// <param name="id"></param>
        /// <param name="hotelDTO"></param>
        /// <returns>the updated hotel</returns>
        Task<HotelDTO> UpdateHotel(int id, HotelDTO hotelDTO);

        /// <summary>
        /// to delete a specific hotel
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>nothing</returns>
        Task DeleteHotel(int Id);

        /// <summary>
        /// to get a specific hotel by it's id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>the hotel itself</returns>
        Task<HotelDTO> GetHotelById(int id);

        /// <summary>
        /// to get a specific hotel by it's name
        /// </summary>
        /// <param name="name"></param>
        /// <returns>the hotel itself</returns>
        Task<HotelDTO> GetHotelByName(string name);


        /// <summary>
        /// to get all hotels
        /// </summary>
        /// <returns>list of hotels</returns>
        Task<List<HotelDTO>> GetAllHotels();

    }
}
