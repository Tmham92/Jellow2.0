using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Jellow2._0.Database;

namespace Jellow2._0.Controllers
{
    public class FreelancerController : Controller
    {
        private DbModelContainer db = new DbModelContainer();

        // GET: Freelancer
        public ActionResult Index(string sortOrder)
        {

            ViewBag.IDParam = String.IsNullOrEmpty(sortOrder) ? "freelancerID_desc" : "";
            ViewBag.NameParam = sortOrder == "NameAsc" ? "NameDesc" : "NameAsc";
            ViewBag.IsEmployedParam = sortOrder == "IsEmployed" ? "IsNotEmployed" : "IsEmployed";
            ViewBag.SkillParam = sortOrder == "SkillAsc" ? "SkillDesc" : "SkillAsc";

            var freelancers = from f in db.Freelancers select f;
            switch (sortOrder)
            {
                case "freelancerID_desc":
                    freelancers = freelancers.OrderByDescending(f => f.FreelancerID);
                    break;
                case "NameAsc":
                    freelancers = freelancers.OrderBy(f => f.Name);
                    break;
                case "NameDesc":
                    freelancers = freelancers.OrderByDescending(f => f.Name);
                    break;
                case "IsEmployed":
                    freelancers = freelancers.OrderByDescending(f => f.IsEmployed);
                    break;
                case "SkillAsc":
                    freelancers = freelancers.OrderBy(f => f.Skill).ThenBy(f => f.Experience);
                    break;
                case "SkillDesc":
                    freelancers = freelancers.OrderByDescending(f => f.Skill).ThenBy(f => f.Experience);
                    break;
                default:
                    freelancers = freelancers.OrderBy(f => f.FreelancerID);
                    break;
            }
            CheckIfCompanyHasJobsOrProjects();
            return View(freelancers.ToList());
        }

        // GET: Freelancer/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Freelancer freelancer = db.Freelancers.Find(id);
            if (freelancer == null)
            {
                return HttpNotFound();
            }
            return View(freelancer);
        }

        // GET: Freelancer/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Freelancer/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FreelancerID,Name,IsEmployed,Skill,Experience")] Freelancer freelancer)
        {
            if (ModelState.IsValid)
            {
                freelancer.Experience = (float)freelancer.Experience;
                db.Freelancers.Add(freelancer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(freelancer);
        }

        // GET: Freelancer/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Freelancer freelancer = db.Freelancers.Find(id);
            if (freelancer == null)
            {
                return HttpNotFound();
            }
            return View(freelancer);
        }

        // POST: Freelancer/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FreelancerID,Name,IsEmployed,Experience")] Freelancer freelancer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(freelancer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(freelancer);
        }

        // GET: Freelancer/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Freelancer freelancer = db.Freelancers.Find(id);
            if (freelancer == null)
            {
                return HttpNotFound();
            }
            return View(freelancer);
        }

        // POST: Freelancer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Freelancer freelancer = db.Freelancers.Find(id);
            db.Freelancers.Remove(freelancer);
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


        public void CheckIfCompanyHasJobsOrProjects()
        {
            var freelancers = db.Freelancers.ToList();
            foreach (var freelancer in freelancers)
            {
                if (freelancer.ProjectsList.Count == 0 && freelancer.JobsList.Count == 0)
                {
                    freelancer.IsEmployed = false;
                }
                else
                {
                    freelancer.IsEmployed = true;
                }
            }
            db.SaveChanges();
        }
    }
}
