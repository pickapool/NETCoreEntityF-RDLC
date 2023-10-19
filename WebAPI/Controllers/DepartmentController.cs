using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.DBContexts;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("department/")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly DatabaseContext _context;
        public DepartmentController(DatabaseContext context)
        {
            _context = context;
        }
        [HttpPost]
        [Route("List")]
        public async Task<ActionResult<IEnumerable<DepartmentModel>>> GetDepartments(FilterParameter param)
        {
            //.Include(a => a.Information)
            return await _context.Departments.ToListAsync();
        }
        [HttpGet]
        [Route("GetDepartment/{id}")]
        public async Task<ActionResult<DepartmentModel>> GetDepartment(int id)
        {
            //  .Include( a => a.Information)
            //  
            var dept = await _context.Departments.FirstOrDefaultAsync(u => u.DepartmentId == id);
            if (dept == null)
            {
                return NotFound();
            }
            return dept;
        }
        [HttpPut]
        [Route("UpdateDepartment/{id}")]
        public async Task<ActionResult<DepartmentModel>> UpdateDepartment(int id, DepartmentModel dept)
        {
            if (id != dept.DepartmentId)
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
                if (!DepartmentExists(id))
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
        [Route("AddDepartment")]
        public async Task<ActionResult<DepartmentModel>> AddDepartment(DepartmentModel dept)
        {
            _context.Departments.Add(dept);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetDepartment", new { id = dept.DepartmentId }, dept);
        }
        private bool DepartmentExists(int id)
        {
            return _context.Departments.Any(e => e.DepartmentId == id);
        }
        [HttpDelete]
        [Route("DeleteDepartment/{id}")]
        public async Task<ActionResult<DepartmentModel>> DeleteDepartment(int id)
        {
            var dept = await _context.Departments.FindAsync(id);
            if (dept == null)
            {
                return NotFound();
            }
            _context.Departments.Remove(dept);
            await _context.SaveChangesAsync();
            return new DepartmentModel();
        }
    }
}
