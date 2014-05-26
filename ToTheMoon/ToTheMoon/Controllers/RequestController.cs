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

        // GET: /Request/Details/<id>
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                return RedirectToAction("Index");
            }
            Request req = db.Requests.Find(id);
            if (req == null)
            {
                return HttpNotFound();
            }
            return View(req);
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
                newspacerequest.RequestTimestamp = DateTime.Now;
                db.NewSpaceRequests.Add(newspacerequest);
                db.SaveChanges();
                return RedirectToAction("Dashboard", "Home");
            }

            return View(newspacerequest);
        }

        // GET: /Request/Comment/<id>
        public ActionResult Comment(int? id)
        {
            if (id == null)
            {
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                return RedirectToAction("Index");
            }
            Request req  = db.Requests.Find(id);
            if (req == null)
            {
                return HttpNotFound();
            }
            return View(req);
        }

        // POST: /Request/Comment/<id>
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Comment([Bind(Include="RequestID,Comment")] Request req)
        {
            if (ModelState.IsValid)
            {
                db.Entry(req).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(req);
        }

        // GET: /Request/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                return RedirectToAction("Index");
            }
            Request req = db.Requests.Find(id);
            if (req == null)
            {
                return HttpNotFound();
            }
            return View(req);
        }

        // POST: /Request/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Request req = db.Requests.Find(id);
            db.Requests.Remove(req);
            db.SaveChanges();
            return RedirectToAction("Index");
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
