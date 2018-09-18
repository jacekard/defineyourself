namespace YouDefine.Controllers
{
    using System;
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
        [Route("")]
        public IEnumerable<Bug> GetBugs()
        {
            return _context.Bugs;
        }

        [HttpGet]
        [Route("active")]
        public IEnumerable<Bug> GetActiveBugs()
        {

            return _context.Bugs.Where(x => x.IsFixed.Equals(false));
        }

        [HttpGet]
        [Route("completed")]
        public IEnumerable<Bug> GetCompletedBugs()
        {
            return _context.Bugs.Where(x => x.IsFixed.Equals(true));
        }

        [HttpGet]
        [Route("progress")]
        public IActionResult GetCalculatedProgress()
        {
            if(GetBugs().Count() == 0)
            {

                return Json(new { Success = -1, Failure = -1 });
            }

            var completed = GetCompletedBugs();
            var active = GetActiveBugs();

            var sum = completed.Count() + active.Count();
            var success = Math.Round((decimal)completed.Count() / sum * 100);
            var failure = 100 - success;

            return Json(new { Success = success, Failure = failure});
        }

        [HttpPut]
        [Route("ok/{id}")]
        public IActionResult CompletedBug([FromRoute] long id)
        {
            try
            {
                var bug = _context.Bugs.Where(x => x.Id.Equals(id)).Single();
                bug.IsFixed = true;
                _context.SaveChanges();

                return Ok();
            }
            catch
            {
                return NotFound();
            }

        }

        [HttpPut]
        [Route("no/{id}")]
        public IActionResult UncompletedBug([FromRoute] long id)
        {
            try
            {
                var bug = _context.Bugs.Where(x => x.Id.Equals(id)).Single();
                bug.IsFixed = false;
                _context.SaveChanges();

                return Ok();
            }
            catch
            {
                return NotFound();
            }
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

            return Ok();
        }
    }
}