using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.DBContexts;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("DepartmentCourse/")]
    [ApiController]
    public class DepartmentCourseController : ControllerBase
    {
        private readonly DatabaseContext _context; 
        public DepartmentCourseController(DatabaseContext context)
        {
            _context = context;
        }
        [HttpPost]
        [Route("List")]
        public async Task<ActionResult<IEnumerable<DepartmentCourseModel>>> GetDepartmentCourses(FilterParameter param)
        {
            //.Include(a => a.Information)
            return await _context.DepartmentCourses
                .Include(d => d.Department)
                .Include(c => c.Course)
                .Where(s => s.DepartmentId == param.DepartmentId)
                .ToListAsync();
        }
        [HttpGet]
        [Route("GetDepartmentCourse/{id}")]
        public async Task<ActionResult<List<DepartmentCourseModel>>> GetDepartmentCourse(int id)
        {
            //  .Include( a => a.Information)
            //  
            var dept = await _context.DepartmentCourses
                .Include(d => d.Department)
                .Include(c => c.Course)
                .Where(u => u.DepartmentCourseId == id).ToListAsync();
            if (dept == null)
            {
                return NotFound();
            }
            return dept;
        }
        [HttpPut]
        [Route("UpdateDepartmentCourse/{id}")]
        public async Task<ActionResult<DepartmentCourseModel>> UpdateDepartmentCourse(int id, DepartmentCourseModel dept)
        {
            if (id != dept.DepartmentCourseId)
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
                if (!DepartmentCourseExists(id))
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
        [Route("AddDepartmentCourse")]
        public async Task<ActionResult<DepartmentCourseModel>> AddDepartmentCourse(DepartmentCourseModel dept)
        {
  
            _context.Entry(dept).State = EntityState.Added;
            _context.Entry(dept).Reference(b => b.Course).IsModified = false;
            _context.Entry(dept).Reference(b => b.Department).IsModified = false;

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDepartmentCourse", new { id = dept.DepartmentCourseId }, dept);
        }
        private bool DepartmentCourseExists(int id)
        {
            return _context.DepartmentCourses.Any(e => e.DepartmentCourseId == id);
        }
        [HttpDelete]
        [Route("DeleteDepartmentCourse/{id}")]
        public async Task<ActionResult<DepartmentCourseModel>> DeleteDepartmentCourse(int id)
        {
            var dept = await _context.DepartmentCourses.FindAsync(id);
            if (dept == null)
            {
                return NotFound();
            }
            _context.DepartmentCourses.Remove(dept);
            await _context.SaveChangesAsync();
            return new DepartmentCourseModel();
        }
    }
}
