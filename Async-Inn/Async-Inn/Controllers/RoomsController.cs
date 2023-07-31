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


        [HttpPost]
        public async Task<ActionResult<Room>> PostRoom(Room room)
        {

            var createdRoom = await _context.CreateRoom(room);

            //return CreatedAtAction("GetRoom", new { id = createdRoom.Id }, createdRoom);

            //return Ok(createdRoom);
            return Ok("Room Added Successfully!");
        }



        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoom(int id, Room room)
        {
            if (id != room.Id)
            {
                return BadRequest();
            }

            var updateRoom = await _context.UpdateRoom(id, room);

            //return Ok(updateRoom);
            return Ok("Room updated Successfully!");
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoom(int id)
        {
            await _context.DeleteRoom(id);

            // return NoContent();
            return Ok("Room deleted Successfully!");
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Room>>> GetRooms()
        {
            var room = await _context.GetAllRooms();
            if (room == null || room.Count == 0)
            {
                return NotFound();
            }
            return Ok(room.ToList());
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Room>> GetRoomById(int id)
        {
            var room = await _context.GetRoomById(id);

            if (room == null)
            {
                return NotFound();
            }


            return Ok(room);
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
