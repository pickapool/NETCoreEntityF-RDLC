using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.DBContexts;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("EventAttendance/")]
    [ApiController]
    public class EventAttendanceController : ControllerBase
    {
        private readonly DatabaseContext _context; 
        public EventAttendanceController(DatabaseContext context)
        {
            _context = context;
        }
        [HttpPost]
        [Route("List")]
        public async Task<ActionResult<IEnumerable<EventAttendanceModel>>> GetEventAttendances(FilterParameter param)
        {
            //.Include(a => a.Information)
            List<StudentModel> list = _context.Students
                .Include(c => c.Course)
                .Include(d => d.Department)
                .Include(s => s.Section)
                .ToListAsync().Result;
            List<EventAttendanceModel> eventsWithStudents = _context.EventAttendances
            .ToList() // Materialize the event attendance list
            .Select(e =>
            {
                e.Student = list.FirstOrDefault(s => s.FacialRecognitionId == e.FacialRecognitionId)?? new();
                return e;
            })
            .ToList();
            return eventsWithStudents;
        }
        [HttpGet]
        [Route("GetEventAttendance/{id}")]
        public async Task<ActionResult<EventAttendanceModel>> GetEventAttendance(int id)
        {
            //  .Include( a => a.Information)
            //  
            var dept = await _context.EventAttendances.FirstOrDefaultAsync(u => u.EventAttendanceId == id);
            if (dept == null)
            {
                return NotFound();
            }
            return dept;
        }
        [HttpPut]
        [Route("UpdateEventAttendance/{id}")]
        public async Task<ActionResult<EventAttendanceModel>> UpdateEventAttendance(int id, EventAttendanceModel dept)
        {
            if (id != dept.EventAttendanceId)
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
                if (!EventAttendanceExists(id))
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
        [Route("AddEventAttendance")]
        public async Task<ActionResult<EventAttendanceModel>> AddEventAttendance(EventAttendanceModel dept)
        {
            _context.EventAttendances.Add(dept);
            _context.Entry(dept).Reference(b => b.Student).IsModified = false;
            await _context.SaveChangesAsync();
            return dept;
        }
        private bool EventAttendanceExists(int id)
        {
            return _context.EventAttendances.Any(e => e.EventAttendanceId == id);
        }
        [HttpDelete]
        [Route("DeleteEventAttendance/{id}")]
        public async Task<ActionResult<EventAttendanceModel>> DeleteEventAttendance(int id)
        {
            var dept = await _context.EventAttendances.FindAsync(id);
            if (dept == null)
            {
                return NotFound();
            }
            _context.EventAttendances.Remove(dept);
            await _context.SaveChangesAsync();
            return new EventAttendanceModel();
        }
    }
}
