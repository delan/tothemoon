using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using ToTheMoon.Models;
using ToTheMoon.DAL;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;

namespace ToTheMoon.Controllers
{
    public class SpaceController : Controller
    {
        private ProjectContext db = new ProjectContext();

        public ActionResult Review(int? id)
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ProjectContext()));
            ApplicationUser currentUser = manager.FindById(User.Identity.GetUserId());

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Space space = db.Spaces.Find(id);
            
            if (space == null)
            {
                return HttpNotFound();
            }

            space.PI = manager.FindById(space.PIKey);

            UserSpace us;
            SpaceRole role;

            if (currentUser.role == GlobalRole.REGULAR)
            {
                try
                {
                    us = db.UserSpaces.ToList().First(usc => usc.userKey == currentUser.Id && usc.space == space);
                    role = us.role;
                }
                catch (Exception e)
                {
                    return HttpNotFound();
                }
            }
            else
            {
                role = SpaceRole.COLLAB_RO;
            }

            ViewBag.readOnlyResearchers = db.UserSpaces.ToList().FindAll(usc => usc.space == space && usc.role == SpaceRole.COLLAB_RO);
            ViewBag.readWriteResearchers = db.UserSpaces.ToList().FindAll(usc => usc.space == space && usc.role == SpaceRole.COLLAB_RW);
            ViewBag.dataManagers = db.UserSpaces.ToList().FindAll(usc => usc.space == space && usc.role == SpaceRole.DATAMANAGER);
            ViewBag.dataManagerUsers = db.UserSpaces.ToList().FindAll(usc => usc.space == space && usc.role == SpaceRole.DATAMANAGER).Select(usc => usc.user);
            ViewBag.canChangeCapacity = false;
            ViewBag.canRequestSpace   = false;
            ViewBag.canChangePI       = false;
            ViewBag.canChangeRoles    = false;
            ViewBag.canAddUsers       = false;

            if (currentUser.role == GlobalRole.ADMIN)
            {
                ViewBag.canChangePI = true;
            }

            if (role == SpaceRole.DATAMANAGER || currentUser.role != GlobalRole.REGULAR)
            {
                ViewBag.canChangeRoles    = true;
                ViewBag.canAddUsers       = true;
            }

            if (currentUser.role != GlobalRole.REGULAR)
            {
                ViewBag.canChangeCapacity = true;
            }

            if (role == SpaceRole.DATAMANAGER)
            {
                ViewBag.canRequestSpace   = true;
            }

            return View(space);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Review([Bind(Include="key,Name,capacity,used,increase,PIKey, PI")] Space space)
        {
            space.PI = (ApplicationUser)db.Users.Find(space.PIKey);
            if (ModelState.IsValid)
            {
                db.Entry(space).State = EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("Review",space.key);
        }

        /********
         * Change Capacity (Admin and Approver ONLY).
         ********/

        public ActionResult ChangeCapacity(int? id)
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ProjectContext()));
            ApplicationUser currentUser = manager.FindById(User.Identity.GetUserId());

            // Permissions Check.
            if (currentUser.role != GlobalRole.ADMIN && currentUser.role != GlobalRole.APPROVER)
            {
                return HttpNotFound();
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Space space = db.Spaces.Find(id);

            if (space == null)
            {
                return HttpNotFound();
            }

            SpaceUpdateCapacityViewModel svm = new SpaceUpdateCapacityViewModel();
            svm.Name = space.Name;
            svm.SpaceID = space.key;
            svm.capacity = space.capacity;
            svm.used = space.used;

            return View(svm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangeCapacity([Bind(Include = "SpaceID,Name,capacity,used")] SpaceUpdateCapacityViewModel space)
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ProjectContext()));
            ApplicationUser currentUser = manager.FindById(User.Identity.GetUserId());

            if (ModelState.IsValid && (currentUser.role == GlobalRole.ADMIN || currentUser.role == GlobalRole.APPROVER))
            {
                Space fullSpace = db.Spaces.Find(space.SpaceID);
                fullSpace.capacity = space.capacity;
                fullSpace.used = space.used;

                db.Entry(fullSpace).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Review", "Space", new {id = space.SpaceID });
            }

            return View();
        }


        // POST: /Space/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            Space space = db.Spaces.Find(id);
            db.Spaces.Remove(space);
            db.SaveChanges();
            return RedirectToAction("Spaces", space.key);
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