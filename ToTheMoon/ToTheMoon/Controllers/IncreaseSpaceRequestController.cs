﻿using System;
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

        // GET: /IncreaseSpaceRequest/Details/5
        public ActionResult Details(int? id)
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
        }

        // GET: /IncreaseSpaceRequest/Create/<id>
        public ActionResult Create(int? SpaceID)
        {
            if(SpaceID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IncreaseSpaceRequest increasespacerequest = new IncreaseSpaceRequest();
            increasespacerequest.space = db.Spaces.Find(SpaceID);
            if(increasespacerequest.space == null)
            {
                return HttpNotFound();
            }
            return View(increasespacerequest);
        }

        // POST: /IncreaseSpaceRequest/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="brief,increase")] IncreaseSpaceRequest increasespacerequest)
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ProjectContext()));
            ApplicationUser currentUser = manager.FindById(User.Identity.GetUserId());

            increasespacerequest.requester = (ApplicationUser)db.Users.Find(currentUser.Id);
            increasespacerequest.timestamp = DateTime.Now;

            if (ModelState.IsValid)
            {
                db.IncreaseSpaceRequests.Add(increasespacerequest);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(increasespacerequest);
        }

        // GET: /IncreaseSpaceRequest/Edit/5
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
        }

        // POST: /IncreaseSpaceRequest/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,brief,increas,timestamp")] IncreaseSpaceRequest increasespacerequest)
        {
            if (ModelState.IsValid)
            {
                db.Entry(increasespacerequest).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(increasespacerequest);
        }

        // GET: /IncreaseSpaceRequest/Delete/5
        public ActionResult Delete(int? id)
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
        }

        // POST: /IncreaseSpaceRequest/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            IncreaseSpaceRequest increasespacerequest = db.IncreaseSpaceRequests.Find(id);
            db.IncreaseSpaceRequests.Remove(increasespacerequest);
            db.SaveChanges();
            return RedirectToAction("Index");
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
