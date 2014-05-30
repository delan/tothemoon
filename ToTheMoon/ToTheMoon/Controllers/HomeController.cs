using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
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

            if (currentUser == null)
            {
                return Redirect("/");
            }

            ViewBag.UserRole = currentUser.role;

            if (currentUser.role == GlobalRole.ADMIN || currentUser.role == GlobalRole.APPROVER)
            {
                ViewBag.NewSpaceRequests = db.NewSpaceRequests.ToList<NewSpaceRequest>();
                ViewBag.IncreaseSpaceRequests = db.IncreaseSpaceRequests.ToList<IncreaseSpaceRequest>();
                ViewBag.Spaces = db.Spaces.ToList<Space>();
            }
            else
            {
                //change this to only show the right things
                ViewBag.NewSpaceRequests = db.NewSpaceRequests.ToList<NewSpaceRequest>().FindAll(nsr => nsr.requester_key.Equals(currentUser.Id));
                ViewBag.IncreaseSpaceRequests = db.IncreaseSpaceRequests.ToList<IncreaseSpaceRequest>().FindAll(isr => isr.requester_key.Equals(currentUser.Id));
                ViewBag.UserSpaces = db.UserSpaces.ToList<UserSpace>().FindAll(us => us.user.UserName.Equals(currentUser.UserName));
            }
            return View();
        }
    }
}