﻿using System;
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
    public class FlightsController : ControllerBase
    {
        private readonly FlightFinderContext _context;

        public FlightsController(FlightFinderContext context)
        {
            _context = context;
        }

        // GET: api/Flights
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Flight>>> GetFlights()
        {
            if (_context.Flights == null)
            {
                return NotFound();
            }
            List<Flight> flights = await _context.Flights.ToListAsync();
            return StatusCode(StatusCodes.Status200OK, flights);
        }

        // GET: api/Flights/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Flight>> GetFlight(int id)
        {
            if (_context.Flights == null)
            {
                return NotFound();
            }
            var flight = await _context.Flights.FindAsync(id);

            if (flight == null)
            {
                return NotFound();
            }

            return flight;
        }

        // PUT: api/Flights/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFlight(int id, Flight flight)
        {
            if (id != flight.Id)
            {
                return BadRequest();
            }

            _context.Entry(flight).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FlightExists(id))
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

        // POST: api/Flights
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Flight>> PostFlight(Flight flight)
        {
            if (_context.Flights == null)
            {
                return Problem("Entity set 'FlightFinderContext.Flights'  is null.");
            }
            _context.Flights.Add(flight);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFlight", new { id = flight.Id }, flight);
        }

        // DELETE: api/Flights/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFlight(int id)
        {
            if (_context.Flights == null)
            {
                return NotFound();
            }
            var flight = await _context.Flights.FindAsync(id);
            if (flight == null)
            {
                return NotFound();
            }

            _context.Flights.Remove(flight);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FlightExists(int id)
        {
            return (_context.Flights?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        //TODO add date
        [HttpGet("{departureId}_{destinationId}")]
        //public async Task<ActionResult<Flight>> GetConection(int departureId, int DestinationId)
        public async Task<ActionResult<List<Flight>>> GetConections(int departureId, int destinationId)
        {
            if (_context.Flights == null)
            {
                return NotFound();
            }
            List<Flight> directFlight = _context.Flights
                                        .Where(e => e.OriginId == departureId && e.DestinationId == destinationId)
                                        .ToList();

            if (directFlight.Count > 0)
            {
                return directFlight;
            }

            List<Flight> connections = RecursiveSearchConnection(departureId, destinationId, new HashSet<int>());

            if (connections == null || connections.Count == 0)
            {
                return NotFound();
            }

            return connections;
        }

        //TODO add date
        private List<Flight> RecursiveSearchConnection(int departureId, int destinationId, HashSet<int> visitedDestinations)
        {
            if (visitedDestinations.Contains(destinationId))
            {
                return null;
            }

            visitedDestinations.Add(destinationId);

            List<Flight> incomingFlights = _context.Flights
                                           .Where(e => e.DestinationId == destinationId)
                                           .ToList();


            foreach(var flight in incomingFlights)
            {
                //Check if there is a direct connection from the origin
                if (flight.OriginId == departureId)
                {
                    return new List<Flight> { flight };
                }

                //If there is no direct connection, search recursively from the current flight origin
                List<Flight> subConnections = RecursiveSearchConnection(departureId, flight.OriginId, visitedDestinations);

                if (subConnections != null && subConnections.Count > 0)
                {
                    subConnections.Add(flight);
                    return subConnections;
                }
            }

            return null;
        }

    }
}
