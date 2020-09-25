using Jellow2._0.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Jellow2._0.ViewModels
{
    public class JobAndFreelancerViewModel
    {
        public List<Freelancer> Freelancers { get; set; }
        public Job job { get; set; }

        public IEnumerable<SelectListItem> FreelancerIDs
        {
            get { return new SelectList(Freelancers, "FreelancerID", "FreelancerID"); }
        }
    }
}