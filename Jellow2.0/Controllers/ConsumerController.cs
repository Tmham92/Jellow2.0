using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Jellow2._0.Database;
using Jellow2._0.ViewModels;

namespace Jellow2._0.Controllers
{
    public class ConsumerController : Controller
    {
        private DbModelContainer db = new DbModelContainer();

        // GET: Consumer
        public ActionResult Index(string sortOrder)
        {
            ViewBag.IDSortParam = String.IsNullOrEmpty(sortOrder) ? "consumerID_desc" : "";
            ViewBag.ProjectPostedParam = sortOrder == "Projectposted" ? "NoProject" : "Projectposted";
            ViewBag.NameParam = sortOrder == "NameOrder" ? "Name_Desc" : "NameOrder";

            var consumers = from c in db.Consumers select c;
            switch (sortOrder)
            {
                case "Projectposted":
                    consumers = consumers.OrderByDescending(c => c.HasProjectPosted);
                    break;
                case "NameOrder":
                    consumers = consumers.OrderBy(c => c.Name);
                    break;
                case "Name_Desc":
                    consumers = consumers.OrderByDescending(c => c.Name);
                    break;
                case "consumerID_desc":
                    consumers = consumers.OrderByDescending(c => c.ConsumerID);
                    break;
                default:
                    consumers = consumers.OrderBy(c => c.ConsumerID);
                    break;
            }
            CheckIfConsumerHasProjects();
            return View(consumers.ToList());
        }

// GET: Consumer/Details/5
public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Consumer consumer = db.Consumers.Find(id);
            if (consumer == null)
            {
                return HttpNotFound();
            }
            return View(consumer);
        }

        // GET: Consumer/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Consumer/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ConsumerID,Name,HasProjectPosted")] Consumer consumer)
        {
            if (ModelState.IsValid)
            {
                db.Consumers.Add(consumer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(consumer);
        }

        // GET: Consumer/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Consumer consumer = db.Consumers.Find(id);
            if (consumer == null)
            {
                return HttpNotFound();
            }
            return View(consumer);
        }

        // POST: Consumer/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ConsumerID,Name,HasProjectPosted")] Consumer consumer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(consumer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(consumer);
        }

        // GET: Consumer/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Consumer consumer = db.Consumers.Find(id);
            if (consumer == null)
            {
                return HttpNotFound();
            }
            return View(consumer);
        }

        // POST: Consumer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Consumer consumer = db.Consumers.Find(id);
            db.Consumers.Remove(consumer);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult ProjectDetails(int? ConsumerID)
        {

            if (ConsumerID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ConsumerViewModel ConsumerVM = new ConsumerViewModel();
            var projects = db.Projects.Where(p => p.ConsumerID == ConsumerID);
            ConsumerVM.allProjects = projects.ToList();
            return View(ConsumerVM);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public void CheckIfConsumerHasProjects()
        {
            var consumers = db.Consumers.ToList();
            foreach (var consumer in consumers)
            {

                if (consumer.ProjectList.Count == 0)
                {
                    consumer.HasProjectPosted = false;
                }
                else
                {
                    consumer.HasProjectPosted = true;
                }
            }
        }
    }
}
