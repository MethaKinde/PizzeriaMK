using PizzeriaMK.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace PizzeriaMK.Controllers
{
    public class UsersController : Controller
    {
        private DBContext db = new DBContext();

        [Authorize(Roles = "admin")]
        // GET: Users
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        // GET: Users/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            User user = db.Users.Find(id);

            if (HttpContext.User.IsInRole("admin"))
            {
                if (user == null)
                {
                    return HttpNotFound();
                }
                return View(user);
            }
            else
            {
                if (HttpContext.User.Identity.Name != id.ToString())
                {
                    return RedirectToAction("Index", "Home");
                }
                return View(user);
            }

        }

        // GET: Users/Create
        public ActionResult Create()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        // POST: Users/Create
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserId,Email,Password,Name,Surname,Phone")] User user)
        {
            if (ModelState.IsValid)
            {
                user.Role = "user";
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View(user);
        }

        // GET: Users/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (HttpContext.User.Identity.Name == id.ToString())
            {
                User user = db.Users.Find(id);
                if (user == null)
                {
                    return HttpNotFound();
                }
                return View(user);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // POST: Users/Edit/5
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserId,Email,Password,Name,Surname,Phone")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: Users/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            User user = db.Users.Find(id);

            if (HttpContext.User.IsInRole("admin"))
            {
                if (user == null)
                {
                    return HttpNotFound();
                }
                return View(user);
            }
            else
            {
                if (HttpContext.User.Identity.Name != id.ToString())
                {
                    return RedirectToAction("Index", "Home");
                }
                return View(user);
            }
        }

        // POST: Users/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();

            if (User.IsInRole("admin"))
            {
                db.Users.Remove(user);
                db.SaveChanges();
            }

            if (User.IsInRole("user"))
            {
                var userId = Convert.ToInt32(HttpContext.User.Identity.Name);
                if (user.UserId == id)
                {
                    db.Users.Remove(user);
                    db.SaveChanges();
                }
            }

            return RedirectToAction("Index", "Home");
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