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

namespace Jellow2._0.Controllers
{
    public class ProjectController : Controller
    {
        private DbModelContainer db = new DbModelContainer();

        // GET: Project
        public ActionResult Index()
        {
            var projects = db.Projects.Include(p => p.Consumer).Include(p => p.Company).Include(p => p.Freelancer);
            return View(projects.ToList());
        }

        // GET: Project/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // GET: Project/Create
        public ActionResult Create()
        {
            
            ViewBag.ConsumerConsumerID = new SelectList(db.Consumers, "ConsumerID", "Name");
            ViewBag.CompanyCompanyID = new SelectList(db.Companies, "CompanyID", "Name");
            ViewBag.FreelancerID = new SelectList(db.Freelancers, "FreelancerID", "Name");
            return View();
        }

        // POST: Project/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProjectID,Name,Description,Experience,Budget,DueDate,ConsumerConsumerID,CompanyCompanyID,SkillRequirement,FreelancerID")] Project project)
        {
            if (ModelState.IsValid)
            {
                db.Projects.Add(project);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ConsumerConsumerID = new SelectList(db.Consumers, "ConsumerID", "Name", project.ConsumerID);
            ViewBag.CompanyCompanyID = new SelectList(db.Companies, "CompanyID", "Name", project.CompanyID);
            ViewBag.FreelancerID = new SelectList(db.Freelancers, "FreelancerID", "Name", project.FreelancerID);
            return View(project);
        }

        // GET: Project/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            ViewBag.ConsumerConsumerID = new SelectList(db.Consumers, "ConsumerID", "Name", project.ConsumerID);
            ViewBag.CompanyCompanyID = new SelectList(db.Companies, "CompanyID", "Name", project.CompanyID);
            ViewBag.FreelancerID = new SelectList(db.Freelancers, "FreelancerID", "Name", project.FreelancerID);
            return View(project);
        }

        // POST: Project/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProjectID,Name,Description,Experience,Budget,DueDate,ConsumerConsumerID,CompanyCompanyID,SkillRequirement,FreelancerID")] Project project)
        {
            if (ModelState.IsValid)
            {
                db.Entry(project).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ConsumerConsumerID = new SelectList(db.Consumers, "ConsumerID", "Name", project.ConsumerID);
            ViewBag.CompanyCompanyID = new SelectList(db.Companies, "CompanyID", "Name", project.CompanyID);
            ViewBag.FreelancerID = new SelectList(db.Freelancers, "FreelancerID", "Name", project.FreelancerID);
            return View(project);
        }

        // GET: Project/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: Project/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Project project = db.Projects.Find(id);
            db.Projects.Remove(project);
            db.SaveChanges();
            return RedirectToAction("Index");
        }



        // GET: Project/Create
        public ActionResult CompanyCreateProject(int? CompanyID)
        {
            ViewBag.CompanyID = CompanyID;
            return View();
        }

        // POST: Project/CompanyCreate
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CompanyCreateProject([Bind(Include = "ProjectID,Name,Description,Experience,Budget,DueDate,CompanyID,SkillRequirement")] Project project)
        {
            if (ModelState.IsValid)
            {
                project.Experience = (float)project.Experience;
                //No Freelancer has the job yet
                project.FreelancerID = null;
                project.ConsumerID = null;
                db.Projects.Add(project);

                //Update Company because it has posted a Job
                var company = db.Companies.Find(project.CompanyID);
                company.ProjectList.Add(project);
                db.Companies.AddOrUpdate(company);


                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(project);
        }

        public ActionResult ConsumerCreateProject(int? ConsumerID)
        {
            ViewBag.ConsumerID = ConsumerID;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ConsumerCreateProject([Bind(Include = "ProjectID, Name, Description, Experience, Budget, DueDate, ConsumerID, SkillRequirement")] Project project)
        {
            if (ModelState.IsValid)
            {
                project.Experience = (float)project.Experience;

                project.FreelancerID = null;
                project.CompanyID = null;
                db.Projects.Add(project);

                //Update Company because it has posted a Job
                var consumer = db.Consumers.Find(project.ConsumerID);
                consumer.ProjectList.Add(project);
                db.Consumers.AddOrUpdate(consumer);

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(project);
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
