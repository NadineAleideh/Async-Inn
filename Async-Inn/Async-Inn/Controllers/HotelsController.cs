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
    [Route("api/[controller]")] // these are called data anotation , properites 
    [ApiController] // this indicates that this is an API controller
    public class HotelsController : ControllerBase
    {
        private readonly IHotel _context;

        public HotelsController(IHotel context)
        {
            _context = context;
        }

        // GET: api/Hotels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Hotel>>> GetHotels()
        {
            if (_context == null)
            {
                return NotFound();
            }
            return await _context.GetHotels();
        }

        // GET: api/Hotels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Hotel>> GetHotel(int id)
        {
            if (_context == null)
            {
                return NotFound();
            }
            var hotel = await _context.GetHotel(id);

            if (hotel == null)
            {
                return NotFound();
            }

            return hotel;
        }

        // PUT: api/Hotels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
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

        // POST: api/Hotels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Hotel>> PostHotel(Hotel hotel)
        {
            if (_context == null)
            {
                return Problem("Entity set 'AsyncInnDbContext.Hotels'  is null.");
            }
            await _context.CreateHotel(hotel);


            return CreatedAtAction("GetHotel", new { id = hotel.Id }, hotel);
        }

        // DELETE: api/Hotels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHotel(int id)
        {
            if (_context == null)
            {
                return NotFound();
            }

            await _context.DeleteHotel(id);

            return NoContent();
        }


        // GET: api/Hotels? name = {name}
        [HttpGet("ByName/{name}")]

        public async Task<ActionResult<Hotel>> FindHotelByName(string name)
        {

            var foundhotel = await _context.GetHotelByName(name);

            if (foundhotel == null)
            {
                return NotFound();
            }

            return foundhotel;
        }


    }
}
