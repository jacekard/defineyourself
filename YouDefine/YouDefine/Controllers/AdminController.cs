namespace YouDefine.Services
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Admin Controller used for returning corresponding views
    /// </summary>
    public class AdminController : Controller
    {
        [Route("Admin/youdefine")]
        public ActionResult Index()
        {
            //User.IsInRole("Admin");
            //User.Identity.IsAuthenticated

            return View();
        }

        [Route("Admin/youdefine/stats")]
        public ActionResult Statistics()
        {
            return View();
        }

        [Route("Admin/youdefine/settings")]
        public ActionResult Settings()
        {
            return View();
        }

        [Route("Admin/youdefine/users")]
        public ActionResult Users()
        {
            return View();
        }

        [Route("Admin/youdefine/bugs")]
        public ActionResult BugReports()
        {
            return View();
        }
    }
}