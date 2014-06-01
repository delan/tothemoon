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

            if (ModelState.IsValid && userspace.userKey != userspace.space.PIKey)
            {
                userspace.role = role;

                //////////////////////////
                //////////////////////////
                ////Send Email to User////
                //////////////////////////
                //////////////////////////

                db.Entry(userspace).State = EntityState.Modified;
                db.SaveChanges();


                return RedirectToAction("../Space/Review/" + userspace.space.key);
            }
            return View(userspace);
        }

        // GET: /UserSpace/Create/5
        public ActionResult Create(int? id)
        {


            if(id == null)
            {
                return Redirect("../Dashboard");
            }
            Space space = db.Spaces.Find(id);
            List<ApplicationUser> alreadyUsers = db.UserSpaces.ToList().FindAll(us => us.spaceKey.Equals(id)).Select(s => s.user).ToList();
            alreadyUsers.Add(space.PI);
            ViewBag.Users = db.Users.ToList().Except(alreadyUsers);

            if(space == null)
            {
                return Redirect("../Dashboard");
            }
            UserSpace userspace = new UserSpace();
            userspace.space = db.Spaces.Find(id);
            userspace.spaceKey = userspace.space.key;
            return View(userspace);
        }

        // POST: /UserSpace/Create/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "spaceKey,role,userKey")] UserSpace userspace)
        {
            userspace.user = (ApplicationUser)db.Users.Find(userspace.userKey);
            userspace.space = db.Spaces.Find(userspace.spaceKey);
            if (ModelState.IsValid && userspace.user != null)
            {

                //////////////////////////
                //////////////////////////
                ////Send Email to User////
                //////////////////////////
                //////////////////////////

                db.UserSpaces.Add(userspace);
                db.SaveChanges();

                return RedirectToAction("Review","Space",new {id = userspace.space.key});
            }
            Space space = db.Spaces.Find(userspace.spaceKey);
            List<ApplicationUser> alreadyUsers = db.UserSpaces.ToList().FindAll(us => us.spaceKey.Equals(userspace.spaceKey)).Select(s => s.user).ToList();
            alreadyUsers.Add(space.PI);
            ViewBag.Users = db.Users.ToList().Except(alreadyUsers);
            return View(userspace);//RedirectToAction("Create", new { id = userspace.spaceKey});
        }

        // POST: /UserSpace/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            UserSpace userspace = db.UserSpaces.Find(id);
            int space_id = userspace.space.key;
            if (userspace.userKey != userspace.space.PIKey)
            {

                //////////////////////////
                //////////////////////////
                ////Send Email to User////
                //////////////////////////
                //////////////////////////

                db.UserSpaces.Remove(userspace);
                db.SaveChanges();
            }
            return RedirectToAction("Review", "Space", new { id = space_id });
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
