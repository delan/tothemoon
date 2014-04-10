using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ToTheMoon.Models;
using ToTheMoon.DAL;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ToTheMoon.Controllers
{
    public class RequestController : Controller
    {
        private static ProjectContext db = new ProjectContext();
        private static UserManager<ApplicationUser> UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

        // GET: /Request/
        public ActionResult Index()
        {
            return View(db.Requests.ToList());
        }

        // GET: /Request/Space
        public ActionResult Space()
        {
            return View("Space");
        }

        // POST: /Request/Space
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Space([Bind(Include = "RequestID,SpaceName,ProjectID,Description,StorageTotal,YearlyGrowth")] NewSpaceRequest newspacerequest)
        {
            if (ModelState.IsValid)
            {
                // newspacerequest.RequestTimestamp = DateTime.UtcNow;
                newspacerequest.StorageUsed = 0;
                newspacerequest.Requester = UserManager.FindById<ApplicationUser>(User.Identity.GetUserId());

                db.NewSpaceRequests.Add(newspacerequest);
                db.SaveChanges();
                return RedirectToAction("Dashboard", "Home");
            }

            return View(newspacerequest);
        }

        /*protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }*/
    }
}
