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
    [Route("api/Hotels")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private readonly IHotel _context;
        public HotelsController(IHotel context)
        {
            _context = context;
        }





        [Authorize(Roles = "District Manager", Policy = "Create")]

        [HttpPost]
        public async Task<ActionResult<HotelDTO>> PostHotel(HotelDTO hotelDTO)
        {
            var createdHotel = await _context.CreateHotel(hotelDTO);
            return Ok(createdHotel);
        }





        [Authorize(Roles = "District Manager", Policy = "Update")]

        [HttpPut("{id}")]
        public async Task<IActionResult> PutHotel(int id, HotelDTO hotelDTO)
        {
            if (id != hotelDTO.ID)
            {
                return BadRequest();
            }

            var updatedHotel = await _context.UpdateHotel(id, hotelDTO);
            return Ok(updatedHotel);
        }






        [Authorize(Roles = "District Manager", Policy = "Delete")]

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHotel(int id)
        {
            await _context.DeleteHotel(id);
            return Ok("Hotel Deleted Successfully!");
        }





        [AllowAnonymous]
        [Authorize(Roles = "District Manager", Policy = "Read")]

        [HttpGet]
        public async Task<ActionResult<List<HotelDTO>>> GetAllHotels()
        {
            var hotels = await _context.GetAllHotels();
            return Ok(hotels);
        }






        [AllowAnonymous]
        [Authorize(Roles = "District Manager", Policy = "Read")]

        [HttpGet("{id}")]
        public async Task<ActionResult<HotelDTO>> GetHotelById(int id)
        {
            var hotel = await _context.GetHotelById(id);
            if (hotel == null)
            {
                return NotFound();
            }

            return Ok(hotel);
        }





        [AllowAnonymous]

        [HttpGet("byName/{name}")]
        public async Task<ActionResult<HotelDTO>> GetHotelByName(string name)
        {
            var hotel = await _context.GetHotelByName(name);
            if (hotel == null)
            {
                return NotFound();
            }

            return Ok(hotel);
        }
    }
}
