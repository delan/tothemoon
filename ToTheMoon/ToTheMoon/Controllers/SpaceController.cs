using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using ToTheMoon.Models;
using ToTheMoon.DAL;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

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

            ViewBag.spaceRole = (db.UserSpaces.ToList<UserSpace>().Find(us => us.user.UserName.Equals(currentUser.UserName) && us.space.Equals(space))).role;

            return View(space);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Review([Bind(Include="key,Name,capacity,used,increase,PIKey")] Space space)
        {
            if (ModelState.IsValid)
            {
                db.Entry(space).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(space);
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

                return RedirectToAction("Index");
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
