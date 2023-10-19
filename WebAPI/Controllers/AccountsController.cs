using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.DBContexts;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("users/")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly DatabaseContext _context;
        public AccountsController(DatabaseContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("GetUsers")]
        public async Task<ActionResult<IEnumerable<AccountModel>>> GetUsers()
        {
            //.Include(a => a.Information)
            return await _context.Accounts.ToListAsync();
        }
        [HttpGet]
        [Route("GetUser/{id}")]
        public async Task<ActionResult<AccountModel>> GetUser(int id)
        {
            //  .Include( a => a.Information)
            //  
            var user = await _context.Accounts.FirstOrDefaultAsync(u => u.UserId == id);
            if (user == null)
            {
                return NotFound();
            }
            return user;
        }
        [HttpPut]
        [Route("UpdateUser/{id}")]
        public async Task<IActionResult> UpdateUser(int id, AccountModel user)
        {
            if (id != user.UserId)
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
            return NoContent();
        }

        [HttpPost]
        [Route("AddUser")]
        public async Task<ActionResult<AccountModel>> AddUser(AccountModel user)
        {
            _context.Accounts.Add(user);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetUser", new { id = user.UserId }, user);
        }
        private bool AccountExist(int id)
        {
            return _context.Accounts.Any(e => e.UserId == id);
        }
        [HttpDelete]
        [Route("DeleteUser/{id}")]
        public async Task<ActionResult<AccountModel>> DeleteUser(int id)
        {
            var movie = await _context.Accounts.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }
            _context.Accounts.Remove(movie);
            await _context.SaveChangesAsync();
            return movie;
        }
        [HttpPost]
        [Route("Authenticate")]
        public async Task<ActionResult<AccountModel>> Authenticate(AccountModel account)
        {
            //.Include(a => a.Information)
            List<AccountModel> accounts  = await _context.Accounts.ToListAsync();
            return accounts.FirstOrDefault(a => a.Username == account.Username && a.Password == account.Password) ?? new();
        }
    }
}
