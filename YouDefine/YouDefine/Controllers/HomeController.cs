namespace YouDefine.Services
{
    using System.Diagnostics;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using YouDefine.Data;

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
        public IActionResult ReportBugs()
        {
            ViewData["Message"] = "Report bugs";

            return View();
        }

        [AllowAnonymous]
        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            });
        }
    }
}
