using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YouDefine.Models;
using Newtonsoft.Json;

namespace YouDefine.Controllers
{
    [Produces("appliation/json")]
    [Route("api/ideas")]
    public class IdeasController : Controller
    {
        private readonly YouDefineContext _context;

        public IdeasController(YouDefineContext context)
        {
            _context = context;
        }

        // GET: api/Ideas
        [HttpGet]
        public IEnumerable<Idea> GetIdea()
        {
            return _context.Idea;
        }

        // GET: api/Ideas/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetIdea([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var idea = await _context.Idea.SingleOrDefaultAsync(m => m.Id == id);

            if (idea == null)
            {
                return NotFound();
            }

            return Ok(idea);
        }

        // PUT: api/Ideas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutIdea([FromRoute] long id, [FromBody] Idea idea)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != idea.Id)
            {
                return BadRequest();
            }

            _context.Entry(idea).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IdeaExists(id))
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

        // POST: api/ideas
        [HttpPost]
        public async Task<IActionResult> PostIdea([FromBody] Idea idea)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Idea.Add(idea);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetIdea", new { id = idea.Id }, idea);
        }

        // DELETE: api/Ideas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIdea([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var idea = await _context.Idea.SingleOrDefaultAsync(m => m.Id == id);
            if (idea == null)
            {
                return NotFound();
            }

            _context.Idea.Remove(idea);
            await _context.SaveChangesAsync();

            return Ok(idea);
        }

        private bool IdeaExists(long id)
        {
            return _context.Idea.Any(e => e.Id == id);
        }
    }
}