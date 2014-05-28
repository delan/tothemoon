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
            newspacerequest.timestamp = DateTime.Now;
    
            if (ModelState.IsValid)
            {

                db.NewSpaceRequests.Add(newspacerequest);
                db.SaveChanges();
                return RedirectToAction("Index");
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
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include="ID,name,brief,comment,timestamp")] NewSpaceRequest newspacerequest)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(newspacerequest).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(newspacerequest);
        //}

        // GET: /NewSpaceRequest/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewSpaceRequest newspacerequest = db.NewSpaceRequests.Find(id);
            if (newspacerequest == null)
            {
                return HttpNotFound();
            }
            return View(newspacerequest);
        }

        // POST: /NewSpaceRequest/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NewSpaceRequest newspacerequest = db.NewSpaceRequests.Find(id);
            db.NewSpaceRequests.Remove(newspacerequest);
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
