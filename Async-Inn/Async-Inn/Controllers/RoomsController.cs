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

namespace Async_Inn.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly IRoom _context;

        public RoomsController(IRoom context)
        {
            _context = context;
        }

        // GET: api/Rooms
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Room>>> GetRooms()
        {
            var room = await _context.GetRooms();
            if (room == null || room.Count == 0)
            {
                return NotFound();
            }
            return Ok(room.ToList());
        }

        // GET: api/Rooms/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Room>> GetRoom(int id)
        {
            var room = await _context.GetRoom(id);

            if (room == null)
            {
                return NotFound();
            }


            return Ok(room);
        }

        // PUT: api/Rooms/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoom(int id, Room room)
        {
            if (id != room.Id)
            {
                return BadRequest();
            }

            var updateRoom = await _context.UpdateRoom(id, room);

            return Ok(updateRoom);
        }

        // POST: api/Rooms
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Room>> PostRoom(Room room)
        {
            if (_context == null)
            {
                return Problem("Entity set 'AsyncInnDbContext.Rooms'  is null.");
            }
            var createdRoom = await _context.CreateRoom(room);

            return CreatedAtAction("GetRoom", new { id = createdRoom.Id }, createdRoom);
        }

        // DELETE: api/Rooms/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoom(int id)
        {
            var room = await _context.GetRoom(id);

            if (room == null)
            {
                return NotFound();
            }
            await _context.DeleteRoom(id);

            return NoContent();
        }

        //Adds an amenity to a room


        [HttpPost("{roomId}/Amenity/{amenityId}")]
        public async Task<IActionResult> AddAmenityToRoom(int roomId, int amenityId)
        {
            try
            {
                await _context.AddAmenityToRoom(roomId, amenityId);
                return Ok("Amenity added to the room successfully !");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }


        //removes an amenity from a room
        [HttpDelete("{roomId}/Amenity/{amenityId}")]
        public async Task<IActionResult> RemoveAmenityFromRoom(int roomId, int amenityId)
        {
            try
            {
                await _context.RemoveAmenityFromRoom(roomId, amenityId);
                return Ok("Amenity removed succsessfully !");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }




    }
}
