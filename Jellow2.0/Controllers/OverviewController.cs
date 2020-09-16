
using Jellow2._0.Database;
using Jellow2._0.ViewModels;
using System;
using System.Collections.Generic;
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

        public ActionResult ProjectAccepted(int? ProjectID, int? FreelancerID)
        {
            _projectID = ProjectID;

            Project project = db.Projects.Find(ProjectID);
            project.FreelancerID = 2;

            Freelancer freelancer = db.Freelancers.Find(project.FreelancerID);
            freelancer.ProjectsList.Add(project);
            db.Projects.AddOrUpdate(project);
            db.SaveChanges();

            ViewBag.Message = "Project is added to Project List";

            return Redirect("Overview");

        }
    }



    //, int? FreelancerID
}