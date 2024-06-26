﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.DBContexts;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("Event/")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly DatabaseContext _context; 
        public EventController(DatabaseContext context)
        {
            _context = context;
        }
        [HttpPost]
        [Route("List")]
        public async Task<ActionResult<IEnumerable<EventModel>>> GetEvents(FilterParameter param)
        {
            //.Include(a => a.Information)
            return await _context.Events.ToListAsync();
        }
        [HttpGet]
        [Route("GetEvent/{id}")]
        public async Task<ActionResult<EventModel>> GetEvent(int id)
        {
            //  .Include( a => a.Information)
            //  
            var dept = await _context.Events.FirstOrDefaultAsync(u => u.EventId == id);
            if (dept == null)
            {
                return NotFound();
            }
            return dept;
        }
        [HttpPut]
        [Route("UpdateEvent/{id}")]
        public async Task<ActionResult<EventModel>> UpdateEvent(int id, EventModel dept)
        {
            if (id != dept.EventId)
            {
                return BadRequest();
            }
           // _context.Entry(user.Information).State = EntityState.Modified;
            _context.Entry(dept).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return dept;
        }

        [HttpPost]
        [Route("AddEvent")]
        public async Task<ActionResult<EventModel>> AddEvent(EventModel dept)
        {
            _context.Events.Add(dept);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetEvent", new { id = dept.EventId }, dept);
        }
        private bool EventExists(int id)
        {
            return _context.Events.Any(e => e.EventId == id);
        }
        [HttpDelete]
        [Route("DeleteEvent/{id}")]
        public async Task<ActionResult<EventModel>> DeleteEvent(int id)
        {
            var dept = await _context.Events.FindAsync(id);
            if (dept == null)
            {
                return NotFound();
            }
            _context.Events.Remove(dept);
            await _context.SaveChangesAsync();
            return new EventModel();
        }
    }
}
