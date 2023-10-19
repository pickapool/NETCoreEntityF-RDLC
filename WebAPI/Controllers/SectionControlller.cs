using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.DBContexts;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("Section/")]
    [ApiController]
    public class SectionController : ControllerBase
    {
        private readonly DatabaseContext _context; 
        public SectionController(DatabaseContext context)
        {
            _context = context;
        }
        [HttpPost]
        [Route("List")]
        public async Task<ActionResult<IEnumerable<SectionModel>>> GetSections(FilterParameter param)
        {
            //.Include(a => a.Information)
            return await _context.Sections.ToListAsync();
        }
        [HttpGet]
        [Route("GetSection/{id}")]
        public async Task<ActionResult<SectionModel>> GetSection(int id)
        {
            //  .Include( a => a.Information)
            //  
            var dept = await _context.Sections.FirstOrDefaultAsync(u => u.SectionId == id);
            if (dept == null)
            {
                return NotFound();
            }
            return dept;
        }
        [HttpPut]
        [Route("UpdateSection/{id}")]
        public async Task<ActionResult<SectionModel>> UpdateSection(int id, SectionModel dept)
        {
            if (id != dept.SectionId)
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
                if (!SectionExists(id))
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
        [Route("AddSection")]
        public async Task<ActionResult<SectionModel>> AddSection(SectionModel dept)
        {
            _context.Sections.Add(dept);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetSection", new { id = dept.SectionId }, dept);
        }
        private bool SectionExists(int id)
        {
            return _context.Sections.Any(e => e.SectionId == id);
        }
        [HttpDelete]
        [Route("DeleteSection/{id}")]
        public async Task<ActionResult<SectionModel>> DeleteSection(int id)
        {
            var dept = await _context.Sections.FindAsync(id);
            if (dept == null)
            {
                return NotFound();
            }
            _context.Sections.Remove(dept);
            await _context.SaveChangesAsync();
            return new SectionModel();
        }
    }
}
