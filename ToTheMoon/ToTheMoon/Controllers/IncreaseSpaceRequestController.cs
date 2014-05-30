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
    public class IncreaseSpaceRequestController : Controller
    {
        private ProjectContext db = new ProjectContext();

        // GET: /IncreaseSpaceRequest/
        public ActionResult Index()
        {
            return View(db.IncreaseSpaceRequests.ToList());
        }

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

            IncreaseSpaceRequest incspacerequest = db.IncreaseSpaceRequests.Find(id);
            if (incspacerequest == null)
            {
                return RedirectToAction("../Dashboard");
            }
            incspacerequest.requester = (ApplicationUser)db.Users.Find(incspacerequest.requester_key);

            return View(incspacerequest);
        }


        // GET: /IncreaseSpaceRequest/Create/<id>
        public ActionResult Create(int? SpaceID)
        {
            if(SpaceID == null)
            {
                return RedirectToAction("../Dashboard");
            }
            IncreaseSpaceRequest increasespacerequest = new IncreaseSpaceRequest();
            increasespacerequest.space = db.Spaces.Find(SpaceID);
            increasespacerequest.SpaceID = (int)SpaceID;
            if(increasespacerequest.space == null)
            {
                return RedirectToAction("../Dashboard");
            }
            return View(increasespacerequest);
        }

        // POST: /IncreaseSpaceRequest/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ID,SpaceID,brief,increase")] IncreaseSpaceRequest increasespacerequest)
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ProjectContext()));
            ApplicationUser currentUser = manager.FindById(User.Identity.GetUserId());
            increasespacerequest.space = db.Spaces.Find(increasespacerequest.SpaceID);
            increasespacerequest.requester = (ApplicationUser)db.Users.Find(currentUser.Id);
            increasespacerequest.requester_key = currentUser.Id;



            increasespacerequest.timestamp = DateTime.Now;

            if (ModelState.IsValid)
            {
                db.IncreaseSpaceRequests.Add(increasespacerequest);
                db.SaveChanges();
                return RedirectToAction("../Dashboard");
            }

            return View(increasespacerequest);
        }

       /* // GET: /IncreaseSpaceRequest/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IncreaseSpaceRequest increasespacerequest = db.IncreaseSpaceRequests.Find(id);
            if (increasespacerequest == null)
            {
                return HttpNotFound();
            }
            return View(increasespacerequest);
        }*/

        // POST: /IncreaseSpaceRequest/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Review")]
        [ValidateAntiForgeryToken]
        public ActionResult Review([Bind(Include = "ID,SpaceID,brief,increase")] IncreaseSpaceRequest increasespacerequest)
        {
            IncreaseSpaceRequest isr = db.IncreaseSpaceRequests.Find(increasespacerequest.ID);
            isr.brief = increasespacerequest.brief;
            isr.space = db.Spaces.Find(increasespacerequest.SpaceID);
            if (ModelState.IsValid)
            {
                isr.timestamp = DateTime.Now;
                db.SaveChanges();
                return RedirectToAction("../Dashboard");
            }
            return View(increasespacerequest);
        }

        // GET: /IncreaseSpaceRequest/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("../Dashboard");
            }
            IncreaseSpaceRequest increasespacerequest = db.IncreaseSpaceRequests.Find(id);
            if (increasespacerequest == null)
            {
                return RedirectToAction("../Dashboard");
            }
            return View(increasespacerequest);
        }

        //[HttpPost, ActionName("Approve")]
        //[ValidateAntiForgeryToken]
        public ActionResult Approve(int id)
        {
            IncreaseSpaceRequest incspacerequest = db.IncreaseSpaceRequests.Find(id);
            Space space = db.Spaces.Find(incspacerequest.SpaceID);
            if(((Int64)space.capacity + (Int64)incspacerequest.increase) > int.MaxValue)
            {
                space.capacity = int.MaxValue;
            }
            else
            {
                space.capacity += incspacerequest.increase;
            }
            
            db.IncreaseSpaceRequests.Remove(incspacerequest);
            db.SaveChanges();

            ///////////////////////////////
            ///////////////////////////////
            ////Send Email to Requester////
            ///////////////////////////////
            ///////////////////////////////


            return RedirectToAction("../Dashboard");
        }

        // POST: /incspacerequest/Delete/5
        //[HttpPost, ActionName("Decline")]
        //[ValidateAntiForgeryToken]
        public ActionResult Decline(int id)
        {
            IncreaseSpaceRequest incspacerequest = db.IncreaseSpaceRequests.Find(id);

            db.IncreaseSpaceRequests.Remove(incspacerequest);
            db.SaveChanges();

            ///////////////////////////////
            ///////////////////////////////
            ////Send Email to Requester////
            ///////////////////////////////
            ///////////////////////////////

            return RedirectToAction("../Dashboard");
        }
/*
        // POST: /IncreaseSpaceRequest/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            IncreaseSpaceRequest increasespacerequest = db.IncreaseSpaceRequests.Find(id);
            db.IncreaseSpaceRequests.Remove(increasespacerequest);
            db.SaveChanges();
            return RedirectToAction("Index");
        }*/

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
