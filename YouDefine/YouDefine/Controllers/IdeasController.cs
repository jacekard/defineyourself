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
    [Produces("application/json")]
    [Route("api/ideas")]
    public class IdeasController : Controller
    {
        private readonly YouDefineContext _context;

        public IdeasController(YouDefineContext context)
        {
            _context = context;

            //if (_context.Ideas.Count() == 0)
            //{
            //    var idea = new Idea("Love", new Definition("love is all we need"));
            //    _context.Ideas.Add(idea);
            //    _context.SaveChanges();
            //}

        }

        [HttpGet]
        [Route("")]
        public IActionResult GetIdeas()
        {
            return Ok(_context.Ideas);
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult GetIdea(long id)
        {
            var idea = _context.Ideas.SingleOrDefault(m => m.Id == id);

            return Ok(idea);

        }

        [HttpGet]
        [Route("random")]
        public IActionResult GetRandomIdea()
        {
            var count = _context.Ideas.Count();
            var random = new Random().Next(0, count - 1);
            var idea = _context.Ideas.Skip(random).Single();

            return Ok(idea);
        }

        [HttpGet]
        [Route("{title:required:alpha:length(3,18)}")]
        public async Task<IActionResult> GetIdea([FromRoute] string title)
        {
            try
            {
                var idea = await _context.Ideas.SingleOrDefaultAsync(m => m.Title == title);
                return Ok(idea);

            }
            catch
            {
                return Json("Not found");
            }
        }

        [HttpGet]
        [Route("{title:required:alpha:length(3,18)}/likes")]
        public async Task<IActionResult> GetIdeaLikes([FromRoute] string title)
        {
            try
            {
                var idea = await _context.Ideas.SingleOrDefaultAsync(m => m.Title == title);
                return Ok(idea.CountLikes());

            }
            catch
            {
                return NotFound();
            }
        }

        [HttpPut]
        [Route("{title}/{definition}")]
        public async Task<IActionResult> PutIdea(string title, string definition)
        {
            try
            {
                var idea = await _context.Ideas.SingleAsync(x => x.Title == title);
                if (idea != null)
                {
                    idea.Append(new Definition(definition));
                    idea.UpdateLastModifiedDate();
                }
                await _context.SaveChangesAsync();
            }
            catch
            {
                return NotFound();
            }

            return CreatedAtAction("GetIdea", title);
        }

        [HttpPost]
        [Route("{title}/{text}")]
        public async Task<IActionResult> PostIdea(string title, string text)
        {

            var idea = new Idea(title);
            var definition = new Definition(text);
            _context.Definitions.Add(definition);
            idea.Append(definition);
            _context.Ideas.Add(idea);
            await _context.SaveChangesAsync();

            return Ok(idea);
        }

        [HttpDelete]
        [Route(("{id}"))]
        public async Task<IActionResult> DeleteIdeaById([FromRoute] long id)
        {

            var idea = await _context.Ideas.SingleOrDefaultAsync(m => m.Id == id);
            if (idea == null)
            {
                return NotFound();
            }

            _context.Ideas.Remove(idea);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [Route(("{title}"))]
        public async Task<IActionResult> DeleteIdeaByTitle([FromRoute] string title)
        {

            var idea = await _context.Ideas.SingleOrDefaultAsync(m => m.Title == title);
            if (idea == null)
            {
                return NotFound();
            }

            _context.Ideas.Remove(idea);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete]
        [Route("destroy")]
        public async Task<IActionResult> DeleteIdeas([FromRoute] long id, [FromRoute] string title)
        {
            _context.Ideas.RemoveRange(_context.Ideas);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpGet]
        [Route("info")]
        public IActionResult GetInfo()
        {

            return Ok(_context.Database.ProviderName);
        }

        private bool IdeaExists(string title)
        {
            return _context.Ideas.Any(e => e.Title == title);
        }
    }
}