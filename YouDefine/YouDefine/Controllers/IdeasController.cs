namespace YouDefine.Services
{
    using System;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using YouDefine.Data;

    /// <summary>
    /// IdeasController - API controller with CRUD methods
    /// </summary>
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
        [Route("titles")]
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
        [Route("")]
        [ActionName("GetIdea")]
        public IActionResult GetIdea(string title)
        {
            var idea = _provider.GetSpecified(title);
            if (idea == null)
            {
                return NoContent();
            }
            return Ok(idea);

        }

        [HttpPut]
        [Route("like")]
        public IActionResult LikeDefinition(string title, long id)
        {
            var result = _provider.LikeDefinition(title, id);
            if (result == null)
            {
                return BadRequest();
            }

            return Ok(result);
        }

        [HttpPut]
        [Route("unlike")]
        public IActionResult UnlikeDefinition(string title, long id)
        {
            var result = _provider.UnlikeDefinition(title, id);
            if (result == null)
            {
                return BadRequest();
            }
            return Ok(result);
        }

        [HttpPut]
        [Route("")]
        public IActionResult Put(string title, string text)
        {
            var idea = _provider.Update(title, text);
            if(idea == null)
            {
                return BadRequest();
            }

            return Ok(idea);
        }

        [HttpPost]
        [Route("")]
        public IActionResult Post(string title, string text)
        {
            var idea = _provider.Add(title, text);
            if(idea == null)
            {
                return BadRequest();
            }

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