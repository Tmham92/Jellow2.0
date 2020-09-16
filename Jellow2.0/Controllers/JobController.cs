using Jellow2._0.Database;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Jellow2._0.Controllers
{
    public class JobController : Controller
    {
        private DbModelContainer db = new DbModelContainer();
        // GET: Job
        public ActionResult Index()
        {
            return View(db.Jobs.ToList());
        }

        public ActionResult CompanyCreateJob(int? CompanyID)
        {
            ViewBag.CompanyID = CompanyID;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CompanyCreateJob([Bind(Include = "JobID, Name, Description, Experience, Salary, StartDate, CompanyID, SkillRequirement, FreelancerID")] Job job)
        {
            if (ModelState.IsValid)
            {
                //No Freelancer has the job yet
                job.FreelancerID = null;
                db.Jobs.Add(job);

                //Update Company because it has posted a Job
                var company = db.Companies.Find(job.CompanyID);
                company.HasJobsPosted = true;
                company.JobList.Add(job);
                db.Companies.AddOrUpdate(company);


                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(job);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job job = db.Jobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }

        // POST: Company/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Job job = db.Jobs.Find(id);
            db.Jobs.Remove(job);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}