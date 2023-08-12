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
    public class AmenitiesController : ControllerBase
    {
        private readonly IAmenity _context;

        public AmenitiesController(IAmenity context)
        {
            _context = context;
        }




        [Authorize(Roles = "District Manager", Policy = "Create")]

        [HttpPost]
        public async Task<ActionResult<AmenityDTO>> PostAmenity(AmenityDTO amenityDTO)
        {
            var createdAmenity = await _context.CreateAmenity(amenityDTO);

            return CreatedAtAction("GetAmenityById", new { id = createdAmenity.ID }, createdAmenity);
        }





        [Authorize(Roles = "District Manager", Policy = "Update")]

        [HttpPut("{id}")]
        public async Task<ActionResult<AmenityDTO>> PutAmenity(int id, AmenityDTO amenityDTO)
        {
            var updatedAmenity = await _context.UpdateAmenity(id, amenityDTO);

            //return updatedAmenity;
            return Ok("Amenity Updated successfully!");
        }




        [Authorize(Roles = "District Manager", Policy = "Delete")]

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAmenity(int id)
        {
            await _context.DeleteAmenity(id);
            return Ok("Amenity Deleted successfully!");
        }






        [AllowAnonymous]
        [Authorize(Roles = "District Manager , Property Manager", Policy = "Read")]

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AmenityDTO>>> GetAllAmenities()
        {
            var amenityDTOs = await _context.GetAllAmenities();
            return Ok(amenityDTOs);
        }





        [AllowAnonymous]
        [Authorize(Roles = "District Manager , Property Manager", Policy = "Read")]

        [HttpGet("{id}")]
        public async Task<ActionResult<AmenityDTO>> GetAmenityById(int id)
        {
            var amenityDTO = await _context.GetAmenityById(id);

            if (amenityDTO == null)
            {
                return NotFound();
            }

            return Ok(amenityDTO);
        }

    }
}
