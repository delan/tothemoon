using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using ToTheMoon.Models;
using ToTheMoon.DAL;

namespace ToTheMoon.Controllers
{
    public class NewSpaceRequestController : Controller
    {
        private ProjectContext db = new ProjectContext();

        // GET: /NewSpaceRequest/
        public ActionResult Index()
        {
            return View(db.NewSpaceRequests.ToList());
        }

        // GET: /NewSpaceRequest/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //       return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //   }
        //    NewSpaceRequest newspacerequest = db.NewSpaceRequests.Find(id);
        //    if (newspacerequest == null)
        //   {
        //        return HttpNotFound();
        //    }
        //    return View(newspacerequest);
        //}

        // GET: /NewSpaceRequest/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /NewSpaceRequest/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="name,brief,SpaceID,capacity,increase")] NewSpaceRequest newspacerequest)
        {

            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ProjectContext()));
            ApplicationUser currentUser = manager.FindById(User.Identity.GetUserId());

            newspacerequest.requester = (ApplicationUser)db.Users.Find(currentUser.Id);
            newspacerequest.requester_key = currentUser.Id;

            newspacerequest.timestamp = DateTime.Now;
    
            if (ModelState.IsValid)
            {
                db.NewSpaceRequests.Add(newspacerequest);
                db.SaveChanges();
                return RedirectToAction("../Dashboard");
            }

            return View(newspacerequest);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Review([Bind(Include = "ID,comment")] NewSpaceRequestCommentViewModel nsrViewModel)
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ProjectContext()));
            ApplicationUser currentUser = manager.FindById(User.Identity.GetUserId());

            if (currentUser.role != GlobalRole.ADMIN)
            {
                return RedirectToAction("Review", "Space", new { ID = nsrViewModel.ID });
            }

            if (ModelState.IsValid)
            {
                NewSpaceRequest nsr = db.NewSpaceRequests.Find(nsrViewModel.ID);
                nsr.comment = nsrViewModel.comment;
                nsr.timestamp = DateTime.Now;

                db.Entry(nsr).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Dashboard", "Home");
            }

            return View(nsrViewModel);
        }

        public ActionResult Review(int? id)
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ProjectContext()));
            ApplicationUser currentUser = manager.FindById(User.Identity.GetUserId());

            ViewBag.UserRole = currentUser.role;

            if (id == null)
            {
                return RedirectToAction("Dashboard", "Home");
            }

            NewSpaceRequest newspacerequest = db.NewSpaceRequests.Find(id);

            if (newspacerequest == null && (currentUser.role != GlobalRole.REGULAR || currentUser.Id == newspacerequest.requester_key))
            {
                return RedirectToAction("Dashboard", "Home");
            }

            if (currentUser.role != GlobalRole.REGULAR)
            {
                ViewBag.canComment = false;
            }
            else
            {
                ViewBag.canComment = true;
            }

            NewSpaceRequestCommentViewModel nsrViewModel = new NewSpaceRequestCommentViewModel();
            nsrViewModel.ID = newspacerequest.ID;
            nsrViewModel.name = newspacerequest.name;
            nsrViewModel.brief = newspacerequest.brief;
            nsrViewModel.capacity = newspacerequest.capacity;
            nsrViewModel.increase = newspacerequest.increase;
            nsrViewModel.requester = manager.FindById(newspacerequest.requester_key);
            nsrViewModel.comment = newspacerequest.comment;

            return View(nsrViewModel);
        }

        // POST: /NewSpaceRequest/Delete/5
        //[HttpPost, ActionName("Review")]
        //[ValidateAntiForgeryToken]
        public ActionResult Approve(int id)
        {
            if (ViewBag.UserRole != GlobalRole.REGULAR)
            {
                NewSpaceRequest newspacerequest = (NewSpaceRequest)db.NewSpaceRequests.Find(id);
                Space space = new Space();
                space.capacity = newspacerequest.capacity;
                space.increase = newspacerequest.increase;
                space.key = newspacerequest.SpaceID;
                space.Name = newspacerequest.name;
                space.PI = (ApplicationUser)db.Users.Find(newspacerequest.requester_key);
                space.PIKey = newspacerequest.requester_key;
                
                //make our progress bars look pretty
                Random random = new Random();
                space.used = random.Next(0, space.capacity);

                UserSpace userspace = new UserSpace();
                userspace.space = space;
                userspace.user = (ApplicationUser)db.Users.Find(newspacerequest.requester_key);
                userspace.userKey = newspacerequest.requester_key;

                userspace.role = SpaceRole.DATAMANAGER;

                db.Spaces.Add(space);
                db.UserSpaces.Add(userspace);
                db.NewSpaceRequests.Remove(newspacerequest);
                db.SaveChanges();

            ///////////////////////////////
            ///////////////////////////////
            ////Send Email to Requester////
            ///////////////////////////////
            ///////////////////////////////

            }


            return RedirectToAction("../Dashboard");
        }

        // POST: /NewSpaceRequest/Delete/5
        //[HttpPost, ActionName("Review")]
        //[ValidateAntiForgeryToken]
        public ActionResult Decline(int id)
        {
            NewSpaceRequest newspacerequest = db.NewSpaceRequests.Find(id);
           
            db.NewSpaceRequests.Remove(newspacerequest);
            db.SaveChanges();

            ///////////////////////////////
            ///////////////////////////////
            ////Send Email to Requester////
            ///////////////////////////////
            ///////////////////////////////

            return RedirectToAction("../Dashboard");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
