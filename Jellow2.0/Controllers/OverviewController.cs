
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

        private int? _projectID;
        private int? _freelancerID;

        // GET: Overview
        public ActionResult Overview()
        {
            ViewBag.Message = String.Empty;
            JobAndProjectViewModel JobAndProjectVM = new JobAndProjectViewModel();
            JobAndProjectVM.allProjects = db.Projects.ToList();
            JobAndProjectVM.allJobs = db.Jobs.ToList();
            return View(JobAndProjectVM);
        }

        public ActionResult AcceptJob(int? JobID)
        {
            
            return View();
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
        public ActionResult AcceptProject([Bind(Include = "ProjectID,Name,Description,Experience,Budget,DueDate,ConsumerConsumerID,CompanyCompanyID,SkillRequirement,FreelancerID")] Project project)
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
    }



    //, int? FreelancerID
}