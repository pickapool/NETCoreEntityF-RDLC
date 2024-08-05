using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.DBContexts;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("reminder/")]
    [ApiController]
    public class RemindersController : ControllerBase
    {
        private readonly DatabaseContext _context;
        public RemindersController(DatabaseContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("GetReminders")]
        public async Task<ActionResult<IEnumerable<RemindersModel>>> GetReminders()
        {
            //.Include(a => a.Information)
            return await _context.Reminders.ToListAsync();
        }
        [HttpGet]
        [Route("GetReminder/{id}")]
        public async Task<ActionResult<RemindersModel>> GetReminder(int id)
        {
            //  .Include( a => a.Information)
            //  
            var user = await _context.Reminders.FirstOrDefaultAsync(u => u.ReminderId == id);
            if (user == null)
            {
                return NotFound();
            }
            return user;
        }
        [HttpPut]
        [Route("UpdateReminder/{id}")]
        public async Task<IActionResult> UpdateReminder(int id, RemindersModel user)
        {
            if (id != user.ReminderId)
            {
                return BadRequest();
            }
           // _context.Entry(user.Information).State = EntityState.Modified;
            _context.Entry(user).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccountExist(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            var updatedUser = await _context.Reminders.FindAsync(id);
            return Ok(updatedUser);
        }

        [HttpPost]
        [Route("AddReminder")]
        public async Task<ActionResult<RemindersModel>> AddReminder(RemindersModel user)
        {
            _context.Reminders.Add(user);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetReminder", new { id = user.ReminderId }, user);
        }
        private bool AccountExist(int id)
        {
            return _context.Reminders.Any(e => e.ReminderId == id);
        }
        [HttpDelete]
        [Route("DeleteReminder/{id}")]
        public async Task<ActionResult<RemindersModel>> DeleteReminder(int id)
        {
            var movie = await _context.Reminders.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }
            _context.Reminders.Remove(movie);
            await _context.SaveChangesAsync();
            return movie;
        }
    }
}
