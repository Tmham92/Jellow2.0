
using Jellow2._0.Database;
using Jellow2._0.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Jellow2._0.Controllers
{
    public class OverviewController : Controller
    {
        private DbModelContainer db = new DbModelContainer();

        // GET: Overview
        public ActionResult Overview()
        {
            ViewBag.Message = String.Empty;
            JobAndProjectViewModel JobAndProjectVM = new JobAndProjectViewModel();
            JobAndProjectVM.allProjects = db.Projects.ToList();
            JobAndProjectVM.allJobs = db.Jobs.ToList();
            return View(JobAndProjectVM);
        }

        public ActionResult AcceptProject(int? ProjectID)
        {
            ProjectAndFreelancerViewModel projectAndFreelancerVM = new ProjectAndFreelancerViewModel();
            projectAndFreelancerVM.allFreelancers = db.Freelancers.ToList();
            projectAndFreelancerVM.project = db.Projects.Find(ProjectID);

            //ViewBag.Freelancers = new List<String>();
            //foreach (var freelancer in projectAndFreelancerVM.allFreelancers)
            //{
            //    ViewBag.Freelancers.Add(freelancer.FreelancerID.ToString());    
            //}
            return View(projectAndFreelancerVM);
        }

        [HttpPost]
        public ActionResult AcceptProject([Bind(Include = "ProjectID,Name,Description,Experience,DueDate,ConsumerID,CompanyID,FreelancerID, SkillRequirement")] Project project)
        {

            if (ModelState.IsValid)
            {
                db.Entry(project).State = EntityState.Modified;

                Freelancer freelancer = db.Freelancers.Find(project.FreelancerID);
                freelancer.ProjectsList.Add(project);

                db.Freelancers.AddOrUpdate(freelancer);
                db.Projects.AddOrUpdate(project);

                db.SaveChanges();
                return RedirectToAction("Overview", "Overview");
            }
            return Redirect("Overview");

        }

        public ActionResult AcceptJob(int? JobID)
        {
            JobAndFreelancerViewModel jobAndFreelancerViewModel = new JobAndFreelancerViewModel();
            jobAndFreelancerViewModel.job = db.Jobs.Find(JobID);
            jobAndFreelancerViewModel.Freelancers = db.Freelancers.ToList();
            return View(jobAndFreelancerViewModel);
        }

        [HttpPost]
        public ActionResult AcceptJob([Bind(Include = "JobID, Name, Description, Experience, Salary, StartDate, CompanyID, SkillRequirement, FreelancerID")] Job job)
        {
            if (ModelState.IsValid)
            {
                db.Entry(job).State = EntityState.Modified;

                Freelancer freelancer = db.Freelancers.Find(job.FreelancerID);
                freelancer.JobsList.Add(job);
                int companyid = job.CompanyID;
                db.Freelancers.AddOrUpdate(freelancer);
                db.Jobs.AddOrUpdate(job);

                db.SaveChanges();
                return RedirectToAction("Overview", "Overview");
            }
            return Redirect("Overview");
        }
    }
}