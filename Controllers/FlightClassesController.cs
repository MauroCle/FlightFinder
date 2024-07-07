using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FlightFinder.Models;

namespace FlightFinder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightClassesController : ControllerBase
    {
        private readonly FlightFinderContext _context;

        public FlightClassesController(FlightFinderContext context)
        {
            _context = context;
        }

        // GET: api/FlightClasses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FlightClass>>> GetFlightClasses()
        {
          if (_context.FlightClasses == null)
          {
              return NotFound();
          }
            return await _context.FlightClasses.ToListAsync();
        }

        // GET: api/FlightClasses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FlightClass>> GetFlightClass(int id)
        {
          if (_context.FlightClasses == null)
          {
              return NotFound();
          }
            var flightClass = await _context.FlightClasses.FindAsync(id);

            if (flightClass == null)
            {
                return NotFound();
            }

            return flightClass;
        }

        // PUT: api/FlightClasses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFlightClass(int id, FlightClass flightClass)
        {
            if (id != flightClass.Id)
            {
                return BadRequest();
            }

            _context.Entry(flightClass).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FlightClassExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/FlightClasses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<FlightClass>> PostFlightClass(FlightClass flightClass)
        {
          if (_context.FlightClasses == null)
          {
              return Problem("Entity set 'FlightFinderContext.FlightClasses'  is null.");
          }
            _context.FlightClasses.Add(flightClass);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFlightClass", new { id = flightClass.Id }, flightClass);
        }

        // DELETE: api/FlightClasses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFlightClass(int id)
        {
            if (_context.FlightClasses == null)
            {
                return NotFound();
            }
            var flightClass = await _context.FlightClasses.FindAsync(id);
            if (flightClass == null)
            {
                return NotFound();
            }

            _context.FlightClasses.Remove(flightClass);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FlightClassExists(int id)
        {
            return (_context.FlightClasses?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
