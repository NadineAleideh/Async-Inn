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
    public class AmenitiesController : ControllerBase
    {
        private readonly IAmenity _context;

        public AmenitiesController(IAmenity context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<Amenity>> PostAmenity(Amenity amenity)
        {

            await _context.CreateAmenity(amenity);

            return CreatedAtAction("GetAmenityById", new { id = amenity.Id }, amenity);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAmenity(int id, Amenity amenity)
        {
            if (id != amenity.Id)
            {
                return BadRequest();
            }

            await _context.UpdateAmenity(id, amenity);

            return Ok("Amenity Updated successfully!");
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAmenity(int id)
        {

            await _context.DeleteAmenity(id);

            return Ok("Amenity Deleted successfully!");
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Amenity>>> GetAllAmenities()
        {
            var amenities = await _context.GetAllAmenities();
            return Ok(amenities);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Amenity>> GetAmenityById(int id)
        {

            var amenity = await _context.GetAmenityById(id);

            if (amenity == null)
            {
                return NotFound();
            }

            return Ok(amenity);
        }

    }
}
