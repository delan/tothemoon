using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ToTheMoon.Models;
using ToTheMoon.DAL;

namespace ToTheMoon.Controllers
{
    public class HomeController : Controller
    {
        private ProjectContext db = new ProjectContext();

        [AllowAnonymous]
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return Redirect("Dashboard");
            }

            return View();
        }

        [Route("Dashboard")]
        public ActionResult Dashboard()
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ProjectContext()));
            ApplicationUser currentUser = manager.FindById(User.Identity.GetUserId());



            if(currentUser.role == GlobalRole.ADMIN || currentUser.role == GlobalRole.APPROVER)
            {
                ViewBag.NewSpaceRequests = db.NewSpaceRequests.ToList<NewSpaceRequest>();
                ViewBag.IncreaseSpaceRequests = db.IncreaseSpaceRequests.ToList<IncreaseSpaceRequest>();
                ViewBag.Spaces = db.Spaces.ToList<Space>();
            }
            else
            {

            }

            
            return View();
        }
    }
}