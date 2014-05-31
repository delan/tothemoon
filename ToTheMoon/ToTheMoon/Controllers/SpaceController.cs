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

        // GET: /Space/
        public ActionResult Index()
        {
            return View(db.Spaces.ToList());
        }

        // GET: /Space/Details/5
        public ActionResult Review(int? id)
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ProjectContext()));
            ApplicationUser currentUser = manager.FindById(User.Identity.GetUserId());

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Space space = db.Spaces.Find(id);


            ViewBag.spaceRole = (db.UserSpaces.ToList<UserSpace>().Find(us => us.user.UserName.Equals(currentUser.UserName) && us.space.Equals(space))).role;
            
            if (space == null)
            {
                return HttpNotFound();
            }
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
