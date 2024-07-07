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
    public class DemandBasisController : ControllerBase
    {
        private readonly FlightFinderContext _context;

        public DemandBasisController(FlightFinderContext context)
        {
            _context = context;
        }

        // GET: api/DemandBasis
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DemandBasis>>> GetDemandBases()
        {
          if (_context.DemandBases == null)
          {
              return NotFound();
          }
            return await _context.DemandBases.ToListAsync();
        }

        // GET: api/DemandBasis/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DemandBasis>> GetDemandBasis(int id)
        {
          if (_context.DemandBases == null)
          {
              return NotFound();
          }
            var demandBasis = await _context.DemandBases.FindAsync(id);

            if (demandBasis == null)
            {
                return NotFound();
            }

            return demandBasis;
        }

        // PUT: api/DemandBasis/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDemandBasis(int id, DemandBasis demandBasis)
        {
            if (id != demandBasis.Id)
            {
                return BadRequest();
            }

            _context.Entry(demandBasis).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DemandBasisExists(id))
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

        // POST: api/DemandBasis
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DemandBasis>> PostDemandBasis(DemandBasis demandBasis)
        {
          if (_context.DemandBases == null)
          {
              return Problem("Entity set 'FlightFinderContext.DemandBases'  is null.");
          }
            _context.DemandBases.Add(demandBasis);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDemandBasis", new { id = demandBasis.Id }, demandBasis);
        }

        // DELETE: api/DemandBasis/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDemandBasis(int id)
        {
            if (_context.DemandBases == null)
            {
                return NotFound();
            }
            var demandBasis = await _context.DemandBases.FindAsync(id);
            if (demandBasis == null)
            {
                return NotFound();
            }

            _context.DemandBases.Remove(demandBasis);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DemandBasisExists(int id)
        {
            return (_context.DemandBases?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
