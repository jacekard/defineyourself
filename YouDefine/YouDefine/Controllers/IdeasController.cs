using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Hangfire;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YouDefine.Data;

namespace YouDefine.Services
{
    [Produces("application/json")]
    [Route("api/ideas")]
    public class IdeasController : Controller
    {
        private readonly YouDefineContext _context;

        private readonly IProviderIdeas _provider;

        public IdeasController(YouDefineContext context, IProviderIdeas provider)
        {
            _context = context;
            _provider = provider;
        }

        [HttpGet]
        [Route("all")]
        public IActionResult GetAll()
        {
            var ideas = _provider.GetAll();
            if (ideas == null)
            {
                return NotFound();
            }

            return Ok(ideas);
        }

        [HttpGet]
        [Route("")]
        public IActionResult GetTitles()
        {
            var ideas = _provider.GetTitles();
            if (ideas == null)
            {
                return NotFound();
            }
    //        String[] obj = {
    //"ActionScript",
    //"AppleScript",
    //"Asp",
    //"BASIC",
    //"C",
    //"C++",
    //"Clojure",
    //"COBOL",
    //"ColdFusion",
    //"Erlang",
    //"Fortran",
    //"Groovy",
    //"Haskell",
    //"Java",
    //"JavaScript",
    //"Lisp",
    //"Perl",
    //"PHP",
    //"Python",
    //"Ruby",
    //"Scala",
    //"Scheme"
    //        };
            return Ok(ideas.ToArray());
        }

        [HttpGet]
        [Route("{id:int}")]
        public ActionResult GetIdea(long id)
        {
            var idea = _provider.GetSpecified(id);
            if (idea == null)
            {
                return NotFound();
            }
            return Ok(idea);
        }

        [HttpGet]
        [Route("{title:alpha}")]
        public IActionResult GetIdea([FromRoute] string title)
        {
            var idea = _provider.GetSpecified(title);
            if (idea == null)
            {
                return Ok(String.Empty);
            }
            return Ok(idea);

        }

        [HttpGet]
        [Route("random")]
        public IActionResult GetRandomIdea()
        {
            var count = _context.Ideas.Count();
            var random = new Random().Next(0, count - 1);
            var idea = _context.Ideas.Skip(random).Single();

            return RedirectToAction("GetIdea", idea.Title);
        }

        [HttpPut]
        [Route("likeDefinition/{title}/{id}")]
        public IActionResult LikeDefinition([FromRoute] string title, long id)
        {
            var result = _provider.LikeDefinition(title, id);
            if(result == -1)
            {
                return BadRequest();
            }
            return Ok(); 
        }

        [HttpPut]
        [Route("{title}/{text}")]
        public IActionResult Put(string title, string text)
        {
            var idea = _provider.Update(title, text);

            return Ok(idea);
        }

        [HttpPost]
        [Route("{title}/{text}")]
        public IActionResult Post(string title, string text)
        {
            var idea = _provider.Add(title, text);

            return Ok(idea);
        }

        [HttpDelete]
        [Route(("{id}"))]
        public IActionResult DeleteIdeaById([FromRoute] long id)
        {

            var idea = _context.Ideas.SingleOrDefault(m => m.IdeaId == id);
            if (idea == null)
            {
                return NotFound();
            }

            _context.Ideas.Remove(idea);
            _context.SaveChangesAsync();

            return Ok();
        }

        [Route(("{title}"))]
        public IActionResult DeleteIdeaByTitle([FromRoute] string title)
        {

            var idea = _context.Ideas.SingleOrDefault(m => m.Title == title);
            if (idea == null)
            {
                return NotFound();
            }

            _context.Ideas.Remove(idea);
            _context.SaveChanges();

            return Ok();
        }

        [HttpDelete]
        [Route("destroy")]
        public IActionResult DeleteAll([FromRoute] long id, [FromRoute] string title)
        {
            _context.Ideas.RemoveRange(_context.Ideas);
            _context.SaveChanges();

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