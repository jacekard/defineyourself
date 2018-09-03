using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YouDefine.Models;

namespace YouDefine.Controllers
{
    [Produces("application/json")]
    [Route("api/Definitions")]
    public class DefinitionsController : Controller
    {
        private readonly YouDefineContext _context;

        public DefinitionsController(YouDefineContext context)
        {
            _context = context;
        }

        // GET: api/Definitions
        [HttpGet]
        public IEnumerable<Definition> GetDefinition()
        {
            return _context.Definition;
        }

        // GET: api/Definitions/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDefinition([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var definition = await _context.Definition.SingleOrDefaultAsync(m => m.Id == id);

            if (definition == null)
            {
                return NotFound();
            }

            return Ok(definition);
        }

        // PUT: api/Definitions/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDefinition([FromRoute] long id, [FromBody] Definition definition)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != definition.Id)
            {
                return BadRequest();
            }

            _context.Entry(definition).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DefinitionExists(id))
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

        // POST: api/Definitions
        [HttpPost]
        public async Task<IActionResult> PostDefinition([FromBody] Definition definition)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Definition.Add(definition);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDefinition", new { id = definition.Id }, definition);
        }

        // DELETE: api/Definitions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDefinition([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var definition = await _context.Definition.SingleOrDefaultAsync(m => m.Id == id);
            if (definition == null)
            {
                return NotFound();
            }

            _context.Definition.Remove(definition);
            await _context.SaveChangesAsync();

            return Ok(definition);
        }

        private bool DefinitionExists(long id)
        {
            return _context.Definition.Any(e => e.Id == id);
        }
    }
}