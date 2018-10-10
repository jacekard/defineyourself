namespace YouDefine.Services
{
    using System.Diagnostics;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using YouDefine.Models;

    /// <summary>
    /// Home Controller used for returning corresponding views
    /// </summary>
    public class HomeController : Controller
    {
        [Route("")]
        [Route("Home")]
        [Route("Home/Index")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("about")]
        public IActionResult About()
        {
            ViewData["Message"] = "About youDefine";

            return View();
        }

        [Route("reportBugs")]
        public IActionResult BugReports()
        {
            ViewData["Message"] = "Report bugs";

            return View("BugReports");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
