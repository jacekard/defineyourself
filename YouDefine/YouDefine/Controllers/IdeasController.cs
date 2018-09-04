﻿using System;
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
            try
            {
                var ideas = _context.Ideas.Include(x => x.Definitions).ToList();
                return Ok(ideas);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Route("{id:int}")]
        public ActionResult GetIdea(long id)
        {
            var idea = _context.Ideas.SingleOrDefault(m => m.IdeaId == id);

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

        [HttpGet]
        [Route("{title:alpha}")]
        public IActionResult GetIdea([FromRoute] string title)
        {
            try
            {
                var idea = _context.Ideas.Where(m => m.Title == title).Include(x => x.Definitions).Single();
                var definitions = from d in idea.Definitions
                                  orderby d.Likes
                                  select new { d.Text, d.Likes };

                return Ok(new { idea.Title, idea.LastModifiedDate, definitions });
                //write extension to DateTime class that formats date of last modified based on today's date.
                // return Ok(new { idea.Title, idea.LastModifiedDate idea.Definitions });

            }
            catch
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Route("{title:alpha}/likes")]
        public IActionResult GetIdeaLikes([FromRoute] string title)
        {
            try
            {
                var idea = _context.Ideas.Where(m => m.Title == title).Include(x => x.Definitions).Single();
                return Ok(new { Likes = idea.CountLikes() });

            }
            catch
            {
                return NotFound();
            }
        }

        [HttpPut]
        [Route("{title}/{text}")]
        public IActionResult PutIdea(string title, string text)
        {
            Idea idea;
            try
            {
                idea = _context.Ideas.Single(x => x.Title == title);
                if (idea != null)
                {
                    var definition = new Definition(text)
                    {
                        IdeaId = idea.IdeaId
                    };
                    idea.Definitions.Add(definition);
                }
                _context.SaveChanges();
            }
            catch
            {
                return NotFound();
            }

            return Ok(idea);
        }

        [HttpPost]
        [Route("{title}/{text}")]
        public IActionResult PostIdea(string title, string text)
        {

            var idea = new Idea(title)
            {
                Definitions = new List<Definition>()
            };
            var definition = new Definition(text)
            {
                IdeaId = idea.IdeaId
            };
            idea.Definitions.Add(definition);
            _context.Ideas.Add(idea);
            _context.SaveChanges();

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
        public IActionResult DeleteIdeas([FromRoute] long id, [FromRoute] string title)
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

        //private string FormatLastModifiedDate(DateTime date)
        //{
        //    var time = date - DateTime.Now;


        //    return "";
        //}
    }
}