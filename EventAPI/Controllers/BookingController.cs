﻿using EventAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;

namespace EventAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly EventDbContext _context;

        public BookingController(EventDbContext context)
        {
            _context = context;
        }

        //Get:api/Booking
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookingTb>>> GetBooking()
        {
            return await _context.BookingTbs.ToListAsync();
        }

        //Post:api/Booking
        [HttpPost]
        public async Task<ActionResult<BookingTb>> PostBooking(BookingTb booking)
        {
            _context.BookingTbs.Add(booking);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetBooking", new { Id = booking.BookingId }, booking);
        }

        [HttpGet("{id}")]

        public async Task<ActionResult<BookingTb>> GetBooking(int id)
        {
            var booking = await _context.BookingTbs.FindAsync(id);
            if (booking == null)
            {
                return NotFound();
            }
            return booking;
        }
        [HttpPut("{id}")]

        // only update booking status
        public async Task<ActionResult> PutBooking(int id,BookingTb bookingTb)
        {
            if (id != bookingTb.BookingId)
            {
                return BadRequest();
            }
            var exixstingbooking = await _context.BookingTbs.FindAsync(id);
            if(exixstingbooking== null)
            {
                return NotFound(new { Message = "booking not found " });

            }
            exixstingbooking.BookingStatus = bookingTb.BookingStatus;
              _context.Entry(exixstingbooking).State=EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookingTbExists(id))
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
        private bool BookingTbExists(int id)
        {
            return _context.BookingTbs.Any(e => e.UserId == id);
        }

    }
      
    }
