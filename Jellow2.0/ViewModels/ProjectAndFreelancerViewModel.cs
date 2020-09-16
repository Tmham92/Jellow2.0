using Jellow2._0.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Jellow2._0.ViewModels
{
    public class ProjectAndFreelancerViewModel
    {
        public List<Freelancer> allFreelancers { get; set; }
        public Project project { get; set; }

        public IEnumerable<SelectListItem> FreelancerIDs
        {
            get { return new SelectList(allFreelancers, "FreelancerID", "FreelancerID"); }
        }
    }
}