using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.DBContexts;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("Course/")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly DatabaseContext _context;
        public CourseController(DatabaseContext context)
        {
            _context = context;
        }
        [HttpPost]
        [Route("List")]
        public async Task<ActionResult<IEnumerable<CourseModel>>> GetCourses(FilterParameter param)
        {
            //.Include(a => a.Information)
            return await _context.Courses.ToListAsync();
        }
        [HttpGet]
        [Route("GetCourse/{id}")]
        public async Task<ActionResult<CourseModel>> GetCourse(int id)
        {
            //  .Include( a => a.Information)
            //  
            var dept = await _context.Courses.FirstOrDefaultAsync(u => u.CourseId == id);
            if (dept == null)
            {
                return NotFound();
            }
            return dept;
        }
        [HttpPut]
        [Route("UpdateCourse/{id}")]
        public async Task<ActionResult<CourseModel>> UpdateCourse(int id, CourseModel dept)
        {
            if (id != dept.CourseId)
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
                if (!CourseExists(id))
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
        [Route("AddCourse")]
        public async Task<ActionResult<CourseModel>> AddCourse(CourseModel dept)
        {
            _context.Courses.Add(dept);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetCourse", new { id = dept.CourseId }, dept);
        }
        private bool CourseExists(int id)
        {
            return _context.Courses.Any(e => e.CourseId == id);
        }
        [HttpDelete]
        [Route("DeleteCourse/{id}")]
        public async Task<ActionResult<CourseModel>> DeleteCourse(int id)
        {
            var dept = await _context.Courses.FindAsync(id);
            if (dept == null)
            {
                return NotFound();
            }
            _context.Courses.Remove(dept);
            await _context.SaveChangesAsync();
            return new CourseModel();
        }
    }
}
