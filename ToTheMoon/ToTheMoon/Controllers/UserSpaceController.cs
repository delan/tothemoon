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
    public class UserSpaceController : Controller
    {
        private ProjectContext db = new ProjectContext();

        // GET: /UserSpace/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                return RedirectToAction("../Dashboard");
            }
            UserSpace userspace = db.UserSpaces.Find(id);
            
            if (userspace == null)
            {
                //return HttpNotFound();
                return RedirectToAction("../Dashboard");
            }

            userspace.user = (ApplicationUser)db.Users.Find(userspace.userKey);

            return View(userspace);
        }

        // POST: /UserSpace/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ProductId,role")] UserSpace userspace)
        {
            SpaceRole role = userspace.role;

            userspace = db.UserSpaces.Find(userspace.ProductId);

            if (ModelState.IsValid && User.Identity.GetUserId() != userspace.space.PIKey)
            {
                userspace.role = role;

                db.Entry(userspace).State = EntityState.Modified;
                db.SaveChanges();


                return RedirectToAction("../Space/Review/" + userspace.space.key);
            }
            return View(userspace);
        }

        // POST: /UserSpace/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            UserSpace userspace = db.UserSpaces.Find(id);
            int space_id = userspace.space.key;
            if (User.Identity.GetUserId() != userspace.space.PIKey)
            {
                db.UserSpaces.Remove(userspace);
                db.SaveChanges();
            }
            return RedirectToAction("../Space/Review/" + space_id);
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
