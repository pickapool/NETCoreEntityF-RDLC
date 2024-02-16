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
            return await _context.Departments
                .Include( c => c.Courses).ThenInclude( c1 => c1.Course)
                .ToListAsync();
        }
        [HttpPost]
        [Route("CollegesMasterList")]
        public async Task<ActionResult<List<DepartmentModel>>> CollegesMasterList(FilterParameter param)
        {
            IEnumerable<DepartmentModel> result = new List<DepartmentModel>();
            if (param.DepartmentId == 0)
            {
                result = await _context.Departments
                   .Include(c => c.Courses)
                        .ThenInclude(c1 => c1.Course)
                        .ThenInclude(e => e.Students)
                        .ThenInclude(e => e.Sanctions)
                        .ThenInclude(e => e.Sanction)
                    .Include(c => c.Courses)
                        .ThenInclude(c1 => c1.Course)
                        .ThenInclude(e => e.Students)
                        .ThenInclude(e => e.Sanctions)
                        .ThenInclude(e => e.Sanction)
                    .Include(c => c.Courses)
                        .ThenInclude(c1 => c1.Course)
                        .ThenInclude(e => e.Students)
                        .ThenInclude(e => e.Section)
                    .ToListAsync();
            }
            else
            {
                result = await _context.Departments.Where(e => e.DepartmentId == param.DepartmentId)
                   .Include(c => c.Courses)
                        .ThenInclude(c1 => c1.Course)
                        .ThenInclude(e => e.Students)
                        .ThenInclude(e => e.Sanctions)
                        .ThenInclude(e => e.Sanction)
                    .Include(c => c.Courses)
                        .ThenInclude(c1 => c1.Course)
                        .ThenInclude(e => e.Students)
                        .ThenInclude(e => e.Sanctions)
                        .ThenInclude(e => e.Sanction)
                    .Include(c => c.Courses)
                        .ThenInclude(c1 => c1.Course)
                        .ThenInclude(e => e.Students)
                        .ThenInclude(e => e.Section)
                    .ToListAsync();
                return result.ToList();
            }
            if (param.IsDate)
            {
                result = result
                .Where(e => e.Courses
                    .Any(c => c.Course.Students
                        .Any(s => s.Sanctions
                            .Any(sanction => sanction.DateRecorded >= param.DateFrom && sanction.DateRecorded <= param.DateTo.AddDays(1)))))
                .ToList();
            }
            if (param.IsCourse)
            {
                result = result.Where(e => e.Courses.Any(e => param.Courses.Any(c => c.CourseId == e.CourseId)));
            }
            if (param.IsDepartment)
            {
                result = result.Where(e => param.Departments.Any(d => d.DepartmentId == e.DepartmentId));
            }
            if (param.IsSection)
            {
                result = result
                .Where(e => e.Courses
                    .Any(c => c.Course.Students
                        .Any(s => param.Sections.Any(e => e.SectionId == s.SectionId))));
            }
            if (param.IsYear)
            {
                result = result
                .Where(e => e.Courses
                    .Any(c => c.Course.Students
                        .Any(s => param.YearLevels.Any(e => e == s.YearLevel))));
            }
            if (param.IsSanction)
            {
                result.ToList().ForEach(department =>
                {
                    department.Courses.ForEach(course =>
                    {
                        course.Course.Students.ForEach(student =>
                        {
                            for (int i = student.Sanctions.Count - 1; i >= 0; i--)
                            {
                                foreach (SanctionModel san in param.Sanctions)
                                {
                                    if (student.Sanctions[i].SanctionId != san.SanctionId)
                                    {
                                        student.Sanctions.RemoveAt(i);
                                    }
                                }
                            }
                        });
                    });
                });
            }
        
            result.ToList().ForEach(e => e.Courses.ForEach(e => e.Course.Students.ForEach(e => e.Sanctions.ForEach(e => e.SanctionImage = new byte[] { })))); 
            return result.ToList();
        }
        [HttpGet]
        [Route("GetDepartment/{id}")]
        public async Task<ActionResult<DepartmentModel>> GetDepartment(int id)
        {
            //  .Include( a => a.Information)
            //  
            var dept = await _context.Departments
                .Include(c => c.Courses).ThenInclude(c1 => c1.Course)
                .FirstOrDefaultAsync(u => u.DepartmentId == id);
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
