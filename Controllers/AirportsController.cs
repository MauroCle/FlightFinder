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
    public class AirportsController : ControllerBase
    {
        private readonly FlightFinderContext _context;

        public AirportsController(FlightFinderContext context)
        {
            _context = context;
        }

        // GET: api/Airports
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Airport>>> GetAirports()
        {
          if (_context.Airports == null)
          {
              return NotFound();
          }
            return await _context.Airports.ToListAsync();
        }

        // GET: api/Airports/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Airport>> GetAirport(int id)
        {
          if (_context.Airports == null)
          {
              return NotFound();
          }
            var airport = await _context.Airports.FindAsync(id);

            if (airport == null)
            {
                return NotFound();
            }

            return airport;
        }

        // PUT: api/Airports/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAirport(int id, Airport airport)
        {
            if (id != airport.Id)
            {
                return BadRequest();
            }

            _context.Entry(airport).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AirportExists(id))
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

        // POST: api/Airports
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Airport>> PostAirport(Airport airport)
        {
          if (_context.Airports == null)
          {
              return Problem("Entity set 'FlightFinderContext.Airports'  is null.");
          }
            _context.Airports.Add(airport);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAirport", new { id = airport.Id }, airport);
        }

        // DELETE: api/Airports/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAirport(int id)
        {
            if (_context.Airports == null)
            {
                return NotFound();
            }
            var airport = await _context.Airports.FindAsync(id);
            if (airport == null)
            {
                return NotFound();
            }

            _context.Airports.Remove(airport);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AirportExists(int id)
        {
            return (_context.Airports?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        //TODO test. Devuelve los aeropuestos disponibles segun el de destino o origen definido
        [HttpGet("LinkedWith/{id}")]
        public async Task<ActionResult<IEnumerable<Airport>>> GetAirportlinkedWith(int id,bool origin )
        {

            object result; //TODO Esta linea podria estar mal, testear llegado el momento 
            if (_context.Airports == null)
            {
                return NotFound();
            }
            
            if (origin == true)
            {
                 result = await (from d in _context.Airports
                                    join f in _context.Flights on d.Id equals f.DestinationId
                                    where f.OriginId == id
                                    select new
                                    {
                                        d.Id
                                    }).ToListAsync();
            }
            else
            {
                 result = await (from o in _context.Airports
                                    join f in _context.Flights on o.Id equals f.OriginId
                                    where f.DestinationId == id
                                    select new
                                    {
                                        o.Id
                                    }).ToListAsync();
            }
        

            if (result == null)
            {
                return NotFound();
            }

            return StatusCode(StatusCodes.Status200OK, result);
        }
    }
}
