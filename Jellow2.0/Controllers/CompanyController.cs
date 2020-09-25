using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Jellow2._0.Database;
using Jellow2._0.ViewModels;

namespace Jellow2._0.Controllers
{
    public class CompanyController : Controller
    {
        private DbModelContainer db = new DbModelContainer();

        // GET: Company
        public ActionResult Index(string sortOrder, string searching)
        {
            ViewBag.IDSortParam = String.IsNullOrEmpty(sortOrder) ? "companyID_asc" : "";
            ViewBag.ProjectPostedParam = sortOrder == "Projectposted";
            ViewBag.JobPostedParam = sortOrder == "Jobposted";
            ViewBag.NameParam = sortOrder == "NameOrder" ? "Name_Desc" : "NameOrder";

            var companies = from c in db.Companies select c;
            switch (sortOrder)
            {
                case "Jobposted":
                    companies = companies.OrderByDescending(c => c.HasJobsPosted);
                    break;
                case "Projectposted":
                    companies = companies.OrderByDescending(c => c.HasProjectsPosted);
                    break;
                case "NameOrder":
                    companies = companies.OrderBy(c => c.Name);
                    break;
                case "Name_Desc":
                    companies = companies.OrderByDescending(c => c.Name);
                    break;
                case "companyID_asc":
                    companies = companies.OrderBy(c => c.CompanyID);
                    break;
                default:
                    companies = companies.OrderBy(c => c.CompanyID);
                    break;
            }
            CheckIfCompanyHasJobsOrProjects();
            return View(companies.ToList());
            //return View(db.Companies.ToList());
        }

        // GET: Company/Details/5
        public ActionResult ProjectDetails(int? CompanyID)
        {

            if (CompanyID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyViewModel CompanyVM = new CompanyViewModel();
            var projects = db.Projects.Where(p => p.CompanyID == CompanyID);
            CompanyVM.allProjects = projects.ToList();
            return View(CompanyVM);
        }

        public ActionResult JobDetails(int? CompanyID)
        {

            if (CompanyID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyViewModel CompanyVM = new CompanyViewModel();
            var jobs = db.Jobs.Where(j => j.CompanyID == CompanyID);
            CompanyVM.allJobs = jobs.ToList();

            return View(CompanyVM);
        }


        // POST: Company/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CompanyID,Name,HasProjectsPosted,HasJobsPosted")] Company company)
        {
            if (ModelState.IsValid)
            {
                db.Companies.Add(company);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(company);
        }

        public ActionResult Create()
        {
            return View();
        }

        // GET: Company/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company company = db.Companies.Find(id);
            if (company == null)
            {
                return HttpNotFound();
            }
            return View(company);
        }

        // POST: Company/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CompanyID,Name,HasProjectsPosted,HasJobsPosted")] Company company)
        {
            if (ModelState.IsValid)
            {
                db.Entry(company).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(company);
        }

        // GET: Company/Delete/5
        public ActionResult Delete(int? CompanyID)
        {
            if (CompanyID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company company = db.Companies.Find(CompanyID);
            if (company == null)
            {
                return HttpNotFound();
            }
            return View(company);
        }

        // POST: Company/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int CompanyID)
        {
            Company company = db.Companies.Find(CompanyID);
            db.Companies.Remove(company);
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
            var companies = db.Companies.ToList();
            foreach (var company in companies)
            {
                if (company.JobList.Count == 0)
                {
                    company.HasJobsPosted = false;
                }
                else
                {
                    company.HasJobsPosted = true;
                }
                if (company.ProjectList.Count == 0)
                {
                    company.HasProjectsPosted = false;
                }
                else
                {
                    company.HasProjectsPosted = true;
                }
            }
        }

    }
}
