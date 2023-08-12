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
using Async_Inn.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using System.Data;

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



        [Authorize(Roles = "District Manager", Policy = "Create")]
        [HttpPost]
        public async Task<ActionResult<RoomDTO>> PostRoom(RoomDTO roomDTO)
        {
            var createdRoom = await _context.CreateRoom(roomDTO);
            return Ok(createdRoom);
        }




        [Authorize(Roles = "District Manager", Policy = "Update")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoom(int id, RoomDTO roomDTO)
        {
            if (id != roomDTO.ID)
            {
                return BadRequest();
            }

            var updateRoom = await _context.UpdateRoom(id, roomDTO);
            return Ok(updateRoom);
        }





        [Authorize(Roles = "District Manager", Policy = "Delete")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoom(int id)
        {
            await _context.DeleteRoom(id);
            return Ok("Room deleted Successfully!");
        }




        [AllowAnonymous]
        [Authorize(Roles = "District Manager", Policy = "Read")]

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoomDTO>>> GetRooms()
        {
            var rooms = await _context.GetAllRooms();
            if (rooms == null || rooms.Count == 0)
            {
                return NotFound();
            }
            return Ok(rooms);
        }





        [AllowAnonymous]
        [Authorize(Roles = "District Manager", Policy = "Read")]

        [HttpGet("{id}")]
        public async Task<ActionResult<RoomDTO>> GetRoomById(int id)
        {
            var room = await _context.GetRoomById(id);

            if (room == null)
            {
                return NotFound();
            }

            return Ok(room);
        }



        //Adds an amenity to a room


        [Authorize(Roles = "District Manager , Property Manager, Agent", Policy = "Create")]
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

        [Authorize(Roles = "District Manager , Agent", Policy = "Delete")]
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
