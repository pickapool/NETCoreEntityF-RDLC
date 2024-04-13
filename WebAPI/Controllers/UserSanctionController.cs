using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.DBContexts;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("UserSanction/")]
    [ApiController]
    public class UserSanctionController : ControllerBase
    {
        private readonly DatabaseContext _context;
        public UserSanctionController(DatabaseContext context)
        {
            _context = context;
        }
        [HttpPost]
        [Route("List")]
        public async Task<ActionResult<IEnumerable<UserSanctionModel>>> GetUserSanctions(FilterParameter param)
        {
            //.Include(a => a.Information)
            return await _context.UserSanctions
                .Include( a => a.Account)
                .Include( b => b.MarkAsPaidByAccount)
                .Include( s => s.Sanction).Where( s => s.StudentId == param.StudentId)
                .ToListAsync();
        }
        [HttpGet]
        [Route("GetUserSanction/{id}")]
        public async Task<ActionResult<UserSanctionModel>> GetUserSanction(int id)
        {
            //  .Include( a => a.Information)
            //  
            var dept = await _context.UserSanctions
                .Include(s => s.Sanction)
                .Include(a => a.Account)
                .Include(b => b.MarkAsPaidByAccount)
                .FirstOrDefaultAsync(u => u.UserSanctionId == id);
            if (dept == null)
            {
                return NotFound();
            }
            return dept;
        }
        [HttpPut]
        [Route("UpdateUserSanction/{id}")]
        public async Task<ActionResult<UserSanctionModel>> UpdateUserSanction(int id, UserSanctionModel dept)
        {
            if (id != dept.UserSanctionId)
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
                if (!UserSanctionExists(id))
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
        [Route("AddUserSanction")]
        public async Task<ActionResult<UserSanctionModel>> AddUserSanction(UserSanctionModel dept)
        {
            //_context.UserSanctions.Add(dept);
            _context.Entry(dept).State = EntityState.Added;
            _context.Entry(dept).Reference(b => b.Sanction).IsModified = false;
            _context.Entry(dept).Reference(b => b.Account).IsModified = false;
            _context.Entry(dept).Reference(b => b.MarkAsPaidByAccount).IsModified = false;

            //Server side date stamp
            dept.DateRecorded = DateTime.Now;
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetUserSanction", new { id = dept.UserSanctionId }, dept);
        }
        private bool UserSanctionExists(int id)
        {
            return _context.UserSanctions.Any(e => e.UserSanctionId == id);
        }
        [HttpDelete]
        [Route("DeleteUserSanction/{id}")]
        public async Task<ActionResult<UserSanctionModel>> DeleteUserSanction(int id)
        {
            var dept = await _context.UserSanctions.FindAsync(id);
            if (dept == null)
            {
                return NotFound();
            }
            _context.UserSanctions.Remove(dept);
            await _context.SaveChangesAsync();
            return new UserSanctionModel();
        }
    }
}
