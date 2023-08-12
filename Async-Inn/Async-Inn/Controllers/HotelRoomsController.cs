using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Async_Inn.Data;
using Async_Inn.Models;
using Async_Inn.Models.Interfaces;
using Async_Inn.Models.Services;
using Async_Inn.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace Async_Inn.Controllers
{
    [Route("api/Hotels/{hotelId}/Rooms")]
    [ApiController]
    public class HotelRoomsController : ControllerBase
    {
        private readonly IHotelRoom _context;

        public HotelRoomsController(IHotelRoom context)
        {
            _context = context;
        }




        [Authorize(Roles = "District Manager , Property Manager", Policy = "Create")]

        [HttpPost]
        public async Task<ActionResult<HotelRoomDTO>> AddHotelRoom(int hotelId, HotelRoomDTO hotelRoomDTO)
        {
            hotelRoomDTO.HotelID = hotelId;
            var addedHotelRoom = await _context.AddHotelRoom(hotelId, hotelRoomDTO);
            return Ok(addedHotelRoom);
        }





        [Authorize(Roles = "District Manager , Property Manager, Agent", Policy = "Update")]

        [HttpPut("{roomNumber}")]
        public async Task<IActionResult> PutHotelRoom(int hotelId, int roomNumber, HotelRoomDTO hotelRoomDTO)
        {
            var updateHotelRoom = await _context.UpdateHotelRoom(hotelId, roomNumber, hotelRoomDTO);
            return Ok(updateHotelRoom);
        }





        [Authorize(Roles = "District Manager", Policy = "Delete")]

        [HttpDelete("{roomNumber}")]
        public async Task<IActionResult> DeleteRoomFromHotel(int hotelId, int roomNumber)
        {
            await _context.DeleteHotelRoom(hotelId, roomNumber);
            return Ok("Hotel room deleted successfully!");
        }






        [AllowAnonymous]
        [Authorize(Roles = "District Manager , Property Manager, Agent", Policy = "Read")]

        [HttpGet]
        public async Task<ActionResult<List<HotelRoomDTO>>> GetHotelRooms(int hotelId)
        {
            var hotelRooms = await _context.GetAllHotelRooms(hotelId);
            return Ok(hotelRooms);
        }





        [AllowAnonymous]
        [Authorize(Roles = "District Manager , Property Manager, Agent", Policy = "Read")]

        [HttpGet("{roomNumber}")]
        public async Task<ActionResult<HotelRoomDTO>> GetHotelRoom(int hotelId, int roomNumber)
        {
            var hotelRoom = await _context.GetHotelRoom(hotelId, roomNumber);
            if (hotelRoom == null)
            {
                return NotFound();
            }

            return Ok(hotelRoom);
        }
    }

}
