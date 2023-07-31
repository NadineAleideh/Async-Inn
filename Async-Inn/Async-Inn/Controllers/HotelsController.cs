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
    [Route("api/Hotels")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        // DI
        private readonly IHotel _context;
        public HotelsController(IHotel context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<Hotel>> PostHotel(Hotel hotel)
        {
            await _context.CreateHotel(hotel);
            // return CreatedAtAction("GetHotelById", new { id = hotel.Id }, hotel);
            return Ok("Hotel Added Successfuly!");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutHotel(int id, Hotel hotel)
        {
            if (id != hotel.Id)
            {
                return BadRequest();
            }

            var UpdatedHotel = await _context.UpdateHotel(id, hotel);

            return Ok(UpdatedHotel);
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteHotel(int id)
        {
            await _context.DeleteHotel(id);
            return Ok("Hotel Deleted Successfuly!");
        }

        [HttpGet]
        public async Task<ActionResult<List<Hotel>>> GetAllHotels()
        {
            var hotels = await _context.GetAllHotels();
            return Ok(hotels);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Hotel>> GetHotelByTd(int id)
        {
            var hotel = await _context.GetHotelById(id);
            return Ok(hotel);
        }

        [HttpGet("byName/{name}")]


        public async Task<ActionResult<Hotel>> GetHotelByName(string name)
        {
            var hotel = await _context.GetHotelByName(name);
            return Ok(hotel);
        }
    }
}
