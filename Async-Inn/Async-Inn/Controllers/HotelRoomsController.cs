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


        // POST to add a room to a hotel: /api/Hotels/{hotelId}/Rooms
        [HttpPost]
        public async Task<ActionResult<HotelRoom>> AddHotelRoom(int hotelId, HotelRoom hotelRoom)
        {
            hotelRoom.HotelId = hotelId;
            var addedHotelRoom = await _context.AddHotelRoom(hotelId, hotelRoom);
            // return CreatedAtAction(nameof(GetHotelRoom), new { hotelId, roomId = addedHotelRoom.RoomId }, addedHotelRoom);
            return Ok("added successfully!");
        }

        // PUT update the details of a specific room: /api/Hotels/{hotelId}/Rooms/{roomNumber}
        [HttpPut("{roomNumber}")]

        public async Task<IActionResult> PutHotelRoom([FromRoute] int hotelId, [FromRoute] int roomNumber, [FromBody] HotelRoom hotelRoom)
        {
            var updateHotelRoom = await _context.UpdateHotelRoom(hotelId, roomNumber, hotelRoom);

            return Ok(updateHotelRoom);
        }


        // DELETE a specific room from a hotel: /api/Hotels/{hotelId}/Rooms/{roomNumber}
        [HttpDelete("{roomNumber}")]
        public async Task<IActionResult> DeleteRoomFromHotel(int hotelId, int roomNmber)
        {
            if (_context == null)
            {
                return NotFound();
            }
            await _context.DeleteHotelRoom(hotelId, roomNmber);


            return Ok("deleted successfully!");

        }

        // GET all the rooms for a hotel: /api/Hotels/{hotelId}/Rooms
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HotelRoom>>> GetHotelRooms(int hotelId)
        {
            if (_context == null)
            {
                return NotFound();
            }

            var hotelRooms = await _context.GetAllHotelRooms(hotelId);

            return Ok(hotelRooms);
        }

        // GET all room details for a specific room: /api/Hotels/{hotelId}/Rooms/{roomNumber}
        [HttpGet("{roomNumber}")]
        public async Task<ActionResult<HotelRoom>> GetHotelRoom(int hotelId, int roomNumber)
        {
            var hotelRoom = await _context.GetHotelRoom(roomNumber, hotelId);
            if (hotelRoom == null)
            {
                return NotFound();
            }

            return Ok(hotelRoom);
        }





    }
}
