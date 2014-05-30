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

<<<<<<< HEAD
            if(currentUser.role != GlobalRole.REGULAR)
=======
            if (currentUser.role == GlobalRole.ADMIN || currentUser.role == GlobalRole.APPROVER)
>>>>>>> 26a8f408ee9eab495d41a47cfd5c4e336c2f7a79
            {
                ViewBag.NewSpaceRequests = db.NewSpaceRequests.ToList<NewSpaceRequest>();
                ViewBag.IncreaseSpaceRequests = db.IncreaseSpaceRequests.ToList<IncreaseSpaceRequest>();
                ViewBag.Spaces = db.Spaces.ToList<Space>();
            }
            else
            {
                //change this to only show the right things
                ViewBag.NewSpaceRequests = db.NewSpaceRequests.ToList<NewSpaceRequest>();
                ViewBag.IncreaseSpaceRequests = db.IncreaseSpaceRequests.ToList<IncreaseSpaceRequest>();
                List<UserSpace> UserSpaces = db.UserSpaces.ToList<UserSpace>().FindAll(us => us.Equals(currentUser));
                ViewBag.Spaces = new List<Space>();
                ViewBag.UserList = new List<Object>();
                foreach (UserSpace us in UserSpaces)
                {
                    ViewBag.Spaces.Append(db.Spaces.Find(us.space));
                    ViewBag.UserList.Append(new {us.space.key, us.user.UserName});
                }
            }
            return View();
        }
    }
}