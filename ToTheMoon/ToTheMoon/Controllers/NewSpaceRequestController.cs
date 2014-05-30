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

        // GET: /NewSpaceRequest/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    NewSpaceRequest newspacerequest = db.NewSpaceRequests.Find(id);
        //    if (newspacerequest == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(newspacerequest);
        //}

        // POST: /NewSpaceRequest/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Review([Bind(Include = "ID,name,SpaceID,capacity,increase,comment")] NewSpaceRequest newspacerequest)
        {
            String comment = newspacerequest.comment;
            NewSpaceRequest nsr = db.NewSpaceRequests.Find(newspacerequest.ID);
            nsr.comment = comment;
            nsr.timestamp = DateTime.Now;

            if (ModelState.IsValid)
            {
                db.Entry(nsr).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("../Dashboard");
            }
            return View(nsr);
        }

        // GET: /NewSpaceRequest/Delete/5
        public ActionResult Review(int? id)
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ProjectContext()));
            ApplicationUser currentUser = manager.FindById(User.Identity.GetUserId());

            ViewBag.UserRole = currentUser.role;

            if (id == null)
            {
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                return RedirectToAction("../Dashboard");
            }
            NewSpaceRequest newspacerequest = db.NewSpaceRequests.Find(id);
            if (newspacerequest == null)
            {
                //return HttpNotFound();
                return RedirectToAction("../Dashboard");
            }

            newspacerequest.requester = (ApplicationUser)db.Users.Find(newspacerequest.requester_key);
            return View(newspacerequest);

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
                space.PI = newspacerequest.requester;
                
                //make our progress bars look pretty
                Random random = new Random();
                space.used = random.Next(0, space.capacity);

                UserSpace userspace = new UserSpace();
                userspace.space = space;
                userspace.user = newspacerequest.requester;
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
