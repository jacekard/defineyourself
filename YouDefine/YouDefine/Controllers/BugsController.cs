namespace YouDefine.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using YouDefine.Data;
    using YouDefine.Models;

    /// <summary>
    /// Bugs Controller - API controller with EF CRUD methods
    /// </summary>
    [Produces("application/json")]
    [Route("api/bugs")]
    public class BugsController : Controller
    {
        private readonly YouDefineContext _context;

        public BugsController(YouDefineContext context)
        {
            _context = context;
        }

        // GET: api/Bugs
        [HttpGet]
        public IEnumerable<Bug> GetBugs()
        {
            return _context.Bugs;
        }

        // GET: api/Bugs/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBug([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var bug = await _context.Bugs.SingleOrDefaultAsync(m => m.Id == id);

            if (bug == null)
            {
                return NotFound();
            }

            return Ok(bug);
        }

        // PUT: api/Bugs/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBug([FromRoute] long id, [FromBody] Bug bug)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != bug.Id)
            {
                return BadRequest();
            }

            _context.Entry(bug).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BugExists(id))
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

        // POST: api/Bugs
        [HttpPost]
        public async Task<IActionResult> PostBug([FromForm]Bug bug)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Bugs.Add(bug);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBug", new { id = bug.Id }, bug);
        }

        // DELETE: api/Bugs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBug([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var bug = await _context.Bugs.SingleOrDefaultAsync(m => m.Id == id);
            if (bug == null)
            {
                return NotFound();
            }

            _context.Bugs.Remove(bug);
            await _context.SaveChangesAsync();

            return Ok(bug);
        }

        private bool BugExists(long id)
        {
            return _context.Bugs.Any(e => e.Id == id);
        }
    }
}