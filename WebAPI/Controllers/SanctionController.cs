using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.DBContexts;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("Sanction/")]
    [ApiController]
    public class SanctionController : ControllerBase
    {
        private readonly DatabaseContext _context;
        public SanctionController(DatabaseContext context)
        {
            _context = context;
        }
        [HttpPost]
        [Route("List")]
        public async Task<ActionResult<IEnumerable<SanctionModel>>> GetSanctions(FilterParameter param)
        {
            //.Include(a => a.Information)
            return await _context.Sanctions.ToListAsync();
        }
        [HttpGet]
        [Route("GetSanction/{id}")]
        public async Task<ActionResult<SanctionModel>> GetSanction(int id)
        {
            //  .Include( a => a.Information)
            //  
            var dept = await _context.Sanctions.FirstOrDefaultAsync(u => u.SanctionId == id);
            if (dept == null)
            {
                return NotFound();
            }
            return dept;
        }
        [HttpPut]
        [Route("UpdateSanction/{id}")]
        public async Task<ActionResult<SanctionModel>> UpdateSanction(int id, SanctionModel dept)
        {
            if (id != dept.SanctionId)
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
                if (!SanctionExists(id))
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
        [Route("AddSanction")]
        public async Task<ActionResult<SanctionModel>> AddSanction(SanctionModel dept)
        {
            _context.Sanctions.Add(dept);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetSanction", new { id = dept.SanctionId }, dept);
        }
        private bool SanctionExists(int id)
        {
            return _context.Sanctions.Any(e => e.SanctionId == id);
        }
        [HttpDelete]
        [Route("DeleteSanction/{id}")]
        public async Task<ActionResult<SanctionModel>> DeleteSanction(int id)
        {
            var dept = await _context.Sanctions.FindAsync(id);
            if (dept == null)
            {
                return NotFound();
            }
            _context.Sanctions.Remove(dept);
            await _context.SaveChangesAsync();
            return new SanctionModel();
        }
    }
}
