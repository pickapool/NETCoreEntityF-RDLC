using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.DBContexts;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("Student/")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly DatabaseContext _context;
        public StudentController(DatabaseContext context)
        {
            _context = context;
        }
        [HttpPost]
        [Route("List")]
        public async Task<ActionResult<IEnumerable<StudentModel>>> GetStudents(FilterParameter param)
        {
            //.Include(a => a.Information)
            List<StudentModel> students =  await _context.Students
                .Include( c => c.Course)
                .Include( d => d.Department)
                //.Include( us => us.Sanctions).ThenInclude(s => s.Sanction)
                .Include( s => s.Section).ToListAsync();

            if (param.IsDate)
            {
                students = students
                    .Where(s => 
                        s.Sanctions.All(sa => 
                            sa.DateRecorded >= param.DateFrom && sa.DateRecorded <= param.DateTo.AddDays(1)
                            )
                        )
                    .ToList();
            }
            if (param.IsCourse)
            {
                students = students.Where(s => param.Courses.Any(c => c.CourseId == s.CourseId)).ToList();
            }
            if (param.IsDepartment)
            {
                students = students.Where(s => param.Departments.Any(c => c.DepartmentId == s.DepartmentId)).ToList();
            }
            if(param.IsSection)
            {
                students = students.Where(s => param.Sections.Any(c => c.SectionId == s.SectionId)).ToList();
            }
            if (param.IsYear)
            {
                students = students.Where(s => param.YearLevels.Any(c => c == s.YearLevel)).ToList();
            }
            if (param.IsSanction)
            {
                students = students.
                    Where(s => param.Sanctions.Any( sanc => s.Sanctions.Any( s => s.SanctionId == sanc.SanctionId)))
                    .ToList();
            }
            return students;
        }
        [HttpGet]
        [Route("GetStudent/{id}")]
        public async Task<ActionResult<StudentModel>> GetStudent(int id)
        {
            //  .Include( a => a.Information)
            //  
            var dept = await _context.Students
                .Include(c => c.Course)
                .Include(d => d.Department)
                .Include(us => us.Sanctions).ThenInclude(s => s.Sanction)
                .Include(s => s.Section).FirstOrDefaultAsync(u => u.StudentId == id);
            if (dept == null)
            {
                return NotFound();
            }
            return dept;
        }
        [HttpPut]
        [Route("UpdateStudent/{id}")]
        public async Task<ActionResult<StudentModel>> UpdateStudent(int id, StudentModel dept)
        {
            if (id != dept.StudentId)
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
                if (!StudentExists(id))
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
        [Route("AddStudent")]
        public async Task<ActionResult<StudentModel>> AddStudent(StudentModel dept)
        {
            _context.Entry(dept).State = EntityState.Added;
            _context.Entry(dept).Reference(b => b.Course).IsModified = false;
            _context.Entry(dept).Reference(b => b.Department).IsModified = false;
            _context.Entry(dept).Reference(b => b.Section).IsModified = false;

            //_context.Students.Add(dept);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetStudent", new { id = dept.StudentId }, dept);
        }
        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.StudentId == id);
        }
        [HttpDelete]
        [Route("DeleteStudent/{id}")]
        public async Task<ActionResult<StudentModel>> DeleteStudent(int id)
        {
            var dept = await _context.Students.FindAsync(id);
            if (dept == null)
            {
                return NotFound();
            }
            _context.Students.Remove(dept);
            await _context.SaveChangesAsync();
            return new StudentModel();
        }
    }
}
